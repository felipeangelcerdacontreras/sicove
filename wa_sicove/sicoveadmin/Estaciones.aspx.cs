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
    public partial class Estaciones : System.Web.UI.Page
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
            public obj(int id, string nombre, string direccion, string zona, int id_zona, int estatus)
            {
                this.id = id;
                this.nombre = nombre;
                this.direccion = direccion;
                this.zona = zona;
                this.id_zona = id_zona;
                this.estatus = estatus;
            }

            public obj(int id, string nombre)
            {
                this.id = id;
                this.nombre = nombre;
            }

            public int id { get; set; }
            public string nombre { get; set; }
            public string direccion { get; set; }
            public string zona { get; set; }
            public int id_zona { get; set; }
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
                var consulta = from est in context.si_estaciones
                               join zn in context.si_zonas on est.id_zona equals zn.id_zona
                               select new
                               {
                                   id = est.id_estacion,
                                   nombre = est.esta_descripcion,
                                   direccion = est.esta_direccion,
                                   desc_zona = zn.cd_descripcion,
                                   id_zona = zn.id_zona,
                                   estatus = est.esta_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new obj(
                       item.id,
                       item.nombre,
                       item.direccion,
                       item.desc_zona,
                       item.id_zona,
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
        public static ajaxResponse cEstaciones()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                    sicoveDBDataContext context = new sicoveDBDataContext();
                    List<obj> lista = new List<obj>();
                    var consulta = from est in context.si_estaciones
                                   select new
                                   {
                                       id = est.id_estacion,
                                       nombre = est.esta_descripcion
                                   };
                    foreach (var item in consulta)
                    {
                        lista.Add(new obj(
                           item.id,
                           item.nombre
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
        public static ajaxResponse guardar(string nombre,string direccion, int zn, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
           
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_estaciones cds = new si_estaciones();
                cds.esta_descripcion = nombre;
                cds.esta_direccion = direccion;
                cds.id_zona = zn;
                cds.esta_estatus = activo;
                cds.esta_lat = 0;
                cds.esta_lon = 0;
                cds.esta_numero = "0";
                context.si_estaciones.InsertOnSubmit(cds);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Estaciones.aspx";
                b.bit_accion = "Guardar Estacion";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Estacion = " + nombre;
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
        public static ajaxResponse actualizar(string nombre, string direccion, int zn, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_estaciones cds = null;
                cds = context.si_estaciones.Where(x => x.id_estacion.Equals(id)).FirstOrDefault();
                cds.esta_descripcion = nombre;
                cds.esta_direccion = direccion;
                cds.id_zona = zn;
                cds.esta_estatus = activo;
                cds.esta_lat = 0;
                cds.esta_lon = 0;
                cds.esta_numero = "0";
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Estaciones.aspx";
                b.bit_accion = "Actualiza Estacion";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Estacion, Id = " + id;
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