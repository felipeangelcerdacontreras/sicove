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
    public partial class ContServicios : System.Web.UI.Page
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

        public class objV
        {
            public int? nServicio { get; set; }
            public DateTime? fechaVenta { get; set; }
            public string estacion { get; set; }
            public string modulo { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse Servicios(DateTime FInicio, DateTime FFin, int idEstacion, int idModulo)
        {
            ajaxResponse Response = new ajaxResponse();
            string sentencia = "";

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();

                List<objV> list = new List<objV>();

                int diasDiff = (FFin - FInicio).Days;

                if (diasDiff < 1){ diasDiff = 1; }
                
                var query = context.SPServiciosXFecha(FInicio, FFin, idEstacion, idModulo, "TODOS",null);

                foreach (var item in query)
                {
                    objV dato = new objV();

                    dato.fechaVenta = item.fechaVenta;
                    dato.nServicio = item.nServicio;
                    dato.estacion = item.estacion;
                    dato.modulo = item.medidor;

                    list.Add(dato);
                }

                var jsonSerialiser = new JavaScriptSerializer();
                jsonSerialiser.MaxJsonLength = int.MaxValue;
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                //Response.Message = "Menor: " + menor + " Mayor: " + mayor;
                Response.Data = json;

            }
            catch (Exception ex)
            {
                Response.Result = false;
                //Response.Message = sentencia + " <br/> estacion: " + idEstacion + " <br/> modulo: " + idModulo + " <br/> Fecha I: " + FInicio + " <br/> Fecha F: " + FFin + " <br/> 0";
                Response.Message = "ERROR: " + ex.ToString();
                Response.Data = null;
            }
            return Response;

        } 

    }
}