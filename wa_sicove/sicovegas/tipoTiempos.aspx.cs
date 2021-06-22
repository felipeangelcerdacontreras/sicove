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
    public partial class tipoTiempos : System.Web.UI.Page
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

        public class objEst
        {
            public objEst(int id, string descripcion)
            {
                this.id = id;
                this.descripcion = descripcion;
            }

            public int id { get; set; }
            public string descripcion { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse cTiempos()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objEst> lista = new List<objEst>();
                var consulta = from tT in context.si_tipo_tiempo select tT;
                foreach (var item in consulta)
                {
                    lista.Add(new objEst(
                        item.id_tipoTiempo,
                        item.descripcion
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