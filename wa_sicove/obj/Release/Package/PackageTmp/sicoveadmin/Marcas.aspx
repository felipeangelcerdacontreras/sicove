<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="wa_sicove.sicoveadmin.Marcas" %>
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
				<li class="active">Marcas</li>
			</ul>
		</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Marcas</h2>
			<em>Catálogos</em>
		</div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Marcas</h3> <em>- Lista de marcas</em>
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
						<table id="tabla_lista" class="table table-sorting">
							<thead>
								<tr>
									<th>Descripción</th>
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
                <div class="widget widget-table " id="segmento_marca">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Marca</h3> <em>- Crear o modificar marca</em>
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
                        <div class="form-group">
                            <label class="control-label"><b>Descripción: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="descripcionI" required>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Marca activa?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" id="validar_marca"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>					
                    </div>
				</div>
            </div>
		</div>
        <div class="main-header col-md-12">
			<h2>Modelos</h2>
			<em>Catálogos</em>
		</div>
        <div class="main-content col-md-12">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Modelos</h3> <em>- Lista de modelos</em>
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
						<table id="tabla_lista_modelos" class="table table-sorting">
							<thead>
								<tr>
									<th>Descripción</th>
                                    <th>Marca</th>
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
                <div class="widget widget-table " id="segmento_modelo">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Modelo</h3> <em>- Crear o modificar modelo</em>
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
                        <div class="form-group">
                            <label class="control-label"><b>Descripción:</b></label>
							<input type="text" class="form-control" id="descripcionMI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Marca:</b></label>
                            <select class="form-control" id="selectMr" required>
                                <option value="">Seleccionar Marca</option>
                            </select>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH_modelo">
								<span>¿Marca activa?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" id="validar_modelos"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar_modelo()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>					
                    </div>
				</div>
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
        var tabla_lista_modelos = $("#tabla_lista_modelos").DataTable({
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
                [1, 'asc']
            ],
            "lengthMenu": [
                [5, 10, 50, 100, -1],
                [5, 10, 50, 100, "Todos"]
            ],
            "pageLength": 5,


        });

        traer_marcas();
        traer_modelos();
        var editando = false;
        var id_editando = 0;
        var editando_modelo = false;
        var id_edicion_modelo = 0;

       
        $("#validar_marca" ).click(function() {
            var descripcion = $("#descripcionI").val();
            if (descripcion.length < 1) {
                $('#form1 #segmento_marca').validator('validate');
                $.gritter.add({
                    title: "Informacion",
                    text: "Verifique que los campos esten completos",
                    sticky: false
                });
            } else {
                guardar()
            }
        });
        function guardar() {
            var descripcion = $("#descripcionI").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
            var urlS = "Marcas.aspx/guardar_m";
            if (!editando) {//GUARDAR
                urlS = "Marcas.aspx/guardar_m";
            } else { //EDITANDO
                urlS = "Marcas.aspx/actualizar_m";
            }

            var datas = {
                desc: descripcion,
                activo: activo,
                id: id_editando
            }


            $.ajax({
                type: "POST",
                url: urlS,
                data: JSON.stringify(datas),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var msj = "Se Actualizo correctamente la marca.";
                        if (!editando) {
                            msj = "Se Guardo correctamente la marca.";
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

        function traer_marcas() {
            $("#selectMr").html('<option value="" selected disabled>Selecciona Marca</option>');
            $.ajax({
                type: "POST",
                url: 'Marcas.aspx/traer_marcas',
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
                            }else{
                                $("#selectMr").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                            }
                            tabla_lista.row.add([
                                parsedTest[i]['descripcion'],
                                '<center>'+stado+'</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_marca(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
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
            $('#form1 #segmento_marca').validator('destroy');
            editando = false;
            $("#descripcionI").val("");
            $("#activoCH").prop('checked', true);
            traer_marcas();
        }

        function ver_marca(id, descripcion, estatus) {
            $('#form1 #segmento_marca').validator('destroy');
            editando = true;
            id_editando = id;
            $("#descripcionI").val(descripcion);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
        }


        function traer_modelos() {
            $.ajax({
                type: "POST",
                url: 'Marcas.aspx/traer_modelos',
                data: '{id_marca:0}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        tabla_lista_modelos.clear().draw();
                        for (var i = 0; i < parsedTest.length; i++) {
                            var stado = '<span class="label label-success">Activo</span>';
                            if (parsedTest[i]['estatus'] == 0) {
                                stado = '<span class="label label-critical">Inactivo</span>';
                            } 
                            tabla_lista_modelos.row.add([
                                parsedTest[i]['descripcion'],
                                parsedTest[i]['marca'],
                                '<center>' + stado + '</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_modelo(\'' +
                                parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' +
                                parsedTest[i]['id_marca'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
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

        function ver_modelo(id, descripcion, id_marca, estatus) {
            $('#form1 #segmento_modelo').validator('destroy');
            editando_modelo = true;
            id_edicion_modelo = id;
            $("#descripcionMI").val(descripcion);
            $("#selectMr").val(id_marca);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH_modelo").prop('checked', true);
            } else {
                $("#activoCH_modelo").prop('checked', false);
            }
        }

        function limpiar_modelo() {
            $('#form1 #segmento_modelo').validator('destroy');
            editando_modelo = false;
            id_edicion_modelo = 0;
            $("#descripcionMI").val("");
            $("#selectMr").html('<option value="" selected disabled>Selecciona Marca</option>');
            $("#activoCH_modelo").prop('checked', true);
            traer_marcas();
        }

        $("#validar_modelos" ).click(function() {
            var descripcion = $("#descripcionMI").val();
            if (descripcion.length < 1) {
                $('#form1 #segmento_modelo').validator('validate');
                $.gritter.add({
                    title: "Informacion",
                    text: "Verifique que los campos esten completos",
                    sticky: false
                });
            } else {
                 guardar_modelo()
            }
        });
        function guardar_modelo() {
            var descripcion = $("#descripcionMI").val();
            var selectMr = $("#selectMr").val();
            var activo = $("#activoCH_modelo").prop('checked') == true ? 1 : 0;
            var urlS = "Marcas.aspx/guardar_modelo";
            if (!editando_modelo) {//GUARDAR
                urlS = "Marcas.aspx/guardar_modelo";
            } else { //EDITANDO
                urlS = "Marcas.aspx/actualizar_modelo";
            }
            var datas = {
                desc: descripcion,
                marca: selectMr,
                activo: activo,
                id: id_edicion_modelo
            }
            $.ajax({
                type: "POST",
                url: urlS,
                data: JSON.stringify(datas),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var msj = "Se Actualizo correctamente el modelo.";
                        if (!editando) {
                            msj = "Se Guardo correctamente el modelo.";
                        }
                        $.gritter.add({
                            title: "Éxito",
                            text: msj,
                            sticky: false
                        });
                        traer_modelos()
                        limpiar_modelo();
                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });

        }
    </script>
    
</asp:Content>
