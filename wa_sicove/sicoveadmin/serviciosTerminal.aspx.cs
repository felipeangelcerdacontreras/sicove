using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;

namespace wa_sicove.sicoveadmin
{
    public partial class serviciosTerminal : System.Web.UI.Page
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
            public string estacion { get; set; }
            public string numero { get; set; }
            public string direccion { get; set; }
            public string zona { get; set; }
            public string ciudad { get; set; }
            public string estado { get; set; }
            public int consumos { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_servicios(DateTime inicio, DateTime final)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                /**recordar: en esta parte es para hacer una sumatoria de un identity**/
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();
                ArrayList arr = new ArrayList();
                var query = from ven in context.si_ventas
                            join es in context.si_estaciones on ven.id_estacion equals es.id_estacion
                            where ven.ve_fecha_venta >= inicio && ven.ve_fecha_venta < final
                            && es.esta_estatus == 1
                            group ven by ven.id_estacion into estacion
                            select new { estacion = estacion.Key, count = (from ven in estacion select ven.id_venta).Distinct().Count() };
                foreach (var item in query)
                {
                    var estaciones = from es in context.si_estaciones
                                 join zo in context.si_zonas on es.id_zona equals zo.id_zona
                                 join ciu in context.si_ciudades on zo.id_ciudad equals ciu.id_ciudad
                                 join est in context.si_estados on zo.id_estado equals est.id_estado
                                 where es.id_estacion == item.estacion
                                 select new
                                 {
                                     estacion = es.esta_descripcion,
                                     numero = es.esta_numero,
                                     direccion = es.esta_direccion,
                                     zona = zo.cd_descripcion,
                                     ciudad = ciu.cd_descripcion,
                                     estado = est.es_descripcion
                                 };
                    arr.Add(item.estacion);
                    foreach (var item2 in estaciones)
                    {
                        objC dato = new objC();
                        dato.estacion = item2.estacion;
                        dato.numero = item2.numero;
                        dato.direccion = item2.direccion;
                        dato.zona = item2.zona;
                        dato.ciudad = item2.ciudad;
                        dato.estado = item2.estado;
                        dato.consumos = item.count;
                        list.Add(dato);
                    }
                }
                var estacionesCero = from es in context.si_estaciones
                             join zo in context.si_zonas on es.id_zona equals zo.id_zona
                             join ciu in context.si_ciudades on zo.id_ciudad equals ciu.id_ciudad
                             join est in context.si_estados on zo.id_estado equals est.id_estado
                             where es.esta_estatus == 1
                             select new
                             {
                                 id_estacion = es.id_estacion,
                                 estacion = es.esta_descripcion,
                                 numero = es.esta_numero,
                                 direccion = es.esta_direccion,
                                 zona = zo.cd_descripcion,
                                 ciudad = ciu.cd_descripcion,
                                 estado = est.es_descripcion
                             };
                foreach (var item3 in estacionesCero)
                {
                    if (arr.Contains(item3.id_estacion))
                    {
                        //si existe no hacemos nada
                    }
                    else
                    {
                        objC dato = new objC();
                        dato.estacion = item3.estacion;
                        dato.numero = item3.numero;
                        dato.direccion = item3.direccion;
                        dato.zona = item3.zona;
                        dato.ciudad = item3.ciudad;
                        dato.estado = item3.estado;
                        dato.consumos = 0;
                        list.Add(dato);
                    }
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
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }
            return Response;
        }
    }
}