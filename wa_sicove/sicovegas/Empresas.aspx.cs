using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;

namespace wa_sicove.sicovegas
{
    public partial class Empresas : System.Web.UI.Page
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
        public class objE
        {
            public objE(int id, string empresa, string zona, int id_zona, int estatus,string contacto, 
                string telefono,string direccion, string correoI, int autoconsumo)
            {
                this.id = id;
                this.empresa = empresa;
                this.zona = zona;
                this.id_zona = id_zona;
                this.estatus = estatus;
                this.contacto = contacto;
                this.telefono = telefono;
                this.direccion = direccion;
                this.correoI = correoI;
                this.autoconsumo = autoconsumo;
            }

            public int id { get; set; }
            public string empresa { get; set; }
            public string zona { get; set; }
            public string contacto { get; set; }
            public string telefono { get; set; }
            public string direccion { get; set; }
            public int id_zona { get; set; }
            public int estatus { get; set; }
            public string correoI { get; set; }
            public int autoconsumo { get; set; }
        }
        public class objFac
        {
            public objFac(int id, string razon, string rfc, string telefono, string correo, string direccion)
            {
                this.id = id;
                this.razon = razon;
                this.rfc = rfc;
                this.telefono = telefono;
                this.correo = correo;
                this.direccion = direccion;
                
            }

            public int id { get; set; }
            public string razon { get; set; }
            public string rfc { get; set; }
            public string telefono { get; set; }
            public string correo { get; set; }
            public string direccion { get; set; }

        }
        #endregion
      
