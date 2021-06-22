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
    public partial class modulos_estacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region CLASSES
        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }
        public class obj
        {
            public obj(int id, string modulo, string estacion, int id_estacion, int estatus)
            {
                this.id = id;
                this.modulo = modulo;
                this.estacion = estacion;
                this.id_estacion = id_estacion;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string modulo { get; set; }
            public string estacion { get; set; }
            public int id_estacion { get; set; }
            public int estatus { get; set; }
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<obj> lista = new List<obj>();
                var consulta = from cd in context.si_modulos_estacions
                               select new
                               {
                                   id = cd.id_modulo,
                                   desc = cd.nombre,
                                   id_estacion = cd.id_estacion,
                                   desc_est = cd.si_estaciones.esta_descripcion,
                                   estatus = cd.estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new obj(
                       item.id,
                       item.desc,
                       item.desc_est,
                       (int)item.id_estacion,
                       (int)item.estatus
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
        public static ajaxResponse modulosEstacion(int estacion)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<obj> lista = new List<obj>();

                var consulta = from cd in context.si_modulos_estacions
                               where cd.id_estacion == estacion
                               select new
                               {
                                   id = cd.id_modulo,
                                   desc = cd.nombre,
                                   id_estacion = cd.id_estacion,
                                   desc_est = cd.si_estaciones.esta_descripcion,
                                   estatus = cd.estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new obj(
                       item.id,
                       item.desc,
                       item.desc_est,
                       (int)item.id_estacion,
                       (int)item.estatus
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
        public static ajaxResponse guardar(string nombre, int est, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modulos_estacion cds = new si_modulos_estacion();
                cds.nombre = nombre;
                cds.id_estacion = est;
                cds.estatus = activo;
                context.si_modulos_estacions.InsertOnSubmit(cds);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "modulos_estacion.aspx";
                b.bit_accion = "Guardar Modulo Estacion";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Modulo Estacion = " + nombre;
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
        public static ajaxResponse actualizar(string nombre, int est, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modulos_estacion cds = null;
                cds = context.si_modulos_estacions.Where(x => x.id_modulo.Equals(id)).FirstOrDefault();
                cds.nombre = nombre;
                cds.id_estacion = est;
                cds.estatus = activo;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "modulo_estacion.aspx";
                b.bit_accion = "Actualiza Modulo Estacion";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Modulo Estacion, Id = " + id;
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

    }
}