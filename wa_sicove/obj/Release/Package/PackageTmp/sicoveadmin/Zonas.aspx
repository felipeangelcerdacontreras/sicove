<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Zonas.aspx.cs" Inherits="wa_sicove.sicoveadmin.Zonas" %>
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
				<li class="active">Zonas</li>
			</ul>
		</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Zonas</h2>
			<em>Catálogos</em>
		</div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Zonas</h3> <em>- Lista de Zonas</em>
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
                                    <th>Ciudad</th>
                                    <th>Estado</th>
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
						<h3><i class="fa fa-group"></i> Zona</h3> <em>- Crear o modificar Zona</em>
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
							<input type="text" class="form-control input-mayus" id="NameI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Estado: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectEst" required>
                               
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Ciudad: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectCd" required>
                               <option value="" selected disabled>Selecciona Ciudad</option>
                            </select>
                        </div>
                        
						<div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Zona activo?</span>
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
        var editando = false;
        var id_editando = 0;
        var id_cd_edit = 0;

        // LLENAR DATOS DE SELECT
        traer_estados();
        function traer_estados() {
            $("#selectEst").html('<option value="" selected disabled>Selecciona Estado</option>');
            $.ajax({
                type: "POST",
                url: 'Estados.aspx/traer_estados',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {

                            if (parsedTest[i]['estatus_rol'] == 1) {
                                console.log(parsedTest[i])
                                $("#selectEst").append('<option value="' + parsedTest[i]['id_rol'] + '">' + parsedTest[i]['descripcion_rol'] + '</option>');
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

        $("#selectEst").change(function () {
            if ( $(this).val() > 0) {
                traer_Ciudades($(this).val());
            }
        });

        function traer_Ciudades(id_estado) {
            $("#selectCd").html('<option value="" selected disabled>Selecciona Ciudad</option>');
            $.ajax({
                type: "POST",
                url: 'Ciudades.aspx/traer_cd',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {
                            
                            if (parsedTest[i]['estatus'] == 1 && parsedTest[i]['id_estado'] == id_estado) {
                                $("#selectCd").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['ciudad'] + '</option>');
                            }
                            
                        }
                        if (id_cd_edit > 0) {
                            $("#selectCd").val(id_cd_edit).trigger('change');
                        }

                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
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
           
            var NameI = $("#NameI").val();
            var selectCd = $("#selectCd").val();
            var selectEst = $("#selectEst").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
            var urlS = "Zonas.aspx/guardar_z";
            if (!editando) {//GUARDAR
                urlS = "Zonas.aspx/guardar_z";
            } else { //EDITANDO
                urlS = "Zonas.aspx/actualizar_z";
            }
               
            var datas = {
                nombre: NameI,
                est: selectEst,
                cd: selectCd,
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
                        var msj = "Se Actualizo correctamente la zona.";
                        if (!editando) {
                            msj = "Se Guardo correctamente la zona.";
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

        traer_Zonas();
        function traer_Zonas() {
            $.ajax({
                type: "POST",
                url: 'Zonas.aspx/traer_z',
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
                                parsedTest[i]['zona'],
                                '<center>' + parsedTest[i]['ciudad'] + '</center>',
                                '<center>' + parsedTest[i]['estado'] + '</center>',
                                '<center>' + stado + '</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_zona(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['zona'] + '\',\'' + parsedTest[i]['ciudad'] + '\',\'' + parsedTest[i]['estado'] + '\',\'' + parsedTest[i]['id_estado'] + '\',\'' + parsedTest[i]['id_ciudad'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
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
            id_cd_edit = 0;
            $("#NameI").val("");
            $("#selectCd").val("-1");
            $("#selectEst").val("-1");
            $("#activoCH").prop('checked', true);
            traer_Zonas();
            traer_estados();
            $("#selectCd").html('<option value="" selected disabled>Selecciona Ciudad</option>');
        }

        function ver_zona(id, zona, ciudad, estado, id_estado, id_ciudad, estatus) {
            $("#form1")[0].reset();
            editando = true;
            id_editando = id;
            $("#NameI").val(zona);
            $("#selectEst").val(id_estado).trigger('change');
            id_cd_edit = id_ciudad;
            //$("#selectCd").val(id_ciudad);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
        }
    </script>
</asp:Content>
