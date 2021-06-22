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
    public partial class configuraciones : System.Web.UI.Page
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
            public int inicio { get; set; }
            public int fin { get; set; }
            public int litros { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_c()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> list = new List<objC>();
                var query = from c in context.si_configuracion
                            select c;
                foreach (var items in query)
                {
                    objC dato = new objC();
                    dato.inicio = (items.dia_corte_inicio != null) ? (int)items.dia_corte_inicio : -1;
                    dato.fin = (items.dia_corte_fin != null) ? (int)items.dia_corte_fin : -1;
                    dato.litros = (items.litros_cortesia != null) ? (int)items.litros_cortesia : -1;
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
        public static ajaxResponse actualizar(int inicio, int final, int litros)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_configuracion co = null;
                co = context.si_configuracion.Where(x => x.id_configuracion.Equals(1)).FirstOrDefault();
                co.dia_corte_inicio = (int)inicio;
                co.dia_corte_fin = (int)final;
                co.litros_cortesia = (int)litros;
                context.SubmitChanges();
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