function aMays(e, elemento) {
    tecla = (document.all) ? e.keyCode : e.which;
    console.log(tecla);

    if ((tecla.charCode === 32) || (tecla.charCode >= 48 && tecla.charCode <= 57) || (tecla.charCode >= 65 && tecla.charCode <= 90) || (tecla.charCode >= 97 && tecla.charCode <= 122)) {

        elemento.value = elemento.value.toUpperCase();
        console.log(elemento.value);

    }
}

var tabla_lista = $("#tabla_lista").DataTable({
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    buttons: [
        // { extend: 'print', className: 'btn dark btn-outline' },
        // { extend: 'pdf', className: 'btn green btn-outline' },
        // { extend: 'csv', className: 'btn purple btn-outline ' }
    ],
    "columnDefs": [
        { "className": "dt-center", "targets": "_all" }
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

$(document).ready(function () {

    $('#selectBases').select2({
        placeholder: "Seleccione una base",
        multiple: true
    });

    traer_bases();
    traer_roles();
});

function traer_bases() {
    
    $.ajax({
        url: "Bases.aspx/traer_bases",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectBases").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['direccion'] + '</option>');
                    }
                }
                $('#selectBases').select2({
                    placeholder: "Seleccionar Base",
                    multiple: true
                });
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}


var editando = false;
var id_editando = 0;

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
    var NombreI = $("#NombreI").val();
    var TelefonoI = $("#TelefonoI").val();
    var CorreoI = $("#CorreoI").val();
    var UsuarioI = $("#UsuarioI").val();
    var ContraseñaI = $("#ContraseñaI").val();
    var Rol = $("#selectRol").val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
    var urlS = "Comodatarios.aspx/guardar_como";
    if (!editando) {//GUARDAR
        urlS = "Comodatarios.aspx/guardar_como";
    } else { //EDITANDO
        urlS = "Comodatarios.aspx/actualizar_como";
    }
    var datas = {
        nombre: NombreI,
        telefono: TelefonoI,
        correo: CorreoI,
        usuario: UsuarioI,
        contraseña: ContraseñaI,
        activo: activo,
        rol: Rol,
        id: id_editando
    }

    $.ajax({
        type: "POST",
        url: urlS,
        data: JSON.stringify(datas),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.d.Result) {
                var msj = "Se Actualizo correctamente el Comodatario.";
                if (!editando) {
                    msj = "Se Guardo correctamente el Comodatario.";
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

// traer_comodatarios()
function traer_comodatarios(t) {

    $.blockUI({
        message: '<h2>Espere por favor</h2>',
    });

    let nombre = $('#inpNombre').val();
    let telefono = $('#inpTelefono').val();
    
    if (t == "2") {
        nombre = '';
        telefono = '';
    }


    const data = JSON.stringify({
        nombre,
        telefono
    });


    $.ajax({
        url: "Comodatarios.aspx/traer_comodatarios",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: data,
        success: function (result) {
            
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                tabla_lista.clear().draw();
                
                for (var i = 0; i < parsedTest.length; i++) {
                    var stado = '<span class="label label-success">Activo</span>';
                    if (parsedTest[i]['activo'] == 0) {
                        stado = '<span class="label label-critical">Inactivo</span>';
                    }
                    tabla_lista.row.add([
                        '<center>' + parsedTest[i]['nombre'] + '</center>',
                        '<center>' + parsedTest[i]['telefono'] + '</center>',
                        '<center>' + parsedTest[i]['correo'] + '</center>',
                        '<center>' + parsedTest[i]['usuario'] + '</center>',
                        '<center>' + parsedTest[i]['contraseña'] + '</center>',
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_comodatario(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['nombre'] + '\',\'' + parsedTest[i]['telefono'] + '\',\'' + parsedTest[i]['correo'] + '\',\'' + parsedTest[i]['usuario'] + '\',\'' + parsedTest[i]['contraseña'] + '\',\'' + parsedTest[i]['idrol'] + '\',\'' + parsedTest[i]['activo'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
                setTimeout($.unblockUI, 500);

            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function traer_roles() {
    $("#selectRol").html('<option value="-1">Seleccionar Rol</option>');
    $.ajax({
        type: "POST",
        url: 'Roles.aspx/traer_roles',
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Result) {
                var parsedTest = JSON.parse(response.d.Data);

                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus_rol'] == 1) {
                        if (parsedTest[i]['descripcion_rol'].match(/COMODAT.*/)) {
                            $("#selectRol").append('<option value="' + parsedTest[i]['id_rol'] + '">' + parsedTest[i]['descripcion_rol'] + '</option>');
                        }
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


function ver_comodatario(id, nombre, telefono, correo, usuario, contraseña, rol, estatus) {

    $("#form1")[0].reset();
    editando = true;
    id_editando = id;
    $("#NombreI").val(nombre);
    $("#TelefonoI").val(telefono);
    $("#CorreoI").val(correo);
    $("#UsuarioI").val(usuario);
    $("#ContraseñaI").val(contraseña);
    $("#selectRol").val(rol);
    if (estatus == 1 || estatus == '1') {
        $("#activoCH").prop('checked', true);
    } else {
        $("#activoCH").prop('checked', false);
    }

}

function limpiar() {
    editando = false;
    id_editando = 0;
    $("#activoCH").prop('checked', true);
    $('#selectBases').val(null).trigger('change');
    $("#form1")[0].reset();
}
