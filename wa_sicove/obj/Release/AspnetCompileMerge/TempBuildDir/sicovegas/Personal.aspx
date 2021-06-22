<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="wa_sicove.sicoveadmin.Personal" %>

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
                <li class="active">Personal</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Personal</h2>
            <em>Catálogos</em>
        </div>
        <div class="main-content">
            <div class="col-md-8">
                <!-- WIDGET TICKET TABLE -->
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Personal</h3>
                        <em>- Lista de Personal</em>
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
                                    <th>Nombre</th>
                                    <th>N&uacute;mero Empleado</th>
                                    <th>Codigo Barra</th>
                                    <th>Tipo</th>
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
                        <h3><i class="fa fa-group"></i>Personal</h3>
                        <em>- Crear o modificar</em>
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
                            <label class="control-label"><b>Nombres: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="txtNombre" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Apellidos: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="txtApellido" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>N&uacute;mero Empleado: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="txtNumEmpleado" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Codigo Barras: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="txtCodigoBarra" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Zona: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectZona" required>
                                
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Tipo Personal: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectTipo" required>
                              
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
                            <label class="col-sm-12 fancy-checkbox">
                                <input type="checkbox" checked="checked" id="activoCH">
                                <span>¿Personal activo?</span>
                            </label>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12" style="margin-bottom: 15px;">
                                <button type="submit" class="btn btn-info  pull-right"><i class="fa fa-check-circle"></i>Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i>Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/Personal.js"></script>
</asp:Content>
