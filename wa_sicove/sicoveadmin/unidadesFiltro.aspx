<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="unidadesFiltro.aspx.cs" Inherits="wa_sicove.sicoveadmin.unidadesFiltro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .dt-center{
        font-size: 12px;
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
            <h2>Modulo Unidades</h2>
            <em>Consulta de Unidades</em>
        </div>
        <div class="main-content">
            <div class="widget">
                <div class="widget-header">
                    <h3>Buscar unidades</h3>
                </div>
                <div class="widget-content">
                    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                        <li class="active"><a href="#zona" role="tab" data-toggle="tab" aria-expanded="true">Estado o Ciudad</a></li>
                        <li class=""><a href="#Economico" role="tab" data-toggle="tab" aria-expanded="false">Numero Economico</a></li>
                        <li class=""><a href="#Contrato" role="tab" data-toggle="tab" aria-expanded="false">Numero De Contrato</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="zona">
                            <div class="row">
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b>Empresa:</b></label>
                                    <select class="form-control" id="selectEstado" required> </select>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b>Giro:</b></label>
                                    <select class="form-control" id="selectCiudad" required> </select>
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b style="color: #f9f9f9;">.</b></label>
                                    <br />
                                    <button class="btn btn-primary" type="button" onclick="buscarZona()">Buscar <i class="fa fa-search"></i></button>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Economico">
                            <div class="row">
                                <div class="form-group col-md-3 col-sm-12">
                                    <label><b>N&uacute;mero Economico:</b></label>
                                    <input class="form-control" type="text" id="numeroEconomico" placeholder="Número" required />
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b style="color: #f9f9f9;">.</b></label>
                                    <br />
                                    <button class="btn btn-primary" type="button" onclick="buscarNumEconom()">Buscar <i class="fa fa-search"></i></button>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Contrato">
                            <div class="row">
                                <div class="form-group col-md-3 col-sm-12">
                                    <label><b>N&uacute;mero de Contrato:</b></label>
                                    <input class="form-control" type="text" id="numeroContrato" placeholder="Número" required />
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b style="color: #f9f9f9;">.</b></label>
                                    <br />
                                    <button class="btn btn-primary" type="button" onclick="buscarNumContra()">Buscar <i class="fa fa-search"></i></button>
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
                    <h3><i class="fa fa-group"></i>Unidades</h3>
                    <em>- Lista de Unidades</em>
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
                    <table id="tabla_lista" class="table table-sorting display nowrap" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Nº Contrato:</th>
                                <th>Nº Economico:</th>
                                <th>Zona:</th>
                                <th>Estado:</th>
                                <th>Ciudad:</th>
                                <th>Modelo:</th>
                                <th>Empresa:</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/unidadesFiltros.js"></script>
</asp:Content>
