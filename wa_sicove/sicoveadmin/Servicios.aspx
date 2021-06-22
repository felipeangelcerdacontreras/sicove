﻿<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Servicios.aspx.cs" Inherits="wa_sicove.sicoveadmin.Servicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
    .dt-center{
        font-size: 12px;
    }
</style>

<div class="row">
    <div class="col-md-12">
        <ul class="breadcrumb">
            <li><i class="fa fa-home"></i><a href="#">Inicio</a></li>
            <li class="active">Consulta de Servicios</li>
        </ul>
    </div>
</div>
<div class="content">
    <div class="main-header">
        <h2>Servicios</h2>
        <em>Consulta de servicios</em>
    </div>
    <div class="main-content">
        <div class="widget">
            <div class="widget-header">
                <h3>Consultar servicios</h3>
            </div>
            
            <div class="widget-content">
                <ul class="nav nav-tabs nav-tabs-custom-colored" role="tablist">
                    <li class="active"><a href="#servXFecha" role="tab" data-toggle="tab" aria-expanded="true">Servicios por Fecha</a></li>
                    <li class=""><a href="#servResumen" role="tab" data-toggle="tab" aria-expanded="false">Servicios Resumen</a></li>
                    <li class=""><a href="#servXUnidad" role="tab" data-toggle="tab" aria-expanded="false">Servicios por Unidad</a></li>
                    <li class=""><a href="#servXEmpresa" role="tab" data-toggle="tab" aria-expanded="false">Servicios por Empresa</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane fade active in" id="servXFecha">
                        <div class="row">
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fInicialS"><b>Fecha Inicial:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fInicialS" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalS"><b>Fecha Final:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fFinalS" required/>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label" for="idEstacion"><b>Estación:</b></label><br />
                                <select class="form-control" id="idEstacion">
                                </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label class="control-label" for="idModulo"><b>Módulo:</b></label><br />
                                <select class="form-control" id="idModulo">
                                </select>
                            </div>

                            <div class="form-group col-md-2">
                                <label class="control-label" for="idTipo"><b>Mostrar:</b></label><br />
                                <select class="form-control" id="idTipo">
                                    <option value="TODOS">Todos</option>
                                    <option value="AUTOCONSUMO">Autoconsumo</option>
                                    <option value="CORTESIA">Cortesia</option>
                                    <option value="VENTA">Venta</option>
                                    <option value="RECIRCULACION">Recirculación</option>
                                    <option value="DOMESTICO">Doméstico</option>
                                </select>
                            </div>

                            <div class="form-group col-md-1">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarServicios()">Buscar <i class="fa fa-search"></i></button>

                                <%--<button class="btn btn-warning" type="button" onclick="EnviarCorreo()" > Correo </button>--%>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="servResumen">
                        <div class="row">
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fInicialR"><b>Fecha Inicial:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fInicialR" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalR"><b>Fecha Final:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fFinalR" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalR"><b>Zona :</b></label><br /> 
                                <select id="zonaV" name="zonaV" class="form-control" style="width: 220px;">
                                </select>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="estatus"><b>Estatus unidad:</b></label><br /> 
                                <select id="estatus" name="estatus[]" multiple="multiple" style="width: 220px;">
                                    <option value="1">Activo</option>
                                    <option value="2">Desinstalado</option>
                                    <option value="3">Vendido</option>
                                    <option value="4">Robado</option>
                                    <option value="5">Cambio de propietario</option>
                                    <option value="6">Proceso Jurídico</option>
                                    <option value="7">Otro</option>
                                </select>
                            </div>

                        </div>
                        <div class="row">
                            <div class="form-group col-md-3" id="divCambiante">
                                <label class="control-label" for="fFinalR"><b>Consumos:</b></label><br /> 
                                <select id="consumos" name="consumos" class="form-control">
                                    <option value="-1">Todos</option>
                                    <option value="1">Menores a 100</option>
                                    <option value="2">Entre 100 y 200</option>
                                    <option value="3">Entre 200 y 400</option>
                                    <option value="4">Entre 400 y 600</option>
                                    <option value="5">Mayores a 595</option>
                                </select>
                            </div>

                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalR"><b>Tipo:</b></label><br /> 
                                <select id="tipoV" name="tipoV" class="form-control" style="width: 220px;">
                                    <option value="0">Sicove</option>
                                    <option value="1">Autoconsumo</option>
                                    <option value="2">Todos</option>
                                    <option value="3">Externos</option>
                                </select>
                            </div>
                            <div class="form-group col-md-1">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="bServiciosResumen()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="servXUnidad">
                        <div class="row">
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fInicialUni"><b>Fecha Inicial:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fInicialUni" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalUni"><b>Fecha Final:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fFinalUni" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="selectUnidad">Unidad:</label><br />
                                <select class="form-control" id="idUnidadU" style="width: 220px;" required>
                                </select>
                            </div>

                        </div>
                        <div class="row">

                            <div class="form-group col-md-3">
                                <label class="control-label" for="idEstacionU"><b>Estación:</b></label><br />
                                <select class="form-control" id="idEstacionU" style="width: 220px;">
                                </select>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="idModuloU"><b>Módulo:</b></label><br />
                                <select class="form-control" id="idModuloU" style="width: 220px;">
                                </select>
                            </div>

                            <div class="form-group col-md-1">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarServiciosU()">Buscar <i class="fa fa-search"></i></button>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane fade" id="servXEmpresa">
                        <div class="row">
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fInicialUni"><b>Fecha Inicial:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fInicialEmp" required/>
                            </div>
                            <div class="form-group col-md-3">
                                <label class="control-label" for="fFinalUni"><b>Fecha Final:</b></label><br />
                                <input type="datetime-local" class="form-control" id="fFinalEmp" required/>
                            </div>
                            <div class="form-group col-md-3 autoconsumo">
                                <label class="control-label" for="selectUnidad">Empresa:</label><br />
                                <select class="form-control" id="idEmpresaE" style="width: 220px;" required>
                                </select>
                            </div>
                            <div class="form-group col-md-3 autoconsumo" >
                                <label class="control-label" for="selectUnidad">Comodatario:</label><br />
                                <select class="form-control" id="idComodatarioE" style="width: 220px;" required>
                                </select>
                            </div>

                        </div>
                        <div class="row">

                            <div class="form-group col-md-3 autoconsumo">
                                <label class="control-label" for="idEstacionU"><b>Estación:</b></label><br />
                                <select class="form-control" id="idEstacionE" style="width: 220px;">
                                </select>
                            </div>
                            <div class="form-group col-md-3 autoconsumo">
                                <label class="control-label" for="idModuloU"><b>Módulo:</b></label><br />
                                <select class="form-control" id="idModuloE" style="width: 220px;">
                                </select>
                            </div>

                            <div class="form-group col-md-1">
                                <label class="control-label"><b style="color:#F9F9F9;">.</b></label>
                                <br />
                                <button class="btn btn-primary" type="button" onclick="buscarServiciosE()">Buscar <i class="fa fa-search"></i></button>
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
                <h3><i class="fa fa-group"></i>Servicios</h3>
                <em>- Lista de Servicios</em>
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
                    <thead>
                        <tr>
                            <th>Tipo de Venta</th>
                            <th>Unidad</th>
                            <th>N° Económico</th>
                            <th>Empresa</th>
                            <th>Despachador</th>
                            <th>Fecha de la Venta</th>
                            <th>Vale de Cortesía</th>
                            <th>Cantidad Cortesía</th>
                            <th>Kilometraje</th>
                            <th>M. Pago</th>
                            <th>Estación</th>
                            <th>Medidor</th>
                            <th>N° de Servicio</th>
                            <th>Litros</th>
                            <th>P. Gas</th>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <br />

                <table id="tabla_resumen" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th>Nº Contrato</th>
                            <th>Nº Economico</th>
                            <th>Empresa</th>
                            <th>Comodatario</th>
                            <th>Dirección</th>
                            <th>Teléfono</th>
                            <th>Fecha Instalación</th>
                            <th>Estatus Unidad</th>
                            <th>N° Servicios</th>
                            <th>Volúmen</th>
                            <th>Días</th>
                            <th>Promedio</th>
                            <th>Ult. Carga</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                
                <br />

                <div class="row" id="divserviciosUnidad">
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>Unidad: <span id="contratoL"></span></b></label>
                    </div>
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>N° Eco: <span id="nEconomicoL"></span></b></label>
                    </div>
                    <div class="form-group col-md-4">
                        <label class="control-label"><b>Comodatario: <span id="comodatarioL"></span></b></label>
                    </div>
                    <div class="form-group col-md-2">
                        <label class="control-label"><b>Placa: <span id="placaL"></span></b></label>
                    </div>
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>Marca: <span id="marcaL"></span></b></label>
                    </div>
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>Modelo: <span id="modeloL"></span></b></label>
                    </div>
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>Año: <span id="anioL"></span></b></label>
                    </div>
                    <div class="form-group col-md-3">
                        <label class="control-label"><b>Empresa: <span id="empresaL"></span></b></label>
                    </div>
                </div>
                <table id="tabla_unidad" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th>Unidad</th>
                            <th>Despachador</th>
                            <th>Módulo</th>
                            <th>Estación</th>
                            <th>Fecha</th>
                            <th>Servicio</th>
                            <th>Precio</th>
                            <th>Volúmen</th>
                            <th>Total</th>
                            <th>Km Inicial</th>
                            <th>Km Final</th>
                            <th>Km Recorridos</th>
                            <th>Rendimiento</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>

                <br />

                <table id="tabla_empresa" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th>Unidad</th>
                            <th>N° Econ.</th>
                            <th>Comodatario</th>
                            <th>Placa</th>
                            <th>Marca</th>
                            <th>Modelo</th>
                            <th>Año</th>
                            <th>Empresa</th>
                            <th>Giro</th>
                            <th>Despachador</th>
                            <th>Módulo</th>
                            <th>Estación</th>
                            <th>Fecha</th>
                            <th>Servicio</th>
                            <th>Precio</th>
                            <th>Volúmen</th>
                            <th>Total</th>
                            <th>Km Inicial</th>
                            <th>Km Final</th>
                            <th>Km Recorridos</th>
                            <th>Rendimiento</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>
    <script src="../assets/js/jquery.blockUI.js"></script>
<script src="../scripts/Servicios.js"></script>

</asp:Content>
