<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Mantenimientos.aspx.cs" Inherits="wa_sicove.sicoveadmin.Mantenimientos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .dt-center{
        font-size: 12px;
    }

            ul.timeline {
                list-style-type: none;
                position: relative;
            }
            ul.timeline:before {
                content: ' ';
                background: #D4D9DF;
                display: inline-block;
                position: absolute;
                left: 29px;
                width: 2px;
                height: 100%;
                z-index: 400;
            }
            ul.timeline > li {
                margin: 20px 0;
                padding-left: 20px;
            }
            ul.timeline > li:before {
                content: ' ';
                background: white;
                display: inline-block;
                position: absolute;
                border-radius: 50%;
                border: 3px solid #43B917;
                left: 20px;
                width: 20px;
                height: 20px;
                z-index: 400;
            }


</style>
<div class="row">
    <div class="col-md-12">
        <ul class="breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Home</a></li>
            <li class="active">Mantenimientos de unidades</li>
        </ul>
    </div>
</div>
<div class="content">
    <div class="main-header">
        <h2>Mantenimientos</h2>
        <em>Mantenimiento de unidades</em>
    </div>
    <div class="main-content">
        <div class="widget">
            <div class="widget-header">
                <h3>Buscar unidades</h3>
            </div>
            <div class="widget-content">
                <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                    <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Empresa y Giro</a></li>
                    <li class=""><a href="#Economico" role="tab" data-toggle="tab" aria-expanded="false">Numero Economico</a></li>
                    <li class=""><a href="#Contrato" role="tab" data-toggle="tab" aria-expanded="false">Numero De Contrato</a></li>
                    <li class=""><a href="#Comodatario" role="tab" data-toggle="tab" aria-expanded="false">Comodatario</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="Empresa">
                        <div class="row">
                            <div class="form-group col-md-2">
                                <label class="control-label" for="selectEmpresa"><b>Empresa: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectEmpresa" required> </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label" for="selectGiro"><b>Giro: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectGiro" required> </select>
                            </div>
                            <div class="form-group col-md-2" id="hideSelectMttoTipo" hidden>
                                <label class="control-label"><b>Tipo de Mtto:</b></label>
                                <select class="form-control" id="selectMttoTipo" style="width: 100%" data-html2canvas-ignore="true">
                                    <option value="0">Todos</option>
                                    <option value="1">Preventivo</option>
                                    <option value="2">Correctivo</option>
                                    <option value="3">Predictivo</option>
                                </select>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label"><b>Rango:</b></label>
                                <input class="date-range form-control" id="rangeEmp" type="text" name="daterange" placeholder="Seleccionar Fechas" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Pendientes:</b></label>
                                <label class="col-sm-12 fancy-checkbox">
								<input class="realizado" type="checkbox" checked="checked" id="activoEmp">
								<span>¿Solo Pendientes?</span>
							</label>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarEmpr()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="Economico">
                        <div class="row">
                            <div class="form-group col-md-3 col-sm-12">
                                <label><b>N&uacute;mero Economico: <span style="color: red;">*</span></b></label>
                                <input class="form-control" type="text" id="numeroEconomico" placeholder="Número" required />
                            </div>
                            <div class="form-group col-md-2" id="hideSelectMttoTipoNe" hidden>
                                <label class="control-label"><b>Tipo de Mtto:</b></label>
                                <select class="form-control" id="selectMttoTipoNe" style="width: 100%" data-html2canvas-ignore="true">
                                    <option value="0">Todos</option>
                                    <option value="1">Preventivo</option>
                                    <option value="2">Correctivo</option>
                                    <option value="3">Predictivo</option>
                                </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Rango:</b></label>
                                <input class="date-range form-control" id="rangeNumEco" type="text" name="daterange" placeholder="Seleccionar Fechas" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Pendientes:</b></label>
                                <label class="col-sm-12 fancy-checkbox">
								<input class="realizado" type="checkbox" checked="checked" id="activoumEco">
								<span>¿Solo Pendientes?</span>
							</label>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarNumEconom()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="Contrato">
                        <div class="row">
                            <div class="form-group col-md-2 col-sm-12">
                                <label><b>N&uacute;mero de Contrato: <span style="color: red;">*</span></b></label>
                                <input class="form-control" type="text" id="numeroContrato" placeholder="Número" required />
                            </div>
                            <div class="form-group col-md-2" id="hideSelectMttoTipoNc" hidden>
                                <label class="control-label"><b>Tipo de Mtto:</b></label>
                                <select class="form-control" id="selectMttoTipoNc" style="width: 100%" data-html2canvas-ignore="true">
                                    <option value="0">Todos</option>
                                    <option value="1">Preventivo</option>
                                    <option value="2">Correctivo</option>
                                    <option value="3">Predictivo</option>
                                </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Rango:</b></label>
                                <input class="date-range form-control" id="rangeNumCon" type="text" name="daterange" placeholder="Seleccionar Fechas" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Pendientes:</b></label>
                                <label class="col-sm-12 fancy-checkbox">
								<input class="realizado" type="checkbox" checked="checked" id="activoumCon">
								<span>¿Solo Pendientes?</span>
							</label>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarNumContra()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="Comodatario">
                        <div class="row">
                            <div class="form-group col-md-2">
                                <label class="control-label" for="selectEmpresa"><b>Comodatario: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectComodatario" required style="width: 100%"></select>
                            </div>
                            <div class="form-group col-md-2" id="hideSelectMttoTipoCom" hidden>
                                <label class="control-label"><b>Tipo de Mtto:</b></label>
                                <select class="form-control" id="selectMttoTipoCom" style="width: 100%" data-html2canvas-ignore="true">
                                    <option value="0">Todos</option>
                                    <option value="1">Preventivo</option>
                                    <option value="2">Correctivo</option>
                                    <option value="3">Predictivo</option>
                                </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Rango:</b></label>
                                <input class="date-range form-control" id="rangeComo" type="text" name="daterange" placeholder="Seleccionar Fechas" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b>Pendientes:</b></label>
                                <label class="col-sm-12 fancy-checkbox">
								<input class="realizado" type="checkbox" checked="checked" id="activoComo">
								<span>¿Solo Pendientes?</span>
							</label>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarComoda()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="main-content">
        <div class="widget widget-table">
            <div class="widget-header">
                <h3><i class="fa fa-group"></i>Mantenimientos</h3>
                <em>- Lista de Mantenimientos</em>
                <div class="btn-group widget-header-toolbar">
                    <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                    <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"><i class="fa fa-chevron-up"></i></a>
                    <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                </div>
                <div class="widget-header-toolbar" style="display: none;">
                    <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                </div>
            </div>
            <div class="widget-content">
                <table id="tabla_lista" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Ver Mtto</th>
                            <th>Fecha Instalacion: </th>
                            <th>Fecha De mtto: </th>
                            <th>Fecha proximo mtto: </th>
                            <th>kilometraje: </th>
                            <th>Estatus Mtto: </th>
                            <th>Tipo de Mtto: </th>
                            <th>Numero Economico: </th>
                            <th>Numero Contrato: </th>
                            <th>Comodatario: </th>
                            <th>Marca Vehiculo: </th>
                            <th>Modelo Vehiculo: </th>
                            <th>Año Vehiculo: </th>
                            <th>Marca Kit: </th>
                            <th>Modelo Kit: </th>
                            <th>Riel: </th>
                            <th>Centralita: </th>
                            <th>Empresa: </th>
                            <th>Giro: </th>
                            <th>Base: </th>
                            <th>Observaciones: </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<div class="modal fade" id="modal_util" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="titulo_modal_util"></h4>
            </div>
            <div class="modal-body">
                <label class="control-label"><b id="label_modal_util"></b></label>
                <input type="text" class="form-control" id="nombre_util" />
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" onclick="guarda_util()">Guardar</button>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modal_comentario" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="">Comentario</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-md-12">
                        <label class="control-label"><b>Fecha de comentario</b></label>
                        <input type="date" class="form-control" id="cm_fecha" disabled/>
                    </div>
                    <div class="form-group col-md-12">
                        <label class="control-label"><b>Comentario</b></label>
                        <textarea class="form-control" id="cm_comentario" rows="3"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" onclick="guardarComentario()">Guardar</button>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modal_dComentarios" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="">Historial de comentarios</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="timeline" id="listado_mensajes" style="max-height:300px; overflow-y:auto; overflow-x:hidden;">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>

