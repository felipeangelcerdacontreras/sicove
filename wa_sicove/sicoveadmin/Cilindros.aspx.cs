using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;

namespace wa_sicove.sicoveadmin
{
    public partial class Cilindros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Entidades
        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }
        public class objC
        {
            public objC(int id, int numero, int estatus)
            {
                this.id = id;
                this.numero = numero;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public int numero { get; set; }
            public int estatus { get; set; }
        }

        #endregion

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_cilindros()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> lista = new List<objC>();
                var consulta = from ro in context.si_cilindros select ro;
                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.ci_estatus)
                        activo = 1;

                    lista.Add(new objC(
                        item.id_cilindro,
                      (int)item.ci_numero_cilindro,
                        activo
                    ));
                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lista);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;

            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_cilindro(int numero, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_cilindros roll = new si_cilindros();

                roll.ci_numero_cilindro = numero;
                roll.ci_estatus = false;
                if (activo == 1)
                    roll.ci_estatus = true;

                context.si_cilindros.InsertOnSubmit(roll);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Cilindros.aspx";
                b.bit_accion = "Guardar Cilindros";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Cilindros = " + numero;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_cilindro(int numero, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_cilindros roll = null;

                roll = context.si_cilindros.Where(x => x.id_cilindro.Equals(id)).FirstOrDefault();
                roll.ci_numero_cilindro = numero;
                roll.ci_estatus = false;
                if (activo == 1)
                    roll.ci_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Cilindros.aspx";
                b.bit_accion = "Actualiza Cilindros";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Cilindros, Id = " + id;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizado Correctamente";
                Response.Data = null;

            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

        #endregion
    }
}