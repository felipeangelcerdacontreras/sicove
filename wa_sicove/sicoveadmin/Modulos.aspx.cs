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
    public partial class Modulos : System.Web.UI.Page
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
            public obj(int id, string mod_nombre, string mod_archivo, string mod_descripcion, int mod_nivel, int mod_orden, int id_padre, string desc_padre, int estatus)
            {
                this.id = id;
                this.mod_nombre = mod_nombre;
                this.mod_archivo = mod_archivo;
                this.mod_descripcion = mod_descripcion;
                this.mod_nivel = mod_nivel;
                this.mod_orden = mod_orden;
                this.id_padre = id_padre;
                this.desc_padre = desc_padre;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string mod_nombre { get; set; }
            public string mod_archivo { get; set; }
            public string mod_descripcion { get; set; }
            public int mod_nivel { get; set; }
            public int mod_orden { get; set; }
            public int id_padre { get; set; }
            public string desc_padre { get; set; }
            public int estatus { get; set; }
        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse CargaPadre()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<obj> lista = new List<obj>();
                var objModulos = (from modulos in context.si_modulos
                                  where modulos.mod_nivel < 1 && modulos.mod_estatus == 1
                                  orderby modulos.id_padre
                                  select modulos).ToList();

                foreach (var item in objModulos)
                {

                    if (item.id_modulo > 0 )
                    {
                        lista.Add(new obj(
                          item.id_modulo,
                          item.mod_nombre,
                          item.mod_archivo,
                          item.mod_descripcion,
                          (int)item.mod_nivel,
                          (int)item.mod_orden,
                          0,
                          "",
                          (int)item.mod_estatus
                        ));
                    }
                    
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
                Response.Message = "Ha ocurrido un error al cargar. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<obj> lista = new List<obj>();
                var objModulos = (
                        from modulos in context.si_modulos
                        join infopadre in context.si_modulos on modulos.id_padre equals infopadre.id_modulo into ps
                        from p in ps.DefaultIfEmpty()
                        orderby modulos.id_padre,modulos.mod_descripcion
                        select new { modulos, p }
                ).ToList();
                foreach (var item in objModulos)
                {
                    int id_p = 0;
                    string padr = "";
                    if (item.modulos.id_padre == null)
                    {
                        id_p = -1;
                        padr = "";
                    }
                    else
                    {
                        id_p = (int)item.modulos.id_padre;
                        padr = item.p.mod_nombre;
                    }
                    if (item.modulos.id_modulo > 0)
                    {
                        lista.Add(new obj(
                          item.modulos.id_modulo,
                          item.modulos.mod_nombre,
                          item.modulos.mod_archivo,
                          item.modulos.mod_descripcion,
                          (int)item.modulos.mod_nivel,
                          (int)item.modulos.mod_orden,
                          id_p,
                          padr,
                          (int)item.modulos.mod_estatus
                        ));
                    }
                    
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
        public static ajaxResponse guardar(string mod_nombre, string mod_archivo, string mod_descripcion, int mod_nivel,
            int mod_orden, int id_padre, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modulos obj = new si_modulos();
                //item.modulos.id_modulo,
                obj.mod_nombre = mod_nombre;
                obj.mod_archivo = mod_archivo;
                obj.mod_descripcion = mod_descripcion;
                obj.mod_nivel = mod_nivel;
                obj.mod_orden = mod_orden;
                obj.id_padre = id_padre < 1 ? 0:id_padre;
                obj.mod_estatus = activo;
                context.si_modulos.InsertOnSubmit(obj);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Modulos.aspx";
                b.bit_accion = "Guardar Modulo";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Modulo = " + mod_nombre;
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
        public static ajaxResponse actualizar(string mod_nombre, string mod_archivo, string mod_descripcion, int mod_nivel,
            int mod_orden, int id_padre, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modulos obj = null;
                obj = context.si_modulos.Where(x => x.id_modulo.Equals(id)).FirstOrDefault();
                //item.modulos.id_modulo,
                obj.mod_nombre = mod_nombre;
                obj.mod_archivo = mod_archivo;
                obj.mod_descripcion = mod_descripcion;
                obj.mod_nivel = mod_nivel;
                obj.mod_orden = mod_orden;
                obj.id_padre = id_padre;
                obj.mod_estatus = activo;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Modulos.aspx";
                b.bit_accion = "Actualiza Modulo";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Modulo, Id = " + id;
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