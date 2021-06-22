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
    public partial class Comodatarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }
        public class objC
        {
            //public objC(int id, string nombre, int[] bases, string telefono, string correo, int activo)
            //{
            //    this.id = id;
            //    this.nombre = nombre;
            //    this.bases = bases;
            //    this.telefono = telefono;
            //    this.correo = correo;
            //    this.activo = activo;
            //}
            public int id { get; set; }
            public string nombre { get; set; }
            public int[] bases { get; set; }
            public string telefono { get; set; }
            public string correo { get; set; }
            public int activo { get; set; }
            public string usuario { get; set; }
            public string contraseña { get; set; }
            public int? idrol { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_comodatarios( string nombre, string telefono)
        {
            ajaxResponse Response = new ajaxResponse();
            /*y
            {*/
                sicoveDBDataContext context = new sicoveDBDataContext();
                context.CommandTimeout = 1000;
                List<objC> lista = new List<objC>();

                if (nombre == null && telefono == null) {
                    var sentencia = "SELECT * FROM si_comodatarios WHERE 1 = 1";
                    var consulta = context.SPTRAERCOMODATARIOS(sentencia, null);

                    int id_comodatario = 0;

                    foreach (var item in consulta)
                    {
                        objC dato = new objC();
                        id_comodatario = item.id_comodatario;
                        dato.id = item.id_comodatario;
                        dato.nombre = item.co_nombre;
                        dato.telefono = item.co_telefono;
                        dato.correo = item.co_correo;
                        dato.activo = (int)item.co_estatus;
                        dato.usuario = item.co_usuario;
                        dato.contraseña = item.co_contraseña;
                        dato.idrol = (int)item.id_rol;

                        lista.Add(dato);
                    }
                } else {
                    nombre = nombre.Trim();
                    var sentencia = "SELECT * FROM si_comodatarios WHERE 1 = 1";

                    string[] nombres = nombre.Split(' ');

                    if (nombre.Length > 0)
                    {
                        sentencia += " AND (";
                        int contador = 1;
                        foreach (var sub in nombres)
                        {
                            sentencia += " co_nombre like '%" + sub + "%' ";
                            contador++;
                            if (nombres.Length >= contador)
                            {
                                sentencia += " OR ";
                            }
                        }
                        sentencia += ") ";

                    }

                    if (telefono != "" || telefono != null)
                    {
                        sentencia += " AND co_telefono like '%" + telefono + "%'";
                    }

                    var consulta = context.SPTRAERCOMODATARIOS(sentencia, telefono);

                    int id_comodatario = 0;

                    foreach (var item in consulta)
                    {
                        objC dato = new objC();
                        id_comodatario = item.id_comodatario;
                        dato.id = item.id_comodatario;
                        dato.nombre = item.co_nombre;
                        dato.telefono = item.co_telefono;
                        dato.correo = item.co_correo;
                        dato.activo = (int)item.co_estatus;
                        dato.usuario = item.co_usuario;
                        dato.contraseña = item.co_contraseña;
                        dato.idrol = (int)item.id_rol;

                        lista.Add(dato);
                    }
                }
                
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(lista);
                Response.Result = true;
            Response.Message = "Correctamente" + nombre;
                Response.Data = json;
            /*
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al intentar traer la informacion." + ex.Message;
                Response.Data = null;
            }*/
            return Response;
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_como(string nombre, string telefono, string correo, string usuario, string contraseña,int rol,int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_comodatarios co = new si_comodatarios();
                co.co_nombre = nombre;
                co.co_telefono = telefono;
                co.co_correo = correo;
                co.co_estatus = activo;
                co.co_usuario = usuario;
                co.co_contraseña = contraseña;
                co.id_rol = rol;
                context.si_comodatarios.InsertOnSubmit(co);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "comodatarios.aspx";
                b.bit_accion = "Agregar Comodatario";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario agrego comodatario = " + nombre;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al intentar insertar un comodatario." + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_como(string nombre, string telefono, string correo, string usuario, string contraseña, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                /**actualizamos informacion del comodatario**/
                si_comodatarios co = null;
                co = context.si_comodatarios.Where(x => x.id_comodatario.Equals(id)).FirstOrDefault();
                co.co_nombre = nombre;
                co.co_telefono = telefono;
                co.co_correo = correo;
                co.co_estatus = activo;
                co.co_usuario = usuario;
                co.co_contraseña = contraseña;
                context.SubmitChanges();
                
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "comodatarios.aspx";
                b.bit_accion = "actualizar Comodatario";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario actualizo comodatario = " + nombre;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizo Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al intentar modificar un comodatario." + ex.Message;
                Response.Data = null;
            }
            return Response;
        }
    }
}