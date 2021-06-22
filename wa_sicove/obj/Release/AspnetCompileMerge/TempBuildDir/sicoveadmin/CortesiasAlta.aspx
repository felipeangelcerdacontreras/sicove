<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="CortesiasAlta.aspx.cs" Inherits="wa_sicove.sicoveadmin.CortesiasAlta" %>
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
                <li class="active">Cortesías</li>
            </ul>
        </div>
</div>
<div class="content">
        <div class="main-header">
            <h2>Alta Cortesías</h2>
            <em>Catálogos</em>
        </div>
            <div class="main-content">
                <div class="widget">
                    <div class="widget-header">
                        <h3>Buscar Cortesías</h3>
                    </div>
                    <div class="widget-content">
                        <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                            <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Filtros</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="Empresa">
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="fInicialS"><b>Fecha Inicial:</b></label>
                                        <input type="datetime-local" class="form-control" id="fInicialS" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="fFinalS"><b>Fecha Final:</b></label>
                                        <input type="datetime-local" class="form-control" id="fFinalS"/>
                                    </div>

                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="selectEmpresa"><b>Empresa:</b></label>
                                        <select class="form-control" id="selectEmpresa"> </select>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="selectUFiltro"><b>Unidad:</b></label>
                                        <select class="form-control" id="selectUFiltro"> </select>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="selectUEstatus"><b>Estatus:</b></label>
                                        <select class="form-control" id="selectUEstatus"> 
                                            <option value="-1">Seleccione Estatus</option>
                                            <option value="1">Activa</option>
                                            <option value="2">Cancelada</option>
                                            <option value="3">Vencida</option>
                                            <option value="4">Por Autorizar</option>
                                            <option value="5">Surtida</option>
                                        </select>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-primary" type="button" onclick="TraerCortesias()">Buscar <i class="fa fa-search"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <div class="main-content">
            <div class="col-md-9">
                <!-- WIDGET TICKET TABLE -->
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Cortesías</h3>
                        <em>- Cortesías  </em>
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
                                    <th>Ver</th>
                                    <th>F Generada</th>
                                    <th>F Aplicada</th>
                                    <th>F Vencimiento</th>
                                    <th>Contrato</th>
                                    <th>Cantidad</th>
                                    <th>Folio</th>
                                    <th>Empresa</th>
                                    <th>Estatus</th>
                                    <th>Estacion</th>
                                    <th>Libre</th>
                                    <th>Imprimir</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <svg id="barcode" style="display: none;"></svg>
                <!-- END WIDGET TICKET TABLE -->
            </div>
            <div class="col-md-3">
                <div class="widget widget-table ">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Datos Para Cortesías</h3>
                        <em>- Crear o modificar Cortesías</em>
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
                        <div class="form-group">
                            <label class="control-label" for="selectEmpresa"><b>Folio: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" name="Litros" id="Folio" disabled />
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="selectEmpresa"><b>Tipo de Cortesia:<span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectTipoCortesias" required>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="selectEmpresa"><b>Unidad: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectUnidad" required>
                               
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="selectEmpresa"><b>Cantidad: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" name="Litros" id="cantidad" placeholder="Cantidad de la cortesia" required/>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="Estatus"><b>Estatus Cortesia:</b></label>
                            <select class="form-control" id="selectEstCortesi">
                                
                            </select>
                        </div>
                        <div id="inpuAutoriza" class="form-group" hidden>
                            <label class="control-label" for="Estatus"><b>Quien Autoriza:</b></label>
                            <select class="form-control" id="Autiriza" >
                                
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="Estatus"><b>Estaciones:</b></label>
                            <select class="form-control" id="selectEstaciones" >
                                
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Libre:</b></label>
                            <label class="col-sm-12 fancy-checkbox">
                                <input type="checkbox" id="libre">
                                <span>¿Cortesía libre?</span>
                            </label>
                        </div>
                        <div class="form-group">
                            <label class="control-label" for="Estatus"><b>Observaciones:</b></label>
                            <textarea class="form-control" id="txtObservacion" rows="3"></textarea>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12" style="margin-bottom: 15px;">
                                <button type="submit" id="guardar" class="btn btn-info pull-right"><i class="fa fa-check-circle"></i>Guardar</button>
                                <button type="button" class="btn btn-critical pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i>Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/CortesiasAlta.js"></script>
    <script src="../assets/js/barcoder/JsBarcode.code128.min.js"></script>
    <script src="../scripts/pdfCortesias.js"></script>
</asp:Content>
