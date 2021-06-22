<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="wa_sicove.sicoveadmin.Permisos" %>
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
			<li class="active">Permisos</li>
		</ul>
	</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Permisos</h2>
			<em>Administración</em>
		</div>
		<div class="main-content">
			<div class="col-md-12">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Permisos</h3> <em>- Lista de permisos</em>
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
						<div class="form-group col-md-3">
                            <label class="control-label"><b>Selecciona un rol:</b></label>
							<select class="form-control" id="selectRol">
                                <option value="-1">Seleccionar Rol</option>
                            </select>
                        </div>	
                        <div class="form-group col-md-3">
                            <center><label class="control-label"><b>Seleccionar todos:</b></label></center>
							<center><label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" id="checkTodos">
								<span></span>
							</label></center>	
                        </div>	
                        <div class="row col-md-12" id="gridModulos">

                        </div>	
                        <div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" onclick="guardar()"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>			
					</div>
				</div>
				<!-- END WIDGET TICKET TABLE -->
            </div>
            
		</div>
	</div>
    <script>

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


        traer_modulos();
        function traer_modulos() {
            $.ajax({
                type: "POST",
                url: 'Modulos.aspx/traer',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        var gridModulos = "";
                        var idpadre = -1;
                       
                        for (var i = 0; i < parsedTest.length; i++) {
                           
                            if (parsedTest[i]['estatus'] == 1) {// cabeceras
                                //if (parsedTest[i]['id_padre'] > 0) {
                                    if (idpadre != parsedTest[i]['id_padre']) {
                                        idpadre = parsedTest[i]['id_padre'];
                                        gridModulos += '<div class="col-md-12" style="font-weight: bold;font-size: 16px;"><hr>' + parsedTest[i]['desc_padre'] + ':<br></div>';
                                    }
                                    gridModulos += 
                                        '<div class="col-md-4">'+
                                            '<label class="col-sm-12 fancy-checkbox">'+
								                '<input type="checkbox" name="ckModulo" value="' + parsedTest[i]['id'] + '">'+
								                '<span style="font-size: 15px;"><b>' + parsedTest[i]['desc_padre'] + '</b> ' + parsedTest[i]['mod_nombre'] + '</span>' +
                                            '</label>'+
								        '</div>';
                                //}  
                            }
                          

                        }
                        $("#gridModulos").html(gridModulos);


                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
        }

 

        $("#checkTodos").change(function () {
            if (this.checked) {
                $('input[name=ckModulo]').each(function () {
                    $(this).prop("checked", true);
                });
            } else {
                limpiarModulos();
            }
        });

        function limpiar() {
            $("#selectRol").val(-1);
            limpiarModulos();
        }

        function limpiarModulos() {
            $('input[name=ckModulo]').each(function () {
                $(this).prop("checked", false);
            });
        }

        function obtenerModulosRoles() {
            if ($("#selectRol").val() == -1) {
                limpiarModulos();
            }
            else {
                limpiarModulos();
                $.ajax({
                    type: "POST",
                    url: "Permisos.aspx/ObtenerModulosRol",
                    data: '{ idRol: "' + $("#selectRol").val() + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        var datos = JSON.parse(response.d.Data);
                        for (var i = 0; i < datos.length; i++) {
                            $('input[name=ckModulo]').each(function () {
                                if ($(this).val() == datos[i].idmodulo)
                                    $(this).prop("checked", true);
                            });
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });

            }
        }

        $("#selectRol").change(function () {
            limpiarModulos();
            obtenerModulosRoles();
        });

        function guardar() {
            var asignacion = new Array()
            $('input[name=ckModulo]').each(function () {
                if (this.checked) {
                    asignacion.push($(this).val());
                }
            });
            if ($("#selectRol").val() == -1 || asignacion.length == 0) {
                toastr.error("Seleccione el Rol y los Modulos que se van a asignar", "Datos incorrectos");
            } else {
                $.ajax({
                    type: "POST",
                    url: "Permisos.aspx/AsignacionPermisos",
                    data: JSON.stringify({ idRol: $("#selectRol").val(), modulos: asignacion }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Result) {
                            $.gritter.add({
                                title: "Éxito",
                                text: "La asignación se ha realizado correctamente",
                                sticky: false
                            });
                            
                            $("#selectRol").val(-1);
                            limpiarModulos();
                        } else {
                            $.gritter.add({
                                title: "Problema de asignación",
                                text: "",
                                sticky: false
                            });
                            
                        }
                    },
                    error: function (error) {
                        console.log(error);
                    }
                });
            }
        }



    </script>
</asp:Content>
