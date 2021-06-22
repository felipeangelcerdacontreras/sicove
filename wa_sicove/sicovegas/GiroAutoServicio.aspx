﻿<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="GiroAutoServicio.aspx.cs" Inherits="wa_sicove.sicoveadmin.GiroAutoServicio" %>

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
                <li class="active">Giro</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Giro Auto Consumo</h2>
            <em>Catálogos</em>
        </div>
        <div class="main-content">
            <div class="col-md-8">
                <!-- WIDGET TICKET TABLE -->
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Giro</h3>
                        <em>- Giro</em>
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
                        <table id="tabla_lista" class="table table-sorting">
                            <thead>
                                <tr>
                                    <th>Giro</th>
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
                        <h3><i class="fa fa-group"></i>Giro</h3>
                        <em>- Crear o modificar Giro</em>
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
                            <label class="control-label" for="txtDescripcion"><b>Giro:</b></label>
                            <input class="form-control" type="text" id="txtDescripcion">
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
                            <label class="col-sm-12 fancy-checkbox">
                                <input type="checkbox" checked="checked" id="activoCH">
                                <span>¿Tipo Activo?</span>
                            </label>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12" style="margin-bottom: 15px;">
                                <button type="button" class="btn btn-info  pull-right" onclick="guardar()"><i class="fa fa-check-circle"></i>Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i>Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/GiroAutoServicio.js"></script>
</asp:Content>
