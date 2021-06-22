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
    public partial class Zonas : System.Web.UI.Page
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
        public class objz
        {
            public objz(int id, string zona, string ciudad, string estado, int id_estado, int id_ciudad, int estatus)
            {
                this.id = id;
                this.zona = zona;
                this.ciudad = ciudad;
                this.estado = estado;
                this.id_estado = id_estado;
                this.id_ciudad = id_ciudad;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string zona { get; set; }
            public string ciudad { get; set; }
            public string estado { get; set; }
            public int id_estado { get; set; }
            public int id_ciudad { get; set; }
            public int estatus { get; set; }
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_z()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objz> lista = new List<objz>();
                var consulta = from zn in context.si_zonas
                               select new
                               {
                                   id = zn.id_zona,
                                   zona = zn.cd_descripcion,
                                   id_estado = zn.id_estado,
                                   desc_est = zn.si_estados.es_descripcion,
                                   id_ciudad = zn.id_ciudad,
                                   desc_cd = zn.si_ciudades.cd_descripcion,
                                   estatus = zn.cd_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objz(
                       item.id,
                       item.zona,
                       item.desc_cd,
                       item.desc_est,
                       (int)item.id_estado,
                       (int)item.id_ciudad,
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
        public static ajaxResponse guardar_z(string nombre, int est, int cd, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_zonas cds = new si_zonas();
                cds.cd_descripcion = nombre;
                cds.id_estado = est;
                cds.id_ciudad = cd;
                cds.cd_estatus = activo;
                cds.cd_numero = "2";
                context.si_zonas.InsertOnSubmit(cds);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Zonas.aspx";
                b.bit_accion = "Guardar Zona";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Zona = " + nombre;
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
        public static ajaxResponse actualizar_z(string nombre, int est, int cd, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_zonas cds = null;
                cds = context.si_zonas.Where(x => x.id_zona.Equals(id)).FirstOrDefault();
                cds.cd_descripcion = nombre;
                cds.id_estado = est;
                cds.id_ciudad = cd;
                cds.cd_estatus = activo;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Zonas.aspx";
                b.bit_accion = "Actualiza Zona";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Zona, Id = " + id;
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