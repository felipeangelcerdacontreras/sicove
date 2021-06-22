$(document).ready(function () {
    
})
var tabla_lista = $("#tabla_lista").DataTable({
    "responsive": {
        details: {
            type: 'column'
        }
    },
    "language": {
        "url": '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Spanish.json'
    },
    dom: 'Blfrtip',
    buttons: [
        {
            extend: 'csv'

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

function buscarConsumos() {
    var range = $("#range").val();
    var startDate = $('#range').data('daterangepicker').startDate._d;
    var endDate = $('#range').data('daterangepicker').endDate._d;
    var inicio = moment(startDate).format('MM/DD/YYYY HH:mm:ss');
    var final = moment(endDate).format('MM/DD/YYYY HH:mm:ss');
    var dias = days_between(startDate, endDate);
    if (!range) {
        $.gritter.add({
            title: "Informacion",
            text: "Selecciona Rango de fechas",
            sticky: false
        });
    } else {
        var datos = JSON.stringify({
            inicio: inicio,
            final: final,
            diffDias: dias
        }); 
    }
    $.ajax({
        url: "gestoriaRecargas.aspx/traer_consumos",
        type: "POST",
        data: datos,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d.Result == false) {
                tabla_lista.clear().draw();
                $.gritter.add({
                    title: "Informacion",
                    text: "No se Encontraron Resultados",
                    sticky: false
                });
            } else {
                var parsedTest = JSON.parse(result.d.Data);
                mttos_tabla = parsedTest;
                tabla_lista.clear().draw();
                for (var i = 0; i < parsedTest.length; i++) {
                    tabla_lista.row.add([
                        parsedTest[i]['contrato'],
                        parsedTest[i]['economico'],
                        parsedTest[i]['marca'],
                        parsedTest[i]['modelo'],
                        parsedTest[i]['anio'],
                        parsedTest[i]['direccion'],
                        parsedTest[i]['telefono'],
                        parsedTest[i]['diasNoConsumos'],
                        parsedTest[i]['consumos'],
                    ]).draw();

                }
            }
        },
        error: function (errormessage) {
            console.log(errormessage);
        }
    });
   
}

function days_between(date1, date2) {

    // The number of milliseconds in one day
    var ONE_DAY = 1000 * 60 * 60 * 24;

    // Convert both dates to milliseconds
    var date1_ms = date1.getTime();
    var date2_ms = date2.getTime();

    // Calculate the difference in milliseconds
    var difference_ms = Math.abs(date1_ms - date2_ms);

    // Convert back to days and return
    return Math.round(difference_ms / ONE_DAY);

}

$(function () {
    $('.date-range').daterangepicker({
        "timePicker": true,
        "locale": {
            "format": "YYYY-MM-DD",
            "separator": " - ",
            "applyLabel": "Guardar",
            "cancelLabel": "Cancelar",
            "fromLabel": "Desde",
            "toLabel": "Hasta",
            "customRangeLabel": "Personalizar",
            "daysOfWeek": [
                "Do",
                "Lu",
                "Ma",
                "Mi",
                "Ju",
                "Vi",
                "Sa"
            ],
            "monthNames": [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Setiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            ],
            "firstDay": 1
        }
    });
});