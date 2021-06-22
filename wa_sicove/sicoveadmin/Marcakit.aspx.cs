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
    public partial class Marcakit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Entidades
        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }

        public class objM
        {
            public objM(int id, string descripcion, int estatus)
            {
                this.id = id;
                this.descripcion = descripcion;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string descripcion { get; set; }
            public int estatus { get; set; }
        }

        public class objMo
        {
            public objMo(int id, string descripcion, string marca, int id_marca, int estatus)
            {
                this.id = id;
                this.descripcion = descripcion;
                this.marca = marca;
                this.id_marca = id_marca;
                this.estatus = estatus;
            }

            public int id { get; set; }
            public string descripcion { get; set; }
            public string marca { get; set; }
            public int id_marca { get; set; }
            public int estatus { get; set; }
        }

        #endregion

        #region WebMethodMarca

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_marcas()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> lista = new List<objM>();
                var consulta = from ro in context.si_marcas_kit select ro;
                foreach (var item in consulta)
                {
                    int estatus = 0;

                    if (item.mak_estatus)
                        estatus = 1;

                    lista.Add(new objM(
                        item.id_marca_kit,
                        item.mak_descripcion,
                       estatus
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
        public static ajaxResponse guardar_marcas(string desc, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_marcas_kit roll = new si_marcas_kit();

                roll.mak_descripcion = desc;
                roll.mak_estatus = false;
                if (activo == 1)
                    roll.mak_estatus = true;

                context.si_marcas_kit.InsertOnSubmit(roll);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "MarcasKit.aspx";
                b.bit_accion = "Guardar MarcaKit";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Marca = " + desc;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;

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
        public static ajaxResponse actualizar_marcas(string desc, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_marcas_kit roll = null;
                roll = context.si_marcas_kit.Where(x => x.id_marca_kit.Equals(id)).FirstOrDefault();
                roll.mak_descripcion = desc;
                roll.mak_estatus = false;
                if (activo == 1)
                    roll.mak_estatus = true;
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "MarcaKit.aspx";
                b.bit_accion = "Actualiza MarcaKit";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Marca, Id = " + id;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizado Correctamente";
                Response.Data = null;

            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

        #endregion

        #region WebMethodModelo

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_modelos(int id_marca)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objMo> lista = new List<objMo>();

                if (id_marca == 0)
                {
                    var consulta = from ro in context.si_modelos_kit orderby ro.id_marca_kit select ro;

                    foreach (var item in consulta)
                    {
                        int estatus = 0;

                        if (item.mok_estatus)
                            estatus = 1;

                        lista.Add(new objMo(
                            item.id_modelo_kit,
                            item.mok_descripcion,
                            item.si_marcas_kit.mak_descripcion,
                            (int)item.id_marca_kit,
                            estatus
                        ));
                    }
                }
                else
                {
                    var consulta = from ro in context.si_modelos_kit where ro.id_marca_kit.Equals(id_marca) orderby ro.id_marca_kit select ro;

                    foreach (var item in consulta)
                    {
                        int estatus = 0;

                        if (item.mok_estatus)
                            estatus = 1;

                        lista.Add(new objMo(
                            item.id_modelo_kit,
                            item.mok_descripcion,
                            item.si_marcas_kit.mak_descripcion,
                            (int)item.id_marca_kit,
                            estatus
                        ));
                    }
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
        public static ajaxResponse guardar_modelo(string desc, int marca, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modelos_kit roll = new si_modelos_kit();

                roll.mok_descripcion = desc;
                roll.id_marca_kit = marca;
                roll.mok_estatus = false;
                if (activo == 1)
                    roll.mok_estatus = true;

                context.si_modelos_kit.InsertOnSubmit(roll);
                context.SubmitChanges();
                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "MarcaKit.aspx";
                b.bit_accion = "Guardar ModeloKit";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nuevo Modelo = " + desc;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Agregado Correctamente";
                Response.Data = null;

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
        public static ajaxResponse actualizar_modelo(string desc, int marca, int activo, int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_modelos_kit roll = null;

                roll = context.si_modelos_kit.Where(x => x.id_modelo_kit.Equals(id)).FirstOrDefault();

                roll.mok_descripcion = desc;
                roll.id_marca_kit = marca;
                roll.mok_estatus = false;
                if (activo == 1)
                    roll.mok_estatus = true;

                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "MarcaKit.aspx";
                b.bit_accion = "Actualiza ModeloKit";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Actualizo Modelo, Id = " + id;
                ClassBicatora.insertBitacora(b);
                Response.Result = true;
                Response.Message = "Actualizado Correctamente";
                Response.Data = null;

            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

        #endregion
    }
}