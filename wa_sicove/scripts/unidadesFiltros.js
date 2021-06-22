$(document).ready(function () {
    traer_estados();
    $("#selectCiudad").html('<option value="" selected disabled>Selecciona Ciudad</option>');

    $("#selectEstado").select2();
    $("#selectCiudad").select2();

})
var tabla_lista = $("#tabla_lista").DataTable({
    "responsive": {
        details: {
            type: 'column'
        }
    },
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    dom: 'Blfrtip',
    buttons: [
        //{
        //    extend: 'csv',
        //    exportOptions: {
        //        columns: [9, 2, 3, 4, 5, 6, 7, 8, 11, 12, 13, 14, 15, 16, 17, 18, 19]
        //    }

        //}
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


function traer_estados() {
    $("#selectEstado").html('<option value="" selected disabled>Selecciona Estado</option>');
    $.ajax({
        type: "POST",
        url: 'Estados.aspx/traer_estados',
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var parsedTest = JSON.parse(response.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus_rol'] == 1) {
                        $("#selectEstado").append('<option value="' + parsedTest[i]['id_rol'] + '">' + parsedTest[i]['descripcion_rol'] + '</option>');
                    }
                }
            } else {

            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
}

$("#selectEstado").change(function () {
    if ($(this).val() > 0) {
        traer_Ciudades($(this).val());
    }
});

function traer_Ciudades(id_estado) {
    $("#selectCiudad").html('<option value="" selected disabled>Selecciona Ciudad</option>');
    $.ajax({
        type: "POST",
        url: 'Ciudades.aspx/traer_cd',
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var parsedTest = JSON.parse(response.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1 && parsedTest[i]['id_estado'] == id_estado) {
                        $("#selectCiudad").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['ciudad'] + '</option>');
                    }
                }
            } else {

            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
}



function buscarZona() {
    var estado = $("#selectEstado").val();
    if (!estado) {
        $.gritter.add({
            title: "Informacion",
            text: "Selecciona almenos un Estado",
            sticky: false
        });
    } else {
        var ciudad = $("#selectCiudad").val();
        if (!ciudad) {
            var url = "unidadesFiltro.aspx/unidades_Est"
            var datos = JSON.stringify({
                estado: estado
            });
        } else {
            var url = "unidadesFiltro.aspx/unidades_EstCiu"
            var datos = JSON.stringify({
                estado: estado,
                ciudad: ciudad
            });
        }
    }

    $.ajax({
        url: url,
        type: "POST",
        data: datos,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result == false) {
                tabla_lista.clear().draw();
                $.gritter.add({
                    title: "Informacion",
                    text: "No se Encontraron Resultados",
                    sticky: false
                });
            } else {
                var parsedTest = JSON.parse(result.d.Data);
                mttos_tabla = parsedTest;
                tabla_lista.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    tabla_lista.row.add([
                        parsedTest[i]['contrato'],
                        parsedTest[i]['economico'],
                        parsedTest[i]['zona'],
                        parsedTest[i]['Estado'],
                        parsedTest[i]['Ciudad'],
                        parsedTest[i]['Marca'],
                        parsedTest[i]['Empresa'],
                    ]).draw();

                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function buscarNumEconom() {
    var economico = $("#numeroEconomico").val();
    if (!economico) {
        $.gritter.add({
            title: "Informacion",
            text: "Escriba un Numero Economico",
            sticky: false
        });
    } else {
        var url = "unidadesFiltro.aspx/unidades_NumEco"
        var datos = JSON.stringify({
            economico: economico
        });
        $.ajax({
            url: url,
            type: "POST",
            data: datos,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d.Result == false) {
                    tabla_lista.clear().draw();
                    $.gritter.add({
                        title: "Informacion",
                        text: "No se Encontraron Resultados",
                        sticky: false
                    });
                } else {
                    var parsedTest = JSON.parse(result.d.Data);
                    mttos_tabla = parsedTest;
                    tabla_lista.clear().draw();
                    for (var i = 0; i < parsedTest.length; i++) {
                        tabla_lista.row.add([
                            parsedTest[i]['contrato'],
                            parsedTest[i]['economico'],
                            parsedTest[i]['zona'],
                            parsedTest[i]['Estado'],
                            parsedTest[i]['Ciudad'],
                            parsedTest[i]['Marca'],
                            parsedTest[i]['Empresa'],
                        ]).draw();

                    }
                }
            },
            error: function (errormessage) {
                console.log(errormessage);
            }
        });
    }
}

function buscarNumContra() {
    var contrato = $("#numeroContrato").val();
    if (!contrato) {
        $.gritter.add({
            title: "Informacion",
            text: "Escriba un Numero Economico",
            sticky: false
        });
    } else {
        var url = "unidadesFiltro.aspx/unidades_NumCon"
        var datos = JSON.stringify({
            contrato: contrato
        });
        $.ajax({
            url: url,
            type: "POST",
            data: datos,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d.Result == false) {
                    tabla_lista.clear().draw();
                    $.gritter.add({
                        title: "Informacion",
                        text: "No se Encontraron Resultados",
                        sticky: false
                    });
                } else {
                    var parsedTest = JSON.parse(result.d.Data);
                    mttos_tabla = parsedTest;
                    tabla_lista.clear().draw();
                    for (var i = 0; i < parsedTest.length; i++) {
                        tabla_lista.row.add([
                            parsedTest[i]['contrato'],
                            parsedTest[i]['economico'],
                            parsedTest[i]['zona'],
                            parsedTest[i]['Estado'],
                            parsedTest[i]['Ciudad'],
                            parsedTest[i]['Marca'],
                            parsedTest[i]['Empresa'],
                        ]).draw();

                    }
                }
            },
            error: function (errormessage) {
                console.log(errormessage);
            }
        });
    }
}