using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;
using System.ComponentModel;

namespace wa_sicove.sicoveadmin
{
    public partial class CortesiasAlta : System.Web.UI.Page
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
            public int id { get; set; }
            public int? id_tipo_cortesia { get; set; }
            public string folioCortesia { get; set; }
            public string cortesia { get; set; }
            public int? id_unidad { get; set; }
            public string unidad { get; set; }
            public double? cantidad { get; set; }
            public int? id_estatus_cortesia { get; set; }
            public string observaciones { get; set; }
            public DateTime? fecha_generada { get; set; }
            public DateTime? fecha_aplicada { get; set; }
            public DateTime? fecha_vencimiento { get; set; }
            public int? Autoriza { get; set; }
            public string SucursalN { get; set; }
            public int? Sucursal { get; set; }
            public bool libre { get; set; }
            public string Empresa { get; set; }
        }

        public class ultimoFolio
        {
            public int folioCortesia { get; set; }
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_C(DateTime fInicial, DateTime fFinal, int? empresa, string unidad, int? estatus)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();
                
                
                var query = context.SPCortesias(fInicial, fFinal, empresa, unidad);



                //var query = from c in context.si_cortesias
                  //          join u in context.si_unidades on c.id_unidad equals u.id_unidad
                    //        join es in context.si_estaciones on c.id_estacion equals es.id_estacion
                      //      join e in context.si_empresas on u.id_empresa equals e.id_empresa
                        //    join tc in context.si_tipos_cortesia on c.id_tipo_cortesia equals tc.id_tipo
                          //  select new { c = c, u = u, tc = tc, es = es, e = e};
                

                foreach (var items in query)
                {
                    objC dato = new objC();
                    dato.id = items.id;
                    dato.id_tipo_cortesia = items.id_tipo_cortesia;
                    dato.folioCortesia = items.folioCortesia;
                    dato.cortesia = items.cortesia;
                    dato.id_unidad = items.id_unidad;
                    dato.unidad = items.unidad;
                    dato.cantidad = items.cantidad;
                    dato.id_estatus_cortesia = items.id_estatus_cortesia;
                    dato.observaciones = items.observaciones;
                    dato.fecha_generada = items.fecha_generada;
                    dato.fecha_aplicada = items.fecha_aplicada;
                    dato.fecha_vencimiento = items.fecha_vencimiento;
                    dato.Autoriza = items.Auoriza;
                    dato.Sucursal = items.Sucursal;
                    dato.SucursalN = items.SucursalN;
                    dato.libre = items.libre;
                    dato.Empresa = items.Empresa;

                    if( estatus == -1) {
                        list.Add(dato);

                    } else {
                        if( estatus == dato.id_estatus_cortesia ) {
                            list.Add(dato);
                        }
                    }


                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente "+ query;
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse infoVales( int? id)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();

                var query = id == null ? 
                from c in context.si_cortesias
                            join u in context.si_unidades on c.id_unidad equals u.id_unidad
                            where c.cor_estatus_cortesia == 1 && c.cor_fecha_cortesia_aplicada == null
                            select new { c = c, u = u }
                :
                from c in context.si_cortesias
                            join u in context.si_unidades on c.id_unidad equals u.id_unidad
                            where c.cor_estatus_cortesia == 1 && c.id_cortesia == id
                            select new { c = c, u = u };

                foreach (var items in query)
                {
                    objC dato = new objC();
                    dato.id = items.c.id_cortesia;
                    dato.folioCortesia = items.c.cor_codigo_cortesia;
                    dato.id_unidad = items.u.id_unidad;
                    dato.unidad = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                    dato.cantidad = (items.c.cor_cantidad_cortesia != null) ? (double)items.c.cor_cantidad_cortesia : -1;
                    dato.fecha_vencimiento = (items.c.cor_fecha_cortesia_vencimiento != null) ? (DateTime)items.c.cor_fecha_cortesia_vencimiento : DateTime.Now;
                    dato.fecha_generada = (items.c.cor_fecha_cortesia_generada != null) ? (DateTime)items.c.cor_fecha_cortesia_generada : DateTime.Now;
                    list.Add(dato);

                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }






        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_C_Atoma()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();
                var query = from c in context.si_cortesias
                            join u in context.si_unidades on c.id_unidad equals u.id_unidad
                            join tc in context.si_tipos_cortesia on c.id_tipo_cortesia equals tc.id_tipo
                            join es in context.si_estaciones on c.id_estacion equals es.id_estacion
                            join e in context.si_empresas on u.id_empresa equals e.id_empresa
                            where c.cor_estatus_cortesia == 4
                            select new { c = c, u = u, tc = tc, es = es, e = e };
                foreach (var items in query)
                {
                    objC dato = new objC();
                    dato.id = items.c.id_cortesia;
                    dato.id_tipo_cortesia = items.tc.id_tipo;
                    dato.folioCortesia = items.c.cor_codigo_cortesia;
                    dato.cortesia = (items.tc.descripcion != null) ? items.tc.descripcion : string.Empty;
                    dato.id_unidad = items.u.id_unidad;
                    dato.unidad = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                    dato.cantidad = (items.c.cor_cantidad_cortesia != null) ? (double)items.c.cor_cantidad_cortesia : -1;
                    dato.id_estatus_cortesia = (items.c.cor_estatus_cortesia != null) ? (int)items.c.cor_estatus_cortesia : -1;
                    dato.observaciones = (items.c.cor_observacion != null) ? items.c.cor_observacion : string.Empty;
                    dato.fecha_generada = (items.c.cor_fecha_cortesia_generada != null) ? (DateTime)items.c.cor_fecha_cortesia_generada : DateTime.Now;
                    dato.fecha_aplicada = (items.c.cor_fecha_cortesia_aplicada != null) ? (DateTime)items.c.cor_fecha_cortesia_aplicada : DateTime.Now;
                    dato.fecha_vencimiento = (items.c.cor_fecha_cortesia_vencimiento != null) ? (DateTime)items.c.cor_fecha_cortesia_vencimiento : DateTime.Now;
                    dato.Autoriza = (items.c.cor_quien_autoriza != null) ? (int)items.c.cor_quien_autoriza : -1;
                    dato.SucursalN = (items.es.esta_descripcion != null) ? items.es.esta_descripcion : string.Empty;
                    dato.Sucursal = (items.c.id_estacion != null) ? (int)items.c.id_estacion : -1;
                    dato.libre = items.c.cor_libre;
                    dato.Empresa = items.e.emp_nombre;
                    list.Add(dato);
                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_F()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<ultimoFolio> list = new List<ultimoFolio>();
                var query = context.si_cortesias.OrderByDescending(x => x.id_cortesia).First();
                ultimoFolio dato = new ultimoFolio();
                dato.folioCortesia = Convert.ToInt32(query.cor_codigo_cortesia);
                list.Add(dato);
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardad_c(int tipoC, int Unidad, double Cantidad, int Estatus, string Obser, string folio, int autoriza, int id, int estacion, bool libre)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                /**recordar validar si una consulta esta vacia**/
                var cortesia_existe = from c in context.si_cortesias
                                      where c.id_unidad == Unidad && c.cor_estatus_cortesia == 1
                                      select c;
                if (!cortesia_existe.Any())
                {
                    si_cortesias co = new si_cortesias();
                    DateTime generada = DateTime.Now;
                    DateTime vencimiento = generada.AddDays(15);

                    var seed = Environment.TickCount;
                    var random = new Random(seed);
                    var nAle = random.Next(0, 100);

                    string nfolio = generada.Year + "" + generada.Month + tipoC + "" + generada.Day + estacion + nAle + "" + generada.Hour + generada.Second + generada.Millisecond+Unidad;

                    co.id_tipo_cortesia = tipoC;
                    co.cor_fecha_cortesia_generada = generada;
                    co.cor_fecha_cortesia_vencimiento = vencimiento;
                    co.id_unidad = Unidad;
                    co.cor_cantidad_cortesia = Cantidad;
                    co.cor_estatus_cortesia = Estatus;
                    co.cor_observacion = Obser;
                    co.cor_codigo_cortesia = nfolio;
                    co.cor_quien_autoriza = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                    co.cor_autorizada = 1;
                    co.id_estacion = estacion;
                    co.cor_libre = libre;
                    context.si_cortesias.InsertOnSubmit(co);
                    context.SubmitChanges();

                    // Alimentamos Bitacora
                    si_bitacora b = new si_bitacora();
                    b.bit_fecha = DateTime.Now.Date;
                    b.bit_tiempo = DateTime.Now.TimeOfDay;
                    b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                    b.bit_modulo = "Cortesias.aspx";
                    b.bit_accion = "alta cortesia";
                    b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Se creo una nueva cortesia";
                    ClassBicatora.insertBitacora(b);
                    Response.Result = true;
                    Response.Message = "Actualizado Correctamente";
                    Response.Data = null;
                }
                else
                {
                    Response.Result = false;
                    Response.Message = "unidad";
                    Response.Data = null;
                }

               
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_c(int tipoC, int Unidad, double Cantidad, int Estatus, string Obser, string folio, int autoriza, int id, int estacion, bool libre)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_cortesias co = null;
                co = context.si_cortesias.Where(x => x.id_cortesia.Equals(id)).FirstOrDefault();
                co.id_tipo_cortesia = tipoC;
                co.id_unidad = Unidad;
                co.cor_cantidad_cortesia = Cantidad;
                co.cor_estatus_cortesia = Estatus;
                co.cor_observacion = Obser;
                co.cor_codigo_cortesia = folio;
                co.cor_libre = libre;

                if (Estatus == 2)
                    co.cor_quien_cancela = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                else
                    co.cor_quien_autoriza = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario; ;

                co.id_estacion = estacion;
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Cortesias.aspx";
                b.bit_accion = "Actualizar Cortesia como realizado";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Se actualizo la cortesia  = " + id;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizado Correctamente";
                Response.Data = null;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

    }
}