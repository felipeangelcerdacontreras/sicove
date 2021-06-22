<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master"  AutoEventWireup="true" CodeBehind="ContServicios.aspx.cs" Inherits="wa_sicove.sicoveadmin.ContServicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="row">
	<div class="col-md-12 ">
		<ul class="breadcrumb">
			<li><i class="fa fa-home"></i><a href="#">Home</a></li>
			<li class="active">Servcios No Encontrados
			</li>
		</ul>
	</div>
</div>
<div class="content">
		<div class="main-header">
			<h2>Servicios</h2>
			<em>No encontrados</em>
		</div>
		        <div class="main-content">
                <div class="widget">
                    <div class="widget-header">
                        <h3>Buscar servicios no encontrados</h3>
                    </div>
                    <div class="widget-content">
                        <%--<ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                            <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Filtro</a></li>
                        </ul>--%>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="Empresa">
                                <div class="row">

                                    <div class="form-group col-md-3">
                                        <label class="control-label" for="FInicio"><b>Fecha Inicial:</b></label><br />
                                        <input type="datetime-local" class="form-control" id="FInicio" required/>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label" for="FFin"><b>Fecha Final:</b></label><br />
                                        <input type="datetime-local" class="form-control" id="FFin" required/>
                                    </div>

                                    <div class="form-group col-md-3">
                                        <label class="control-label" for="idEstacion"><b>Estación:</b></label><br />
                                        <select class="form-control" id="idEstacion" style="width: 220px;">
                                        </select>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label" for="idModulo"><b>Módulo:</b></label><br />
                                        <select class="form-control" id="idModulo" style="width: 220px;">
                                        </select>
                                    </div>

									<div class="form-group col-md-1" style="margin-left: 87%;">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-primary" type="button" onclick="traer_servicios()"> Buscar <i class="fa fa-search"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
		<div class="main-content">
			<div class="col-md-12">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Servicios </h3> <em>- Lista de Servicios No Registrados</em>
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
									<th>Estación</th>
                                    <th>Modulo</th>
                                    <th>Servicio Faltante</th>
                                    <th>Servicio Anterior</th>
                                    <th>Hora Servicio Anterior</th>
                                    <th>Siguiente Servicio</th>
                                    <th>Hora Siguiente Servicio</th>
								</tr>
							</thead>
							<tbody>
								
							</tbody>
						</table>
					</div>
				</div>
				<!-- END WIDGET TICKET TABLE -->
            </div>
		</div>
	</div>
	<script src="../assets/js/jquery.blockUI.js"></script>
<script src="../scripts/ContServicios.js"></script>
</asp:Content>