using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;

namespace wa_sicove.sicoveadmin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (Session["sesionUsuario"] == null)
                {
                    Response.Redirect("~/");
                }
                string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
                string[] Separado = cadena.Split('/');
                string Final = Separado[Separado.Length - 1];
                if (Final != "Default.aspx")
                {
                    sicoveDBDataContext context = new sicoveDBDataContext();
                    var objModulos = (from modulos in context.si_modulos
                                      join permisos in context.si_permisos on modulos.id_modulo equals permisos.id_modulo
                                      where permisos.id_rol == ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_rol
                                       && modulos.mod_estatus == 1 && modulos.mod_archivo.Equals(Final)
                                      select modulos).Count();
                    if (objModulos == 0 && Final.StartsWith("RegistroUnidades.aspx?Unidades"))
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
        }
        
    }
}