<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="wa_sicove.sicoveadmin.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .dt-center{
        font-size: 12px;
    }
</style>
      <div class="row">
		<div class="col-md-12 ">
			<ul class="breadcrumb">
				<li><i class="fa fa-home"></i><a href="#">Home</a></li>
				<li class="active">Usuarios</li>
			</ul>
		</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Usuarios</h2>
			<em>Administración</em>
		</div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Usuarios</h3> <em>- Lista de usuarios</em>
						<div class="btn-group widget-header-toolbar">
							<a href="#" title="Focus" class="btn-borderless btn-focus" style="display:none;"><i class="fa fa-eye"></i></a>
							<a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"><i class="fa fa-chevron-up"></i></a>
							<a href="#" title="Remove" class="btn-borderless btn-remove" style="display:none;"><i class="fa fa-times"></i></a>
						</div>
						<div class="widget-header-toolbar" style="display:none;">
							<div class="label label-danger"><i class="fa fa-warning"></i> 2 Critical Messages</div>
						</div>
					</div>
					<div class="widget-content">
						<table id="tabla_lista" class="table table-sorting">
							<thead>
								<tr>
									<th>Nombre</th>
                                    <th>Usuario</th>
                                    <th>Rol</th>
									<th>Estatus</th>
									<th>Modificar</th>
										
								</tr>
							</thead>
							<tbody>
								
							</tbody>
						</table>
					</div>
				</div>
				<!-- END WIDGET TICKET TABLE -->
            </div>
            <div class="col-md-4">
                <div class="widget widget-table ">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Usuario</h3> <em>- Crear o modificar usuario</em>
						<div class="btn-group widget-header-toolbar">
							<a href="#" title="Focus" class="btn-borderless btn-focus" style="display:none;"><i class="fa fa-eye"></i></a>
							<a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"><i class="fa fa-chevron-up"></i></a>
							<a href="#" title="Remove" class="btn-borderless btn-remove" style="display:none;"><i class="fa fa-times"></i></a>
						</div>
						<div class="widget-header-toolbar" style="display:none;">
							<div class="label label-danger"><i class="fa fa-warning"></i> 2 Critical Messages</div>
						</div>
					</div>
					<div class="widget-content">
                        <div class="form-group">
                            <label class="control-label"><b>Nombre: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="NameI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Usuario: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="UserI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Contraseña: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="PasswordI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Rol: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectRol" required>
                                <option value="-1">Seleccionar Rol</option>
                            </select>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo: </b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Usuario activo?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" onclick="guardar()"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>					
                    </div>
				</div>
            </div>
		</div>
	</div>

    <script type="text/javascript">
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

        traer_Usuarios();
        
        //$("#selectRol").select2();
        var editando = false;
        var id_editando = 0;

        function guardar() {
           
            var NameI = $("#NameI").val();
            var UserI = $("#UserI").val();
            var PasswordI = $("#PasswordI").val();
            var selectRol = $("#selectRol").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
            if (NameI.length > 3 && UserI.length > 3 && PasswordI.length > 3 && selectRol > 0) {
                var urlS = "Usuarios.aspx/guardar_user";
                if (!editando) {//GUARDAR
                    urlS = "Usuarios.aspx/guardar_user";
                } else { //EDITANDO
                    urlS = "Usuarios.aspx/actualizar_user";
                }

                var datas = {
                    nombre: NameI,
                    usuario: UserI,
                    password: PasswordI,
                    rol: selectRol,
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
                            var msj = "Se Actualizo correctamente el usuario.";
                            if (!editando) {
                                msj = "Se Guardo correctamente el usuario.";
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
            } else {
                $.gritter.add({
                    title: "Error",
                    text: "El nombre, usuario y contraseña deben seer mayores a 3 dígitos",
                    sticky: false
                });
            }
            
        }

        function traer_Usuarios() {
            $.ajax({
                type: "POST",
                url: 'Usuarios.aspx/traer_usuarios',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        tabla_lista.clear().draw();
                        for (var i = 0; i < parsedTest.length; i++) {
                            var stado = '<span class="label label-success">Activo</span>';
                            if (parsedTest[i]['estatus'] == 0) {
                                stado = '<span class="label label-critical">Inactivo</span>';
                            }
                            tabla_lista.row.add([
                                parsedTest[i]['nombre'],
                                '<center>' + parsedTest[i]['usuario'] + '</center>',
                                '<center>' + parsedTest[i]['descripcion_rol'] + '</center>',
                                '<center>'+stado+'</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_usuario(\'' + parsedTest[i]['id_usuario'] + '\',\'' + parsedTest[i]['nombre'] + '\',\'' + parsedTest[i]['usuario'] + '\',\'' + parsedTest[i]['password'] + '\',\'' + parsedTest[i]['id_rol'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
                            ]).draw();

                        }


                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
        }

        traer_roles();
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
                                $("#selectRol").append('<option value="' + parsedTest[i]['id_rol'] + '">' + parsedTest[i]['descripcion_rol'] + '</option>');
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

        function limpiar() {
            editando = false;
            $("#NameI").val("");
            $("#UserI").val("");
            $("#PasswordI").val("");
            $("#selectRol").val("-1");
            $("#activoCH").prop('checked', true);
            traer_Usuarios();
        }

        function ver_usuario(id_usuario, nombre, usuario,password,id_rol,estatus) {
            editando = true;
            id_editando = id_usuario;
            $("#NameI").val(nombre);
            $("#UserI").val(usuario);
            $("#PasswordI").val(password);
            $("#selectRol").val(id_rol);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
        }
    </script>
</asp:Content>
