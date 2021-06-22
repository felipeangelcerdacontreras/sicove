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
    public partial class TipoTanque : System.Web.UI.Page
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
        public class objT
        {
            public objT(int id, string descripcion, int estatus)
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
        public static ajaxResponse traer_tipo()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objT> lista = new List<objT>();

                var consulta = from ro in context.si_tipo_tanque select ro;

                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.ttq_estatus)
                        activo = 1;

                    lista.Add(new objT(
                        item.id_tipo_tanque,
                        item.ttq_descripcion,
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
        public static ajaxResponse guardar_tipo(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_tipo_tanque roll = new si_tipo_tanque();

                roll.ttq_descripcion = descripcion;
                roll.ttq_estatus = false;
                if (activo == 1)
                    roll.ttq_estatus = true;

                context.si_tipo_tanque.InsertOnSubmit(roll);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "TipoTanque.aspx";
                b.bit_accion = "Guardar TipoTanque";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo TipoTanque = " + descripcion;
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
        public static ajaxResponse actualizar_tipo(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_tipo_tanque roll = null;

                roll = context.si_tipo_tanque.Where(x => x.id_tipo_tanque.Equals(id)).FirstOrDefault();
                roll.ttq_descripcion = descripcion;
                roll.ttq_estatus = false;
                if (activo == 1)
                    roll.ttq_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "TipoTanque.aspx";
                b.bit_accion = "Actualiza TipoTanque";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo TipoTanque, Id = " + id;
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