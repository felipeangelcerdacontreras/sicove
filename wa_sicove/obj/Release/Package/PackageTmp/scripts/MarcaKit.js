$(document).ready(function () {
    GetMarca();
    GetModelo();
});

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

var tabla_lista_modelos = $("#tabla_lista_modelos").DataTable({
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    buttons: [],
    "columnDefs": [
        { "className": "dt-center", "targets": "_all" }
    ],
    "order": [
        [1, 'asc']
    ],
    "lengthMenu": [
        [5, 10, 50, 100, -1],
        [5, 10, 50, 100, "Todos"]
    ],
    "pageLength": 5,
});

var editando = false;
var id_editando = 0;
var editando_modelo = false;
var id_edicion_modelo = 0;

function GetMarca() {
    $("#selectMr").html('<option value="" selected disabled>Selecciona Marca</option>');
    $.ajax({
        url: "MarcaKit.aspx/traer_marcas",
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
                    } else {
                        $("#selectMr").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                    tabla_lista.row.add([
                        parsedTest[i]['descripcion'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_marca(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

$("#validar_marca").click(function () {
    var descripcion = $("#descripcionI").val();
    if (descripcion.length < 1) {
        $('#form1 #segmento_marca').validator('validate');
        $.gritter.add({
            title: "Informacion",
            text: "Verifique que los campos esten completos",
            sticky: false
        });
    } else {
        PostMarca()
    }
});
function PostMarca() {
    var descripcion = $("#descripcionI").val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
    var urlS = "MarcaKit.aspx/guardar_marcas";
    if (!editando) {//GUARDAR
        urlS = "MarcaKit.aspx/guardar_marcas";
    } else { //EDITANDO
        urlS = "MarcaKit.aspx/actualizar_marcas";
    }
    var datas = {
        desc: descripcion,
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
};

function limpiar() {
    $('#form1 #segmento_marca').validator('destroy');
    editando = false;
    $("#descripcionI").val("");
    $("#activoCH").prop('checked', true);
    GetMarca();
};

function ver_marca(id, descripcion, estatus) {
    editando = true;
    id_editando = id;
    $("#descripcionI").val(descripcion);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }
};

function GetModelo() {
    $.ajax({
        type: "POST",
        url: 'MarcaKit.aspx/traer_modelos',
        data: '{id_marca:0}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var parsedTest = JSON.parse(response.d.Data);
                tabla_lista_modelos.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    var stado = '<span class="label label-success">Activo</span>';
                    if (parsedTest[i]['estatus'] == 0) {
                        stado = '<span class="label label-critical">Inactivo</span>';
                    }
                    tabla_lista_modelos.row.add([
                        parsedTest[i]['descripcion'],
                        parsedTest[i]['marca'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_modelo(\'' +
                        parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' +
                        parsedTest[i]['id_marca'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();

                }
            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
};

$("#validar_modelos").click(function () {
    var descripcion = $("#descripcionMI").val();
    if (descripcion.length < 1) {
        $('#form1 #segmento_modelo').validator('validate');
        $.gritter.add({
            title: "Informacion",
            text: "Verifique que los campos esten completos",
            sticky: false
        });
    } else {
        PostModelo()
    }
});
function PostModelo() {
    var descripcion = $("#descripcionMI").val();
    var selectMr = $("#selectMr").val();
    var activo = $("#activoCH_modelo").prop('checked') == true ? 1 : 0;
    var urlS = "MarcaKit.aspx/guardar_modelo";
    if (!editando_modelo) {//GUARDAR
        urlS = "MarcaKit.aspx/guardar_modelo";
    } else { //EDITANDO
        urlS = "MarcaKit.aspx/actualizar_modelo";
    }
    var datas = {
        desc: descripcion,
        marca: selectMr,
        activo: activo,
        id: id_edicion_modelo
    }
    $.ajax({
        type: "POST",
        url: urlS,
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var msj = "Se Actualizo correctamente el modelo.";
                if (!editando) {
                    msj = "Se Guardo correctamente el modelo.";
                }
                $.gritter.add({
                    title: "Éxito",
                    text: msj,
                    sticky: false
                });
                GetModelo()
                limpiar_modelo();
            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
};

function limpiar_modelo() {
    $('#form1 #segmento_modelo').validator('destroy');
    editando_modelo = false;
    id_edicion_modelo = 0;
    $("#descripcionMI").val("");
    $("#selectMr").html('<option value="" selected disabled>Selecciona Marca</option>');
    $("#activoCH_modelo").prop('checked', true);
    GetMarca();
};

function ver_modelo(id, descripcion, id_marca, estatus) {
    editando_modelo = true;
    id_edicion_modelo = id;
    $("#descripcionMI").val(descripcion);
    $("#selectMr").val(id_marca);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH_modelo").prop('checked', true);
    } else {
        $("#activoCH_modelo").prop('checked', false);
    }
};