$(document).ready(function () {
    /*clase que admite solamente numeros*/
    $('.input-only-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    /*clase para rfc con limite de 12*/
    $(".input-only-rfc").attr('maxlength', "12");
    /*poner las letras del input en mayuscula*/
    $('.input-mayus').keyup(function () {
        this.value = this.value.toUpperCase();
    });
    $("#numTelefono").mask("(999) 999-9999");
});