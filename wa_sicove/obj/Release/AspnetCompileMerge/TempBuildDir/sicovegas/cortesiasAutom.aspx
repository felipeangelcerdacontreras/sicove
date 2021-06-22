<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="cortesiasAutom.aspx.cs" Inherits="wa_sicove.sicoveadmin.cortesiasAutom" %>
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
                <li class="active">Cortesías Automaticas</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header">
            <h2>Cortesias</h2>
            <em>Autorizar Cortesías</em>
        </div>
       
        <div class="main-content">
            <div class="col-md-12">
                <button title="autorizar todas las cortesias" onclick="confirmar_modal()" type="button" class="btn btn-info "> Autorizar Cortesías</button>
                <button title="Imprime las cortesias" onclick="cortesiasActivas()" type="button" class="btn btn-info pull-right "> Imprimir Cortesias Activas</button>
                <svg id="barcode" style="display: none;"></svg>
                <br /> <br />
                <div class="widget widget-table">
                    
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Cortesías Automaticas</h3>
                        <em>- Lista de Cortesías</em>
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
                                    <th>F Generada</th>
                                    <th>F Aplicada</th>
                                    <th>F Vencimiento</th>
                                    <th>Contrato</th>
                                    <th>Cantidad</th>
                                    <th>Folio</th>
                                    <th>Empresa</th>
                                    <th>Estatus</th>
                                    <th>Estación</th>
                                    <th>Libre</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal_util" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" >Cambiar Estación</h4>
                </div>
                <div class="modal-body">
                    <label class="control-label" for="selectEmpresa"><b>Estaciones: <span style="color: red;">*</span></b></label>
                    <select class="form-control" id="selectEstaciones" required style="width: 100%"></select>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" onclick="guarda_util()">Guardar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="confirmar" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-center" >Confirmar todas las cortesías como listas</h4>
                </div>
                <div class="modal-body">
                    <div class="row text-center">
                        <div class="col-md-6">
                            <button title="autorizar todas las cortesias" onclick="confirmar_modal_ok()" type="button" class="btn btn-info ">Confirmar</button>
                        </div>
                         <div class="col-md-6">
                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>

                        </div>
                          
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../scripts/cortesiasAtomat.js"></script>
    <script src="../assets/js/barcoder/JsBarcode.code128.min.js"></script>
    <script src="../scripts/pdfCortesias.js"></script>

</asp:Content>
