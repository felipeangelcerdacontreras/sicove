<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Modulos.aspx.cs" Inherits="wa_sicove.sicoveadmin.Modulos" %>
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
			<li class="active">Modulos</li>
		</ul>
	</div>
    
    <div class="content">
		<div class="main-header">
			<h2>Modulos</h2>
			<em>Administración</em>
		</div>
		<div class="main-content">
			<div class="col-md-8">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-group"></i> Modulos</h3> <em>- Lista de modulos</em>
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
                                    <th>Pertenece A</th>
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
						<h3><i class="fa fa-group"></i> Modulo</h3> <em>- Crear o modificar modulos</em>
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
                            <label class="control-label"><b>Nombre: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="NombreI" required >
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Archivo: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="ArchivoI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Descripcion: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="DescripcionI" required>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Pertenece a: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectPadre" required>
                                <option value="0">Seleccionar Padre</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Nivel: <span style="color: red;">*</span></b></label>
                            <select class="form-control" id="selectNivel" required>
                                <option value="-1">Seleccionar Nivel</option>
                                <option value="0">Padre</option>
                                <option value="1">Hijo</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><b>Orden: <span style="color: red;">*</span></b></label>
							<input type="text" class="form-control" id="OrdenI" required>
                        </div>
						<div class="form-group">
                            <label class="control-label"><b>Activo:</b></label>
							<label class="col-sm-12 fancy-checkbox">
								<input type="checkbox" checked="checked" id="activoCH">
								<span>¿Modulo activo?</span>
							</label>	
                        </div>					
						<div class="form-group" >
							<div class="col-sm-12" style="margin-bottom: 15px;">
								<button type="button" class="btn btn-info  pull-right" onclick="guardar()"><i class="fa fa-check-circle"></i> Guardar</button>
                                <button type="button" class="btn btn-critical  pull-right" onclick="limpiar()" style="margin-right: 15px;"><i class="fa fa-cancel"></i> Cancelar</button>
							</div>
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
                [1, 'asc']
            ],
            "lengthMenu": [
                [5, 10, 50, 100, -1],
                [5, 10, 50, 100, "Todos"]
            ],
            "pageLength": 5,


        });
        var editando = false;
        var id_editando = 0;


        traer_padre();
        function traer_padre() {
            $("#selectPadre").html('<option value="0">Es Modulo Padre</option>');
            $.ajax({
                type: "POST",
                url: 'Modulos.aspx/CargaPadre',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {

                            if (parsedTest[i]['estatus'] == 1) {
                                $("#selectPadre").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['mod_nombre'] + ' - ' + parsedTest[i]['mod_descripcion'] + '</option>');
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

        traer_modulos();
        function traer_modulos() {
            $.ajax({
                type: "POST",
                url: 'Modulos.aspx/traer',
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
                                parsedTest[i]['mod_nombre'],
                                '<center>' + parsedTest[i]['desc_padre'] + '</center>',
                                '<center>' + stado + '</center>',
                                '<center><button type="button" class="btn btn-info " onclick="ver_modulo(\'' +
                                parsedTest[i]['id'] + '\',\'' + parsedTest[i]['mod_nombre'] + '\',\'' +
                                parsedTest[i]['mod_archivo'] + '\',\'' + parsedTest[i]['mod_descripcion'] + '\',\'' +
                                parsedTest[i]['mod_nivel'] + '\',\'' + parsedTest[i]['mod_orden'] + '\',\'' +
                                parsedTest[i]['id_padre'] + '\',\'' + parsedTest[i]['estatus'] + '\')"><i class="fa fa-edit"></i></button></center>'
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

        function ver_modulo(id, mod_nombre, mod_archivo, mod_descripcion, mod_nivel, mod_orden, id_padre, estatus) {
            editando = true;
            id_editando = id;
            $("#NombreI").val(mod_nombre);
            $("#ArchivoI").val(mod_archivo);
            $("#DescripcionI").val(mod_descripcion);
            $("#selectNivel").val(mod_nivel);
            $("#selectPadre").val(id_padre);
            $("#OrdenI").val(mod_orden);
            $("#activoCH").prop('checked', true);
            if (estatus == 1 || estatus == '1') {
                $("#activoCH").prop('checked', true);
            } else {
                $("#activoCH").prop('checked', false);
            }
        }

        function limpiar() {
            editando = false;
            id_editando = 0;
            $("#NombreI").val("");
            $("#ArchivoI").val("");
            $("#DescripcionI").val("");
            $("#selectNivel").val("-1");
            $("#selectPadre").val("0");
            $("#OrdenI").val("");
            $("#activoCH").prop('checked', true);
            traer_padre();
            traer_modulos();
        }

        function guardar() {

            var NombreI = $("#NombreI").val();
            var ArchivoI = $("#ArchivoI").val();
            var DescripcionI = $("#DescripcionI").val();
            var selectNivel = $("#selectNivel").val();
            var selectPadre = $("#selectPadre").val();
            var OrdenI = $("#OrdenI").val();
            var activo = $("#activoCH").prop('checked') == true ? 1 : 0;

            if (NombreI.length > 3 && ArchivoI.length > 0) {
                var urlS = "Modulos.aspx/guardar";
                if (!editando) {//GUARDAR
                    urlS = "Modulos.aspx/guardar";
                } else { //EDITANDO
                    urlS = "Modulos.aspx/actualizar";
                }
                
                var datas = {
                    mod_nombre: NombreI,
                    mod_archivo: ArchivoI,
                    mod_descripcion: DescripcionI,
                    mod_nivel: selectNivel,
                    mod_orden: OrdenI,
                    id_padre: selectPadre,
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
                            var msj = "Se Actualizo correctamente el modulo.";
                            if (!editando) {
                                msj = "Se Guardo correctamente el modulo.";
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

        }


    </script>
</asp:Content>
