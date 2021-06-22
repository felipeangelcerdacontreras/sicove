
var tabla_lista = $("#tabla_lista").DataTable({
    "language": {
    "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
        },
        buttons: [
            // {extend: 'print', className: 'btn dark btn-outline' },
            // {extend: 'pdf', className: 'btn green btn-outline' },
            // {extend: 'csv', className: 'btn purple btn-outline ' }
        ],
        "columnDefs": [
        {"className": "dt-center", "targets": "_all" }
        ],
        //responsive: {
    //    details: {
    //    }
    //},
    "order": [
            [0, 'desc']
        ],
        "lengthMenu": [
            [5, 10, 50, 100, -1],
            [5, 10, 50, 100, "Todos"]
        ],
        "pageLength": 5,

});

var editando = false;
var id_editando = 0;

traer_Refacciones();

function traer_Refacciones() {
    $.ajax({
        type: "POST",
        url: 'TipoRefacciones.aspx/traer_refacciones',
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var parsedTest = JSON.parse(response.d.Data);
                tabla_lista.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    var stado = '<span class="label label-success">Activo</span>';
                    if (parsedTest[i]['tr_estatus'] == false) {
                        stado = '<span class="label label-critical">Inactivo</span>';
                    }

                    tabla_lista.row.add([
                        '<center>' + parsedTest[i]['tr_nombre'] + '</center>',
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_refaccion(\'' +
                        parsedTest[i]['id_refaccion'] + '\',\'' + parsedTest[i]['tr_nombre'] + '\',\'' +
                        + parsedTest[i]['tr_estatus'] +
                        '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }


            } else {
                $.gritter.add({
                    title: "Informacion",
                    text: "No se han encontrado refacciones",
                    sticky: false
                });
            }
        },
        error: function (error) {
            console.log("ERROR: " + error);
        }
    });
}

function ver_refaccion(id, nombre, estatus) {
    $("#form1")[0].reset();

    editando = true;
    id_editando = id;
    $("#nombre").val(nombre);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }
}

function limpiar() {
$("#form1")[0].reset();
    editando = false;
    $("#nombre").val("");
    $("#activoCH").prop('checked', true);
    traer_Dispositivos();
    traer_Zonas()
}

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
    var nombre = $("#nombre").val();
    var activo = $("#activoCH").prop('checked') == true ? true : false;

    var urlS = "TipoRefacciones.aspx/guardar_r";

    if (editando) {//Editar 
        urlS = "TipoRefacciones.aspx/actualizar_r";
    }

    var datas = {
        id: id_editando,
        nombre: nombre,
        activo: activo,
    }

    $.ajax({
        type: "POST",
        url: urlS,
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                traer_Refacciones()
                var msj = "Se Actualizo correctamente el dispositivo.";
                if (!editando) {
                msj = "Se Guardo correctamente el dispositivo.";
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

