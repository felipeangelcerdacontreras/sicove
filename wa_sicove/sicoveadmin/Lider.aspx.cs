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
    public partial class Lider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Etnidades

        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }

        public class objL
        {
            public objL(int id, string nombre, string apellido, string telefono, string correo, decimal comicion, int estatus)
            {
                this.id = id;
                this.nombre = nombre;
                this.apellido = apellido;
                this.telefono = telefono;
                this.correo = correo;
                this.comicion = comicion;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string telefono { get; set; }
            public string correo { get; set; }
            public decimal comicion { get; set; }
            public int estatus { get; set; }
        }

        #endregion

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_l()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objL> lista = new List<objL>();

                var consulta = from ro in context.si_lideres select ro;

                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.li_estatus)
                        activo = 1;

                    lista.Add(new objL(
                        item.id_lider,
                        item.li_nombre,
                        item.li_apellido,
                        item.li_telefono,
                        item.li_correo,
                        (decimal)item.li_comicion,
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
        public static ajaxResponse guardar_l(string nombre, string apellido, string telefono, string correo, decimal comicion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_lideres roll = new si_lideres();

                roll.li_nombre = nombre;
                roll.li_apellido = apellido;
                roll.li_telefono = telefono;
                roll.li_correo = correo;
                roll.li_comicion = comicion;
                roll.li_estatus = false;
                if (activo == 1)
                    roll.li_estatus = true;

                context.si_lideres.InsertOnSubmit(roll);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Lider.aspx";
                b.bit_accion = "Guardar Lider";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Lider = " + nombre + " " + apellido;
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
        public static ajaxResponse actualizar_l(string nombre, string apellido, string telefono, string correo, decimal comicion, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_lideres roll = null;

                roll = context.si_lideres.Where(x => x.id_lider.Equals(id)).FirstOrDefault();

                roll.li_nombre = nombre;
                roll.li_apellido = apellido;
                roll.li_telefono = telefono;
                roll.li_correo = correo;
                roll.li_comicion = comicion;
                roll.li_estatus = false;
                if (activo == 1)
                    roll.li_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Lider.aspx";
                b.bit_accion = "Actualiza Lider";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Lider, Id = " + id;
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