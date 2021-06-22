using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.Services;
using wa_sicove.core;

namespace wa_sicove.sicovegas
{
    public partial class ReportesMtto : System.Web.UI.Page
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

        public class objM
        {
            public string un_codigo_barra { get; set; }
            public DateTime? mtto_fecha_original { get; set; }
            public DateTime? un_fecha_instalacion { get; set; }
            public string emp_nombre { get; set; }
            public string co_nombre { get; set; }
            public string emp_direccion { get; set; }
            public string emp_telefono { get; set; }
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse RepMantenimiento(int tipo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                context.CommandTimeout = 4000;
                List<objM> list = new List<objM>();

                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                //Asi obtenemos el primer dia del mes actual
                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                DateTime oUltimoDiaDelMes = new DateTime(date.Year, date.Month + 1, 1).AddDays(-1);

                DateTime MenosSeisMeses  = new DateTime(date.Year, date.Month, 1).AddDays(-1).AddMonths(-6);

                DateTime MesesMenosDias = new DateTime(date.Year, date.Month + 1, 1).AddDays(-1).AddMonths(-6);




                if (tipo == 0) {
                    var query = from m in context.si_mantenimineto
                        join u in context.si_unidades on m.id_unidad equals u.id_unidad
                        join e in context.si_empresas on u.id_empresa equals e.id_empresa
                        join c in context.si_comodatarios on u.id_comodatario equals c.id_comodatario
                        where m.id_estatus == 3 orderby u.un_codigo_barra
                                select new
                        {
                            contrato = u.un_codigo_barra,
                            fehcaMtt = m.mtto_fecha_original,
                            fecha = u.un_fecha_instalacion,
                            empresa = e.emp_nombre,
                            comodatario = c.co_nombre,
                            direccion = e.emp_direccion,
                            telefono = e.emp_telefono
                        };

                        foreach (var item in query)
                        {
                            objM dato = new objM();

                            dato.un_codigo_barra = item.contrato;
                            dato.mtto_fecha_original = item.fehcaMtt;
                            dato.un_fecha_instalacion = item.fecha;
                            dato.emp_nombre = item.empresa;
                            dato.co_nombre = item.comodatario;
                            dato.emp_telefono = item.telefono;
                            dato.emp_direccion = item.direccion;

                            list.Add(dato);

                        }
                }
                else if (tipo == 1) {
                    var query = from m in context.si_mantenimineto
                        join u in context.si_unidades on m.id_unidad equals u.id_unidad
                        join e in context.si_empresas on u.id_empresa equals e.id_empresa
                        join c in context.si_comodatarios on u.id_comodatario equals c.id_comodatario
                        where m.id_estatus == 3 && m.mtto_fecha_original >= oPrimerDiaDelMes && m.mtto_fecha_original <= oUltimoDiaDelMes
                                orderby u.un_codigo_barra
                                select new
                                {
                                    contrato = u.un_codigo_barra,
                                    fehcaMtt = m.mtto_fecha_original,
                                    fecha = u.un_fecha_instalacion,
                                    empresa = e.emp_nombre,
                                    comodatario = c.co_nombre,
                                    direccion = e.emp_direccion,
                                    telefono = e.emp_telefono
                                };

                    foreach (var item in query)
                    {
                        objM dato = new objM();

                        dato.un_codigo_barra = item.contrato;
                        dato.mtto_fecha_original = item.fehcaMtt;
                        dato.un_fecha_instalacion = item.fecha;
                        dato.emp_nombre = item.empresa;
                        dato.co_nombre = item.comodatario;
                        dato.emp_telefono = item.telefono;
                        dato.emp_direccion = item.direccion;

                        list.Add(dato);

                    }
                }
                else if (tipo == 2) {//mayores a 6 meses
                    var query = from m in context.si_mantenimineto
                        join u in context.si_unidades on m.id_unidad equals u.id_unidad
                        join e in context.si_empresas on u.id_empresa equals e.id_empresa
                        join c in context.si_comodatarios on u.id_comodatario equals c.id_comodatario
                        where m.id_estatus == 3 && m.mtto_fecha_original <= MesesMenosDias
                                orderby u.un_codigo_barra
                                select new
                                {
                                    contrato = u.un_codigo_barra,
                                    fehcaMtt = m.mtto_fecha_original,
                                    fecha = u.un_fecha_instalacion,
                                    empresa = e.emp_nombre,
                                    comodatario = c.co_nombre,
                                    direccion = e.emp_direccion,
                                    telefono = e.emp_telefono
                                };

                    foreach (var item in query)
                    {
                        objM dato = new objM();

                        dato.un_codigo_barra = item.contrato;
                        dato.mtto_fecha_original = item.fehcaMtt;
                        dato.un_fecha_instalacion = item.fecha;
                        dato.emp_nombre = item.empresa;
                        dato.co_nombre = item.comodatario;
                        dato.emp_telefono = item.telefono;
                        dato.emp_direccion = item.direccion;

                        list.Add(dato);

                    }
                }
                else if (tipo == 3) //menores a 6 meses 
                {
                    var query = from m in context.si_mantenimineto
                        join u in context.si_unidades on m.id_unidad equals u.id_unidad
                        join e in context.si_empresas on u.id_empresa equals e.id_empresa
                        join c in context.si_comodatarios on u.id_comodatario equals c.id_comodatario
                        where m.id_estatus == 3 && m.mtto_fecha_original >= MenosSeisMeses && m.mtto_fecha_original <= date
                                orderby u.un_codigo_barra descending
                                select new
                                {
                                    contrato = u.un_codigo_barra,
                                    fehcaMtt = m.mtto_fecha_original,
                                    fecha = u.un_fecha_instalacion,
                                    empresa = e.emp_nombre,
                                    comodatario = c.co_nombre,
                                    direccion = e.emp_direccion,
                                    telefono = e.emp_telefono
                                };

                    foreach (var item in query)
                    {
                        objM dato = new objM();

                        dato.un_codigo_barra = item.contrato;
                        dato.mtto_fecha_original = item.fehcaMtt;
                        dato.un_fecha_instalacion = item.fecha;
                        dato.emp_nombre = item.empresa;
                        dato.co_nombre = item.comodatario;
                        dato.emp_telefono = item.telefono;
                        dato.emp_direccion = item.direccion;

                        list.Add(dato);

                    }
                }

            var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente:";
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