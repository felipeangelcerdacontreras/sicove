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
    public partial class Cortesias : System.Web.UI.Page
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

        public class lista_unidades
        {
            public lista_unidades(int id_unidad, string codigo, string giro, string empresa, string fecha, int tipo_instalacion)
            {
                this.id_unidad = id_unidad;
                this.codigo = codigo;
                this.giro = giro;
                this.empresa = empresa;
                this.fecha = fecha;
                this.tipo_instalacion = tipo_instalacion;
            }

            public int id_unidad { get; set; }
            public string codigo { get; set; }
            public string giro { get; set; }
            public string empresa { get; set; }
            public string fecha { get; set; }
            public int tipo_instalacion { get; set; }
        }
        #endregion



        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_unidades_tipo(int id_tipo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<lista_unidades> lista = new List<lista_unidades>();


                var unidades = from s in context.si_unidades
                               where !context.si_cortesias.Any(es => (es.id_unidad == s.id_unidad) && (s.un_estatus.Equals(1) && (es.id_tipo_cortesia.Equals(id_tipo)))) && (s.un_estatus.Equals(1))
                               select s;


                foreach (var u in unidades)
                {
                    lista.Add(new lista_unidades(
                       u.id_unidad,
                       u.un_codigo_barra,
                       u.si_giros.gi_descripcion,
                       u.si_empresas.emp_nombre,
                       u.un_fecha_instalacion.ToString(),
                       0
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
        public static ajaxResponse cancelar_autom_c(DateTime fechaHoy)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();

                var query = from co in context.si_cortesias
                            where co.cor_fecha_cortesia_vencimiento < fechaHoy
                            select co;

                if (query.Count() >= 1)
                {

                    foreach (var item in query)
                    {
                        si_cortesias co = null;
                        co = context.si_cortesias.Where(x => x.id_cortesia.Equals(item.id_cortesia)).FirstOrDefault();
                        co.cor_estatus_cortesia = 3;
                        context.SubmitChanges();
                    }

                    var query1 = from co in context.si_cortesias
                                 where co.cor_fecha_cortesia_vencimiento < fechaHoy && co.cor_cantidad_cortesia > 0
                                 select co;

                    foreach (var item in query1)
                    {
                        si_cortesias co = null;
                        co = context.si_cortesias.Where(x => x.id_cortesia.Equals(item.id_cortesia)).FirstOrDefault();
                        co.cor_estatus_cortesia = 5;
                        context.SubmitChanges();
                    }

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
        public static ajaxResponse Crear_autom_c(DateTime fechaInicio, DateTime fechaFin, float Litros)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                context.CommandTimeout = 1000;

                decimal Litros2 = (decimal)Litros;

                var unidades = from ven in context.si_ventas
                               where ven.ve_fecha_venta >= fechaInicio && ven.ve_fecha_venta < fechaFin
                               group ven by ven.id_unidad into ventas
                               select new { unidad = ventas.Key };

                var cortesias = context.SPCORTESIASAUTOM(fechaInicio, fechaFin, Litros2);

                DateTime Fin_fechaFin = fechaFin.AddDays(1);

                int diasDiff = (Fin_fechaFin - fechaInicio).Days;

                foreach (var cortesia in cortesias)
                {

                    var cortesia_existe = from c in context.si_cortesias
                                          where c.id_unidad == cortesia.id_unidad && (c.cor_estatus_cortesia == 4 || c.cor_estatus_cortesia == 1)
                                          select c;

                    if (!cortesia_existe.Any())
                    {
                        si_cortesias co = new si_cortesias();
                        DateTime generada = DateTime.Now;
                        DateTime vencimiento = generada.AddDays(15);

                        var seed = Environment.TickCount;
                        var random = new Random(seed);
                        var nAle = random.Next(0, 100);

                        int dias = 1;

                        string nfolio = generada.Year + "" + generada.Month + 2 + "" + generada.Day + cortesia.estacion + nAle + "" + generada.Hour + generada.Second + generada.Millisecond + cortesia.id_unidad;

                        var un = from u in context.si_unidades
                                 where u.id_unidad == cortesia.id_unidad
                                 select u;

                        foreach (var item in un)
                        {

                            if (item.un_fecha_instalacion > fechaInicio)
                            {
                                DateTime fech_inst = (DateTime)item.un_fecha_instalacion;

                                int diasDiff2 = (fechaFin - fech_inst).Days;

                                if (diasDiff2 < 1)
                                    diasDiff2 = 1;


                                dias = diasDiff2;

                            }
                            else
                            {
                                dias = diasDiff;
                            }
                        }

                        decimal cantidad_cortesia = Math.Round((decimal)cortesia.volumen / dias);

                        co.id_tipo_cortesia = 2;
                        co.cor_fecha_cortesia_generada = generada;
                        co.cor_fecha_cortesia_vencimiento = vencimiento;
                        co.id_unidad = cortesia.id_unidad;
                        co.cor_cantidad_cortesia = (double)cantidad_cortesia;
                        co.cor_estatus_cortesia = 4;
                        co.cor_observacion = "Generada Automaticamente";
                        co.cor_codigo_cortesia = nfolio;
                        co.id_estacion = cortesia.estacion;
                        co.cor_autorizada = 0;
                        context.si_cortesias.InsertOnSubmit(co);
                        context.SubmitChanges();

                    }
                }

                Response.Result = true;
                Response.Message = "CORTESIAS GENERADAS CORRECTAMENTE";
                Response.Data = "CORTESIAS GENERADAS CORRECTAMENTE";

            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error" + ex.Message;
                Response.Data = null;
            }
            return Response;
        }
    }
}