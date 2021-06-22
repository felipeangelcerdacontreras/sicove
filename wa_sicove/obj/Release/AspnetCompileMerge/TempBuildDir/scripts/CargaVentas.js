$(document).ready(function () {
    $("#Estacion").select2();
    $('#operador').select2();
});

cEstaciones();
function cEstaciones() {

    $.ajax({
        url: "Estaciones.aspx/cEstaciones",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $('#Estacion').append("<option value='-1' selected>Seleccione</option>");
                for (var i = 0; i < parsedTest.length; i++) {
                    $('#Estacion').append("<option value='" + parsedTest[i].id + "'>" + parsedTest[i].nombre + "</option>");
                }
            }

        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });

}

traer_personal();
function traer_personal() {
    $.ajax({
        url: "Personal.aspx/traer_p",
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result) {
                var parsedTest = JSON.parse(result.d.Data);
                $('#operador').append("<option value='-1' selected>Seleccione</option>");
                for (var i = 0; i < parsedTest.length; i++) {
                    if (parsedTest[i]['estatus'] == 0) {
                    } else {
                        $('#operador').append("<option value='" + parsedTest[i].id + "'>" + parsedTest[i].nombre + "</option>");
                        //+ parsedTest[i]['id'] + '\',\'' + parsedTest[i]['nombre'] + 
                    }
                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
};



function enviarFormulario() {
    var input = document.getElementById('inputArchivo');
    var files = input.files;
    var formData = new FormData($('#form1')[0]);

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }

    formData.append("Estacion", $("#Estacion").val());
    formData.append("operador", $("#operador").val());

    $.ajax({
        type: "POST",
        url: "FileUploadHandler.ashx",
        data: formData,
        contentType: false, // Not to set any content header  
        processData: false, // Not to process data 
        // dataType: "dataType",
        success: function (response) {

            $.gritter.add({
                title: "Éxito",
                text: response,
                sticky: false
            });
            console.log(response);
        }
    });
}