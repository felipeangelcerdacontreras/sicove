<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Giros.aspx.cs" Inherits="wa_sicove.sicoveadmin.Giros" %>
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
				<li class="active">Giros</li>
			</ul>
		</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Giros</h2>
			<em>Catálogos</em>
		</div>
		<div class="main-content">
            <%--tabla de giros--%>
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Giros</h3> <em>- Lista de giros</em>
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
            <%--formulario para registro de giros--%>
            <div class="col-md-4">
                <div class="widget widget-table ">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Giro</h3> <em>- Crear o modificar giro</em>
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
						<div class="form-group col-md-6">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Giro activo?</span>
							</label>	
                        </div>
                        <div class="form-group col-md-6">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" id="activoCH_Autoconsumo">
								<span>¿Giro Autoconsumo?</span>
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

        traer_giros();

        var editando = false;
        var id_editando = 0;

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
            var descripcion = $("#descripcionI").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
            var Autoconsumo = $("#activoCH_Autoconsumo").prop('checked') == true ? 1 : 0;
            var urlS = "Giros.aspx/guardar_giro";
            if (!editando) {//GUARDAR
                urlS = "Giros.aspx/guardar_giro";
            } else { //EDITANDO
                urlS = "Giros.aspx/actualizar_giro";
            }

            var datas = {
                desc: descripcion,
                activo: activo,
                autoconsumo: Autoconsumo,
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
                        var msj = "Se Actualizo correctamente el giro.";
                        if (!editando) {
                            msj = "Se Guardo correctamente el giro.";
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

        function traer_giros() {
            $.ajax({
                type: "POST",
                url: 'Giros.aspx/traer_giros',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        console.log(parsedTest);
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
                                parsedTest[i]['descripcion'],
                                '<center>'+autoconsumo+'</center>',
                                '<center>' + stado + '</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_giro(\'' + parsedTest[i]['id'] + '\',\'' + parsedTest[i]['descripcion'] + '\',\'' + parsedTest[i]['estatus'] + '\',\'' + parsedTest[i]['autoconsumo'] + '\')"><i class="fa fa-edit"></i></button></center>'
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
            id_editando = 0;
            $("#descripcionI").val("");
            $("#activoCH").prop('checked', true);
            traer_giros();
        }

        function ver_giro(id, descripcion, estatus, Autoconsumo) {
            $("#form1")[0].reset();
            editando = true;
            id_editando = id;
            $("#descripcionI").val(descripcion);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
            if (Autoconsumo == 1 || Autoconsumo == '1') {
                $("#activoCH_Autoconsumo").prop('checked', true);
            } else {
                $("#activoCH_Autoconsumo").prop('checked', false);
            }
        }
    </script>
    
</asp:Content>
