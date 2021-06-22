using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using wa_sicove.core;
using System.Text.RegularExpressions;
using System.Globalization;

namespace wa_sicove.sicovegas
{
    /// <summary>
    /// Descripción breve de FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {

        public bool IsReusable
        {
            get { return true; }
        }



        public void ProcessRequest(HttpContext context)
        {

            try
            {

                int uu = 0;
                if (context.Request.Files.Count > 0)
                {
                    string patron = @"(?:- *)?\d+(?:\.\d+)?";
                    HttpFileCollection files = context.Request.Files;
                    string estacion = context.Request.Form["Estacion"];
                    string operador = context.Request.Form["operador"];



                    for (int i = 0; i < files.Count; i++)
                    {

                        sicoveDBDataContext db = new sicoveDBDataContext();

                        HttpPostedFile file = files[i];


                        string data = new StreamReader(file.InputStream).ReadToEnd();

                        int o = 0;
                        foreach (var row in data.Split('\n'))
                        {
                            if (o != 0 && row != "")
                            {
                                string cb = row.Split(',')[1];
                                var query = from u in db.si_unidades where u.un_codigo_barra.Equals(cb) select u;

                                foreach (var items in query)
                                {
                                    int id = items.id_unidad;

                                    si_ventas venta = new si_ventas();

                                    venta.id_unidad = id;
                                    venta.ve_observacion = "VENTA";
                                    string fecha = row.Split(',')[0];
                                    venta.ve_fecha_venta = DateTime.Parse(fecha);
                                    venta.ve_kilometros = uint.Parse(row.Split(',')[2]);
                                    venta.ve_servicio = int.Parse(row.Split(',')[3]);

                                    string lit = row.Split(',')[4];
                                    string precioGas = row.Split(',')[5];

                                    Regex regex = new Regex(patron);

                                    foreach (Match m in regex.Matches(precioGas))
                                    {
                                        venta.ve_precio_gas = Convert.ToDecimal(m.Value, CultureInfo.InvariantCulture);
                                    }

                                    foreach (Match m in regex.Matches(lit))
                                    {
                                        decimal r = Convert.ToDecimal(m.Value, CultureInfo.InvariantCulture);
                                        // r = Decimal.Round(r);
                                        venta.ve_litros = Convert.ToDecimal(r);
                                    }

                                    venta.ve_medidor = "0";
                                    venta.id_metodo = 1;
                                    venta.id_despachador = int.Parse(operador);
                                    venta.id_estacion = int.Parse(estacion);
                                    decimal? total = venta.ve_litros * venta.ve_precio_gas;
                                    venta.ve_total = total;

                                    db.si_ventas.InsertOnSubmit(venta);
                                    db.SubmitChanges();
                                    uu++;
                                }
                            }
                            o++;
                        }
                    }
                }
                //return 
                context.Response.ContentType = "text/plain";
                context.Response.Write("Se agregaron correctamente " + uu + " ventas.");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Se detecto el error " + ex + "  en la parte de la carga.");
            }
            
        }
    }
}