<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Comodatarios.aspx.cs" Inherits="wa_sicove.sicoveadmin.Comodatarios" %>
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
			<li class="active">Comodatarios
			</li>
		</ul>
	</div>
</div>
<div class="content">
		<div class="main-header">
			<h2>Comodatarios</h2>
			<em>Catálogos</em>
		</div>
		        <div class="main-content">
                <div class="widget">
                    <div class="widget-header">
                        <h3>Buscar Comodatarios</h3>
                    </div>
                    <div class="widget-content">
                        <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                            <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Filtro</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="Empresa">
                                <div class="row">

                                    <div class="form-group col-md-3">
                                        <label class="control-label"><b>Nombre:</b></label>
                                        <input type="text" id="inpNombre" class="form-control" "/>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label"><b>Teléfono:</b></label>
                                        <input type="text" id="inpTelefono" class="form-control"  maxlength="10"/>
                                    </div>
									<div class="form-group col-md-1">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-primary" type="button" onclick="traer_comodatarios('1')">Buscar <i class="fa fa-search"></i></button>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-success" type="button" onclick="traer_comodatarios('2')">Todos <i class="fa fa-search"></i></button> <br />  <br />  
                                        <em>Los tíempos de respuesta pueden ser mas extensos de lo normal.</em>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Comodatarios</h3> <em>- Lista de Comodatarios</em>
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
									<th>Telefono</th>
									<th>Correo</th>
                                    <th>Usuario</th>
                                    <th>Contraseña</th>
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
						<h3><i class="fa fa-group"></i> Comodatarios</h3> <em>- Crear o modificar comodatarios</em>
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
							<input type="text" class="form-control input-mayus" id="NombreI" required onkeypress="return ((event.charCode >= 48 && event.charCode <= 57) || (event.charCode >= 65 && event.charCode <= 90) || (event.charCode >= 97 && event.charCode <= 122))">
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Telefono: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-only-number" id="TelefonoI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Correo: <span style="color: red;">*</span></b></label>
							<input type="email" class="form-control" id="CorreoI" required>
                        </div>
                         <div class="form-group">
                            <label class="control-label"><b>Usuario: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-mayus" id="UsuarioI" required">
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Contraseña: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-mayus" id="ContraseñaI" required">
                        </div>
                         <div class="form-group">
                            <label class="control-label"><b>Rol: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectRol" required>
                                <option value="-1">Seleccionar Rol</option>
                            </select>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo: <span style="color: red;">*</span></b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH" required>
								<span>¿Comodatario activo?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="submit" class="btn btn-info  pull-right"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>					
                    </div>
				</div>
            </div>
		</div>
	</div>
	<script src="../assets/js/jquery.blockUI.js"></script>
<script src="../scripts/Comodatarios.js"></script>
</asp:Content>