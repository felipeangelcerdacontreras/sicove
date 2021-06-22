let auxEmpresa = null, auxGiro = null, auxLider = null, auxComodatario = null, auxBase = null, auxTecnico = null, auxMarcaVehiculo = null, auxModelovehiculo = null, auxNCilVehiculo = null,
    auxMarcaKit = null, auxModeloKit = null, auxNCilKit = null, auxMTanq1 = null, auxTTanq1 = null, auxCTanq1 = null, auxMTanq2 = null, auxTTanq2 = null, auxCTanq2 = null, auxMTanq3 = null, auxTTanq3 = null, auxCTanq3 = null;
let agTanque = -1;
let auxtanq = 0;

$(document).ready(function () {
    GetEmpresas();
    GetGiro();
    GetLider();
    GetComodatario();
    GetEstInstalacion()
    $("#selectBase").html('<option value="-1" selected disabled>Seleccionar Base</option>');
    GetTecnico();
    GetMarcaVehiculo();
    $("#selectModeloVehiculo").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    GetCilindraje();
    GetAnios();
    GetKitMarca();
    $("#selectModeloKit").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    GetMarcaT('.marca');
    GetTipoT('.tipo');
    GetCapacidadT('.capacidad');

    $('#fechaInstalacion').val(toDay());
    $('#fechaFabricacion0').val(toDay());

    $("#selectEmpresa").select2();
    $("#selectGiro").select2();
    $("#selectLider").select2();
    $("#selectComodatario").select2();
    $("#selectTecnico").select2();
    $("#selectMarcaVehiculo").select2();
    $("#selectModeloVehiculo").select2();
    $("#selecetEstInst").select2();
    $("#selectBase").select2();
    $("#selectCV").select2();
    $("#selectAnio").select2();
    $("#selectMarcaKit").select2();
    $("#selectModeloKit").select2();
    $("#selectCK").select2();
    $("#selectTiempoMtto").select2();
    $('.sc2').select2();

    $("#selectEmpresa").change(function () {
        let emp = $("#selectEmpresa").val();

        if (emp == null && auxEmpresa != null) {
            $("#selectEmpresa").val(auxEmpresa);
            $('#selectEmpresa').trigger('change.select2');
            auxEmpresa = null;
        }

        if ($(this).val() > 0) {
            GetBases($(this).val());
        } else if ($(this).val() == -99) {
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

    $("#selectLider").change(function () {
        let emp = $("#selectLider").val();

        if (emp == null && auxLider != null) {
            $("#selectLider").val(auxLider);
            $('#selectLider').trigger('change.select2');
            auxLider = null;
        }

        if ($(this).val() == -99) {
            openmodal("agregarLider");
        }
    });

    $("#selectComodatario").change(function () {
        let emp = $("#selectComodatario").val();

        if (emp == null && auxComodatario != null) {
            $("#selectComodatario").val(auxComodatario);
            $('#selectComodatario').trigger('change.select2');
            auxComodatario = null;
        }

        if ($(this).val() > 0) {
            //GetBases($(this).val());
        } else if ($(this).val() == -99) {
            openmodal("agregarComodatario");
        }
    });

    $("#selectBase").change(function () {
        let emp = $("#selectBase").val();

        if (emp == null && auxBase != null) {
            $("#selectBase").val(auxBase);
            $('#selectBase').trigger('change.select2');
            auxBase = null;
        }

        if ($(this).val() == -99 ) {
            openmodal("agregarBaseComo");
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
        if (funcion == "agregarLider") {
            $("#selectLider").val("").trigger("change");
        }
        if (funcion == "agregarComodatario") {
            $("#selectComodatario").val("").trigger("change");
        }
        if (funcion == "agregarBaseComo") {
            $("#selectBase").val("").trigger("change");
        }
        if (funcion == "agregarTecnico") {
            $("#selectTecnico").val("").trigger("change");
        }
        if (funcion == "agregarMarcaV") {
            $("#selectMarcaVehiculo").val("").trigger("change");
        }
        if (funcion == "agregarModeloV") {
            $("#selectModeloVehiculo").val("").trigger("change");
        }
        if (funcion == "agregarCilindrosV") {
            $("#selectCV").val("").trigger("change");
        }
        if (funcion == "agregarCilindrosK") {
            $("#selectCK").val("").trigger("change");
        }
        if (funcion == "agregarMarcaK") {
            $("#selectMarcaKit").val("").trigger("change");
        }
        if (funcion == "agregarModeloK") {
            $("#selectModeloKit").val("").trigger("change");
        }
        
    });


    var id = getGET();
    if (id != undefined) {
        setTimeout(function () { ver_unidad(id); }, 2500);
    }

});


function getGET() {
    var loc = document.location.href;
    if (loc.indexOf('?') > 0) {
        var getString = loc.split('?')[1];
        var GET = getString.split('&');
        var get = "";
        for (var i = 0, l = GET.length; i < l; i++) {
            var tmp = GET[i].split('=');
            get = unescape(decodeURI(tmp[1]));
        }
        return get;
    }
}

var tanq = 1;
var editando = false;
var id_editando = 0;
var id_modelo_ed = 0;
var selectModeloVeh = 0;
var selectbase = 0;

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
    if (id == "agregarLider") {
        titulo_txt = "Agregar Lider";
        label_txt = "Nombre del lider:";
    }
    if (id == "agregarComodatario") {
        titulo_txt = "Agregar Comodatario";
        label_txt = "Nombre del Comodatario:";
    }
    if (id == "agregarBaseComo") {
        titulo_txt = "Agregar Base";
        label_txt = "Direccion de la base:";
    }
    if (id == "agregarTecnico") {
        titulo_txt = "Agregar Técnico";
        label_txt = "Nombre del Técnico:";
    }
    if (id == "agregarMarcaV") {
        titulo_txt = "Agregar Marca";
        label_txt = "Nombre de la Marca:";
    }
    if (id == "agregarModeloV") {
        titulo_txt = "Agregar Modelo";
        label_txt = "Nombre del Modelo:";
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

    if (!$("#selectEmpresa").val() && id == "agregarBaseComo") {
        $.gritter.add({
            title: "Informacion",
            text: "Seleccione una Empresa",
            sticky: false
        });
        GetBases($("#selectComodatario").val())
    } else {
        $("#label_modal_util").html(label_txt);
        $("#titulo_modal_util").html(titulo_txt);
        $("#nombre_util").data("tipo", id);
        $("#modal_util").modal("show");
        document.getElementById("nombre_util").focus();
        // console.log($("#nombre_util").data("tipo"));
    }
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
    if (funcion == "agregarLider") {
        guardar_lider();
    }
    if (funcion == "agregarComodatario") {
        guardar_comodatario();
    }
    if (funcion == "agregarBaseComo") {
        guardar_Base();
    }
    if (funcion == "agregarTecnico") {
        guardar_tecnico();
    }
    if (funcion == "agregarMarcaV") {
        guardar_marcav();
    }
    if (funcion == "agregarModeloV") {
        guardar_modelov();
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

function GetEmpresas( selEmpresa = '') {
    $("#selectEmpresa").html('<option value="-1" selected disabled>Seleccionar Empresa</option>');
    $.ajax({
        url: "Empresas.aspx/traer_empresas_no_autoconsumo",
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

function GetGiro( seleccionado = '') {
    $("#selectGiro").html('<option value="-1" selected disabled>Seleccionar Giro</option>');
    $.ajax({
        url: "Giros.aspx/traer_giros_no_autoconsumo",
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

function GetLider( seleccionado = '') {
    $("#selectLider").html('<option value="-1" selected disabled>Seleccionar Lider</option>');
    $.ajax({
        url: "Lider.aspx/traer_l",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectLider").append('<option value="-99">*****Agregar Lider</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectLider").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['nombre'] + ' ' + parsedTest[i]['apellido'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectLider option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectLider").val();
                    auxLider = valorS;
                    $("#selectLider").val(valorS).change();

                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetComodatario( seleccionado = '') {

    const data = JSON.stringify({
        nombre: '',
        telefono: ''
    });

    $("#selectComodatario").html('<option value="-1" selected disabled>Seleccionar Comodatario</option>');
    $.ajax({
        url: "Comodatarios.aspx/traer_comodatarios",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        data: data,
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectComodatario").append('<option value="-99">*****Agregar Comodatario</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['activo'] == 1) {
                        $("#selectComodatario").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['nombre'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectComodatario option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectComodatario").val();
                    auxComodatario = valorS;
                    $("#selectComodatario").val(valorS).change();
                    GetBases();
                }

            }
            
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetBases( seleccionado = '' ) {
    $("#selectBase").html('<option value="-1" selected disabled>Seleccionar Base</option>');
    var empresa = $("#selectEmpresa").val();
    $.ajax({
        url: "Bases.aspx/traer_bases_comodatario",
        type: "POST",
        data: '{id_empresa:"' + empresa + '"}',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectBase").append('<option value="-99">*****Agregar Base</option>');
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 1) {
                        $("#selectBase").append('<option value="' + parsedTest[i]['id'] + '">' + parsedTest[i]['direccion'] + '</option>');
                    }
                }

                if (seleccionado != '') {
                    $("#selectBase option:contains(" + seleccionado + ")").attr('selected', 'selected');
                    let valorS = $("#selectBase").val();
                    auxBase = valorS;
                    $("#selectBase").val(valorS);
                    $('#selectBase').trigger('change.select2');
                }

            }
            
            if (selectbase > 0) {
                $('#selectBase').val(selectbase);
                $('#selectBase').trigger('change.select2');
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function GetTecnico( seleccionado = '') {
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

function GetEstInstalacion() {
    $("#selecetEstInst").append(' <option value="1">Activo</option>' +
        ' <option value="2">Desinstalado</option>' +
        ' <option value="3">Vendido</option>' +
        ' <option value="4">Robado</option>' +
        ' <option value="5">Cambio de Propietario</option>' +
        ' <option value="6">Proceso Jurídico</option>' +
        ' <option value="7"> Otro</option>');
}

function GetMarcaVehiculo( seleccionado = '') {
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
                var parsedTest = JSON.parse(result.d.Data);
                $("#selectModeloVehiculo").append('<option value="-99">*****Agregar Modelo</option>');
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

function GetKitMarca( seleccionado = '' ) {
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
    var autoconsumo = 0;
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
        };

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
                    //$("#selectEmpresa").focus();
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
            autoconsumo: 0
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
                    //$("#selectGiro").focus();

                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//COMODATARIOS
function guardar_comodatario() {
    var descripcion = $("#nombre_util").val();
    var activo = 1;
    if (descripcion.length > 3) {
        var urlS = "Comodatarios.aspx/guardar_como";
        var datas = {
            nombre: descripcion,
            bases: [],
            telefono: 0,
            correo: 'pendiente',
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
                    var msj = "Se guardo correctamente el Comodatario.";
                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetComodatario(descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectComodatario").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    }
}

//BASES 
function guardar_Base() {
    var descripcion = $("#nombre_util").val();
    var selectEm = $("#selectEmpresa").val();
    //var selectCo = $("#selectComodatario").val();
    var activo = 1;
    if (descripcion.length > 1) {
        var urlS = "Bases.aspx/guardar_base_comodatario";
        var datas = {
            direccion: descripcion,
            empresa: selectEm,
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
                    GetBases(descripcion);
                    $("#modal_util").modal("hide");
                    $("#selectComodatario").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    } else {
        $.gritter.add({
            title: "Falló",
            text: "El nombre de la base debe ser mínimo de 5 caracteres.",
            sticky: false
        });
    }
}

//LIDER
function guardar_lider() {
    var nombre = $("#nombre_util").val();
    var apellido = "PENDIENTE";
    var telefono = "PENDIENTE";
    var correo = "PENDIENTE";
    var comicion = 5;
    var activo = 1;

    if (nombre.length > 3 && apellido.length > 3 && comicion > 0) {
        urlS = "Lider.aspx/guardar_l";


        var datas = {
            nombre: nombre,
            apellido: apellido,
            telefono: telefono,
            correo: correo,
            comicion: comicion,
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
                    var msj = "Se Guardo correctamente.";

                    $.gritter.add({
                        title: "Éxito",
                        text: msj,
                        sticky: false
                    });
                    $("#nombre_util").val("");
                    GetLider(nombre);
                    $("#modal_util").modal("hide");
                    $("#selectLider").focus();
                } else {

                }
            },
            error: function (error) {
                console.log("ERROR: " + error);
            }
        });
    };
};

//TECNICO
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
                    $("#modal_util").modal("hide");
                    GetMarcaVehiculo(descripcion);
                    //$("#selectMarcaVehiculo").focus();
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

// MARCA Tanque
function guardar_marcaTanq(cambiar = -1) {

    var descripcion = $("#nombre_util").val();
    var activo = 1;

    if (descripcion.length > 1 ) {
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


//  sas  


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
    $("#selectMT1").val(-1).trigger("change");
    $("#selectTT1").val(-1).trigger("change");
    $("#selectCT1").val(-1).trigger("change");
    $("#selectMT2").val(-1).trigger("change");
    $("#selectTT2").val(-1).trigger("change");
    $("#selectCT2").val(-1).trigger("change");
    $("#selectAnio").val(-1).trigger("change");
    
    $("#selecetEstInst").html('');
    GetEstInstalacion()
    $("#selectBase").html('<option value="-1" selected disabled>Seleccionar Base</option>');
    $("#selectModeloVehiculo").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    $("#selectModeloKit").html('<option value="-1" selected disabled>Seleccionar Modelo</option>');
    limpiartanq();
    $("#form1")[0].reset();
    tanq = 1;
    editando = false;
    id_editando = 0;
    id_modelo_ed = 0;
    selectModeloVeh = 0;
    $('#fechaInstalacion').val(toDay());
    $('#fechaFabricacion0').val(toDay());
};

function limpiartanq() {
    tanq = 1;

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
    if (auxtanq != 0 ) {
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


function ver_unidad(id) {
    limpiartanq()
    $("#form1")[0].reset();
    $("#numTelefono").mask("(999) 999-9999");
    $('#txtCodigoBarra').attr("disabled", true);
    editando = true;
    id_editando = id;
    ver_Vehiculo(id)
    ver_kit(id);
    ver_tanque(id);
};

function ver_kit(id) {
    $.ajax({
        url: "Unidades.aspx/traer_k",
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
        url: "Unidades.aspx/traer_t",
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
        url: "Unidades.aspx/traer_unid",
        type: "POST",
        data: JSON.stringify({ id: id }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                if (parsedTest.length > 0) {
                    for (var i = 0; i < parsedTest.length; i++) {
                        $('#selectEmpresa').val(parsedTest[i]['id_empresa']).trigger('change');
                        $('#selectGiro').val(parsedTest[i]['id_giro']).trigger('change');
                        $('#selectLider').val(parsedTest[i]['id_lider']).trigger('change');
                        $('#selectComodatario').val(parsedTest[i]['comodatario']).trigger('change');

                        $('#numEconomico').val(parsedTest[i]['numero_economico']);
                        $('#txtCodigoBarra').val(parsedTest[i]['codigo_barra']);
                        $('#numTelefono').val(parsedTest[i]['telefono']);
                        $('#txtDireccion').val(parsedTest[i]['direccion']);
                        $('#fechaInstalacion').val($.formattedDate(new Date(parseInt(parsedTest[i]['fecha_instalacion'].substr(6)))));

                        $('#selectTecnico').val(parsedTest[i]['id_persona']).trigger('change');

                        if (parsedTest[i]['estatus'] == 0 || parsedTest[i]['estatus'] == '0') {
                            $('#activoCH').prop('checked', false);
                        } else {
                            $('#activoCH').prop('checked', true);
                        }

                        $('#txtObservacion').val(parsedTest[i]['observacion']);

                        $('#selectMarcaVehiculo').val(parsedTest[i]['id_marca']).trigger('change');
                        $('#selecetEstInst').val(parsedTest[i]['id_estatus_instalacion']).trigger('change');
                        selectModeloVeh = parsedTest[i]['id_modelo'];
                        selectbase = parsedTest[i]['id_base'];
                        //$('#selectModeloVehiculo').val(parsedTest[i]['id_modelo']).trigger('change');
                        $('#selectModeloVehiculo').val(parsedTest[i]['id_modelo']).trigger('change');
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
    e.preventDefault();
    guardar();

    /*
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
    */
});



function guardar() {
    /**
     * Propiedades para registrar la Unidad.
     **/

    var id_empresa = $('#selectEmpresa').val() == null ? 0 : $('#selectEmpresa').val();
    var id_giro = $('#selectGiro').val() == null ? 0 : $('#selectGiro').val();
    var id_lider = $('#selectLider').val() == null ? 0 : $('#selectLider').val();
    var numero_economico = $('#numEconomico').val() == null ? 0 : $('#numEconomico').val();
    var comodatario = $('#selectComodatario').val() == null ? 0 : $('#selectComodatario').val();
    var bases = $('#selectBase').val();
    if (bases == null) { bases = 0; }
    var estatus_instalacion = $("#selecetEstInst").val() == null ? 1 : $("#selecetEstInst").val();
    var codigo_barra = $('#txtCodigoBarra').val();

    if (!codigo_barra.includes("-")) {
        $.gritter.add({
            title: "Informacion",
            text: "El número de contrato debe de contener un guion en la nomenclatura",
            sticky: false
        });
        return;
    }


    $("#numTelefono").mask("9999999999");
    var telefono = $("#numTelefono").val();
    var direccion = $('#txtDireccion').val();
    var fecha_instalacion = $('#fechaInstalacion').val() == null ? Date.now : $('#fechaInstalacion').val();
    var id_persona = $('#selectTecnico').val() == null ? 2 : $('#selectTecnico').val();
    var activo = $("#activoCH").prop('checked') == true ? 1 : 0;
    var observacion = $('#txtObservacion').val();
    var id_marca = $('#selectMarcaVehiculo').val() == null ? 0 : $('#selectMarcaVehiculo').val();
    var id_modelo = $('#selectModeloVehiculo').val() == null ? 0 : $('#selectModeloVehiculo').val();
    var id_cilindro = $('#selectCV').val() == null ? 1 : $('#selectCV').val();
    var numero_serie = $('#numSerie').val();
    var anio = $('#selectAnio').val() == null ? 2020 : $('#selectAnio').val();
    var placa = $('#txtPlaca').val();


    var tMantenimiento = $('#tMantenimiento').val() == "" ? 6 : $('#tMantenimiento').val();
    var tTiempo = parseInt($('#selectTiempoMtto').val()) ;
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
        marcaT[i] = $('#selectMT' + i).val() == null ? 0 : $('#selectMT' + i).val();
        tipoT[i] = $('#selectTT' + i).val() == null ? 0 : $('#selectTT' + i).val();
        capacidadT[i] = $('#selectCT' + i).val() == null ? 0 : $('#selectCT' + i).val();
        numSerieT[i] = $('#numSerieT' + i).val() == null ? "" : $('#numSerieT' + i).val();
        fechaFT[i] = $('#fechaFabricacion' + i).val() == null ? "" : $('#fechaFabricacion' + i).val();
        multvT[i] = $('#numMV' + i).val() == null ? "" : $('#numMV' + i).val() ;
    }
    var url_peticion = "Unidades.aspx/guardar_u";
    if (editando) {
        url_peticion = "Unidades.aspx/actualizar_u";
    }

    var datos = {
        numero_economico: numero_economico,
        numero_serie: numero_serie,
        comodatario: comodatario,
        bases: bases,
        estatus_instalacion: estatus_instalacion,
        fecha_instalacion: fecha_instalacion,
        codigo_barra: codigo_barra,
        telefono: telefono,
        direccion: direccion,
        anio: anio,
        placa: placa,
        observacion: observacion,
        estatus: activo,
        id_empresa: id_empresa,
        id_giro: id_giro,
        id_marca: id_marca,
        id_modelo: id_modelo,
        id_persona: id_persona,
        id_cilindro: id_cilindro,
        id_lider: id_lider,
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
        tMantenimiento: tMantenimiento,
        tTiempo: tTiempo
    }

    if (editando && id_editando > 0) {//editando unidad

        url_peticion = "Unidades.aspx/actualizar_u";
    }
    $.ajax({
        type: "POST",
        url: url_peticion,
        data: JSON.stringify(datos),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            limpiar();
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