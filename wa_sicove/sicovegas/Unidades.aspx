<%@ Page Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Unidades.aspx.cs" Inherits="wa_sicove.sicoveadmin.Unidades" %>

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
                <li class="active">Unidades</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header" style="margin-bottom: 20px;">
            <h2>Unidades</h2>
            <em>Catálogo</em>
        </div>
        <div class="main-content">
                <div class="widget">
                    <div class="widget-header">
                        <h3>Buscar Unidades</h3>
                    </div>
                    <div class="widget-content">
                        <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                            <li class="active"><a href="#Empresa" role="tab" data-toggle="tab" aria-expanded="true">Filtro</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane fade active in" id="Empresa">
                                <div class="row">

                                    <div class="form-group col-md-3">
                                        <label class="control-label"><b>Empresa:</b></label>
                                        <input type="text" id="inpEmpresa" class="form-control" onkeyup="aMays(event, this)" onblur="aMays(event, this)"/>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label class="control-label"><b>N° Contrato:</b></label>
                                        <input type="text" id="inpContrato" class="form-control" onkeyup="aMays(event, this)" onblur="aMays(event, this)"/>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label"><b>N° Economico:</b></label>
                                        <input type="text" id="inpEconomico" class="form-control" onkeyup="aMays(event, this)" onblur="aMays(event, this)"/>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="selectUFiltro"><b>Comodatario:</b></label>
                                        <input type="text" id="inpComodatario" class="form-control" onkeyup="aMays(event, this)" onblur="aMays(event, this)"/>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label class="control-label" for="selectUFiltro"><b>Placa:</b></label>
                                        <input type="text" id="inpPlaca" class="form-control" onkeyup="aMays(event, this)" onblur="aMays(event, this)"/>
                                    </div>

                                    <div class="form-group col-md-8">
                                    </div>

                                    <div class="form-group col-md-1">
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-primary" type="button" onclick="traer_unidades('1')">Buscar <i class="fa fa-search"></i></button>
                                    </div>
                                    <div class="form-group col-md-3" >
                                        <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                        <br />
                                        <button class="btn btn-success" type="button" onclick="traer_unidades('2')">Todos <i class="fa fa-search"></i></button> <br />  <br />  
                                        <em>Los tíempos de respuesta pueden ser mas extensos de lo normal.</em>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <!-- WIDGET TICKET TABLE -->
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
                        <table id="tabla_lista" class="table table-sorting display" style="width: 100%">
                            <thead>
                                <tr>
                                    <th>Empresa</th>
                                    <th>Giro</th>
                                    <th>N° de Contrato</th>
                                    <th>N&uacute;mero economico</th>
                                    <th>Placa</th>
                                    <th>Comodatario</th>
                                    <th>Dirección</th>
                                    <th>Teléfono</th>
                                    <th>Estatus Instalación</th>
                                    <th>Fecha Instalación</th>
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
        </div>

    <div class="modal fade" id="modal_util" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title" id="titulo_modal_util"></h4>
           
          </div>
          <div class="modal-body">
           
                <label class="control-label"><b id="label_modal_util"></b></label>
				<input type="text" class="form-control" id="nombre_util" >
           
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" onclick="guarda_util()">Guardar</button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          </div>
        </div>
      </div>
    </div>
    <script src="../assets/js/jquery.blockUI.js"></script>
    <script src="../scripts/Unidades.js"></script>
</asp:Content>
