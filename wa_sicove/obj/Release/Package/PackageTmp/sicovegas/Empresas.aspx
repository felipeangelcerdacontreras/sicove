<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="wa_sicove.sicoveadmin.Empresas" %>
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
			<li class="active">Empresas</li>
		</ul>
	</div>
    </div>
    <div class="content">
		<div class="main-header" style="margin-bottom: 20px;">
			<h2>Empresas</h2>
			<em>Catálogos</em>
		</div>
		<div class="main-content">
            <%--Empresa--%>
            <div class="col-md-12">
                <div class="widget widget-table ">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Empresa</h3> <em>- Crear o modificar</em>
						<div class="btn-group widget-header-toolbar">
							<a href="#" title="Focus" class="btn-borderless btn-focus" style="display:none;"><i class="fa fa-eye"></i></a>
							<a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display:none;"><i class="fa fa-chevron-up"></i></a>
							<a href="#" title="Remove" class="btn-borderless btn-remove" style="display:none;"><i class="fa fa-times"></i></a>
						</div>
						<div class="widget-header-toolbar" style="display:none;">
							<div class="label label-danger"><i class="fa fa-warning"></i> 2 Critical Messages</div>
						</div>
					</div>
					<div class="widget-content">
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Empresa: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="NameI" required>
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label"><b>Dirección: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="DireccionI" required>
                        </div>
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Zona: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectZn" required>
                                <option value="" selected disabled>Seleccionar Zona</option>
                            </select>
                        </div>
                        <div class="form-group col-md-2">
                            <label class="control-label"><b>Contacto: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="ContactoI" required>
                        </div>
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Teléfono: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-only-number" id="TelefonoI" required>
                        </div>
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Correo: <span style="color: red;">*</span></b></label>
							<input type="email" class="form-control" id="CorreoI" required>
                        </div>
						<div class="form-group col-md-2">
                            <label class="control-label"><b>Activo: </b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Empresa activa?</span>
							</label>	
                        </div>	
                        <div class="form-group col-md-2">
                            <label class="control-label"><b>Autoconsumo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" id="activoCH_Autoconsumo">
								<span>¿Empresa Autoconsumo?</span>
							</label>	
                        </div>	
						<%--<div class="form-group col-md-3" style="margin-top: 15px;">
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" onclick="guardar()"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()"  style="margin-right:15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>		--%>			
                    </div>
				</div>
            </div>
            <%--Facturacion--%>
            <div class="col-md-12">
                <div class="widget widget-table ">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Facturación</h3> <em>- Datos de facturación</em>
						<div class="btn-group widget-header-toolbar">
							<a href="#" title="Focus" class="btn-borderless btn-focus" style="display:none;"><i class="fa fa-eye"></i></a>
							<a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand" style="display:none;"><i class="fa fa-chevron-up"></i></a>
							<a href="#" title="Remove" class="btn-borderless btn-remove" style="display:none;"><i class="fa fa-times"></i></a>
						</div>
						<div class="widget-header-toolbar" style="display:none;">
							<div class="label label-danger"><i class="fa fa-warning"></i> 2 Critical Messages</div>
						</div>
					</div>
					<div class="widget-content">
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Razón Social:</b></label>
							<input type="text" class="form-control" id="RazonFacI" required>
                        </div>
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>R.F.C:</b></label>
							<input type="text" class="form-control input-only-rfc" id="RFCFacI" required>
                        </div>

                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Teléfono:</b></label>
							<input type="text" class="form-control input-only-number" id="TelefonoFacI" required>
                        </div>
                                               
                        <div class="form-group col-md-3">
                            <label class="control-label"><b>Correo Electrónico:</b></label>
							<input type="email" class="form-control" id="CorreoFacI" required>
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label"><b>Direccion:</b></label>
							<input type="text" class="form-control" id="DireccionFacI" required>
                        </div>
		
						<div class="form-group col-md-6" style="margin-top: 15px;">
							<%--<div class="col-sm-12" style="margin-bottom: 15px;">--%>
								<button type="submit" class="btn btn-info  pull-right"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()"  style="margin-right:15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							<%--</div>--%>
						</div>					
                    </div>
				</div>
            </div>
            <%--Tabla de empresas--%>
			<div class="col-md-12">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Empresas</h3> <em>- Lista de empresas</em>
						<div class="btn-group widget-header-toolbar">
							<a href="#" title="Focus" class="btn-borderless btn-focus" style="display:none;"><i class="fa fa-eye"></i></a>
							<a href="#" title="Expand/Collapse" class="btn-borderless btn-toggle-expand"><i class="fa fa-chevron-up"></i></a>
							<a href="#" title="Remove" class="btn-borderless btn-remove" style="display:none;"><i class="fa fa-times"></i></a>
						</div>
						<div class="widget-header-toolbar" style="display:none;">
							<div class="label label-danger"><i class="fa fa-warning"></i> 2 Critical Messages</div>
						</div>
					</div>
					<div class="widget-content">
						<table id="tabla_lista" class="table table-sorting display" style="width:100%">
							<thead>
								<tr>
									<th>Empresa</th>
                                    <th>Dirección</th>
                                    <th>Zona</th>
                                    <th>Contácto</th>
                                    <th>Telefono</th>
                                    <th>Autoconsumo</th>
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

    <script type="text/javascript">
        var tabla_lista = $("#tabla_lista").DataTable({
            "language": {
                "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
            },
            buttons: [
               // { extend: 'print', className: 'btn dark btn-outline' },
               // { extend: 'pdf', className: 'btn green btn-outline' },
               // { extend: 'csv', className: 'btn purple btn-outline ' }
            ],
            "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
            ],
            //responsive: {
            //    details: {
            //    }
            //},
            "order": [
                [0, 'desc']
            ],
            "lengthMenu": [
                [5, 10, 50, 100, -1],
                [5, 10, 50, 100, "Todos"]
            ],
            "pageLength": 5,


        });

        traer_Zonas();
        function traer_Zonas() {
            $("#selectZn").html('<option value="" selected disabled>Seleccionar Zona</option>');
            $.ajax({
                type: "POST",
                url: 'Zonas.aspx/traer_z',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {

                            if (parsedTest[i]['estatus'] == 1) {
                                $("#selectZn").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['zona'] + '</option>');
                            }

                        }
                       

                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
        }

        traer_Empresas();
        
        var editando = false;
        var id_editando = 0;
        var id_editando_rfc = 0;

        /*validar formulario antes de hacer funcion de guardar*/
        $('#form1').validator().on('submit', function (e) {
            if (e.isDefaultPrevented()) {
                $.gritter.add({
				    title: "Informacion",
				    text: "Verifique que los campos esten completos",
				    sticky: false
				});
            } else {
                e.preventDefault();
                guardar();
          }
        })
        function guardar() {
            var NameI = $("#NameI").val();
            var selectZn = $("#selectZn").val();
            var DireccionI = $("#DireccionI").val();
            var ContactoI = $("#ContactoI").val();
            var CorreoI = $("#CorreoI").val();
            var TelefonoI = $("#TelefonoI").val();
            var RazonFacI = $("#RazonFacI").val();
            var RFCFacI = $("#RFCFacI").val();
            var TelefonoFacI = $("#TelefonoFacI").val();
            var CorreoFacI = $("#CorreoFacI").val();
            var DireccionFacI = $("#DireccionFacI").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
            var Autoconsumo = $("#activoCH_Autoconsumo").prop('checked') == true ? 1 : 0;

			var urlS = "Empresas.aspx/guardar_e";
			if (!editando) {//GUARDAR
				urlS = "Empresas.aspx/guardar_e";
			} else { //EDITANDO
				urlS = "Empresas.aspx/actualizar_e";
			}

			var datas = {
				nombre: NameI,
				est: selectZn,
				activo: activo,
				id: id_editando,
				contacto: ContactoI,
				telefono: TelefonoI,
				direccion: DireccionI,
				razon: RazonFacI,
				rfc: RFCFacI,
				telfac: TelefonoFacI,
				correo: CorreoFacI,
				dirfac: DireccionFacI,
                correoI: CorreoI,
                autoconsumo: Autoconsumo
            }
            
			$.ajax({
			    type: "POST",
			    url: urlS,
			    data: JSON.stringify(datas),
			    contentType: "application/json; charset=utf-8",
			    dataType: "json",
			    success: function (response) {
			        if (response.d.Result) {
			            var msj = "Se Actualizo correctamente la empresa.";
			            if (!editando) {
			                msj = "Se Guardo correctamente la empresa.";
			            }
			            $.gritter.add({
			                title: "Éxito",
			                text: msj,
			                sticky: false
			            });
			            limpiar();
			        } else {

			        }
			    },
			    error: function (error) {
			        console.log("ERROR: " + error);
			    }
			});
            
        }

        function traer_Empresas() {
            $.ajax({
                type: "POST",
                url: 'Empresas.aspx/traer_empresas',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        tabla_lista.clear().draw();
                        for (var i = 0; i < parsedTest.length; i++) {
                            var stado = '<span class="label label-success">Activo</span>';
                            if (parsedTest[i]['estatus'] == 0) {
                                stado = '<span class="label label-critical">Inactivo</span>';
                            }
                            var autoconsumo = '<span class="label label-success">Si</span>';
                            if (parsedTest[i]['autoconsumo'] == 0) {
                                autoconsumo = '<span class="label label-critical">No</span>';
                            }
                            tabla_lista.row.add([
                                '<center style="font-size: 14px;">' + parsedTest[i]['empresa'] + '</center>',
                                '<center style="font-size: 14px;">' + parsedTest[i]['direccion'] + '</center>',
                                '<center style="font-size: 14px;">' + parsedTest[i]['zona'] + '</center>',
                                '<center style="font-size: 14px;">' + parsedTest[i]['contacto'] + '</center>',
                                '<center style="font-size: 14px;">' + parsedTest[i]['telefono'] + '</center>',
                                '<center style="font-size: 14px;">' + autoconsumo + '</center>',
                                '<center style="font-size: 14px;">' + stado + '</center>',
                                '<center ><button type="button" class="btn btn-info " onclick="ver_empresa(\'' +
                                parsedTest[i]['id'] + '\',\'' + parsedTest[i]['empresa'] + '\',\'' + parsedTest[i]['zona'] + '\',\'' +
                                parsedTest[i]['id_zona'] + '\',\'' + parsedTest[i]['estatus'] + '\',\'' + parsedTest[i]['direccion'] + '\',\'' +
                                parsedTest[i]['contacto'] + '\',\'' + parsedTest[i]['telefono'] + '\',\'' + parsedTest[i]['correoI'] + '\',\'' +
                                parsedTest[i]['autoconsumo'] + '\')"><i class="fa fa-edit"></i></button></center>'
                            ]).draw();

                        }


                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
        }

        function limpiar() {
            $("#form1")[0].reset();
            editando = false;
            $("#NameI").val("");
            $("#DireccionI").val("");
            $("#ContactoI").val("");
            $("#TelefonoI").val("");
            $("#CorreoI").val("");
            $("#selectZn").val("-1");
            $("#activoCH").prop('checked', true);
            traer_Empresas();
            limpiar_fact();
            traer_Zonas();
        }

        function ver_empresa(id, empresa, zona, id_zona, estatus, direccion, contacto, telefono, correI, autoconsumo) {
            $("#form1")[0].reset();
            editando = true;
            id_editando = id;
            $("#NameI").val(empresa);
            $("#DireccionI").val(direccion);
            $("#ContactoI").val(contacto);
            $("#TelefonoI").val(telefono);
            $("#CorreoI").val(correI);
            $("#selectZn").val(id_zona).trigger('change');
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
            if (autoconsumo == 1 || autoconsumo == '1') {
                $("#activoCH_Autoconsumo").prop('checked', true);
            } else {
                $("#activoCH_Autoconsumo").prop('checked', false);
            }
            traer_info_facturacion();
        }

        function traer_info_facturacion() {
            if (id_editando > 0) {// cargar informacion de facturación de la empresa.

                $.ajax({
                    type: "POST",
                    url: 'Empresas.aspx/traer_facturacion',
                    data: '{id_empresa:' + id_editando + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d.Result) {
                            var parsedTest = JSON.parse(response.d.Data);
                            for (var i = 0; i < parsedTest.length; i++) {

                                $("#RazonFacI").val(parsedTest[i]['razon']);
                                $("#RFCFacI").val(parsedTest[i]['rfc']);
                                $("#TelefonoFacI").val(parsedTest[i]['telefono']);
                                $("#CorreoFacI").val(parsedTest[i]['correo']);
                                $("#DireccionFacI").val(parsedTest[i]['direccion']);
                            }


                        } else {

                        }
                    },
                    error: function (error) {
                        console.log("ERROR: " + error);
                    }
                });
            }
        }

        function limpiar_fact() {
            $("#RazonFacI").val("");
            $("#RFCFacI").val("");
            $("#TelefonoFacI").val("");
            $("#CorreoFacI").val("");
            $("#DireccionFacI").val("");
        }

    </script>
</asp:Content>
