<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="gestoriaRecargas.aspx.cs" Inherits="wa_sicove.sicoveadmin.gestoriaRecargas" %>
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
                <li class="active">Consultar Consumos</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Gestoría</h2>
            <em>Consulta de consumos</em>
        </div>
        <div class="main-content">
            <div class="widget">
                <div class="widget-header">
                    <h3>Consultas</h3>
                </div>
                <div class="widget-content">
                    <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                        <li class="active"><a href="#consumos" role="tab" data-toggle="tab" aria-expanded="true">Consultar Consumos</a></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade active in" id="consumos">
                            <div class="row align-items-end">
                                <div class="form-group col-md-3">
                                    <label class="control-label"><b>Rango:</b></label>
                                    <input class="date-range form-control" id="range" type="text" name="daterange" placeholder="Seleccionar Fechas" />
                                </div>
                                <div class="form-group col-md-2">
                                    <label class="control-label"><b style="color: #f9f9f9;">.</b></label>
                                    <br />
                                    <button class="btn btn-primary" type="button" onclick="buscarConsumos()">Consultar <i class="fa fa-search"></i></button>
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
                    <h3><i class="fa fa-group"></i>Consumos</h3>
                    <em>- Lista de Consumos</em>
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
                                <th>Marca:</th>
                                <th>Modelo:</th>
                                <th>Año:</th>
                                <th>Direccion:</th>
                                <th>Telefono:</th>
                                <th>Dias Sin Consumir:</th>
                                <th>Consumos:</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/gestoriaConsulta.js"></script>
</asp:Content>
