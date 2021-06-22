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
    public partial class gestoriaRecargas : System.Web.UI.Page
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
            public string contrato { get; set; }
            public string economico { get; set; }
            public string marca { get; set; }
            public string modelo { get; set; }
            public int anio { get; set; }
            public string direccion { get; set; }
            public string telefono { get; set; }
            public int diasNoConsumos { get; set; }
            public int consumos { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_consumos(DateTime inicio, DateTime final, int diffDias)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();
                ArrayList arr = new ArrayList();
                var query = from ven in context.si_ventas
                            join u in context.si_unidades on ven.id_unidad equals u.id_unidad
                            where ven.ve_fecha_venta >= inicio && ven.ve_fecha_venta < final
                            && u.estatus_instalacion == 1 && u.un_estatus == true
                            group ven by ven.id_unidad into ventas
                            select new { unidad = ventas.Key, count = (from ven in ventas select ven.id_venta).Distinct().Count() };
                foreach (var item in query)
                {
                    var unidad = from u in context.si_unidades
                                 join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                 join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                 where u.id_unidad == item.unidad
                                 select new
                                 {
                                     contrato = u.un_codigo_barra,
                                     economico = u.un_numero_economico,
                                     marca = ma.ma_descripcion,
                                     modelo = mo.mod_descripcion,
                                     anio = u.un_anio,
                                     direccion = u.un_direccion,
                                     telefono = u.un_telefono
                                 };
                    arr.Add(item.unidad);
                    foreach (var item2 in unidad)
                    {
                        objC dato = new objC();
                        dato.contrato = item2.contrato;
                        dato.economico = item2.economico;
                        dato.marca = item2.marca;
                        dato.modelo = item2.modelo;
                        dato.anio = (int)item2.anio;
                        dato.direccion = item2.direccion;
                        dato.telefono = item2.telefono;
                        dato.diasNoConsumos = diffDias - item.count;
                        dato.consumos = item.count;
                        list.Add(dato);
                        
                    }
                }
                var unidadCero = from u in context.si_unidades
                             join ma in context.si_marcas on u.id_marca equals ma.id_marca
                             join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                             where u.estatus_instalacion == 1 && u.un_estatus == true
                             select new
                             {
                                 id_unidad = u.id_unidad,
                                 contrato = u.un_codigo_barra,
                                 economico = u.un_numero_economico,
                                 marca = ma.ma_descripcion,
                                 modelo = mo.mod_descripcion,
                                 anio = u.un_anio,
                                 direccion = u.un_direccion,
                                 telefono = u.un_telefono
                             };
                foreach (var item3 in unidadCero)
                {
                    if (arr.Contains(item3.id_unidad))
                    {
                        //si existe no hacemos nada
                    }
                    else
                    {
                        objC dato = new objC();
                        dato.contrato = item3.contrato;
                        dato.economico = item3.economico;
                        dato.marca = item3.marca;
                        dato.modelo = item3.modelo;
                        dato.anio = (int)item3.anio;
                        dato.direccion = item3.direccion;
                        dato.telefono = item3.telefono;
                        dato.diasNoConsumos = diffDias;
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