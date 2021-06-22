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
    public partial class Giros : System.Web.UI.Page
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
        public class objG
        {
            public objG(int id, string descripcion, int estatus, int autoconsumo)
            {
                this.id = id;
                this.descripcion = descripcion;
                this.estatus = estatus;
                this.autoconsumo = autoconsumo;
            }

            public int id { get; set; }
            public string descripcion { get; set; }
            public int estatus { get; set; }
            public int autoconsumo { get; set; }
        }
        #endregion

        /**funcion para traer listado de los giros registrados**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_giros()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objG> lista = new List<objG>();
                var consulta = from ro in context.si_giros select ro;
                foreach (var item in consulta)
                {
                    lista.Add(new objG(
                        item.id_giro,
                        item.gi_descripcion,
                        (int)item.gi_estatus,
                        (int)item.gi_autoconsumo
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

        /**funcion para traer listado de los giros registrados que no tienen autoconsumo**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_giros_no_autoconsumo()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objG> lista = new List<objG>();
                var consulta = from ro in context.si_giros.Where(x => x.gi_autoconsumo.Equals(0)) select ro;
                foreach (var item in consulta)
                {
                    lista.Add(new objG(
                        item.id_giro,
                        item.gi_descripcion,
                        (int)item.gi_estatus,
                        (int)item.gi_autoconsumo
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

        /**funcion para traer listado de los giros registrados que no tienen autoconsumo**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_giros_si_autoconsumo()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objG> lista = new List<objG>();
                var consulta = from ro in context.si_giros.Where(x => x.gi_autoconsumo.Equals(1)) select ro;
                foreach (var item in consulta)
                {
                    lista.Add(new objG(
                        item.id_giro,
                        item.gi_descripcion,
                        (int)item.gi_estatus,
                        (int)item.gi_autoconsumo
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

        /**funcion para guadar giro nuevo**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_giro(string desc, int activo, int id, int autoconsumo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_giros roll = new si_giros();
                roll.gi_descripcion = desc;
                roll.gi_estatus = activo;
                roll.gi_autoconsumo = autoconsumo;
                context.si_giros.InsertOnSubmit(roll);
                context.SubmitChanges();


                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Giros.aspx";
                b.bit_accion = "Guardar Giro";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Giro = " + desc;
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

        /**funcion para actualizar informacion de el giro**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_giro(string desc, int activo, int id, int autoconsumo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_giros roll = null;
                roll = context.si_giros.Where(x => x.id_giro.Equals(id)).FirstOrDefault();
                roll.gi_descripcion = desc;
                roll.gi_estatus = activo;
                roll.gi_autoconsumo = autoconsumo;
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Giros.aspx";
                b.bit_accion = "Actualiza Giro";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Giro, Id = " + id;
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