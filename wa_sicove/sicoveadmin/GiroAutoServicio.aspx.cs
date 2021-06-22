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
    public partial class GiroAutoServicio : System.Web.UI.Page
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
        public class objG
        {
            public objG(int id, string descripcion, int estatus)
            {
                this.id = id;
                this.descripcion = descripcion;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string descripcion { get; set; }
            public int estatus { get; set; }
        }

        #endregion

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_giro()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objG> lista = new List<objG>();

                var consulta = from ro in context.si_giros_autoservicio select ro;

                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.gia_estatus)
                        activo = 1;

                    lista.Add(new objG(
                        item.id_giro_autoservicio,
                        item.gia_descripcion,
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
        public static ajaxResponse guardar_giro(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_giros_autoservicio roll = new si_giros_autoservicio();

                roll.gia_descripcion = descripcion;
                roll.gia_estatus = false;
                if (activo == 1)
                    roll.gia_estatus = true;

                context.si_giros_autoservicio.InsertOnSubmit(roll);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "GiroAutoServicio.aspx";
                b.bit_accion = "Guardar GiroAutoServicio";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo GiroAutoServicio = " + descripcion;
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
        public static ajaxResponse actualizar_giro(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_giros_autoservicio roll = null;

                roll = context.si_giros_autoservicio.Where(x => x.id_giro_autoservicio.Equals(id)).FirstOrDefault();
                roll.gia_descripcion = descripcion;
                roll.gia_estatus = false;
                if (activo == 1)
                    roll.gia_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "GiroAutoServicio.aspx";
                b.bit_accion = "Actualiza GiroAutoServicio";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo GiroAutoServicio, Id = " + id;
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