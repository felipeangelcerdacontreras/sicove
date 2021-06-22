<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="ReportesMtto.aspx.cs" Inherits="wa_sicove.sicoveadmin.ReportesMtto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .dt-center{
        font-size: 11px;
    }
</style>

<div class="row">
    <div class="col-md-12">
        <ul class="breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Home</a></li>
            <li class="active">Consulta de Mantenimientos</li>
        </ul>
    </div>
</div>
<div class="content">
    <div class="main-header">
        <h2>Indicadores</h2>
        <em>Consulta de Mantenimientos</em>
    </div>
    <div class="main-content">
        <div class="widget">
            <div class="widget-header">
                <h3>Consultar Mantenimientos</h3>
            </div>
            <div class="widget-content">
                <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                    <li class="active"><a href="#indPorunidad" role="tab" data-toggle="tab" aria-expanded="true">Mantenimientos por Pendientes</a></li>
                    <!-- <li class=""><a href="#servResumen" role="tab" data-toggle="tab" aria-expanded="false">Servicios Resumen</a></li> -->
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="indPorunidad">
                        <div class="row">

                            <div class="form-group col-md-3">
                                <label class="control-label" for="uniDe"><b>Mantenimientos:</b></label>
                                <select class="form-control" id="uniDe">
                                    <option value="0">Todos</option>
                                    <option value="1">Del Mes</option>
                                    <option value="2">Mayores a 6 Meses</option>
                                    <option value="3">Menores a 6 Meses</option>
                                </select>
                            </div>

                            <div class="form-group col-md-1">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" id="bucarIndU" onclick="indicador()">Buscar <i class="fa fa-search"></i></button>
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
                <em>- Lista de mantenimientos</em>
                <div class="btn-group widget-header-toolbar">
                    <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                    <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"><i class="fa fa-chevron-up"></i></a>
                    <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                </div>
                <div class="widget-header-toolbar" style="display: none;">
                    <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                </div>
            </div>
            <div class="widget-content" id="Tablas">
                <table id="tabla_lista" class="table table-sorting display nowrap" style="width: 100%">
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
    <script src="../assets/js/jquery.blockUI.js"></script>
<script src="../scripts/ReportesMtto.js"></script>




</asp:Content>
