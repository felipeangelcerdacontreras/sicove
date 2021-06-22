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
    public partial class unidadesFiltro : System.Web.UI.Page
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

        public class objU
        {
            public string contrato { get; set; }
            public string economico { get; set; }
            public string zona { get; set; }
            public string Estado { get; set; }
            public string Ciudad { get; set; }
            public string Marca { get; set; }
            public string Empresa { get; set; }

        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse unidades_Est(int estado)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();
                var query = from un in context.si_unidades
                            join em in context.si_empresas on un.id_empresa equals em.id_empresa
                            join ma in context.si_marcas on un.id_marca equals ma.id_marca
                            join zo in context.si_zonas on em.id_zona equals zo.id_zona
                            join es in context.si_estados on zo.id_estado equals es.id_estado
                            join ci in context.si_ciudades on zo.id_ciudad equals ci.id_ciudad
                            where es.id_estado == estado
                            select new { un = un, em = em, ma = ma, zo = zo, es = es, ci = ci };
                foreach (var items in query)
                {
                    objU dato = new objU();
                    dato.contrato = (items.un.un_codigo_barra != null) ? items.un.un_codigo_barra : string.Empty;
                    dato.economico = (items.un.un_numero_economico != null) ? items.un.un_numero_economico : string.Empty;
                    dato.zona = (items.zo.cd_descripcion != null) ? items.zo.cd_descripcion : string.Empty;
                    dato.Estado = (items.es.es_descripcion != null) ? items.es.es_descripcion : string.Empty;
                    dato.Ciudad = (items.ci.cd_descripcion != null) ? items.ci.cd_descripcion : string.Empty;
                    dato.Marca = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                    dato.Empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                    list.Add(dato);
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(list);
                    Response.Result = true;
                    Response.Message = "Correctamente";
                    Response.Data = json;
                }
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
        public static ajaxResponse unidades_EstCiu(int estado, int ciudad)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();
                var query = from un in context.si_unidades
                            join em in context.si_empresas on un.id_empresa equals em.id_empresa
                            join ma in context.si_marcas on un.id_marca equals ma.id_marca
                            join zo in context.si_zonas on em.id_zona equals zo.id_zona
                            join es in context.si_estados on zo.id_estado equals es.id_estado
                            join ci in context.si_ciudades on zo.id_ciudad equals ci.id_ciudad
                            where es.id_estado == estado && ci.id_ciudad == ciudad
                            select new { un = un, em = em, ma = ma, zo = zo, es = es, ci = ci };
                foreach (var items in query)
                {
                    objU dato = new objU();
                    dato.contrato = (items.un.un_codigo_barra != null) ? items.un.un_codigo_barra : string.Empty;
                    dato.economico = (items.un.un_numero_economico != null) ? items.un.un_numero_economico : string.Empty;
                    dato.zona = (items.zo.cd_descripcion != null) ? items.zo.cd_descripcion : string.Empty;
                    dato.Estado = (items.es.es_descripcion != null) ? items.es.es_descripcion : string.Empty;
                    dato.Ciudad = (items.ci.cd_descripcion != null) ? items.ci.cd_descripcion : string.Empty;
                    dato.Marca = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                    dato.Empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                    list.Add(dato);
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(list);
                    Response.Result = true;
                    Response.Message = "Correctamente";
                    Response.Data = json;
                }
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
        public static ajaxResponse unidades_NumEco(string economico)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();
                var query = from un in context.si_unidades
                            join em in context.si_empresas on un.id_empresa equals em.id_empresa
                            join ma in context.si_marcas on un.id_marca equals ma.id_marca
                            join zo in context.si_zonas on em.id_zona equals zo.id_zona
                            join es in context.si_estados on zo.id_estado equals es.id_estado
                            join ci in context.si_ciudades on zo.id_ciudad equals ci.id_ciudad
                            where un.un_numero_economico.StartsWith(economico)
                            select new { un = un, em = em, ma = ma, zo = zo, es = es, ci = ci };
                foreach (var items in query)
                {
                    objU dato = new objU();
                    dato.contrato = (items.un.un_codigo_barra != null) ? items.un.un_codigo_barra : string.Empty;
                    dato.economico = (items.un.un_numero_economico != null) ? items.un.un_numero_economico : string.Empty;
                    dato.zona = (items.zo.cd_descripcion != null) ? items.zo.cd_descripcion : string.Empty;
                    dato.Estado = (items.es.es_descripcion != null) ? items.es.es_descripcion : string.Empty;
                    dato.Ciudad = (items.ci.cd_descripcion != null) ? items.ci.cd_descripcion : string.Empty;
                    dato.Marca = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                    dato.Empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                    list.Add(dato);
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(list);
                    Response.Result = true;
                    Response.Message = "Correctamente";
                    Response.Data = json;
                }
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
        public static ajaxResponse unidades_NumCon(string contrato)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();
                var query = from un in context.si_unidades
                            join em in context.si_empresas on un.id_empresa equals em.id_empresa
                            join ma in context.si_marcas on un.id_marca equals ma.id_marca
                            join zo in context.si_zonas on em.id_zona equals zo.id_zona
                            join es in context.si_estados on zo.id_estado equals es.id_estado
                            join ci in context.si_ciudades on zo.id_ciudad equals ci.id_ciudad
                            where un.un_codigo_barra.StartsWith(contrato)
                            select new { un = un, em = em, ma = ma, zo = zo, es = es, ci = ci };
                foreach (var items in query)
                {
                    objU dato = new objU();
                    dato.contrato = (items.un.un_codigo_barra != null) ? items.un.un_codigo_barra : string.Empty;
                    dato.economico = (items.un.un_numero_economico != null) ? items.un.un_numero_economico : string.Empty;
                    dato.zona = (items.zo.cd_descripcion != null) ? items.zo.cd_descripcion : string.Empty;
                    dato.Estado = (items.es.es_descripcion != null) ? items.es.es_descripcion : string.Empty;
                    dato.Ciudad = (items.ci.cd_descripcion != null) ? items.ci.cd_descripcion : string.Empty;
                    dato.Marca = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                    dato.Empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                    list.Add(dato);
                    var jsonSerialiser = new JavaScriptSerializer();
                    var json = jsonSerialiser.Serialize(list);
                    Response.Result = true;
                    Response.Message = "Correctamente";
                    Response.Data = json;
                }
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