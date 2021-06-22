﻿let auxEmpresa = null, auxGiro = null, auxTecnico = null, auxMarcaVehiculo = null, auxModelovehiculo = null, auxNCilVehiculo = null,
    auxMarcaKit = null, auxModeloKit = null, auxNCilKit = null, auxMTanq1 = null, auxTTanq1 = null, auxCTanq1 = null, auxMTanq2 = null, auxTTanq2 = null, auxCTanq2 = null, auxMTanq3 = null, auxTTanq3 = null, auxCTanq3 = null;
let agTanque = -1;
let auxtanq = 0;

$(document).ready(function () {
    traer_unidades();
    GetEmpresas();
    GetGiro();
    GetMarcaVehiculo();
    $("#selectModeloVehiculo").html('<option value="" selected disabled>Seleccionar Modelo</option>');
    GetCilindraje();
    GetAnios();
    GetKitMarca();
    $("#selectModeloKit").html('<option value="" selected disabled>Seleccionar Modelo</option>');
    GetMarcaT('.marca');
    GetTipoT('.tipo');
    GetCapacidadT('.capacidad');
    GetTecnico();
    GetTiempoMtto();

    $('#fechaInstalacion').val(toDay());
    $('#fechaFabricacion0').val(toDay());

    $("#selectEmpresa").select2();
    $("#selectGiro").select2();
    $("#selectMarcaVehiculo").select2();
    $("#selectModeloVehiculo").select2();
    $("#selectCV").select2();
    $("#selectAnio").select2();
    $("#selectMarcaKit").select2();
    $("#selectModeloKit").select2();
    $("#selectCK").select2();
    $('.sc2').select2();
    $("#selectTecnico").select2();
    $("#selectTiempoMtto").select2();
    $("#estatusU").select2();



    $("#selectEmpresa").change(function () {
        let emp = $("#selectEmpresa").val();

        if (emp == null && auxEmpresa != null) {
            $("#selectEmpresa").val(auxEmpresa);
            $('#selectEmpresa').trigger('change.select2');
            auxEmpresa = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarEmpresa");
        }
    });

    $("#selectGiro").change(function () {
        let emp = $("#selectGiro").val();

        if (emp == null && auxGiro != null) {
            $("#selectGiro").val(auxGiro);
            $('#selectGiro').trigger('change.select2');
            auxGiro = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarGiro");
        }
    });

    $("#selectMarcaVehiculo").change(function () {
        let marca = $("#selectMarcaVehiculo").val();

        if (marca == null && auxMarcaVehiculo != null) {
            $("#selectMarcaVehiculo").val(auxMarcaVehiculo);
            $('#selectMarcaVehiculo').trigger('change.select2');
            auxMarcaVehiculo = null;
        }

        if ($(this).val() > 0) {
            GetModeloVehiculo($(this).val());
        } else if ($(this).val() == -99) {
            openmodal("agregarMarcaV");
        }
    });

    $("#selectModeloVehiculo").change(function () {
        let marca = $("#selectModeloVehiculo").val();

        if (marca == null && auxModelovehiculo != null) {
            $("#selectModeloVehiculo").val(auxModelovehiculo);
            $('#selectModeloVehiculo').trigger('change.select2');
            auxModelovehiculo = null;
        }

        if ($(this).val() == -99 && $("#selectMarcaVehiculo").val() > 0) {
            openmodal("agregarModeloV");
        }
    });

    $("#selectCV").change(function () {
        let marca = $("#selectCV").val();

        if (marca == null && auxNCilVehiculo != null) {
            $("#selectCV").val(auxNCilVehiculo);
            $('#selectCV').trigger('change.select2');
            auxNCilVehiculo = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarCilindrosV");
        }
    });

    $("#selectMarcaKit").change(function () {
        let marca = $("#selectMarcaKit").val();

        if (marca == null && auxMarcaKit != null) {
            $("#selectMarcaKit").val(auxMarcaKit);
            $('#selectMarcaKit').trigger('change.select2');
            auxMarcaKit = null;
        }

        if ($(this).val() > 0) {
            GetKitModelo($(this).val());
        } else if ($(this).val() == -99) {
            openmodal("agregarMarcaK");
        }
    });

    $("#selectModeloKit").change(function () {
        let marca = $("#selectModeloKit").val();

        if (marca == null && auxModeloKit != null) {
            $("#selectModeloKit").val(auxModeloKit);
            $('#selectModeloKit').trigger('change.select2');
            auxModeloKit = null;
        }

        if ($(this).val() == -99 && $("#selectMarcaKit").val() > 0) {
            openmodal("agregarModeloK");
        }
    });

    $("#selectCK").change(function () {
        let marca = $("#selectCK").val();

        if (marca == null && auxNCilKit != null) {
            $("#selectCK").val(auxNCilKit);
            $('#selectCK').trigger('change.select2');
            auxNCilKit = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarCilindrosK");
        }
    });

    $("#selectTecnico").change(function () {
        let emp = $("#selectTecnico").val();

        if (emp == null && auxTecnico != null) {
            $("#selectTecnico").val(auxTecnico);
            $('#selectTecnico').trigger('change.select2');
            auxTecnico = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarTecnico");
        }
    });


    $("#selectMT0").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTanque");
        }
    });

    $("#selectMT1").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTanque1");
        }
    });

    $("#selectMT2").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTanque2");
        }
    });

    $("#selectTT0").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTT0");
        }
    });

    $("#selectTT1").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTT1");
        }
    });

    $("#selectTT2").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarTT2");
        }
    });

    $("#selectCT0").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarCT0");
        }
    });

    $("#selectCT1").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarCT1");
        }
    });

    $("#selectCT2").change(function () {
        if ($(this).val() == -99) {
            openmodal("agregarCT2");
        }
    });


    $('#modal_util').on('hidden.bs.modal', function (event) {
        var funcion = $("#nombre_util").data("tipo");

        if (funcion == "agregarEmpresa") {
            $("#selectEmpresa").val("").trigger("change");
        }
        if (funcion == "agregarGiro") {
            $("#selectGiro").val("").trigger("change");
        }
        if (funcion == "agregarMarcaV") {
            $("#selectMarcaVehiculo").val("").trigger("change");
        }
        if (funcion == "agregarModeloV") {
            $("#selectModeloVehiculo").val("").trigger("change");
        }
        if (funcion == "agregarCilindrosV") {
           // $("#selectCV").val("").trigger("change");
        }
        if (funcion == "agregarCilindrosK") {
           // $("#selectCK").val("").trigger("change");
        }
        if (funcion == "agregarMarcaK") {
            $("#selectMarcaKit").val("").trigger("change");
        }
        if (funcion == "agregarModeloK") {
            $("#selectModeloKit").val("").trigger("change");
        }
    });
});

