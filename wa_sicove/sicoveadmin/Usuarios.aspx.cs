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
    public partial class Usuarios : System.Web.UI.Page
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
        public class objUser
        {
            public objUser(int id_usuario, string nombre, string usuario, string password, string descripcion_rol, int id_rol, int estatus)
            {
                this.id_usuario = id_usuario;
                this.nombre = nombre;
                this.usuario = usuario;
                this.password = password;
                this.descripcion_rol = descripcion_rol;
                this.id_rol = id_rol;
                this.estatus = estatus;
            }

            public int id_usuario { get; set; }
            public string nombre { get; set; }
            public string usuario { get; set; }
            public string password { get; set; }
            public string descripcion_rol { get; set; }
            public int id_rol { get; set; }
            public int estatus { get; set; }
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_usuarios()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objUser> lista = new List<objUser>();
                var consulta = from user in context.si_usuarios
                               join roles in context.si_roles on user.id_rol equals roles.id_rol 
                               select new {
                                   id_usuario = user.id_usuario,
                                   nombre = user.us_nombre,
                                   usuario = user.us_user,
                                   passw = user.us_password,
                                   id_rol = user.id_rol,
                                   desc_rol = user.si_roles.rol_descripcion,
                                   estatus = user.us_estatus
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objUser(
                       item.id_usuario,
                       item.nombre,
                       item.usuario,
                       item.passw,
                       item.desc_rol,
                       (int)item.id_rol,
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
        public static ajaxResponse guardar_user(string nombre, string usuario, string password, int rol, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_usuarios user = new si_usuarios();
                user.us_nombre = nombre;
                user.us_user = usuario;
                user.us_password = password;
                user.id_rol = rol;
                user.us_estatus = activo;
                user.us_bloqueado = 0;
                context.si_usuarios.InsertOnSubmit(user);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Usuarios.aspx";
                b.bit_accion = "Guardar Usuario";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Usuario = " + nombre;
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
        public static ajaxResponse actualizar_user(string nombre, string usuario, string password, int rol, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_usuarios user = null;
                user = context.si_usuarios.Where(x => x.id_usuario.Equals(id)).FirstOrDefault();
                user.us_nombre = nombre;
                user.us_user = usuario;
                user.us_password = password;
                user.id_rol = rol;
                user.us_estatus = activo;
                user.us_bloqueado = 0;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Usuarios.aspx";
                b.bit_accion = "Actualiza Usuario";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Usuario, Id = " + id;
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