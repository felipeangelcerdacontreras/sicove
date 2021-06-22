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
    public partial class cortesiasAutom : System.Web.UI.Page
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

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizarZona(int id_cortesia, int idEstacion)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_cortesias co = null;
                co = context.si_cortesias.Where(x => x.id_cortesia.Equals(id_cortesia)).FirstOrDefault();
                co.id_estacion = idEstacion;
                context.SubmitChanges();
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


        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizarActivo()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                //si_cortesias co = null;
                //co = context.si_cortesias.Where(x => x.cor_estatus_cortesia.Equals(4)).SingleOrDefault();
                var cos = from cor in context.si_cortesias
                         where cor.cor_estatus_cortesia == 4
                         select cor;
                foreach (var item in cos)
                {
                    item.cor_estatus_cortesia = 1;
                    context.SubmitChanges();
                }
                Response.Result = true;
                Response.Message = "Actualizado Correctamente ";
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


        [WebMethod(EnableSession = true)]
        public static ajaxResponse cambiarLibre(int id, bool s)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_cortesias co = null;
                co = context.si_cortesias.Where(x => x.id_cortesia.Equals(id)).FirstOrDefault();
                // var cos = (from cor in context.si_cortesias
                    //      where cor.id_cortesia == id
                      ///   select cor).FirstOrDefault();

                co.cor_libre = s;
                context.SubmitChanges();
                
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