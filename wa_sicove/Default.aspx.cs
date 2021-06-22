using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;
using System.Diagnostics;

namespace wa_sicove
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse Login(string usuario, string password)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_usuarios objUsuario = null;
                si_configuracion objConfiguracion = null;
                objUsuario = context.si_usuarios.Where(x => x.us_user == usuario && x.us_password == password).SingleOrDefault();
                objConfiguracion = context.si_configuracion.SingleOrDefault();

                sicoveDBDataContext context1 = new sicoveDBDataContext();
                si_comodatarios objComodatario = null;
                objComodatario = context1.si_comodatarios.Where(x => x.co_usuario == usuario && x.co_contraseña == password).SingleOrDefault();
                objConfiguracion = context.si_configuracion.SingleOrDefault();

                if (objUsuario != null)
                {
                    if (objUsuario.us_estatus.Equals(1) && objUsuario.us_bloqueado.Equals(0))
                    {

                        Response.Result = true;
                        Response.Message = "Acceso Correcto";
                        Response.Data = "USUARIO";
                        objUsuario.us_ultimo_acceso = DateTime.Now;
                        context.SubmitChanges();
                        HttpContext.Current.Session["sesionUsuario"] = objUsuario;
                        HttpContext.Current.Session["ConfiguracionCortesias"] = objConfiguracion;
                        // Alimentamos Bitacora
                        si_bitacora b = new si_bitacora();
                        b.bit_fecha = DateTime.Now.Date;
                        b.bit_tiempo = DateTime.Now.TimeOfDay;
                        b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                        b.bit_modulo = "Default.aspx";
                        b.bit_accion = "Login";
                        b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Inicia Sesion";
                        ClassBicatora.insertBitacora(b);
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Message = "El usuario de acceso no esta activo, verifique por favor.";
                        Response.Data = null;
                    }
                }
                else if (objComodatario != null)
                {
                    if (objComodatario.co_estatus.Equals(1))
                    {

                        Response.Result = true;
                        Response.Message = "Acceso Correcto";
                        Response.Data = "COMODATARIO";
                        Debug.WriteLine(objComodatario);
                        HttpContext.Current.Session["sesionUsuario"] = objComodatario;
                        System.Diagnostics.Debug.WriteLine(objComodatario);
                        // Alimentamos Bitacora
                        si_bitacora b = new si_bitacora();
                        b.bit_fecha = DateTime.Now.Date;
                        b.bit_tiempo = DateTime.Now.TimeOfDay;
                        b.id_usuario = null; //((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).id_comodatario;
                        b.bit_modulo = "Default.aspx";
                        b.bit_accion = "Login";
                        b.bit_observaciones = null; // ((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).co_nombre + "(" + ((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).co_usuario + ") - Usuario Inicia Sesion";
                        ClassBicatora.insertBitacora(b);
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Message = "El comodatario de acceso no esta activo, verifique por favor.";
                        Response.Data = null;
                    }
                }
                else
                {
                    Response.Result = false;
                    Response.Message = "Datos de Acceso incorrectos, verifique por favor.";
                    Response.Data = null;
                }
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.ToString();
                Response.Data = null;
            }

            return Response;
        }

    }
}