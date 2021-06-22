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
    public partial class Dispositivos : System.Web.UI.Page
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
            public obj(int id, string dis_no_equipo, string dis_no_serie, string dis_no_chip, int id_zona, int estatus,string zona_desc)
            {
                this.id = id;
                this.dis_no_equipo = dis_no_equipo;
                this.dis_no_serie = dis_no_serie;
                this.dis_no_chip = dis_no_chip;
                this.id_zona = id_zona;
                this.estatus = estatus;
                this.zona_desc = zona_desc;
            }

            public int id { get; set; }
            public string dis_no_equipo { get; set; }
            public string dis_no_serie { get; set; }
            public string dis_no_chip { get; set; }
            public int id_zona { get; set; }
            public string zona_desc { get; set; }
            public int estatus { get; set; }
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_d()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<obj> lista = new List<obj>();
                var consulta = from dis in context.si_dispositivos
                               join zon in context.si_zonas on dis.id_zona equals zon.id_zona
                               select new
                               {
                                   id = dis.id_dispositivo,
                                   equipo = dis.dis_no_equipo,
                                   serie = dis.dis_no_serie,
                                   chip = dis.dis_no_chip,
                                   id_zn = zon.id_zona,
                                   desc_zn = zon.cd_descripcion,
                                   estatus = dis.dis_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new obj(
                       item.id,item.equipo,item.serie,item.chip,item.id_zn,(int)item.estatus,item.desc_zn
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
        public static ajaxResponse guardar_d(string dis_no_equipo, string dis_no_serie, string dis_no_chip,
            int id_zona, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_dispositivos obj = new si_dispositivos();
                obj.dis_no_equipo = dis_no_equipo;
                obj.dis_no_serie = dis_no_serie;
                obj.dis_no_chip = dis_no_chip;
                obj.id_zona = id_zona;
                obj.dis_estatus = activo;
                context.si_dispositivos.InsertOnSubmit(obj);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Dispositivos.aspx";
                b.bit_accion = "Guardar Dispositivo";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Dispositivo = " + dis_no_equipo;
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
        public static ajaxResponse actualizar_d(string dis_no_equipo, string dis_no_serie, string dis_no_chip,
            int id_zona, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_dispositivos obj = null;
                obj = context.si_dispositivos.Where(x => x.id_dispositivo.Equals(id)).FirstOrDefault();
                obj.dis_no_equipo = dis_no_equipo;
                obj.dis_no_serie = dis_no_serie;
                obj.dis_no_chip = dis_no_chip;
                obj.id_zona = id_zona;
                obj.dis_estatus = activo;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Dispositivos.aspx";
                b.bit_accion = "Actualiza Dispositivo";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Dispositivo, Id = " + id;
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