var tabla_lista = $("#tabla_lista").DataTable({
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    dom: 'Blfrtip',
    buttons: [{
        extend: 'pdfHtml5',
        orientation: 'portrait',
        pageSize: 'A4',
        title: '',
        exportOptions: {
            columns: [0, 1, 2, 3, 4]
        },
        customize: function (doc) {

            var cols = [];
            cols[1] = {
                stack: [
                    'Servicios Sicove'
                ],
                alignment: 'center',
                bold: true,
                fontSize: 18,
                margin: [0, 10, 0, 0]
            };

            cols[2] = {
                stack: [
                    'Parque Vehicular Autoconsumo'
                ],
                alignment: 'center',
                bold: true,
                fontSize: 14,
                margin: [0, 10, 0, 0]
            };
            // cols[2] = { text: 'Del ' + fInicialT + ' al ' + fFinalT, alignment: 'center', bold: true, fontSize: 12, margin: [0, 15, 0, 0] };
            cols[0] = {
                margin: [40, 10, 0, 0],
                alignment: 'left',
                image: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAx8AAAF8CAYAAACuWwZ4AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAACAASURBVHic7N1pcxz3lSb65zm5VBWAwkIQ4CKKkkhqsSDLbqNlW9e94E73yM24Y0ffF/g+/X0YcV90R4cjNJ47jJnb4fbcZrfdtiRLoiiKorhhR+25/J95kVkgJC8t2yI2nl9ERS0okAUgKytPnv85B3DOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84555xzzjnnnHPOOeecc84599XiYb8A55xzh+J37f+/ys8G/YFfc845dwJ58OGcc08HAjAAWFlZ4dramo1GI164cIGj0YhZlu19HszPzzPLMuZ5zvn5+d/7P9rY2ECSJErTVBsbG3sBRrvdDq1WSx9++CEajYYWFhbC9evXhSoIGV+cc86dYB58OOfcweO+a66urmJtbY2dTocAcOnSJXa7XQLAYDDgaDQiAJw+fZoAML4/Dhimp6cJAHme7+3T998upgpGnShK09TKsmQcxzYcDqNGo8Hx/SzLIgBAA4iKyJIkYQiBIQ6/9+eEFaY4jkOe58otF0bV42malkVRhCiKNBqN1Gw2y6IoQqPRCFmWhVFrFOJuvBeAJEnyudsb2ECyWz2WpqkAoNFoCADW19c1vt1qtQQAd6buqHmrKQBot9taWFjQtWvXgM8HOh7wOOfcAfLgwznnDg5XVlai0WiUzM7OJs1mM+n3+2mapgnJtCzLJIqipCzLxMySEEISQkjMLEGE2GSJpFimOEIUS4oBxAAimWKCEYBIUkwyQoCRjAJChIBIlJEkRSNpgkwQ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',
                fit: [90, 105],
                width: 70,
            };
            var objHeader = {};
            objHeader['columns'] = cols;
            objHeader['heigth'] = 60;
            doc['header'] = objHeader;
            doc['pageMargins'] = [40, 65, 40, 60];

        }
    },
    {
        extend: 'excelHtml5',
        exportOptions: {
            columns: [0, 1, 2, 3, 4]
        },
        customize: function (xlsx) {
            var sheet = xlsx.xl.worksheets['sheet1.xml'];

            $('row:first c', sheet).attr('s', '42');


            /*if (cambioColor == "TODOS") {

                $('row c[r^="A"]', sheet).each(function () {
                    //Spliteamos el valor del texto para definir el color del estilo que aplica
                    var valor = $('is t', this).text();

                    // Get the value
                    if (valor == "RECIRCULACION") {
                        //rojo 14
                        $(this).parents('row').attr('s', '14');
                    }
                    else if (valor == "DOMESTICO") {
                        //amarillo
                        $(this).parents('row').attr('s', '21');
                    }
                    else if (valor == "AUTOCONSUMO") {
                        //verde
                        $(this).parents('row').attr('s', '17');
                    }
                });
            }*/

        }
    }
    ],
    "columnDefs": [
        { "className": "dt-center", "targets": "_all" }
    ],
    "order": [
        [0, 'desc']
    ],
    "lengthMenu": [
        [5, 10, 50, 100, -1],
        [5, 10, 50, 100, "Todos"]
    ],
    "pageLength": 5,
});


var tanq = 1;
var editando = false;
var id_editando = 0;
var id_modelo_ed = 0;
var selectModeloVeh = 0;

