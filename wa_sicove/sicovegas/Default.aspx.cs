﻿using System;
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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Enumerables
        public class ajaxResponse
        {
            public bool Result { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }

        public class menuClass
        {
            public int idmodulo { get; set; }
            public string titulo { get; set; }
            public string url { get; set; }
            public int idpadre { get; set; }
            public int? nivel { get; set; }
            public menuClass(int idmodulo, string titulo, string url, int idpadre, int? nivel)
            {
                this.idmodulo = idmodulo;
                this.titulo = titulo;
                this.url = url;
                this.idpadre = idpadre;
                this.nivel = nivel;
            }

        }
        #endregion

        [WebMethod(EnableSession = true)]
        public static ajaxResponse CerrarSesion(int idusuario)
        {
               
            ajaxResponse Response = new ajaxResponse();



                HttpContext.Current.Session["sesionUsuario"] = null;

                Response.Result = true;
                Response.Message = "Sesion Cerrada";
                Response.Data = null;
                return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse CargaMenuPadre()
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
               
                    sicoveDBDataContext context = new sicoveDBDataContext();
                    var objModulos = (from modulos in context.si_modulos
                                      join permisos in context.si_permisos on modulos.id_modulo equals permisos.id_modulo
                                      where permisos.id_rol == ((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).id_rol
                                      && (modulos.id_padre == 0 || modulos.id_padre < 1 || modulos.id_padre == null) &&
                                      modulos.mod_estatus == 1
                                      select modulos).ToList();

                    List<menuClass> menu = new List<menuClass>();
                    foreach (var grupo in objModulos)
                    {
                        menu.Add(new menuClass(grupo.id_modulo, grupo.mod_nombre, grupo.mod_archivo, 0, grupo.mod_nivel));
                    }
                    var jsonSerialiser = new JavaScriptSerializer();
                    var jsonModulos = jsonSerialiser.Serialize(menu);

                    if (jsonModulos != null)
                    {
                        Response.Result = true;
                        Response.Message = "";
                        Response.Data = jsonModulos;
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Message = "El Rol de usuario no cuenta con modulos visibles, verifique por favor.";
                        Response.Data = null;
                    }
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al cargar. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse CargaMenuHijo(int idmodulo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
               
                    sicoveDBDataContext context = new sicoveDBDataContext();
                    var objModulos = (from modulos in context.si_modulos
                                      join permisos in context.si_permisos on modulos.id_modulo equals permisos.id_modulo
                                      where permisos.id_rol == ((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).id_rol
                                      && modulos.id_padre == idmodulo && modulos.mod_estatus == 1
                                      orderby modulos.mod_orden ascending
                                      select modulos).ToList();

                    List<menuClass> menu = new List<menuClass>();
                    foreach (var grupo in objModulos)
                    {
                        menu.Add(new menuClass(grupo.id_modulo, grupo.mod_nombre, grupo.mod_archivo, (int)grupo.id_padre, grupo.mod_nivel));
                    }
                    var jsonSerialiser = new JavaScriptSerializer();
                    var jsonModulos = jsonSerialiser.Serialize(menu);

                    if (jsonModulos != null)
                    {
                        Response.Result = true;
                        Response.Message = "";
                        Response.Data = jsonModulos;
                    }
                    else
                    {
                        Response.Result = false;
                        Response.Message = "El Rol de usuario no cuenta con modulos visibles, verifique por favor.";
                        Response.Data = null;
                    }
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al cargar. " + ex.Message;
                Response.Data = null;
            }

            return Response;
        }

    }
}