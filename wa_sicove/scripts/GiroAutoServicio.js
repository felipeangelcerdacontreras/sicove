﻿$(document).ready(function () {
    GetGiro();
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

function GetGiro() {
    $.ajax({
        url: "GiroAutoServicio.aspx/traer_giro",
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
                        parsedTest[i]['descripcion'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_giro(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function guardar() {
    var descripcion = $("#txtDescripcion").val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;

    if (descripcion.length > 2) {
        var urlS = "GiroAutoServicio.aspx/guardar_giro";

        if (!editando) {//GUARDAR
            urlS = "GiroAutoServicio.aspx/guardar_giro";
        } else { //EDITANDO
            urlS = "GiroAutoServicio.aspx/actualizar_giro";
        }

        var datas = {
            descripcion: descripcion,
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
                    var msj = "Se Actualizo correctamente la marca.";
                    if (!editando) {
                        msj = "Se Guardo correctamente la marca.";
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
    }
};

function ver_giro(id, descripcion, estatus) {
    editando = true;
    id_editando = id;
    $("#txtDescripcion").val(descripcion);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }
};

function limpiar() {
    editando = false;
    $("#txtDescripcion").val("");
    $("#activoCH").prop('checked', true);
    GetGiro();
};