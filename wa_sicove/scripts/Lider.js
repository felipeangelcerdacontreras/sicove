$(document).ready(function () {
    traer_lideres();
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
    var telefono = $("#numTelefono").val();
    var correo = $("#txtCorreo").val();
    var comicion = $("#numComicion").val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;

    urlS = "Lider.aspx/guardar_l";
    if (!editando) {//GUARDAR
        urlS = "Lider.aspx/guardar_l";
    } else { //EDITANDO
        urlS = "Lider.aspx/actualizar_l";
    }

    var datas = {
        nombre: nombre,
        apellido: apellido,
        telefono: telefono,
        correo: correo,
        comicion: comicion,
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
                var msj = "Se Actualizo correctamente.";
                if (!editando) {
                    msj = "Se Guardo correctamente.";
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
    $("#form1")[0].reset();
    editando = false;
    $("#txtNombre").val("");
    $("#txtApellido").val("");
    $("#numTelefono").val("");
    $("#txtCorreo").val("");
    $("#numComicion").val("");
    $("#activoCH").prop('checked', true);
    traer_lideres();
};

function traer_lideres() {
    $.ajax({
        url: "Lider.aspx/traer_l",
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
                        parsedTest[i]['telefono'],
                        parsedTest[i]['correo'],
                        parsedTest[i]['comicion'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_lider(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['nombre'] + '\',\'' + parsedTest[i]['apellido'] + '\',\'' + parsedTest[i]['telefono'] + '\',\'' + parsedTest[i]['correo'] + '\',\'' + parsedTest[i]['comicion'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function ver_lider(id, nombre, apellido, telefono, correo, comicion, estatus) {
    $("#form1")[0].reset();
    editando = true;
    id_editando = id;

    $("#txtNombre").val(nombre);
    $("#txtApellido").val(apellido);
    $("#numTelefono").val(telefono);
    $("#txtCorreo").val(correo);
    $("#numComicion").val(comicion);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }
};