        /**funcion para traer listado de las empresas registradas**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_empresas()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objE> lista = new List<objE>();
                var consulta = from em in context.si_empresas
                               join zn in context.si_zonas on em.id_zona equals zn.id_zona
                               select new
                               {
                                   id = em.id_empresa,
                                   desc = em.emp_nombre,
                                   contacto = em.emp_contacto,
                                   telefono = em.emp_telefono,
                                   direccion = em.emp_direccion,
                                   id_zona = zn.id_zona,
                                   desc_zn = zn.cd_descripcion,
                                   estatus = em.emp_estatus,
                                   correo = em.emp_correo,
                                   autoconsumo = em.emp_autoconsumo
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objE(
                       item.id,
                       item.desc,
                       item.desc_zn,
                       (int)item.id_zona,
                       (int)item.estatus,
                       item.contacto,
                       item.telefono,
                       item.direccion,
                       item.correo == null? "": item.correo,
                       (int)item.autoconsumo
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

        /**funcion para traer listado de las empresas registradas**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_empresas_no_autoconsumo()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objE> lista = new List<objE>();
                var consulta = from em in context.si_empresas.Where(x => x.emp_autoconsumo.Equals(0))
                               join zn in context.si_zonas on em.id_zona equals zn.id_zona
                               select new
                               {
                                   id = em.id_empresa,
                                   desc = em.emp_nombre,
                                   contacto = em.emp_contacto,
                                   telefono = em.emp_telefono,
                                   direccion = em.emp_direccion,
                                   id_zona = zn.id_zona,
                                   desc_zn = zn.cd_descripcion,
                                   estatus = em.emp_estatus,
                                   correo = em.emp_correo,
                                   autoconsumo = em.emp_autoconsumo
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objE(
                       item.id,
                       item.desc,
                       item.desc_zn,
                       (int)item.id_zona,
                       (int)item.estatus,
                       item.contacto,
                       item.telefono,
                       item.direccion,
                       item.correo == null ? "" : item.correo,
                       (int)item.autoconsumo
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

        /**funcion para traer listado de las empresas registradas**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_empresas_si_autoconsumo()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objE> lista = new List<objE>();
                var consulta = from em in context.si_empresas.Where(x => x.emp_autoconsumo.Equals(1))
                               join zn in context.si_zonas on em.id_zona equals zn.id_zona
                               select new
                               {
                                   id = em.id_empresa,
                                   desc = em.emp_nombre,
                                   contacto = em.emp_contacto,
                                   telefono = em.emp_telefono,
                                   direccion = em.emp_direccion,
                                   id_zona = zn.id_zona,
                                   desc_zn = zn.cd_descripcion,
                                   estatus = em.emp_estatus,
                                   correo = em.emp_correo,
                                   autoconsumo = em.emp_autoconsumo
                               };
                foreach (var item in consulta)
                {
                    lista.Add(new objE(
                       item.id,
                       item.desc,
                       item.desc_zn,
                       (int)item.id_zona,
                       (int)item.estatus,
                       item.contacto,
                       item.telefono,
                       item.direccion,
                       item.correo == null ? "" : item.correo,
                       (int)item.autoconsumo
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

        /**funcion para guardar empresa nueva**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_e(string nombre, int est, int activo, int id, string contacto, string telefono,
            string direccion, string razon, string rfc, string telfac, string correo, string dirfac, string correoI, int autoconsumo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_empresas cds = new si_empresas();
                cds.emp_nombre = nombre;
                cds.id_zona = est;
                cds.emp_estatus = activo;
                cds.emp_contacto = contacto;
                cds.emp_telefono = telefono;
                cds.emp_direccion = direccion;
                cds.emp_lat = 0;
                cds.emp_lon = 0;
                cds.emp_correo = correoI;
                cds.emp_autoconsumo = autoconsumo;
                context.si_empresas.InsertOnSubmit(cds);
                context.SubmitChanges();

                si_facturacion fac = new si_facturacion();
                fac.fa_direccion = dirfac;
                fac.fa_email = correo;
                fac.fa_estatus = cds.emp_estatus;
                fac.id_empresa = cds.id_empresa;
                fac.fa_rfc = rfc;
                fac.fa_telefono = telfac;
                fac.fa_razon_social = razon;
                context.si_facturacion.InsertOnSubmit(fac);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Empresas.aspx";
                b.bit_accion = "Guardar Empresa";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Empresa = " + nombre;
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

        /**funcion para actualizar informacion de la empresa**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_e(string nombre, int est, int activo, int id, string contacto, string telefono,
            string direccion, string razon, string rfc, string telfac, string correo, string dirfac,string correoI, int autoconsumo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_empresas cds = null;
                cds = context.si_empresas.Where(x => x.id_empresa.Equals(id)).FirstOrDefault();
                cds.emp_nombre = nombre;
                cds.id_zona = est;
                cds.emp_estatus = activo;
                cds.emp_contacto = contacto;
                cds.emp_telefono = telefono;
                cds.emp_direccion = direccion;
                cds.emp_lat = 0;
                cds.emp_lon = 0;
                cds.emp_correo = correoI;
                cds.emp_autoconsumo = autoconsumo;
                context.SubmitChanges();

                si_facturacion fact = null;
                fact = context.si_facturacion.Where(x => x.id_empresa.Equals(id)).FirstOrDefault();

                if (fact == null)
                {
                    si_facturacion fac = new si_facturacion();
                    fac.fa_direccion = dirfac;
                    fac.fa_email = correo;
                    fac.fa_estatus = cds.emp_estatus;
                    fac.id_empresa = cds.id_empresa;
                    fac.fa_rfc = rfc;
                    fac.fa_telefono = telfac;
                    fac.fa_razon_social = razon;
                    context.si_facturacion.InsertOnSubmit(fac);
                    context.SubmitChanges();
                }
                else
                {
                    fact.fa_direccion = dirfac;
                    fact.fa_email = correo;
                    fact.fa_estatus = activo;
                    fact.fa_rfc = rfc;
                    fact.fa_telefono = telfac;
                    fact.fa_razon_social = razon;
                    context.SubmitChanges();
                }
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Empresas.aspx";
                b.bit_accion = "Actualiza Empresa";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Empresa, Id = " + id;
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

        /**funcion para traer los datos de facturacion**/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_facturacion(int id_empresa)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objFac> lista = new List<objFac>();
                var consulta = from em in context.si_facturacion
                               where em.id_empresa.Equals(id_empresa)
                               select em;
                foreach (var item in consulta)
                {
                    lista.Add(new objFac(
                      item.id_facturacion,
                      item.fa_razon_social,
                      item.fa_rfc,
                      item.fa_telefono,
                      item.fa_email,
                      item.fa_direccion
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

    }
}