<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Lider.aspx.cs" Inherits="wa_sicove.sicoveadmin.Lider" %>

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
                <li class="active">Lideres</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Lideres</h2>
            <em>Catálogos</em>
        </div>
        <div class="main-content">
            <div class="col-md-8">
                <!-- WIDGET TICKET TABLE -->
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Lideres</h3>
                        <em>- Lista de lideres</em>
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
                                    <th>Telefono</th>
                                    <th>Correo</th>
                                    <th>Comisi&oacute;n</th>
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
                        <h3><i class="fa fa-group"></i>Lideres</h3>
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
                            <label class="control-label"><b>Telefono: <span style="color: red;">*</span></b></label>
                            <input class="form-control input-only-number" type="tel" id="numTelefono" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Correo: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="email" id="txtCorreo" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Comisi&oacute;n: <span style="color: red;">*</span></b></label>
                            <input class="form-control input-only-number" type="text" id="numComicion" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
                            <label class="col-sm-12 fancy-checkbox">
                                <input type="checkbox" checked="checked" id="activoCH">
                                <span>¿Lider activo?</span>
                            </label>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12" style="margin-bottom: 15px;">
                                <button type="submit" class="btn btn-info  pull-right""><i class="fa fa-check-circle"></i>Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i>Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/Lider.js"></script>
</asp:Content>
