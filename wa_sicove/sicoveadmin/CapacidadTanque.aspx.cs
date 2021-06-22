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
    public partial class CapacidadTanque : System.Web.UI.Page
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
            public objC(int id, string descripcion, int estatus)
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
        public static ajaxResponse traer_capacidad()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> lista = new List<objC>();

                var consulta = from ro in context.si_capasidad_tanque select ro;

                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.ctp_estatus)
                        activo = 1;

                    lista.Add(new objC(
                        item.id_capasidad,
                        item.ctp_capasidad,
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
        public static ajaxResponse guardar_capacidad(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_capasidad_tanque roll = new si_capasidad_tanque();

                roll.ctp_capasidad = descripcion;
                roll.ctp_estatus = false;
                if (activo == 1)
                    roll.ctp_estatus = true;

                context.si_capasidad_tanque.InsertOnSubmit(roll);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "TipoTanque.aspx";
                b.bit_accion = "Guardar CapacidadTanque";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo CapacidadTanque = " + descripcion;
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
        public static ajaxResponse actualizar_capacidad(string descripcion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_capasidad_tanque roll = null;

                roll = context.si_capasidad_tanque.Where(x => x.id_capasidad.Equals(id)).FirstOrDefault();
                roll.ctp_capasidad = descripcion;
                roll.ctp_estatus = false;
                if (activo == 1)
                    roll.ctp_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "CapacidadTanque.aspx";
                b.bit_accion = "Actualiza CapacidadTanque";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo CapacidadTanque, Id = " + id;
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