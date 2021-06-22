$(document).ready(function () {
    traer_estatus();
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

function traer_estatus() {
    $.ajax({
        url: "EstatusMatto.aspx/traer_e",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                tabla_lista.clear().draw();

                for (var i = 0; i < parsedTest.length; i++) {
                    tabla_lista.row.add([
                        parsedTest[i]['descripcion'],
                        '<center><button type="button" class="btn btn-info " onclick="ver_estatus(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

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
    var descripcion = $("#txtDescripcion").val();
    var urlS = "EstatusMatto.aspx/guardar_e";

    if (!editando) {//GUARDAR
        urlS = "EstatusMatto.aspx/guardar_e";
    } else { //EDITANDO
        urlS = "EstatusMatto.aspx/actualizar_e";
    }

    var datas = {
        descripcion: descripcion,
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
                var msj = "Se Actualizo correctamente el estatus.";
                if (!editando) {
                    msj = "Se Guardo correctamente el estatus.";
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

function ver_estatus(id, descripcion) {
    $("#form1")[0].reset();
    editando = true;
    id_editando = id;
    $("#txtDescripcion").val(descripcion);

};

function limpiar() {
    editando = false;
    $("#txtDescripcion").val("");
    traer_estatus();
    $("#form1")[0].reset();
};