function openmodal(id) {
    var titulo_txt = "";
    var label_txt = "";
    if (id == "agregarEmpresa") {
        titulo_txt = "Agregar Empresa";
        label_txt = "Empresa:";
    }
    if (id == "agregarGiro") {
        titulo_txt = "Agregar Giro";
        label_txt = "Giro:";
    }
    if (id == "agregarMarcaV") {
        titulo_txt = "Agregar Marca";
        label_txt = "Nombre de la Marca:";
    }
    if (id == "agregarModeloV") {
        titulo_txt = "Agregar Modelo";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTecnico") {
        titulo_txt = "Agregar Técnico";
        label_txt = "Nombre del Técnico:";
    }
    if (id == "agregarCilindrosV") {
        titulo_txt = "Agregar Cilindros";
        label_txt = "Número de cilindros:";
    }
    if (id == "agregarCilindrosK") {
        titulo_txt = "Agregar Cilindros";
        label_txt = "Número de cilindros:";
    }
    if (id == "agregarMarcaK") {
        titulo_txt = "Agregar Marca";
        label_txt = "Nombre de la Marca:";
    }
    if (id == "agregarModeloK") {
        titulo_txt = "Agregar Modelo";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTanque") {
        titulo_txt = "Agregar Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTanque1") {
        titulo_txt = "Agregar Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTanque2") {
        titulo_txt = "Agregar Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTT0") {
        titulo_txt = "Agregar Tipo de Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTT1") {
        titulo_txt = "Agregar Tipo de Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarTT2") {
        titulo_txt = "Agregar Tipo de Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarCT0") {
        titulo_txt = "Agregar Capacidad de Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarCT1") {
        titulo_txt = "Agregar Capacidad de Tanque";
        label_txt = "Nombre del Modelo:";
    }
    if (id == "agregarCT2") {
        titulo_txt = "Agregar Capacidad de Tanque";
        label_txt = "Nombre del Modelo:";
    }

    $("#label_modal_util").html(label_txt);
    $("#titulo_modal_util").html(titulo_txt);
    $("#nombre_util").data("tipo", id);
    $("#modal_util").modal("show");
    // console.log($("#nombre_util").data("tipo"));
}

// Modal
$('#modal_util').on('shown.bs.modal', function (e) {
    $('#nombre_util').focus();
});

function guarda_util() {
    var funcion = $("#nombre_util").data("tipo");
    if (funcion == "agregarEmpresa") {
        guardar_empresa();
    }
    if (funcion == "agregarGiro") {
        guardar_giro();
    }
    if (funcion == "agregarMarcaV") {
        guardar_marcav();
    }
    if (funcion == "agregarModeloV") {
        guardar_modelov();
    }
    if (funcion == "agregarTecnico") {
        guardar_tecnico();
    }
    if (funcion == "agregarCilindrosV") {
        guardar_cilindros(1);
    }
    if (funcion == "agregarCilindrosK") {
        guardar_cilindros(2);
    }
    if (funcion == "agregarMarcaK") {
        guardar_marcak();
    }
    if (funcion == "agregarModeloK") {
        guardar_modelok();
    }
    if (funcion == "agregarTanque") {
        guardar_marcaTanq(0);
    }
    if (funcion == "agregarTanque1") {
        guardar_marcaTanq(1);
    }
    if (funcion == "agregarTanque2") {
        guardar_marcaTanq(2);
    }
    if (funcion == "agregarTT0") {
        guardar_tipoTanq(0);
    }
    if (funcion == "agregarTT1") {
        guardar_tipoTanq(1);
    }
    if (funcion == "agregarTT2") {
        guardar_tipoTanq(2);
    }
    if (funcion == "agregarCT0") {
        guardar_captanq(0);
    }
    if (funcion == "agregarCT1") {
        guardar_captanq(1);
    }
    if (funcion == "agregarCT2") {
        guardar_captanq(2);
    }

}

function GetEmpresas(selEmpresa = '' ) {
    $("#selectEmpresa").html('<option value="-1" selected disabled>Seleccionar Empresa</option>');
    $.ajax({
        url: "Empresas.aspx/traer_empresas_si_autoconsumo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectEmpresa").append('<option value="-99">*****Agregar Empresa</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectEmpresa").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['empresa'] + '</option>');
                    }
                }

                if (selEmpresa != '') {
                    $("#selectEmpresa option:contains(" + selEmpresa + ")").attr('selected', 'selected');
                    let valorS = $("#selectEmpresa").val();
                    auxEmpresa = valorS;
                    $("#selectEmpresa").val(valorS);
                    $('#selectEmpresa').trigger('change.select2');

                }

            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });

};

