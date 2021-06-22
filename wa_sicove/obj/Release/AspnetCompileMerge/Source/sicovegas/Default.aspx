<%@ Page Title="" Language="C#" MasterPageFile="~/sicovegas/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wa_sicove.sicovegas.Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row">
        <div class='col-lg-4 col-md-2'>
            <div class='box-header'>
                <h4 class='box-title'>CONSUMO MENSUAL</h4>
            </div>
            <div class='box-body'>
                <div class='box-body chart-responsive'>
                    <div class='chart' id='bar-chart1' style='height: 300px;'></div>
                </div>
                <div class='row'>
                    <div class='col-lg-6'>
                        <label>Mes Anterior: </label>
                        <br />
                        <label id="lblMesAnterior"></label>
                    </div>
                    <div class='col-lg-6'>
                        <label style='color: red'>Mes Actual: </label>
                        <br />
                        <label id="lblMesActual"></label>
                    </div>
                </div>
            </div>
        </div>
        <div class='col-lg-4 col-md-2'>
            <div class='box-header'>
                <h4 class='box-title'>CONSUMOS AYER</h4>
            </div>
            <div class='box-body'>
                <div class='box-body chart-responsive'>
                    <div class='chart' id='bar-chart2' style='height: 300px;'></div>
                </div>
                <div class='row'>
                    <div class='col-lg-6'>
                        <label>Unidades Sin Consumo: </label>
                        <br />
                        <label id="lblUnidadS"></label>
                    </div>
                    <div class='col-lg-6'>
                        <label style='color: red'>Unidades Con Consumo: </label>
                        <br />
                        <label id="lblUnidadC"></label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <br />
    </div>
    <div class="row">
        <div class='col-lg-6 col-md-2'>
            <div class='box-header'>
                <h4 class='box-title'>UNIDADES SIN CONSUMO DEL <label id="LblSinConsumo0"></label> AL <label id="LblSinConsumo1"></label></h4>
            </div>
            <div class='box-body'>
                <table id="tabla_sin" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th>Unidad</th>
                            <th>N° Económico</th>
                            <th>Empresa</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class='col-lg-6 col-md-2'>
            <div class='box-header'>
                <h4 class='box-title'>UNIDADES CON MEJOR CONSUMO DEL <label id="LblConsumo0"></label> AL <label id="LblConsumo1"></label></h4>
            </div>
            <div class='box-body'>
                <table id="tabla_mejor" class="table table-sorting display nowrap" style="width: 100%">
                    <thead>
                        <tr>
                            <th>Unidad</th>
                            <th>N° Económico</th>
                            <th>Empresa</th>
                            <th>Consumo</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <style>
        .buttons-excel {
            background-color: lawngreen !important;
            border-top-left-radius: 10px !important;
            border-bottom-left-radius: 10px !important;
            border-top-right-radius: 10px !important;
            border-bottom-right-radius: 10px !important;
        }
         .buttons-pdf {
            background-color: #f73434 !important;
            border-top-left-radius: 10px !important;
            border-bottom-left-radius: 10px !important;
            border-top-right-radius: 10px !important;
            border-bottom-right-radius: 10px !important;
            left: 3%;
        }
    </style>
    <script>
        var numUnidades = 0;
        $(document).ready(function () {
            unidadesnum()
            buscarServicios()
            consumosAyer()
            unidadesSinConsumo()
            unidadesMejorConsumo()
        });
        var t = $('#tabla_sin').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            "scrollX": true,
            dom: '<"top"i><"clear"><"float-left"l><"bottom"f>Brt<"bottom"p>',
            buttons: [
            'excel', 'pdf'
            ]
        });
        var y = $('#tabla_mejor').DataTable({
             "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            },
            "scrollX": true,
            dom: '<"top"i><"clear"><"float-left"l><"bottom"f>Brt<"bottom"p>',
            buttons: [
            'excel', 'pdf'
            ]
        });

        function buscarServicios() {
            //fecha mes pasado 
            const lastmonthlastdate = moment().subtract(1, 'months').startOf('month').format('YYYY-MM-DD');
            const lastmonthfirstdate = moment().subtract(1, 'months').endOf('month').format('YYYY-MM-DD');

            //gnerar fehca de mes anterior con dia primero y ultimo  
            const startOfMonth = moment().startOf('month').format('YYYY-MM-DD');
            const endOfMonth = moment().endOf('month').format('YYYY-MM-DD');

            const fInicial = lastmonthlastdate;
            const fFinal = lastmonthfirstdate;
            const idEstacion = "-1";
            const tipo = "TODOS";
            const modulo = "-1";
            let mesAnterior = 0;
            let resultMes = 0;
            let mesActual = 0;
            let resultMesAc = 0;


            const data = {
                fInicial, fFinal, idEstacion, modulo, tipo
            };

            $.ajax({
                url: "Servicios.aspx/serviciosXFecha",
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(data),
                dataType: "json",
                success: function (result) {

                    if (result.d.Result) {

                        var parsedTest = JSON.parse(result.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {
                            mesAnterior = mesAnterior + parsedTest[i]['litros']
                        }
                        resultMes = mesAnterior.toFixed(2);
                    }
                    const fInicial = startOfMonth;
                    const fFinal = endOfMonth;
                    const idEstacion = "-1";
                    const tipo = "TODOS";
                    const modulo = "-1";


                    const data = {
                        fInicial, fFinal, idEstacion, modulo, tipo
                    };

                    $.ajax({
                        url: "Servicios.aspx/serviciosXFecha",
                        type: "POST",
                        contentType: "application/json;charset=utf-8",
                        data: JSON.stringify(data),
                        dataType: "json",
                        success: function (result) {

                            if (result.d.Result) {

                                var parsedTest = JSON.parse(result.d.Data);
                                for (var i = 0; i < parsedTest.length; i++) {
                                    mesActual = mesActual + parsedTest[i]['litros']
                                }

                                resultMesAc = mesActual.toFixed(2);
                            }
                            $("#lblMesAnterior").text(resultMes);
                            $("#lblMesActual").text(resultMesAc);
                            var bar = new Morris.Bar({
                                element: 'bar-chart1',
                                resize: true,
                                data: [{
                                    y: 'LITROS',
                                    a: resultMes,
                                    b: resultMesAc
                                },],
                                barColors: ['#EF280F', '#E36B2C'],
                                xkey: 'y',
                                ykeys: ['a', 'b'],
                                labels: ['Anterior', 'Actual'],
                                hideHover: 'auto'
                            });
                        }
                    });

                }
            });
        }

        function unidadesnum() {
            var data = '';
            data = JSON.stringify({
                empresa: null,
                ncontrato: null,
                neconomico: null,
                comodatario: $("#sesNom").val(),
                placa: null
            });



            let tUsuario = $('#sesRol').val();
            let url = "Unidades.aspx/traer_u";

            if (tUsuario == 7) {
                url = "Autoconsumo.aspx/traer_u"
            }


            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: data,
                dataType: "json",
                success: function (result) {
                    if (result.d.Result) {
                        var parsedTest = JSON.parse(result.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {
                            if (parsedTest[i]['estatus'] == 1) {
                                numUnidades = numUnidades + 1;
                            }
                        }
                    }
                }
            });
        }

        function consumosAyer() {
            const yesterday = moment(new Date()).add(-1, 'days').format('YYYY-MM-DDT00:00');
            const yesterday2 = moment(new Date()).add(-1, 'days').format('YYYY-MM-DDT23:59');

            let sinconsumo = 0;
            let consumo = 0;
            let consumoTotal = 0;

            var data = '';
            data = JSON.stringify({
                empresa: null,
                ncontrato: null,
                neconomico: null,
                comodatario: $("#sesNom").val(),
                placa: null
            });


            let tUsuario = $('#sesRol').val();
            let url = "Unidades.aspx/traer_u";

            if (tUsuario == 7) {
                url = "Autoconsumo.aspx/traer_u"
            }


            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: data,
                dataType: "json",
                success: function (result) {
                    if (result.d.Result) {
                        var parsedTest = JSON.parse(result.d.Data);
                        for (var i = 0; i < parsedTest.length; i++) {
                            if (parsedTest[i]['estatus'] == 1) {
                                //despues de obtener id de unidad hacer servicios por unidad

                                const fFinal = yesterday2;
                                const fInicial = yesterday;
                                const idEstacionU = "-1";
                                const idModuloU = "";
                                const idUnidadU = parsedTest[i]['id'];
                                const data = {
                                    fInicial, fFinal, idUnidadU, idEstacionU, idModuloU
                                };

                                $.ajax({
                                    url: "Servicios.aspx/serviciosUnidad",
                                    type: "POST",
                                    contentType: "application/json;charset=utf-8",
                                    data: JSON.stringify(data),
                                    dataType: "json",
                                    success: function (result) {
                                        var parsedTest = JSON.parse(result.d.Data);
                                        if (parsedTest.length > 0) {
                                            consumo++;
                                        } else {
                                            sinconsumo++;
                                        }

                                        $("#lblUnidadS").text(sinconsumo);
                                        $("#lblUnidadC").text(consumo);

                                        if (numUnidades == (sinconsumo + consumo)) {
                                            setTimeout($.unblockUI, 500);
                                            GraficaAyer(sinconsumo, consumo);
                                        }

                                    }
                                });

                            }
                        }

                    }
                },
                error: function (errormessage) {
                    console.log(errormessage);
                }
            });
        }

        function GraficaAyer(sinconsumo, consumo) {

            var bar = new Morris.Bar({
                element: 'bar-chart2',
                resize: true,
                data: [{
                    y: 'LITROS',
                    a: sinconsumo,
                    b: consumo
                },],
                barColors: ['#EF280F', '#E36B2C'],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Sin Consumo', 'Consumo'],
                hideHover: 'auto'
            });
        }

        function unidadesSinConsumo() {
            $.blockUI({
                message: '<h2>Espere por favor</h2>',
            });
            const day = moment(new Date()).format('YYYY-MM-DDT23:59');
            const dayOfFive = moment(new Date()).add(-5, 'days').format('YYYY-MM-DDT00:00');

            var dayOfFive1 = moment(dayOfFive).format('DD-MM-YYYY');

            var day1 = moment(day).format('DD-MM-YYYY');

            $("#LblSinConsumo0").text(dayOfFive1);

            $("#LblSinConsumo1").text(day1);

            var data = '';
            data = JSON.stringify({
                empresa: null,
                ncontrato: null,
                neconomico: null,
                comodatario: $("#sesNom").val(),
                placa: null
            });


            let tUsuario = $('#sesRol').val();
            let url = "Unidades.aspx/traer_u";

            if (tUsuario == 7) {
                url = "Autoconsumo.aspx/traer_u"
            }


            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: data,
                dataType: "json",
                success: function (result) {
                    if (result.d.Result) {
                        var parsedT = JSON.parse(result.d.Data);
                        for (var i = 0; i < parsedT.length; i++) {
                            if (parsedT[i]['estatus'] == 1) {
                                //despues de obtener id de unidad hacer servicios por unidad
                                const fFinal = day;
                                const fInicial = dayOfFive;
                                const idEstacionU = "-1";
                                const idModuloU = "";
                                const idUnidadU = parsedT[i]['id'];

                                let codigo_barra = '';
                                let nEco = '';
                                let empresa = '';
                                var tr = '';

                                //datos 
                                codigo_barra = parsedT[i]['codigo_barra'];
                                nEco = parsedT[i]['numero_economico'];
                                empresa = parsedT[i]['empresa'];
                                const data = {
                                    fInicial, fFinal, idUnidadU, idEstacionU, idModuloU
                                };

                                $.ajax({
                                    url: "Servicios.aspx/serviciosUnidad",
                                    type: "POST",
                                    contentType: "application/json;charset=utf-8",
                                    data: JSON.stringify(data),
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.d.Result) {

                                            var parsedTest = JSON.parse(result.d.Data);
                                            if (parsedTest.length > 0) {
                                            } else {
                                                t.row.add([
                                                    codigo_barra,
                                                    nEco,
                                                    empresa
                                                ]).draw(false);
                                            }
                                        }

                                    }
                                });
                            }
                        }
                    }
                },
                error: function (errormessage) {
                    console.log(errormessage);
                }
            });
        }

        function unidadesMejorConsumo() {

            const day = moment(new Date()).format('YYYY-MM-DDT23:59');
            const dayOfFive = moment(new Date()).add(-7, 'days').format('YYYY-MM-DDT00:00');

             var dayOfFive1 = moment(dayOfFive).format('DD-MM-YYYY');

            var day1 = moment(day).format('DD-MM-YYYY');

            $("#LblConsumo0").text(dayOfFive1);

            $("#LblConsumo1").text(day1);

            var data = '';
            data = JSON.stringify({
                empresa: null,
                ncontrato: null,
                neconomico: null,
                comodatario: $("#sesNom").val(),
                placa: null
            });


            let tUsuario = $('#sesRol').val();
            let url = "Unidades.aspx/traer_u";

            if (tUsuario == 7) {
                url = "Autoconsumo.aspx/traer_u"
            }


            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json;charset=utf-8",
                data: data,
                dataType: "json",
                success: function (result) {
                    if (result.d.Result) {
                        var parsedT = JSON.parse(result.d.Data);
                        for (var i = 0; i < parsedT.length; i++) {
                            if (parsedT[i]['estatus'] == 1) {
                                //despues de obtener id de unidad hacer servicios por unidad
                                const fFinal = day;
                                const fInicial = dayOfFive;
                                const idEstacionU = "-1";
                                const idModuloU = "";
                                const idUnidadU = parsedT[i]['id'];

                                let codigo_barra = '';
                                let nEco = '';
                                let empresa = '';
                                let lts = 0;

                                //datos 
                                codigo_barra = parsedT[i]['codigo_barra'];
                                nEco = parsedT[i]['numero_economico'];
                                empresa = parsedT[i]['empresa'];
                                const data = {
                                    fInicial, fFinal, idUnidadU, idEstacionU, idModuloU
                                };

                                $.ajax({
                                    url: "Servicios.aspx/serviciosUnidad",
                                    type: "POST",
                                    contentType: "application/json;charset=utf-8",
                                    data: JSON.stringify(data),
                                    dataType: "json",
                                    success: function (result) {
                                        if (result.d.Result) {
                                            var parsedTest = JSON.parse(result.d.Data);
                                            if (parsedTest.length > 0) {
                                                for (var i = 0; i < parsedTest.length; i++) {
                                                    if (parsedTest[i]['un_codigo_barra'] == codigo_barra) {
                                                        lts = lts + parsedTest[i]['ve_litros'];
                                                    }
                                                }
                                                if (lts > 105) {
                                                    y.row.add([
                                                        codigo_barra,
                                                        nEco,
                                                        empresa,
                                                        lts.toFixed(2) + " Lts"
                                                    ]).draw(false);
                                                    lts = 0;
                                                }
                                            }
                                        }

                                    }
                                });
                            }
                        }
                    }
                },
                error: function (errormessage) {
                    console.log(errormessage);
                }
            });
        }
    </script>
    <script src="../assets/js/jquery.blockUI.js"></script>
</asp:Content>