<div class="modal fade" id="modal_unidad" tabindex="-1" role="dialog" >
    <div class="modal-dialog" style="width:900px;" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">Ver Mantenimiento</h4>
                <button type="button" class="btn btn-danger btn-sm pull-right" onclick="exportt()">PDF</button>
            </div>
            <div class="modal-body" id="modalbody">
                <br />
                <h4><strong>Empresa</strong></h4>
                <%--Empresa--%>
                <div class="row">
                    <div class="col-md-4">
                        <p id="contrato"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="economico"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="comodatario"></p>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <p id="empresa"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="giro"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="base"></p>
                    </div>
                </div>
                <br />
                <h4><strong>Vehiculo</strong></h4>
                <%--Vehiculo--%>
                <div class="row">
                    <div class="col-md-4">
                        <p id="marcaV"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="modeloV"></p>
                    </div>
                    <div class="col-md-4">
                        <p id="anioV"></p>
                    </div>
                </div>
                <br />  
                <h4><strong>Kit</strong></h4>
                <%--Kit--%>
                <div class="row">
                    <div class="col-md-3">
                        <p id="marcaK"></p>
                    </div>
                    <div class="col-md-3">
                        <p id="modeloK"></p>
                    </div>
                    <div class="col-md-3">
                        <p id="riel"></p>
                    </div>
                    <div class="col-md-3">
                        <p id="centralita"></p>
                    </div>
                </div>
                <br />
                <h4><strong>Mantenimiento</strong></h4>
                <%-- Mantenimiento --%>
                <div class="row">
                    <div class="col-md-6">
                        <p id="EstatusMtto"></p>
                    </div>
                    <div class="col-md-6">
                        <p id="tipoMtto"></p>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <label class="control-label"><b>Estatus Mtto:</b></label>
                        <select class="form-control" id="mttoEstatus" style="width: 100%" data-html2canvas-ignore="true">
                                    
                        </select>
                    </div>
                    <div class="col-md-6">
                        <label class="control-label"><b>Tipo de Mtto:</b></label>
                        <select class="form-control" id="mttoTipo" style="width: 100%" data-html2canvas-ignore="true">
                            <option value="1">Preventivo</option>
                            <option value="2">Correctivo</option>
                            <option value="3">Predictivo</option>
                        </select>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <label class="control-label"><b>Refacciones:</b></label>
                        <select class="form-control" id="tRefaccion" style="width: 100%" name="tRefaccion[]" multiple="multiple">

                        </select>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <label class="col-md-3 control-label"><b>Fecha Programada:</b></label>
                    <div class="col-md-8">
                        <input class="form-control datapickers" type="date" id="fechaInstalacion" />
                    </div>
                </div>
                <br />
                <div class="row" id="divFechaReal" >
                    <label class="col-md-3 control-label"><b>Fecha de Mantenimiento:</b></label>
                    <div class="col-md-8">
                        <input class="form-control datapickers" type="date" id="fechaRealizacion" />
                    </div>
                </div>
                <br />
                <div class="row" >   
                    <label class="col-md-3 control-label"><b>Tecnico:</b></label>
                    <div class="col-md-8">
                        <select class="form-control" id="selectTecnico" style="width: 100%" data-html2canvas-ignore="true">
                                    
                        </select>
                        <label style="display:none;">Hola mundo texto</label>
                    </div>
                </div>
                <br />
                <div class="row" >   
                    <label class="col-md-3 control-label"><b>Kilometraje:</b></label>
                    <div class="col-md-8">
                        <input class="form-control" type="text" id="kilometraje"/>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group col-md-12">
                        <label class="control-label" for="txtObservacion"><b>Observación</b></label>
                        <textarea class="form-control" id="txtObservacion" rows="3"></textarea>
                    </div>
                </div>
                <div class="row" data-html2canvas-ignore="true">
                    <div class="form-group col-md-2 pull-left" id="divAgrComentario">
                        <button type="button" class="btn btn-warning btn-sm" onclick="agregarComentario()"><b>Agregar Comentario</b></button>
                    </div>
                    <div class="form-group col-md-2 pull-right">
                        <button type="button" class="btn btn-info btn-sm" onclick="verComentarios()"><b>Ver comentarios</b></button>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-primary" id="guardar" onclick="Guardar_mtto()">Guardar</button>
                <button type="button" class="btn btn-secondary" onclick="limpiar()" data-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>
<script src="../scripts/Mantenimientos.js"></script>


    
</asp:Content>
