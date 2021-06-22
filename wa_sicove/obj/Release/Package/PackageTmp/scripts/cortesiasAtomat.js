$(document).ready(function () {
    TraerCortesias();
    Traer_Estaciones();
})
var id_editar = 0;
var tabla_lista = $("#tabla_lista").DataTable({
    "responsive": {
        details: {
            type: 'column'
        }
    },
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    buttons: [
    ],
    "columnDefs": [
        { "className": "dt-center", "targets": "_all" }
    ],
    "order": [
        [0, 'desc']
    ],
    "lengthMenu": [
        [5, 10, 50, 100, -1],
        [5, 10, 50, 100, "Todos"]
    ],
    "pageLength": 5
});

function TraerCortesias() {
    $.ajax({
        url: "CortesiasAlta.aspx/traer_C_Atoma",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (!result.d.Data) {
                $.gritter.add({
                    title: "Informacion",
                    text: "No hay cortesias por autorizar",
                    sticky: false
                });
            }
            else {
                var parsedTest = JSON.parse(result.d.Data);
                tabla_lista.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    console.log(parsedTest[i]['id_estatus_cortesia']);
                    if (parsedTest[i]['id_estatus_cortesia'] == 5) {
                        var fecha = $.formattedDate(new Date(parseInt(parsedTest[i]['fecha_aplicada'].substr(6))));
                    } else {
                        var fecha = "Pendiente";
                    }
              
                    if (parsedTest[i]['id_estatus_cortesia'] == 1) {
                        var estatus = "Activo"
                    } else if (parsedTest[i]['id_estatus_cortesia'] == 3) {
                        var estatus = "Vencida"
                    } else if (parsedTest[i]['id_estatus_cortesia'] == 2) {
                        var estatus = "Cancelada"
                    } else if (parsedTest[i]['id_estatus_cortesia'] == 4) {
                        var estatus = "Por Autorizar"
                    }

                    const libre = parsedTest[i]['libre'] == true ? 'Sí': 'No';
                    const clase = parsedTest[i]['libre'] == true ? 'btn-success': 'btn-info';

                    tabla_lista.row.add([
                        //parsedTest[i]['cortesia'],
                        $.formattedDate(new Date(parseInt(parsedTest[i]['fecha_generada'].substr(6)))),
                        fecha,
                        $.formattedDate(new Date(parseInt(parsedTest[i]['fecha_vencimiento'].substr(6)))),
                        parsedTest[i]['unidad'],
                        parsedTest[i]['cantidad'].toFixed(2),
                        parsedTest[i]['folioCortesia'],
                        parsedTest[i]['Empresa'],
                        estatus,
                        parsedTest[i]['SucursalN'],
                        '<center><button title="Cambiar Estatus" id="btnl' + parsedTest[i]['id'] + '" onclick="liberarCortesia(\'' + parsedTest[i]['id'] + '\')" ' +
                        'type="button" class= "btn ' + clase+' ">' + libre +'</button ></center > ',
                        '<center><button title="Ver Mtto" onclick="cambiar_sucursal(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['Sucursal'] + '\')" ' +
                        'type="button" class= "btn btn-info " > <i class="fa fa-eye"></i></button ></center > ',
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function liberarCortesia(id) {
    const status = $("#btnl" + id).text();
    let s = false;

    if (status == "No") {
        $("#btnl" + id).text("Sí");
        $("#btnl" + id).removeClass("btn-info");
        $("#btnl" + id).addClass("btn-success");
        s = true;
    } else {
        $("#btnl" + id).text("No");
        $("#btnl" + id).removeClass("btn-success");
        $("#btnl" + id).addClass("btn-info");
        s = false;
    }

    const data = JSON.stringify({
        id: id,
        s: s
    });

    $.ajax({
        url: "cortesiasAutom.aspx/cambiarLibre",
        type: "POST",
        data: data,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                $("#modal_util").modal("hide");
                $.gritter.add({
                    title: "Informacion",
                    text: "Cortesia Actualizada Correctamente",
                    sticky: false
                });
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function cambiar_sucursal(id) {
    $("#modal_util").modal("show");
    id_editar = id
}

function Traer_Estaciones() {
    $("#selectEstaciones").html('<option value="" selected disabled>Seleccionar Estacion</option>');
    $.ajax({
        url: "Estaciones.aspx/traer",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectEstaciones").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['nombre'] + ' - ' + parsedTest[i]['zona'] + '</option>');
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function guarda_util() {
    var estacion = $("#selectEstaciones").val();
    var datos = JSON.stringify({ id_cortesia: id_editar, idEstacion: estacion});
    $.ajax({
        url: "cortesiasAutom.aspx/actualizarZona",
        type: "POST",
        data: datos,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                $("#modal_util").modal("hide");
                var msj = "Se Reprogramo Correctamente el Mantenimiento.";
                $.gritter.add({
                    title: "Informacion",
                    text: "Estacion Cambiada Correctamente",
                    sticky: false
                });
                tabla_lista.clear().draw();
                TraerCortesias();
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function confirmar_modal() {
    $("#confirmar").modal("show");
}
function confirmar_modal_ok() {
    $.ajax({
        url: "cortesiasAutom.aspx/actualizarActivo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                $("#modal_util").modal("hide");
                var msj = "Cortesías Autorizadas Correctamente.";
                $.gritter.add({
                    title: "Éxito",
                    text: "Cortesias Autorizadas Correctamente",
                    sticky: false
                });
                tabla_lista.clear().draw();
                TraerCortesias()
                $("#confirmar").modal("hide");
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}
$.formattedDate = function (dateToFormat) {
    var dateObject = new Date(dateToFormat);
    var day = dateObject.getDate();
    var month = dateObject.getMonth() + 1;
    var year = dateObject.getFullYear();
    day = day < 10 ? "0" + day : day;
    month = month < 10 ? "0" + month : month;
    var formattedDate = year + "-" + month + "-" + day;
    return formattedDate;
};