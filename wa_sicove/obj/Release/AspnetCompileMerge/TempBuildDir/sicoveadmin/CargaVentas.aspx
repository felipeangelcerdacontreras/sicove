<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="CargaVentas.aspx.cs" Inherits="wa_sicove.sicoveadmin.CargaVentas" %>
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
                <li class="active">Carga de Ventas</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Carga de Ventas</h2>
            <em>Ventas</em>
        </div>
            <div class="main-content">
                <div class="widget">
                    <div class="widget-header">
                        <h3>Cargar Ventas</h3>
                    </div>
                    <div class="widget-content">
                        <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                            <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Filtros</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="Empresa">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label class="control-label" for="Estacion"><b>Estación:</b></label>
                                        <select id="Estacion" class="form-control">
                                        </select>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label class="control-label" for="Estacion"><b>Operador:</b></label>
                                        <select id="operador" class="form-control">
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-9">
                                        <label class="control-label" for="fInicialS"><b>Seleccionar archivo:</b></label>
                                        <input type="file" class="form-control" id="inputArchivo" />
                                    </div>

                                    <div class="form-group col-md-1">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-primary" type="button" onclick="enviarFormulario()">Subir <i class="fa fa-upload"></i></button>
                                    </div>

                                    <div class="form-group col-md-1">
                                        <a class="btn btn-warning" href="../assets/doc/CARGA MANUAL.csv" style="margin-top: 23px;"> Descargar Plantilla <i class="fa fa-download"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <script src="../scripts/CargaVentas.js"></script>
</asp:Content>
