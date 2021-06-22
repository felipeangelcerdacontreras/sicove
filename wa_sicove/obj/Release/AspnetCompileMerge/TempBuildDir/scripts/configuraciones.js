$(document).ready(function () {
    traerConfig();
});

function traerConfig() {
    $.ajax({
        url: "configuraciones.aspx/traer_c",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                for (var i = 0; i < parsedTest.length; i++) {
                    $("#inicio").val(parsedTest[i]['inicio']);
                    $("#fin").val(parsedTest[i]['fin']);
                    $("#litrosss").val(parsedTest[i]['litros']);
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}

function actualizaConfi() {
    var inicioo = $("#inicio").val();
    var finaal = $("#fin").val();
    var litross = $("#litrosss").val();
    var datas = {
        inicio: inicioo,
        final: finaal,
        litros: litross,
    }
    $.ajax({
        url: "configuraciones.aspx/actualizar",
        type: "POST",
        data: JSON.stringify(datas),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result == false) {
                $.gritter.add({
                    title: "Exito",
                    text: "Configuracion Acualizada Correctamente",
                    sticky: false
                });
                traerConfig();
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
}