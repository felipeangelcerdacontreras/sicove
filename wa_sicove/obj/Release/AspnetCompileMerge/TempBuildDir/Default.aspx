<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="wa_sicove.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>SICOVEGAS | LOGIN</title>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
	<meta name="description" content="Sicove gas | AWSOFTWARE" />
	<meta name="author" content="A.W. Software | ISC.Luis Rodriguez Lopez" />
	<!-- CSS -->
	<!-- CSS -->
	<link href="assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
	<link href="assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<link href="assets/css/main.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/my-custom-styles.css" rel="stylesheet" type="text/css" />
	<!--[if lte IE 9]>
		<link href="assets/css/main-ie.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/main-ie-part2.css" rel="stylesheet" type="text/css" />
	<![endif]-->
    <!-- CSS for demo style switcher. you can remove this -->
	<link href="demo-style-switcher/assets/css/style-switcher.css" rel="stylesheet" type="text/css" />
	<!-- Fav and touch icons -->
	<link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/ico/kingadmin-favicon144x144.png" />
	<link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/ico/kingadmin-favicon114x114.png" />
	<link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/ico/kingadmin-favicon72x72.png" />
	<link rel="apple-touch-icon-precomposed" sizes="57x57" href="assets/ico/kingadmin-favicon57x57.png" />
	<link rel="shortcut icon" href="assets/ico/favicon.png" />
</head>
<body>
   <div class="wrapper full-page-wrapper page-auth page-login text-center">
		<div class="inner-page">
			<div class="logo">
				<a href="#"><img src="assets/ico/logo.png" alt="" style="height:150px;width:250px;"/></a>
			</div>
			<button  style="display:none;" type="button" class="btn btn-auth-facebook"><span>Login via Facebook</span></button>
			<div  style="display:none;" class="separator"><span>OR</span></div>
			<div class="login-box center-block" style="border: 1px solid #999898; width: 20% !important;">
				<form class="form-horizontal" role="form">
					<p class="title" style="font-size: 25px;">Inicio de Sesión</p>
					<div class="form-group">
						<label for="username" class="control-label sr-only">Usuario</label>
						<div class="col-sm-12">
							<div class="input-group">
								<input type="text" placeholder="Usuario" id="username" class="form-control" autocomplete="off" />
								<span class="input-group-addon"><i class="fa fa-user"></i></span>
							</div>
						</div>
					</div>
					<label for="password" class="control-label sr-only">Contraseña</label>
					<div class="form-group">
						<div class="col-sm-12">
							<div class="input-group"> 
								<input type="password" placeholder="Contraseña" id="password" class="form-control"  autocomplete="off" />
								<span class="input-group-addon"><i class="fa fa-lock"></i></span>
							</div>
						</div>
					</div>
					<label class="fancy-checkbox"  style="display:none;">
						<input type="checkbox" />
						<span>Remember me next time</span>
					</label>
					<button type="button" class="btn btn-custom-primary btn-lg btn-block btn-auth"  onclick="iniciar()"><i class="fa fa-arrow-circle-o-right"></i> Entrar</button>
				</form>
				<div class="links" style="display:none;">
					<p><a href="#">Forgot Username or Password?</a></p>
					<p><a href="#">Create New Account</a></p>
				</div>
			</div>
		</div>
	</div>
	<footer class="footer">&copy; Sicove</footer>
	<!-- Javascript -->
	<script src="assets/js/jquery/jquery-2.1.0.min.js"></script>
	<script src="assets/js/bootstrap/bootstrap.js"></script>
	<script src="assets/js/plugins/modernizr/modernizr.js"></script>
    <script src="assets/js/plugins/jquery-gritter/jquery.gritter.min.js"></script>
    <script>
        function iniciar() {
            login();
        }
        $('#username').keypress(function (event) {
            if (event.keyCode == 13) {
                $("#notificacionError").hide("fast");
                login();
            }
        });

        $('#password').keypress(function (event) {
            if (event.keyCode == 13) {
                $("#notificacionError").hide("fast");
                login();
            }
        });
        function login() {
            var user = $("#username").val();
            var pass = $("#password").val();
            if (user.length > 3 && pass.length > 3) {
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/Login",
                    data: '{ usuario:"' + user + '", password:"' + pass + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d.Result && response.d.Data == "USUARIO") {
                            window.location = "sicoveadmin/";
                        }else if (response.d.Result && response.d.Data == "COMODATARIO") {
                            window.location = "sicovegas/Default.aspx";
                        }else {
                            $.gritter.add({
                                title: "Error",
                                text: "Verifique sus datos de usuario.",
                                sticky: false
                            });
                        }
                    },
                    error: function (error) {
                        console.log("ERROR: " + error);
                    }
                });
            } else {
                $.gritter.add({
                    title: "Error",
                    text: "Verifique sus datos de usuario.",

                    sticky: false
                });
            }
            
        }
    </script>
</body>
</html>
