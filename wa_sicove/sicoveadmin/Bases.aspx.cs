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
    public partial class Bases : System.Web.UI.Page
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
        public class objB
        {
            public objB(int id, string direccion, int id_empresa, string nom_empresa, int estatus)
            {
                this.id = id;
                this.direccion = direccion;
                this.id_empresa = id_empresa;
                this.nom_empresa = nom_empresa;
                this.estatus = estatus;
            }
            public int id { get; set; }
            public string direccion { get; set; }
            public int id_empresa { get; set; }
            public string nom_empresa { get; set; }
            public int estatus { get; set; }

        }
        #endregion

        /**funcion para traer listado de las bases registradas**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_bases()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objB> lista = new List<objB>();
                var consulta = from ba in context.si_bases
                               join em in context.si_empresas on ba.id_empresa equals em.id_empresa
                               select new
                               {
                                   id = ba.id_base,
                                   desc = ba.ba_direccion,
                                   id_empresa = em.id_empresa,
                                   nom_empresa = em.emp_nombre,
                                   estatus = ba.ba_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objB(
                        item.id,
                        item.desc,
                        item.id_empresa,
                        item.nom_empresa,
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
        public static ajaxResponse traer_bases_comodatario(int id_empresa)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objB> lista = new List<objB>();
                var consulta = from ba in context.si_bases
                               join em in context.si_empresas on ba.id_empresa equals em.id_empresa
                               where ba.id_empresa == id_empresa
                               select new
                               {
                                   id = ba.id_base,
                                   desc = ba.ba_direccion,
                                   id_empresa = em.id_empresa,
                                   nom_empresa = em.emp_nombre,
                                   estatus = ba.ba_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objB(
                        item.id,
                        item.desc,
                        item.id_empresa,
                        item.nom_empresa,
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

        /**funcion para guardar una base nueva**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_base(string direccion, int empresa, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_bases ba = new si_bases();
                ba.ba_direccion = direccion;
                ba.id_empresa = empresa;
                ba.ba_estatus = activo;
                context.si_bases.InsertOnSubmit(ba);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Bases.aspx";
                b.bit_accion = "Guardar Base";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Base = " + direccion;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al intentar insertar una base." + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_base_comodatario(string direccion, int empresa, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_bases ba = new si_bases();
                ba.ba_direccion = direccion;
                ba.id_empresa = empresa;
                ba.ba_estatus = activo;
                context.si_bases.InsertOnSubmit(ba);
                context.SubmitChanges();

                /**recordar traer ultimo id**
                var consulta_tabla = context.si_bases.OrderByDescending(x => x.id_base).First();
                var ultimo_id = consulta_tabla.id_base;
                si_comodatarios_bases comoba = new si_comodatarios_bases();
                comoba.id_comodatario = comodatario;
                comoba.id_base = ultimo_id;
                context.si_comodatarios_bases.InsertOnSubmit(comoba);
                context.SubmitChanges();*/

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Bases.aspx";
                b.bit_accion = "Guardar Base";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Base = " + direccion;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al intentar insertar una base." + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        /**function para actualizar base**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_base(string direccion, int empresa, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_bases ba = null;
                ba = context.si_bases.Where(x => x.id_base.Equals(id)).FirstOrDefault();
                ba.ba_direccion = direccion;
                ba.id_empresa = empresa;
                ba.ba_estatus = activo;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Bases.aspx";
                b.bit_accion = "Actualizo Base";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario actualizo Base = " + direccion;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizo Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al actualizar la base. " + ex.Message;
                Response.Data = null;
            }
            return Response;
        }
    }
}