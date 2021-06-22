$(document).ready(function () {
    traer_zona();
    traer_tipoPersonal();
    traer_personal();
});

var editando = false;
var id_editando = 0;

var tabla_lista = $("#tabla_lista").DataTable({
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    buttons: [],
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
    "pageLength": 5,
});

$('#form1').validator().on('submit', function (e) {
    if (e.isDefaultPrevented()) {
        $.gritter.add({
            title: "Informacion",
            text: "Verifique que los campos esten completos",
            sticky: false
        });
    } else {
        e.preventDefault();
        guardar();
    }
})
function guardar() {
    var nombre = $("#txtNombre").val();
    var apellido = $("#txtApellido").val();
    var numEmpleado = $("#txtNumEmpleado").val();
    var codigoBarra = $("#txtCodigoBarra").val();
    var zona = $("#selectZona").val();
    var tipo = $("#selectTipo").val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
    urlS = "Personal.aspx/guardar_p";
    if (!editando) {//GUARDAR
        urlS = "Personal.aspx/guardar_p";
    } else { //EDITANDO
        urlS = "Personal.aspx/actualizar_p";
    }
    var datas = {
        nombre: nombre,
        apellido: apellido,
        numero_empleado: numEmpleado,
        codigo_barras: codigoBarra,
        id_zona: zona,
        id_tipo_personal: tipo,
        activo: activo,
        id: id_editando
    }
    $.ajax({
        type: "POST",
        url: urlS,
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var msj = "Se Actualizo correctamente el personal.";
                if (!editando) {
                    msj = "Se Guardo correctamente el personal.";
                }
                $.gritter.add({
                    title: "Éxito",
                    text: msj,
                    sticky: false
                });
                limpiar();
            } else {

            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
};

function limpiar() {
    editando = false;

    $("#txtNombre").val("");
    $("#txtApellido").val("");
    $("#txtNumEmpleado").val("");
    $("#txtCodigoBarra").val("");
    $("#selectZona").val("-1");
    $("#selectTipo").val("-1");
    $("#activoCH").prop('checked', true);

    traer_personal();

    $("#form1")[0].reset();
};

function traer_personal() {
    $.ajax({
        url: "Personal.aspx/traer_p",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                tabla_lista.clear().draw();

                for (var i = 0; i < parsedTest.length; i++) {
                    var stado = '<span class="label label-success">Activo</span>';
                    if (parsedTest[i]['estatus'] == 0) {
                        stado = '<span class="label label-critical">Inactivo</span>';
                    }
                    tabla_lista.row.add([
                        parsedTest[i]['nombre'] + ' ' + parsedTest[i]['apellido'],
                        parsedTest[i]['numero_empleado'],
                        parsedTest[i]['codigo_barra'],
                        parsedTest[i]['tipo_personal'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_personal(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['nombre'] + '\',\'' + parsedTest[i]['apellido'] + '\',\'' + parsedTest[i]['numero_empleado'] + '\',\'' + parsedTest[i]['codigo_barra'] + '\',\'' + parsedTest[i]['id_zona'] + '\',\'' + parsedTest[i]['id_tipo_personal'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function ver_personal(id, nombre, apellido, numEmpleado, codigoBarra, id_zona, id_tipo_personal, estatus) {
    $("#form1")[0].reset();
    editando = true;
    id_editando = id;

    $("#txtNombre").val(nombre);
    $("#txtApellido").val(apellido);
    $("#txtNumEmpleado").val(numEmpleado);
    $("#txtCodigoBarra").val(codigoBarra);
    $("#selectZona").val(id_zona).trigger('change');
    $("#selectTipo").val(id_tipo_personal).trigger('change');

    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }
};

function traer_zona() {
    $("#selectZona").html('<option value="" selected disabled>Seleccionar Zona</option>');
    $.ajax({
        url: "Zonas.aspx/traer_z",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectZona").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['zona'] + '</option>');
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function traer_tipoPersonal() {
    $("#selectTipo").html('<option value="" selected disabled>Seleccionar Tipo</option>');
    $.ajax({
        url: "TipoPersonal.aspx/traer_tipo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectTipo").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};