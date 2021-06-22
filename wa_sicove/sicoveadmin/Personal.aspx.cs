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
    public partial class Personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Estidades

        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }

        public class objp
        {
            public objp(int id, string nombre, string apellido, string numero_empleado, string codigo_barra, int? id_zona, string zona, int? id_tipo_personal, string tipo_personal, int estatus)
            {
                this.id = id;
                this.nombre = nombre;
                this.apellido = apellido;
                this.numero_empleado = numero_empleado;
                this.codigo_barra = codigo_barra;
                this.id_zona = id_zona;
                this.zona = zona;
                this.id_tipo_personal = id_tipo_personal;
                this.tipo_personal = tipo_personal;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string numero_empleado { get; set; }
            public string codigo_barra { get; set; }
            public int? id_zona { get; set; }
            public string zona { get; set; }
            public int? id_tipo_personal { get; set; }
            public string tipo_personal { get; set; }
            public int estatus { get; set; }
        }

        #endregion

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_p()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objp> lista = new List<objp>();

                var consulta = from p in context.si_personal
                               select new
                               {
                                   id = p.id_persona,
                                   nombre = p.pe_nombre,
                                   apellido = p.pe_apellido,
                                   numeroEmpleado = p.pe_numero_empleado,
                                   codigoBarra = p.pe_codigo_barras,
                                   idZona = (p.id_zona != null) ? (p.id_zona) : -1,
                                   zona = p.si_zonas.cd_descripcion,
                                   idTipoPersonal = (p.id_tipo_personal != null) ? (p.id_tipo_personal) : -1,
                                   tipoPersonal = p.si_tipo_personal.tp_tipo_personal,
                                   estatus = p.pe_estatus
                               };
                foreach (var item in consulta)
                {
                    int activo = 0;

                    if (item.estatus)
                        activo = 1;

                    lista.Add(new objp(
                       item.id,
                       item.nombre,
                       item.apellido,
                       item.numeroEmpleado,
                       item.codigoBarra,
                       item.idZona,
                       item.zona,
                       item.idTipoPersonal,
                       item.tipoPersonal,
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
        public static ajaxResponse guardar_p(string nombre, string apellido, string numero_empleado, string codigo_barras, int id_zona, int id_tipo_personal, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_personal cds = new si_personal();

                cds.pe_nombre = nombre;
                cds.pe_apellido = apellido;
                cds.pe_numero_empleado = numero_empleado;
                cds.pe_codigo_barras = codigo_barras;
                cds.id_zona = id_zona;
                cds.id_tipo_personal = id_tipo_personal;
                cds.pe_estatus = false;
                if (activo == 1)
                    cds.pe_estatus = true;

                context.si_personal.InsertOnSubmit(cds);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Personal.aspx";
                b.bit_accion = "Guardar Personal";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Personal = " + nombre + " " + apellido;
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
        public static ajaxResponse actualizar_p(string nombre, string apellido, string numero_empleado, string codigo_barras, int id_zona, int id_tipo_personal, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_personal cds = null;

                cds = context.si_personal.Where(x => x.id_persona.Equals(id)).FirstOrDefault();

                cds.pe_nombre = nombre;
                cds.pe_apellido = apellido;
                cds.pe_numero_empleado = numero_empleado;
                cds.pe_codigo_barras = codigo_barras;
                cds.id_zona = id_zona;
                cds.id_tipo_personal = id_tipo_personal;
                cds.pe_estatus = false;
                if (activo == 1)
                    cds.pe_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Personal.aspx";
                b.bit_accion = "Actualiza Personal";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Personal, Id = " + id;
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