<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="configuraciones.aspx.cs" Inherits="wa_sicove.sicoveadmin.configuraciones" %>
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
            <li class="active">Configuración</li>
        </ul>
    </div>
</div>
<div class="content">
    <div class="main-header">
        <h2>Configuración</h2>
        <em>Configuraciónes</em>
    </div>
    <div class="main-content">
        <div class="widget">
            <div class="widget-header">
                <h3>Lista de Configuración</h3>
            </div>
            <div class="widget-content">
                <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                    <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Cortesias</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="Empresa">
                        <div class="row">
                            <div class="form-group col-md-2">
                                <label class="control-label" for="selectEmpresa"><b>Dia inicio del Corte:</b></label>
                                <input id="inicio" type="text" name="name" value="" class="form-control" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label" for="selectGiro"><b>Dia Final del Corte:</b></label>
                                <input id="fin" type="text" name="name" value="" class="form-control" />
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="selectGiro"><b>Litros para aplicar cortesia Automatica:</b></label>
                                <input id="litrosss" type="text" name="name" value="" class="form-control" />
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="actualizaConfi()">Actualizar <i class="fa fa-floppy-o"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    <script src="../scripts/configuraciones.js"></script>
</asp:Content>
