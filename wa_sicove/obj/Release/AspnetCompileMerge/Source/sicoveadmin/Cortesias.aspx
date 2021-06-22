<%@ Page Title="" Language="C#" MasterPageFile="~/sicoveadmin/Admin.Master" AutoEventWireup="true" CodeBehind="Cortesias.aspx.cs" Inherits="wa_sicove.sicoveadmin.Cortesias" %>
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
				<li class="active">Cortesias</li>
			</ul>
		</div>
    </div>
    <div class="content">
		<div class="main-header">
			<h2>Cortesias</h2>
			<em>Monitores</em>
		</div>
		<div class="main-content">
			<div class="col-md-12">
				<!-- WIDGET TICKET TABLE -->
				<div class="widget widget-table">
					<div class="widget-header">
						<h3><i class="fa fa-edit"></i> Listado de Unidades</h3> <em>carga inicial: cortesía por instalación.</em>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label"><b>Tipo de cortesía:</b></label>
                                <select class="form-control" id="selectCortesias">
                                    <option value="-1">Seleccionar</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label"><b>Fecha Inicio:</b></label>
                                <div class="input-group">
									<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
									<input type="text" id="datepicker1" class="form-control">
								</div>
                                
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label"><b>Fecha Final:</b></label>
                                <div class="input-group">
									<span class="input-group-addon"><i class="fa fa-calendar"></i></span>
									<input type="text" id="datepicker2" class="form-control">
								</div>
                                
                            </div>
                        </div>


                        <div class="col-md-12" style="margin-top:25px;">
						    <table id="tabla_lista" class="table table-sorting">
							<thead>
								<tr>
                                    <th>Código</th>
									<th>Unidad</th>
                                    <th>Giro</th>
									<th>Empresa</th>
                                    <th>Fecha Instalación</th>
									<th>Ver</th>
									<th>Cortesía</th>
								</tr>
							</thead>
							<tbody>
                                 <tr>
                                    <td>CT2015236</td>
									<td>050</td>
                                    <td>TAXI</td>
									<td>20 20</td>
                                    <td>2020-03-12</td>
									<td><button type="button" class="btn btn-info "><i class="fa fa-eye"></i></button></td>
									<td><button type="button" class="btn btn-success "><i class="fa fa-check"></i> Autorizar</button></td>
								</tr>
							</tbody>
						</table>
					
                        </div>
                    </div>
				</div>
				<!-- END WIDGET TICKET TABLE -->
            </div>
		</div>
	</div>

    <div id="modal_autoriza" class="modal fade" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Autorizar Cortesía por Instalación</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <p>Se autorizara la cortesía por instalación a la unidad CT15235623. </p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-success">Autorizar</button>
            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>


    <script>
        $(document).ready(function () {
            var fecha_final = $('#datepicker2').datepicker({
                format: "dd-mm-yyyy",
                language: "es",
                immediateUpdates: true,
                defaultDate: new Date(),
                todayHighlight: true
            })
		    .on('changeDate', function (e) {
		        fecha_final.datepicker('hide');
		    });


            var fecha_inicio = $('#datepicker1').datepicker({
                format: "dd-mm-yyyy",
                language: "es",
                immediateUpdates: true,
                defaultDate: new Date(),
                todayHighlight: true
            })
		    .on('changeDate', function (e) {
		        fecha_inicio.datepicker('hide');
		    });

            $("#datepicker1,#datepicker2").datepicker("setDate", new Date());
        });
        
        traer_tipos();
        function traer_tipos() {
            $("#selectCortesias").html('<option value="-1">Seleccionar</option>');
            $.ajax({
                type: "POST",
                url: 'TiposCortesia.aspx/traer',
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d.Result) {
                        var parsedTest = JSON.parse(response.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {

                            if (parsedTest[i]['estatus'] == 1) {
                                
                                $("#selectCortesias").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                            }

                        }
                        $("#selectCortesias").val("1"); // Instalacion

                    } else {

                    }
                },
                error: function (error) {
                    console.log("ERROR: " + error);
                }
            });
        }


        function aurtorizaModal() {
            $("#modal_autoriza").modal('show');
        }

       
    </script>
</asp:Content>