function GetGiro(seleccionado = '') {
    $("#selectGiro").html('<option value="-1" selected disabled>Seleccionar Giro</option>');
    $.ajax({
        url: "Giros.aspx/traer_giros_si_autoconsumo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);

                $("#selectGiro").append('<option value="-99">*****Agregar Giro</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectGiro").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectGiro option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectGiro").val();
                    auxGiro = valorS;
                    $("#selectGiro").val(valorS).change();

                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetMarcaVehiculo(seleccionado = '') {
    $("#selectMarcaVehiculo").html('<option value="-1" selected disabled>Seleccionar Marca</option>');
    $.ajax({
        url: "Marcas.aspx/traer_marcas",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectMarcaVehiculo").append('<option value="-99">*****Agregar Marca</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectMarcaVehiculo").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {

                    $("#selectMarcaVehiculo option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectMarcaVehiculo").val();
                    auxMarcaVehiculo = valorS;
                    $("#selectMarcaVehiculo").val(valorS);
                    $('#selectMarcaVehiculo').trigger('change.select2');
                    return;
                }
            }
            
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetModeloVehiculo(id_marca, seleccionado = '') {
    $("#selectModeloVehiculo").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    $.ajax({
        url: "Marcas.aspx/traer_modelos",
        type: "POST",
        data: '{id_marca:"' + id_marca + '"}',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                $("#selectModeloVehiculo").append('<option value="-99">*****Agregar Modelo</option>');
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectModeloVehiculo").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectModeloVehiculo option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectModeloVehiculo").val();
                    auxModelovehiculo = valorS;
                    $("#selectModeloVehiculo").val(valorS);
                }
            }
            
            if (selectModeloVeh > 0) {
                $('#selectModeloVehiculo').val(selectModeloVeh).trigger('change');
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetCilindraje(seleccionado = '', inputsel = 0) {
    const cil1 = $("#selectCV").val();
    const cil2 = $("#selectCK").val();

    $("#selectCV").html('<option value="-1" selected disabled>Seleccionar Cilindros</option>');
    $("#selectCK").html('<option value="-1" selected disabled>Seleccionar Cilindros</option>');
    $.ajax({
        url: "Cilindros.aspx/traer_cilindros",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectCV").append('<option value="-99">*****Agregar Cilindraje</option>');
                $("#selectCK").append('<option value="-99">*****Agregar Cilindraje</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectCV").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['numero'] + '</option>');
                        $("#selectCK").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['numero'] + '</option>');

                    }
                }

                if (inputsel == 1) {
                    if (seleccionado != '') {
                        $("#selectCV option:contains(" + seleccionado + ")").attr('selected', 'selected');
                        let valorS = $("#selectCV").val();
                        $("#selectCV").val(valorS).change();
                        $("#selectCK").val(cil2).change();
                    }
                } else if (inputsel == 2) {
                    if (seleccionado != '') {
                        $("#selectCK option:contains(" + seleccionado + ")").attr('selected', 'selected');
                        let valorS = $("#selectCK").val();
                        $("#selectCK").val(valorS).change();
                        $("#selectCV").val(cil1).change();
                    }
                }

            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetAnios() {
    $("#selectAnio").html('<option value="-1" selected disabled>Seleccionar Año</option>');
    var year = moment().year();
    year = year + 1;
    for (var i = 70; i >= 0; i--) {
        year = year - 1
        $("#selectAnio").append('<option value="' + year + '">' + year + '</option>');
    }
}

function GetKitMarca(seleccionado = '' ) {
    $("#selectMarcaKit").html('<option value="-1" selected disabled>Seleccionar Marca</option>');
    $.ajax({
        url: "Marcakit.aspx/traer_marcas",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectMarcaKit").append('<option value="-99">*****Agregar Marca</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectMarcaKit").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectMarcaKit option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectMarcaKit").val();
                    auxMarcaKit = valorS;
                    $("#selectMarcaKit").val(valorS).change();
                }
                
            }

        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetKitModelo(id_kit_marca, seleccionado = '') {
    $("#selectModeloKit").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    $.ajax({
        url: "Marcakit.aspx/traer_modelos",
        type: "POST",
        data: '{id_marca:"' + id_kit_marca + '"}',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectModeloKit").append('<option value="-99">*****Agregar Modelo</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectModeloKit").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectModeloKit option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectModeloKit").val();
                    auxModeloKit = valorS;
                    $("#selectModeloKit").val(valorS).change();
                }
            }
            
            if (id_modelo_ed > 0) {
                $('#selectModeloKit').val(id_modelo_ed).trigger('change');
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetMarcaT(selector, seleccionado = '', cambio = -1) {

    const sel1 = $('#selectMT0').val();
    const sel2 = $('#selectMT1').val();
    const sel3 = $('#selectMT2').val();

    $(selector).html('<option value="-1" selected disabled>Selecciona Marca</option>');
    $.ajax({
        url: "MarcaTanque.aspx/traer_marca",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $(selector).append('<option value="-99">*****Agregar Marca</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $(selector).append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    let valorS = "";
                    switch (auxtanq) {
                        case 0:
                            $("#selectMT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                            valorS = $("#selectMT0").val();
                            $("#selectMT0").val(valorS).change();
                            break;
                        case 1:
                            if (cambio == 1) {
                                $("#selectMT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectMT1").val();
                                $("#selectMT1").val(valorS).change();
                                $("#selectMT0").val(sel1).change();
                            } else {
                                $("#selectMT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectMT0").val();
                                $("#selectMT0").val(valorS).change();
                                $("#selectMT1").val(sel2).change();
                            }
                            break;
                        case 2:
                            switch (cambio) {
                                case 0:
                                    $("#selectMT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectMT0").val();
                                    $("#selectMT2").val(sel3).change();
                                    $("#selectMT1").val(sel2).change();
                                    $("#selectMT0").val(valorS).change();
                                    break;
                                case 1:
                                    $("#selectMT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectMT1").val();
                                    $("#selectMT2").val(sel3).change();
                                    $("#selectMT1").val(valorS).change();
                                    $("#selectMT0").val(sel1).change();
                                    break;
                                case 2:
                                    $("#selectMT2 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectMT2").val();
                                    $("#selectMT2").val(valorS).change();
                                    $("#selectMT1").val(sel2).change();
                                    $("#selectMT0").val(sel1).change();
                                    break;
                            }
                            break;
                    }
                }
            }

        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetTipoT(selector, seleccionado = '', cambio = -1) {

    const sel1 = $('#selectTT0').val();
    const sel2 = $('#selectTT1').val();
    const sel3 = $('#selectTT2').val();

    $(selector).html('<option value="-1" selected disabled>Selecciona Tipo</option>');
    $.ajax({
        url: "TipoTanque.aspx/traer_tipo",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $(selector).append('<option value="-99">*****Agregar Tipo</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $(selector).append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    let valorS = "";
                    switch (auxtanq) {
                        case 0:
                            $("#selectTT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                            valorS = $("#selectTT0").val();
                            $("#selectTT0").val(valorS).change();
                            break;
                        case 1:
                            if (cambio == 1) {
                                $("#selectTT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectTT1").val();
                                $("#selectTT1").val(valorS).change();
                                $("#selectTT0").val(sel1).change();
                            } else {
                                $("#selectTT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectTT0").val();
                                $("#selectTT0").val(valorS).change();
                                $("#selectTT1").val(sel2).change();
                            }
                            break;
                        case 2:
                            switch (cambio) {
                                case 0:
                                    $("#selectTT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectTT0").val();
                                    $("#selectTT2").val(sel3).change();
                                    $("#selectTT1").val(sel2).change();
                                    $("#selectTT0").val(valorS).change();
                                    break;
                                case 1:
                                    $("#selectTT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectTT1").val();
                                    $("#selectTT2").val(sel3).change();
                                    $("#selectTT1").val(valorS).change();
                                    $("#selectTT0").val(sel1).change();
                                    break;
                                case 2:
                                    $("#selectTT2 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectTT2").val();
                                    $("#selectTT2").val(valorS).change();
                                    $("#selectTT1").val(sel2).change();
                                    $("#selectTT0").val(sel1).change();
                                    break;
                            }
                            break;
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetCapacidadT(selector, seleccionado = '', cambio = -1) {

    const sel1 = $('#selectCT0').val();
    const sel2 = $('#selectCT1').val();
    const sel3 = $('#selectCT2').val();

    $(selector).html('<option value="-1" selected disabled>Selecciona Capacidad</option>');
    $.ajax({
        url: "CapacidadTanque.aspx/traer_capacidad",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $(selector).append('<option value="-99">*****Agregar Capacidad</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $(selector).append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                    }

                }

                if (seleccionado != '') {
                    let valorS = "";
                    switch (auxtanq) {
                        case 0:
                            $("#selectCT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                            valorS = $("#selectCT0").val();
                            $("#selectCT0").val(valorS).change();
                            break;
                        case 1:
                            if (cambio == 1) {
                                $("#selectCT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectCT1").val();
                                $("#selectCT1").val(valorS).change();
                                $("#selectCT0").val(sel1).change();
                            } else {
                                $("#selectCT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                valorS = $("#selectCT0").val();
                                $("#selectCT0").val(valorS).change();
                                $("#selectCT1").val(sel2).change();
                            }
                            break;
                        case 2:
                            switch (cambio) {
                                case 0:
                                    $("#selectCT0 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectCT0").val();
                                    $("#selectCT2").val(sel3).change();
                                    $("#selectCT1").val(sel2).change();
                                    $("#selectCT0").val(valorS).change();
                                    break;
                                case 1:
                                    $("#selectCT1 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectCT1").val();
                                    $("#selectCT2").val(sel3).change();
                                    $("#selectCT1").val(valorS).change();
                                    $("#selectCT0").val(sel1).change();
                                    break;
                                case 2:
                                    $("#selectCT2 option:contains(" + seleccionado + ")").attr('selected', 'selected');
                                    valorS = $("#selectCT2").val();
                                    $("#selectCT2").val(valorS).change();
                                    $("#selectCT1").val(sel2).change();
                                    $("#selectCT0").val(sel1).change();
                                    break;
                            }
                            break;
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetTecnico(seleccionado = '') {
    $("#selectTecnico").html('<option value="-1" selected disabled>Seleccionar Técnico</option>');
    $.ajax({
        url: "Personal.aspx/traer_p",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectTecnico").append('<option value="-99">*****Agregar Técnico</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1 && parsedTest[i]['id_tipo_personal'] == 2) {
                        $("#selectTecnico").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['nombre'] + ' ' + parsedTest[i]['apellido'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectTecnico option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectTecnico").val();
                    auxTecnico = valorS;
                    $("#selectTecnico").val(valorS);
                    $('#selectTecnico').trigger('change.select2');
                }
            }
            
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function GetTiempoMtto() {
    $("#selectTiempoMtto").html('<option value="-1" selected disabled>Seleccionar Periodo de tiempo</option>');
    $.ajax({
        url: "tipoTiempos.aspx/cTiempos",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    $("#selectTiempoMtto").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['descripcion'] + '</option>');
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

async function b2() {
    if (tanq < 3) {
        tanq = tanq + 1;

        let coT = auxtanq + 1;
        $('#fechaFabricacion' + coT).val(toDay());
        $('#tanque' + coT).show();

        $('#selectMT' + coT).prop('required', true);
        $('#selectTT' + coT).prop('required', true);
        $('#selectCT' + coT).prop('required', true);
        $('#numSerieT' + coT).prop('required', true);
        $('#numMV' + coT).prop('required', true);
        auxtanq++;
    }
}

//EMPRESA
function guardar_empresa() {

    var NameI = $("#nombre_util").val();
    var selectZn = 1;
    var DireccionI = "PENDIENTE";
    var ContactoI = "PENDIENTE";
    var CorreoI = "PENDIENTE";
    var TelefonoI = "PENDIENTE";
    var RazonFacI = "PENDIENTE";
    var RFCFacI = "PENDIENTE";
    var TelefonoFacI = "PENDIENTE";
    var CorreoFacI = "PENDIENTE";
    var DireccionFacI = "PENDIENTE";
    var activo = 1;
    var autoconsumo = 1;
    if (NameI.length > 3 && selectZn > 0 && DireccionI.length > 3 && ContactoI.length > 3 && TelefonoI.length > 3) {
        var urlS = "Empresas.aspx/guardar_e";
        var datas = {
            nombre: NameI,
            est: selectZn,
            activo: activo,
            id: 0,
            contacto: ContactoI,
            telefono: TelefonoI,
            direccion: DireccionI,
            razon: RazonFacI,
            rfc: RFCFacI,
            telfac: TelefonoFacI,
            correo: CorreoFacI,
            dirfac: DireccionFacI,
            correoI: CorreoI,
            autoconsumo: autoconsumo
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente la empresa.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetEmpresas(NameI);
                    $("#modal_util").modal("hide");
                    $("#selectEmpresa").focus();
                } else {
                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//GIROS
function guardar_giro() {
    var descripcion = $("#nombre_util").val();
    var activo = 1;
    if (descripcion.length > 3) {
        var urlS = "Giros.aspx/guardar_giro";
        var datas = {
            desc: descripcion,
            activo: activo,
            id: 0,
            autoconsumo: 1
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el giro.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetGiro(descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectGiro").focus();

                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//MARCA VEHICULOS
function guardar_marcav() {
    var descripcion = $("#nombre_util").val();
    var activo = 1;
    if (descripcion.length > 1) {
        var urlS = "Marcas.aspx/guardar_m";

        var datas = {
            desc: descripcion,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente la marca.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetMarcaVehiculo(descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectMarcaVehiculo").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//MODELOS VEHICULOS
function guardar_modelov() {
    var descripcion = $("#nombre_util").val();
    var selectMr = $("#selectMarcaVehiculo").val();
    var activo = 1;
    if (descripcion.length > 1 && selectMr > 0) {
        var urlS = "Marcas.aspx/guardar_modelo";
        var datas = {
            desc: descripcion,
            marca: selectMr,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el modelo.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetModeloVehiculo(selectMr, descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectModeloVehiculo").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//CILINDRAJE
function guardar_cilindros(select = 0) {
    var numCilindro = $("#nombre_util").val();
    var activo = 1;

    if (numCilindro.length > 0) {
        var urlS = "Cilindros.aspx/guardar_cilindro";
        var datas = {
            numero: numCilindro,
            activo: activo,
            id: 0
        }

        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente la marca.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetCilindraje(numCilindro, select);
                    $("#modal_util").modal("hide");

                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

//MARCA KIT
function guardar_marcak() {
    var descripcion = $("#nombre_util").val();
    var activo = 1;

    if (descripcion.length > 3) {
        var urlS = "MarcaKit.aspx/guardar_marcas";

        var datas = {
            desc: descripcion,
            activo: activo,
            id: 0
        }

        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente la marca.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetKitMarca(descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectMarcaKit").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

//MODELO KIT 
function guardar_modelok() {

    var descripcion = $("#nombre_util").val();
    var selectMr = $("#selectMarcaKit").val();
    var activo = 1;

    if (descripcion.length > 1 && selectMr > 0) {
        var urlS = "MarcaKit.aspx/guardar_modelo";
        var datas = {
            desc: descripcion,
            marca: selectMr,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el modelo.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetKitModelo(selectMr, descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectModeloKit").focus();
                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

function guardar_tecnico() {
    var nombre = $("#nombre_util").val();
    var apellido = "PENDIENTE";
    var numEmpleado = "PENDIENTE";
    var codigoBarra = "PENDIENTE";
    var zona = 1;
    var tipo = 2;
    var activo = 1;

    if (nombre.length > 3 && apellido.length > 3 && zona > 0 && tipo > 0) {
        urlS = "Personal.aspx/guardar_p";


        var datas = {
            nombre: nombre,
            apellido: apellido,
            numero_empleado: numEmpleado,
            codigo_barras: codigoBarra,
            id_zona: zona,
            id_tipo_personal: tipo,
            activo: activo,
            id: 0
        }

        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el personal.";
                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetTecnico(nombre);
                    $("#modal_util").modal("hide");
                    $("#selectTecnico").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    };
};

// MARCA Tanque
function guardar_marcaTanq(cambiar = -1) {

    var descripcion = $("#nombre_util").val();
    var activo = 1;

    if (descripcion.length > 1) {
        var urlS = "MarcaTanque.aspx/guardar_marca";
        var datas = {
            descripcion: descripcion,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el modelo.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetMarcaT('.marca', descripcion, cambiar);
                    $("#modal_util").modal("hide");
                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

// Tipo Tanque
function guardar_tipoTanq(cambiar = -1) {

    var descripcion = $("#nombre_util").val();
    var activo = 1;

    if (descripcion.length > 1) {
        var urlS = "TipoTanque.aspx/guardar_tipo";
        var datas = {
            descripcion: descripcion,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el modelo.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetTipoT('.tipo', descripcion, cambiar);
                    $("#modal_util").modal("hide");
                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

// Capacidad Tanque
function guardar_captanq(cambiar = -1) {

    var descripcion = $("#nombre_util").val();
    var activo = 1;

    if (descripcion.length > 1) {
        var urlS = "CapacidadTanque.aspx/guardar_capacidad";
        var datas = {
            descripcion: descripcion,
            activo: activo,
            id: 0
        }
        $.ajax({
            type: "POST",
            url: urlS,
            data: JSON.stringify(datas),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.d.Result) {
                    var msj = "Se Guardo correctamente el modelo.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetCapacidadT('.capacidad', descripcion, cambiar);
                    $("#modal_util").modal("hide");
                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
};

function limpiar() {
    $('#numEconomico').attr("disabled", false);
    $('.marca select option').remove();
    $("#selectEmpresa").val(-1).trigger("change");
    $("#selectGiro").val(-1).trigger("change");
    $("#selectLider").val(-1).trigger("change");
    $("#selectComodatario").val(-1).trigger("change");
    $("#selectTecnico").val(-1).trigger("change");
    $("#selecetEstInst").val(-1).trigger("change");
    $("#selectMarcaVehiculo").val(-1).trigger("change");
    $("#selectCV").val(-1).trigger("change");
    $("#selectCK").val(-1).trigger("change");
    $("#selectMarcaKit").val(-1).trigger("change");
    $("#selectMT0").val(-1).trigger("change");
    $("#selectTT0").val(-1).trigger("change");
    $("#selectCT0").val(-1).trigger("change");
    $("#selectAnio").val(-1).trigger("change");

    $("#selecetEstInst").html('');
    //GetEstInstalacion()

    $("#selectEmpresa").val(-1).trigger("change");
    $("#selectGiro").val(-1).trigger("change");
    $("#selectModeloVehiculo").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    $("#selectModeloKit").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    limpiartanq();
    $("#form1")[0].reset();
    tanq = 1;
    editando = false;
    id_editando = 0;
    id_modelo_ed = 0;
    selectModeloVeh = 0;
    GetTiempoMtto();
    $('#fechaInstalacion').val(toDay());
    $('#fechaFabricacion0').val(toDay());

    /*
    GetGiro();
    GetMarcaVehiculo();
    GetCilindraje();
    GetKitMarca();
    GetMarcaT()
    GetTipoT();
    GetCapacidadT();*/
};

function limpiartanq() {
    tanq = 1;
    auxtanq = 0;

    $('#selectMT1').prop('required', false);
    $('#selectTT1').prop('required', false);
    $('#selectCT1').prop('required', false);
    $('#numSerieT1').prop('required', false);
    $('#numMV1').prop('required', false);
    $('#tanque1').hide();

    $('#selectMT2').prop('required', false);
    $('#selectTT2').prop('required', false);
    $('#selectCT2').prop('required', false);
    $('#numSerieT2').prop('required', false);
    $('#numMV2').prop('required', false);
    $('#tanque2').hide();
}

function limpiartanq1() {
    auxtanq = auxtanq == 3 ? 2 : auxtanq;

    if (auxtanq != 0) {
        tanq -= 1;
        tanq = tanq == 0 ? 1 : tanq; 

        $('#selectMT' + auxtanq).prop('required', false);
        $('#selectTT' + auxtanq).prop('required', false);
        $('#selectCT' + auxtanq).prop('required', false);
        $('#numSerieT' + auxtanq).prop('required', false);
        $('#numMV' + auxtanq).prop('required', false);

        $('#selectMT' + auxtanq).val('-1').change();
        $('#selectTT' + auxtanq).val('-1').change();
        $('#selectCT' + auxtanq).val('-1').change();
        $('#numSerieT' + auxtanq).val('');
        $('#numMV' + auxtanq).val('');

        $('#tanque' + auxtanq).hide();
        auxtanq--;
    }

}

function toDay() {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);

    return today;
};

function traer_unidades() {
    $.ajax({
        url: "Autoconsumo.aspx/traer_u",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                tabla_lista.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    var stado = '<span class="label label-success">Activo</span>';

                    if (parsedTest[i]['estatus'] == 0) {
                        stado = '<span class="label label-critical">Inactivo</span>';
                    }

                    tabla_lista.row.add([
                        parsedTest[i]['empresa'],
                        parsedTest[i]['giro'],
                        parsedTest[i]['codigo_barra'],
                        parsedTest[i]['numero_economico'],
                        '<center>' + stado + '</center>',
                        '<center><button type="button" class="btn btn-info " onclick="ver_unidad(\'' + parsedTest[i]['id'] + '\')"><i class="fa fa-edit"></i></button></center>'
                    ]).draw();
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function ver_unidad(id) {
    limpiartanq()
    $("#form1")[0].reset();
    $('#numEconomico').attr("disabled", true);
    editando = true;
    id_editando = id;
    ver_kit(id);
    ver_tanque(id);
    ver_Vehiculo(id);
};

function ver_kit(id) {
    $.ajax({
        url: "Autoconsumo.aspx/traer_k",
        type: "POST",
        data: JSON.stringify({ id: id }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);

                for (var i = 0; i < parsedTest.length; i++) {
                    $("#selectMarcaKit").val(parsedTest[i]['id_marca']).trigger('change');
                    id_modelo_ed = parsedTest[i]['id_modelo'];
                    $("#selectCK").val(parsedTest[i]['id_cilindro']).trigger('change');
                    $("#numVaporizador").val(parsedTest[i]['vaporizador']);
                    $("#numRiel").val(parsedTest[i]['riel']);
                    $("#numCentralita").val(parsedTest[i]['centralita']);
                    $("#txtObservacionKit").val(parsedTest[i]['observacion']);
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};

function ver_tanque(id) {
    $.ajax({
        url: "Autoconsumo.aspx/traer_t",
        type: "POST",
        data: JSON.stringify({ id: id }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);

                if (parsedTest.length > 0) {
                    for (var i = 0; i < parsedTest.length; i++) {
                        auxtanq++;
                        $('#selectMT' + i).val(parsedTest[i]['id_marca_tanque']).trigger('change');
                        $('#selectTT' + i).val(parsedTest[i]['id_tipo_tanque']).trigger('change');
                        $('#selectCT' + i).val(parsedTest[i]['id_capacidad']).trigger('change');
                        $('#numSerieT' + i).val(parsedTest[i]['numero_serie']);
                        $('#fechaFabricacion' + i).val($.formattedDate(new Date(parseInt(parsedTest[i]['fecha_fabricacion'].substr(6)))));
                        $('#numMV' + i).val(parsedTest[i]['multivalvula']);
                        $('#tanque' + i).show();
                    }
                    tanq = tanq + (auxtanq - 1);
                    auxtanq = tanq - 1;
                }

            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function ver_Vehiculo(id) {
    $.ajax({
        url: "Autoconsumo.aspx/traer_unid",
        type: "POST",
        data: JSON.stringify({ id: id }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);

                if (parsedTest.length > 0) {
                    console.log(parsedTest);
                    for (var i = 0; i < parsedTest.length; i++) {
                        $('#selectEmpresa').val(parsedTest[i]['id_empresa']).trigger('change');
                        $('#selectGiro').val(parsedTest[i]['id_giro']).trigger('change');

                        $('#numEconomico').val(parsedTest[i]['numero_economico']);
                        $('#txtCodigoBarra').val(parsedTest[i]['codigo_barra']);
                        $('#fechaInstalacion').val($.formattedDate(new Date(parseInt(parsedTest[i]['fecha_instalacion'].substr(6)))));
                        $('#selectTecnico').val(parsedTest[i]['id_persona']).trigger('change');

                        $('#selectTecnico').val(parsedTest[i]['estatusInstalacion']).trigger('change');

                        if (parsedTest[i]['estatus'] == 0 || parsedTest[i]['estatus'] == '0') {
                            $('#activoCH').prop('checked', false);
                        } else {
                            $('#activoCH').prop('checked', true);
                        }

                        $('#txtObservacion').val(parsedTest[i]['observacion']);

                        $('#selectMarcaVehiculo').val(parsedTest[i]['id_marca']).trigger('change');
                        selectModeloVeh = parsedTest[i]['id_modelo'];
                        //$('#selectModeloVehiculo').val(parsedTest[i]['id_modelo']);
                        //$('#selectModeloVehiculo').val(parsedTest[i]['id_modelo']).trigger('change');
                        $('#selectCV').val(parsedTest[i]['id_cilindro']).trigger('change');

                        $('#numSerie').val(parsedTest[i]['numero_serie']);
                        $('#selectAnio').val(parsedTest[i]['anio']).trigger('change');
                        $('#txtPlaca').val(parsedTest[i]['placa']);

                        $('#tMantenimiento').val(parsedTest[i]['tMantenimiento']);
                        $('#selectTiempoMtto').val(parsedTest[i]['tTiempo']).trigger('change');

                    }
                }

            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

$.formattedDate = function (dateToFormat) {
    var dateObject = new Date(dateToFormat);
    var day = dateObject.getDate();
    var month = dateObject.getMonth() + 1;
    var year = dateObject.getFullYear();
    day = day < 10 ? "0" + day : day;
    month = month < 10 ? "0" + month : month;
    var formattedDate = year + "-" + month + "-" + day;
    return formattedDate;
};

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
    /**
     * Propiedades para registrar la Unidad.
     **/

    var id_empresa = $('#selectEmpresa').val();
    var id_giro = $('#selectGiro').val();
    var id_persona = $('#selectTecnico').val();
    var numero_economico = $('#numEconomico').val();
    var codigo_barra = $('#txtCodigoBarra').val();

    if (!codigo_barra.includes("-")) {
        $.gritter.add({
            title: "Informacion",
            text: "El número de contrato debe de contener un guion en la nomenclatura",
            sticky: false
        });
        return;
    }

    var fecha_instalacion = $('#fechaInstalacion').val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
    var estatus_instalacion = $("#estatusU").val();
    var observacion = $('#txtObservacion').val();
    var id_marca = $('#selectMarcaVehiculo').val();
    var id_modelo = $('#selectModeloVehiculo').val();
    var id_cilindro = $('#selectCV').val();
    var numero_serie = $('#numSerie').val();
    var anio = $('#selectAnio').val();
    var placa = $('#txtPlaca').val();
    var tMantenimiento = $('#tMantenimiento').val();
    var tTiempo = $('#selectTiempoMtto').val();
    /**
     * Propiedades para registrar Kit.
     **/
    var id_modeloK = $('#selectModeloKit').val();
    var id_cilindroKit = $('#selectCK').val();
    var vaporizador = $('#numVaporizador').val();
    var riel = $('#numRiel').val();
    var centralita = $('#numCentralita').val();
    var observacionK = $('#txtObservacionKit').val();
    /**
     * Propiedades para registrar tanque.
     **/
    var marcaT = [];
    var tipoT = [];
    var capacidadT = [];
    var numSerieT = [];
    var fechaFT = [];
    var multvT = [];

    for (var i = 0; i < tanq; i++) {
        marcaT[i] = $('#selectMT' + i).val();
        tipoT[i] = $('#selectTT' + i).val();
        capacidadT[i] = $('#selectCT' + i).val();
        numSerieT[i] = $('#numSerieT' + i).val();
        fechaFT[i] = $('#fechaFabricacion' + i).val();
        multvT[i] = $('#numMV' + i).val();
    }
    var url_peticion = "Autoconsumo.aspx/guardar_u";
    if (editando) {
        url_peticion = "Unidades.aspx/actualizar_u";
    }

    var datos = {
        numero_economico: numero_economico,
        numero_serie: numero_serie,
        fecha_instalacion: fecha_instalacion,
        codigo_barra: codigo_barra,
        anio: anio,
        placa: placa,
        observacion: observacion,
        estatus: activo,
        estatusInstalacion: estatus_instalacion,
        id_empresa: id_empresa,
        id_giro: id_giro,
        id_marca: id_marca,
        id_modelo: id_modelo,
        id_cilindro: id_cilindro,
        vaporizador: vaporizador,
        riel: riel,
        centralita: centralita,
        observacionK: observacionK,
        id_modeloK: id_modeloK,
        id_cilindroKit: id_cilindroKit,
        numSerieT: numSerieT,
        fechaFT: fechaFT,
        multvT: multvT,
        marcaT: marcaT,
        tipoT: tipoT,
        capacidadT: capacidadT,
        id: id_editando,
        kilometraje: 0,
        id_persona: id_persona,
        tMantenimiento : tMantenimiento,
        tTiempo: tTiempo
    }

    if (editando && id_editando > 0) { //editando unidad

        url_peticion = "Autoconsumo.aspx/actualizar_u";
    }
    $.ajax({
        type: "POST",
        url: url_peticion,
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d.Message == "Ya Existe una unidad con este mismo numero economico. ") {
                $.gritter.add({
                    title: "informacion",
                    text: "Ya Existe una unidad con este mismo numero economico. ",
                    sticky: false
                });
            } else {
                if (response.d.Result) {
                    limpiar();
                    var msj = "Se Actualizo correctamente la unidad.";
                    if (!editando) {
                        msj = "Se Guardo correctamente la unidad.";
                    }
                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    traer_unidades()
                }
            }
        },
        error: function (error) {

        }
    });
};