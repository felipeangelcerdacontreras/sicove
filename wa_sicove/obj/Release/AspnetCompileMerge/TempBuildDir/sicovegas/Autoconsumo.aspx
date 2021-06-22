<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Autoconsumo.aspx.cs" Inherits="wa_sicove.sicoveadmin.Autoconsumo" %>
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
                <li class="active">Autoconsumo</li>
            </ul>
        </div>
    </div>
    <div class="content">
        <div class="main-header" style="margin-bottom: 20px;">
            <h2>Autoconsumo</h2>
            <em>Catálogos</em>
        </div>
        <div class="main-content">
            <div class="col-md-12">
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Generales</h3>
                        <em>- Crear o modificar</em>
                        <div class="btn-group widget-header-toolbar">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                            <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display: none;"><i class="fa fa-chevron-up"></i></a>
                            <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                        </div>
                        <div class="widget-header-toolbar" style="display: none;">
                            <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group col-md-4">
                            <label class="control-label" for="selectEmpresa"><b>Empresa: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectEmpresa" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="selectGiro"><b>Giro: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectGiro" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="numEconomico"><b>N&uacute;mero Economico: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="numEconomico" required/>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="txtCodigoBarra"><b>C&oacute;digo de Barras: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="txtCodigoBarra" required/>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="fechaInstalacion"><b>Fecha Instalaci&oacute;n: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="date" id="fechaInstalacion" required/>
                        </div>

                        <div class="form-group col-md-4">
                            <label  class="control-label" for="selectTecnico"><b>T&eacute;cnico Instalador: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectTecnico" required>
                            </select>
                        </div>

                        <div class="form-group col-md-4">
                        </div>

                        <div class="form-group col-md-4">
                            <label  class="control-label" for="tMantenimiento"><b>Mantenimiento cada: <span style="color: red;">*</span></b></label>
                            <input type="number" class="form-control" id="tMantenimiento" min="1" required/>
                        </div>

                        <div class="form-group col-md-4">
                            <label  class="control-label" for="selectTiempoMtto"><b>Tiempo: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectTiempoMtto" required>
                                <option value="1">Semanas</option>
                                <option value="2">Meses</option>
                            </select>
                        </div>

                        <div class="form-group col-md-4">
                            <label  class="control-label" for="estatusU"><b>Estatus Instalación: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="estatusU" required>
                                    <option value="1">Activo</option>
                                    <option value="2">Desinstalado</option>
                                    <option value="3">Vendido</option>
                                    <option value="4">Robado</option>
                                    <option value="5">Cambio de propietario</option>
                                    <option value="6">Proceso Jurídico</option>
                                    <option value="7">Otro</option>
                            </select>
                        </div>
                        
                        <div class="form-group col-md-4">
                        </div>
                            
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Activo:</b></label>
                            <label class="col-sm-12 fancy-checkbox">
                                <input type="checkbox" checked="checked" id="activoCH">
                                <span>¿Unidad activa?</span>
                            </label>
                        </div>
                        
                        <div class="form-group col-md-12">
                            <label class="control-label" for="txtObservacion"><b>Observaci&oacute;n</b></label>
                            <textarea class="form-control" id="txtObservacion" rows="3"></textarea>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Vehiculo</h3>
                        <em>- Datos de Vehiculo</em>
                        <div class="btn-group widget-header-toolbar">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                            <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display: none;"><i class="fa fa-chevron-up"></i></a>
                            <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                        </div>
                        <div class="widget-header-toolbar" style="display: none;">
                            <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="form-group col-md-4">
                            <label class="control-label" for="selectMarcaVehiculo"><b>Marca: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectMarcaVehiculo" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="selectModeloVehiculo"><b>Modelo: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectModeloVehiculo" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="selectTecnico"><b>N&uacute;mero de Cilindros: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectCV" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="numSerie"><b>N&uacute;mero Serie: <span style="color: red;">*</span></b></label>
                            <input class="form-control" type="text" id="numSerie" required/>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="txtAnio"><b>Año: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectAnio" required>
                                
                            </select>
                        </div>
                        <div class="form-group col-md-4">
                            <label class="control-label" for="txtPlaca"><b>Placas: <span style="color: red;">*</span></b></label>
                            <input class="form-control " type="text" id="txtPlaca" required/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Kit</h3>
                        <em>- Datos de Kit</em>
                        <div class="btn-group widget-header-toolbar">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                            <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display: none;"><i class="fa fa-chevron-up"></i></a>
                            <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                        </div>
                        <div class="widget-header-toolbar" style="display: none;">
                            <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                        </div>
                    </div>
                    <div class="widget-content">
                        <div class="row">
                            <div class="form-group col-md-4">
                                <label class="control-label" for="selectMarcaKit"><b>Marca: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectMarcaKit" required>
                                    
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="selectModeloKit"><b>Modelo: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectModeloKit" required>
                                    
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="selectCK"><b>N&uacute;mero de Cilindros: <span style="color: red;">*</span></b></label>
                                <select class="form-control" id="selectCK" required>
                                    
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="numVaporizador"><b>Vaporizador: <span style="color: red;">*</span></b></label>
                                <input class="form-control" type="text" id="numVaporizador" placeholder="Número de serie" required/>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="numRiel"><b>Riel Inyector: <span style="color: red;">*</span></b></label>
                                <input class="form-control" type="text" id="numRiel" placeholder="Número de serie" required/>
                            </div>
                            <div class="form-group col-md-4">
                                <label class="control-label" for="numCentralita"><b>Centralita: <span style="color: red;">*</span></b></label>
                                <input class="form-control" type="text" id="numCentralita" placeholder="Número de serie" required/>
                            </div>
                            <div class="form-group col-md-12">
                                <label class="control-label" for="txtObservacionKit"><b>Observaci&oacute;n:</b></label>
                                <textarea class="form-control" id="txtObservacionKit" rows="3"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="widget widget-table">
                    <div class="widget-header">
                        <h3><i class="fa fa-group"></i>Tanques</h3>
                        <em>- Datos de Tanque</em>
                        <div class="btn-group widget-header-toolbar">
                            <a href="#" title="Focus" class="btn-borderless btn-focus" style="display: none;"><i class="fa fa-eye"></i></a>
                            <a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display: none;"><i class="fa fa-chevron-up"></i></a>
                            <a href="#" title="Remove" class="btn-borderless btn-remove" style="display: none;"><i class="fa fa-times"></i></a>
                        </div>
                        <div class="widget-header-toolbar" style="display: none;">
                            <div class="label label-danger"><i class="fa fa-warning"></i>2 Critical Messages</div>
                        </div>
                    </div>
                        <div class="widget-content">
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <div class="portlet light ">
                                        <div class="portlet-body">
                                            <div class="row" id="MuestraTanques">
                                                <div class="form-group col-md-4 col-sm-12">
                                                    <label class="control-label"><b>Marca Tanque: <span style="color: red;">*</span></b></label>
                                                    <select class="form-control marca sc2 mrk selMrkT" id="selectMT0" required>
                                                    
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-12">
                                                    <label class="control-label"><b>Tipo Tanque: <span style="color: red;">*</span></b></label>
                                                    <select class="form-control tipo sc2 tik" id="selectTT0" required>
                                                    
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-4 col-sm-12">
                                                    <label class="control-label"><b>Capacidad Tanque: <span style="color: red;">*</span></b></label>
                                                    <select class="form-control capacidad sc2 cak" id="selectCT0" required>
                                                   
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-3 col-sm-12">
                                                    <label><b>Tanque: <span style="color: red;">*</span></b></label>
                                                    <input class="form-control" type="text" id="numSerieT0" placeholder="Número de serie" required/>
                                                </div>
                                                <div class="form-group col-md-3 col-sm-12">
                                                    <label><b>Fecha Fabricaci&oacute;n:</b></label>
                                                    <input class="form-control" type="date" id="fechaFabricacion0" />
                                                </div>
                                                <div class="form-group col-md-3 col-sm-12">
                                                    <label><b>Multivalvula: <span style="color: red;">*</span></b></label>
                                                    <input class="form-control" type="text" id="numMV0" placeholder="Número de serie" required/>
                                                </div>
                                                <div class="col-lg-1 col-md-3 col-sm-12 col-xs-12">
                                                    <label style="color: white"><b>.</b></label>
                                                    <button id="btn12" class="btn btn-danger" type="button" style="display: block;" onclick="b2();">+</button>
                                                </div>
                                                <div class="col-lg-1 col-md-3 col-sm-12 col-xs-12">
                                                    <label style="color: white"><b>.</b></label>
                                                    <button class="btn btn-default" type="button" style="display: block;" onclick="limpiartanq1();">LIMPIAR SECCION</button>
                                                </div>
                                            </div>
                                        <div class="row" id="addmore2">
                                            <div class="form-group col-md-12 col-sm-12">
                                                <div class="row" id="tanque1" style="display: none;">
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Marca Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control marca sc2 mrk selMrkT" id="selectMT1" style="width: 100%">
                                                    
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Tipo Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control tipo sc2 tik" id="selectTT1" style="width: 100%">
                                                    
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Capacidad Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control capacidad sc2 cak" id="selectCT1" style="width: 100%">
                                                   
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Tanque: <span style="color: red;">*</span></b></label>
                                                        <input class="form-control" type="text" id="numSerieT1" placeholder="Número de serie"/>
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Fecha Fabricaci&oacute;n:</b></label>
                                                        <input class="form-control" type="date" id="fechaFabricacion1" />
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Multivalvula: <span style="color: red;">*</span></b></label>
                                                        <input class="form-control" type="text" id="numMV1" placeholder="Número de serie"/>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-12 col-sm-12">
                                                <div class="row" id="tanque2" style="display: none;">
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Marca Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control marca sc2 mrk selMrkT" id="selectMT2" style="width: 100%">
                                                    
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Tipo Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control tipo sc2 tik" id="selectTT2" style="width: 100%">
                                                    
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-4 col-sm-12">
                                                        <label class="control-label"><b>Capacidad Tanque: <span style="color: red;">*</span></b></label>
                                                        <select class="form-control capacidad sc2 cak" id="selectCT2" style="width: 100%">
                                                   
                                                        </select>
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Tanque: <span style="color: red;">*</span></b></label>
                                                        <input class="form-control" type="text" id="numSerieT2" placeholder="Número de serie"/>
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Fecha Fabricaci&oacute;n:</b></label>
                                                        <input class="form-control" type="date" id="fechaFabricacion2" />
                                                    </div>
                                                    <div class="form-group col-md-3 col-sm-12">
                                                        <label><b>Multivalvula: <span style="color: red;">*</span></b></label>
                                                        <input class="form-control" type="text" id="numMV2" placeholder="Número de serie"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-12" style="margin-top: 15px;">
                                <button type="submit" class="btn btn-info  pull-right"><i class="fa fa-check-circle"></i>Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i>Cancelar</button>
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
                                    <th>Codigo de barras</th>
                                    <th>N&uacute;mero economico</th>
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
    <script src="../scripts/Autoconsumo.js"></script>
</asp:Content>
