<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Dispositivos.aspx.cs" Inherits="wa_sicove.sicoveadmin.Dispositivos" %>
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
			<li class="active">Dispositivos</li>
		</ul>
	</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Dispositivos</h2>
			<em>Catálogos</em>
		</div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Dispositivos</h3> <em>- Lista de dispositivos</em>
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
									<th>No. Equipo</th>
                                    <th>No. Serie</th>
									<th>No. Chip</th>
                                    <th>Zona</th>
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
						<h3><i class="fa fa-group"></i> Dispositivo</h3> <em>- Crear o modificar</em>
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
                            <label class="control-label"><b>No. Equipo: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-only-number" id="noequipo" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>No. Serie: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-only-number" id="noserie" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>No. Chip: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control input-only-number" id="nochip" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Zona: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectZn" required>
                                <option value="">Seleccionar Zona</option>
                            </select>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Dispositivo activo?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="submit" class="btn btn-info  pull-right"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
						</div>					
                    </div>
				</div>
            </div>
		</div>
	</div>
    <script>  
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

        var editando = false;
        var id_editando = 0;

        traer_Dispositivos();
        function traer_Dispositivos() {
            $.ajax({
                type: "POST",
                url: 'Dispositivos.aspx/traer_d',
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
                          
                            tabla_lista.row.add([
                                '<center>' +parsedTest[i]['dis_no_equipo'] + '</center>',
                                '<center>' + parsedTest[i]['dis_no_serie'] + '</center>',
                                '<center>' + parsedTest[i]['dis_no_chip'] + '</center>',
                                '<center>' + parsedTest[i]['zona_desc'] + '</center>',
                                '<center>' + stado + '</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_dispositivo(\'' + 
                                    parsedTest[i]['id'] + '\',\'' + parsedTest[i]['dis_no_equipo'] + '\',\'' + 
                                    parsedTest[i]['dis_no_serie'] + '\',\'' + parsedTest[i]['dis_no_chip'] + '\',\''
                                    + parsedTest[i]['id_zona'] + '\',\''
                                    + parsedTest[i]['estatus'] + 
                                    '\')"><i class="fa fa-edit"></i></button></center>'
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

        traer_Zonas();
        function traer_Zonas() {
            $("#selectZn").html('<option value="" selected disabled>Selecciona Zona</option>');
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

        function ver_dispositivo(id, equipo, serie, chip, id_zona, estatus) {
            $("#form1")[0].reset();
            editando = true;
            id_editando = id;
            $("#noequipo").val(equipo);
            $("#noserie").val(serie);
            $("#nochip").val(chip);
            $("#selectZn").val(id_zona);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
        }

        function limpiar() {
            $("#form1")[0].reset();
            editando = false;
            $("#noequipo").val("");
            $("#noserie").val("");
            $("#nochip").val("");
            $("#selectZn").html('<option value="" selected disabled>Selecciona Zona</option>');
            $("#activoCH").prop('checked', true);
            traer_Dispositivos();
            traer_Zonas()
        }

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
            var equipo = $("#noequipo").val();
            var serie = $("#noserie").val();
            var chip = $("#nochip").val();
            var zona = $("#selectZn").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;

            var urlS = "Dispositivos.aspx/guardar_cd";
            if (!editando) {//GUARDAR
                urlS = "Dispositivos.aspx/guardar_d";
            } else { //EDITANDO
                urlS = "Dispositivos.aspx/actualizar_d";
            }
            var datas = {
                dis_no_equipo: equipo,
                dis_no_serie: serie,
                dis_no_chip: chip,
                id_zona: zona,
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
                        traer_Dispositivos()
                        var msj = "Se Actualizo correctamente el dispositivo.";
                        if (!editando) {
                            msj = "Se Guardo correctamente el dispositivo.";
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


    </script>
</asp:Content>
