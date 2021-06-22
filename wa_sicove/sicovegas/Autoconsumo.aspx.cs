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
    public partial class Autoconsumo : System.Web.UI.Page
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

        public class objU
        {
            public int id { get; set; }
            public string numero_economico { get; set; }
            public string numero_serie { get; set; }
            public string codigo_barra { get; set; }
            public DateTime fecha_instalacion { get; set; }
            public int id_persona { get; set; }
            public int anio { get; set; }
            public string placa { get; set; }
            public string observacion { get; set; }
            public int estatus { get; set; }
            public int? estatusInstalacion { get; set; }
            public int id_empresa { get; set; }
            public string empresa { get; set; }
            public int id_giro { get; set; }
            public string giro { get; set; }
            public int id_modelo { get; set; }

            public int id_marca { get; set; }
            public int id_cilindro { get; set; }
            public int tMantenimiento { get; set; }
            public int tTiempo { get; set; }

        }

        public class objK
        {
            public int id { get; set; }
            public string vaporizador { get; set; }
            public string riel { get; set; }
            public string centralita { get; set; }
            public string observacion { get; set; }
            public bool estatus { get; set; }
            public int id_marca { get; set; }
            public int id_modelo { get; set; }
            public int id_cilindro { get; set; }
            public int id_unidad { get; set; }
        }

        public class objT
        {
            public int id { get; set; }
            public string numero_serie { get; set; }
            public DateTime fecha_fabricacion { get; set; }
            public string multivalvula { get; set; }
            public bool estatus { get; set; }
            public int id_marca_tanque { get; set; }
            public int id_tipo_tanque { get; set; }
            public int id_capacidad { get; set; }
            public int id_unidad { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_u()
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> _list = new List<objU>();

                var query = from u in context.si_unidades
                            join gi in context.si_giros on u.id_giro equals gi.id_giro
                            where gi.gi_autoconsumo == 1
                            select u;

                foreach (var items in query)
                {
                    objU dato = new objU();

                    dato.id = items.id_unidad;
                    dato.numero_economico = (items.un_numero_economico != null) ? items.un_numero_economico : string.Empty;
                    dato.numero_serie = (items.un_numero_serie != null) ? items.un_numero_serie : string.Empty;
                    dato.codigo_barra = (items.un_codigo_barra != null) ? items.un_codigo_barra : string.Empty;
                    dato.anio = (items.un_anio != null) ? (int)items.un_anio : 0;
                    dato.placa = (items.un_placa != null) ? items.un_placa : string.Empty;
                    dato.observacion = (items.un_observacion != null) ? items.un_observacion : string.Empty;
                    dato.estatus = (items.un_estatus == true) ? 1 : 0;
                    dato.id_empresa = (items.id_empresa != null) ? (int)items.id_empresa : -1;
                    dato.empresa = (items.si_empresas != null) ? items.si_empresas.emp_nombre : string.Empty;
                    dato.id_cilindro = (items.id_cilindro != null) ? (int)items.id_cilindro : -1;
                    dato.id_giro = (items.id_giro != null) ? (int)items.id_giro : -1;
                    dato.giro = (items.si_giros != null) ? items.si_giros.gi_descripcion : string.Empty;
                    dato.id_modelo = (items.id_modelo != null) ? (int)items.id_modelo : -1;
                    dato.id_marca = (items.si_modelos.id_marca != null) ? (int)items.si_modelos.id_marca : -1;
                    _list.Add(dato);
                }

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(_list);
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
        public static ajaxResponse traer_unid(int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> _list = new List<objU>();

                var query = from u in context.si_unidades where u.id_unidad.Equals(id) select u;

                foreach (var items in query)
                {
                    objU dato = new objU();

                    dato.id = items.id_unidad;
                    dato.numero_economico = (items.un_numero_economico != null) ? items.un_numero_economico : string.Empty;
                    dato.numero_serie = (items.un_numero_serie != null) ? items.un_numero_serie : string.Empty;
                    dato.codigo_barra = (items.un_codigo_barra != null) ? items.un_codigo_barra : string.Empty;
                    dato.fecha_instalacion = (items.un_fecha_instalacion != null) ? (DateTime)items.un_fecha_instalacion : DateTime.Now;
                    dato.id_persona = (items.id_persona != null) ? (int)items.id_persona : -1;
                    dato.anio = (items.un_anio != null) ? (int)items.un_anio : 0;
                    dato.placa = (items.un_placa != null) ? items.un_placa : string.Empty;
                    dato.observacion = (items.un_observacion != null) ? items.un_observacion : string.Empty;
                    dato.estatus = (items.un_estatus == true) ? 1 : 0;
                    dato.estatusInstalacion = items.estatus_instalacion;
                    dato.id_empresa = (items.id_empresa != null) ? (int)items.id_empresa : -1;
                    dato.empresa = (items.si_empresas != null) ? items.si_empresas.emp_nombre : string.Empty;
                    dato.id_cilindro = (items.id_cilindro != null) ? (int)items.id_cilindro : -1;
                    dato.id_giro = (items.id_giro != null) ? (int)items.id_giro : -1;
                    dato.giro = (items.si_giros != null) ? items.si_giros.gi_descripcion : string.Empty;
                    dato.id_modelo = (items.id_modelo != null) ? (int)items.id_modelo : -1;
                    dato.id_marca = (items.si_modelos.id_marca != null) ? (int)items.si_modelos.id_marca : -1;
                    dato.tMantenimiento = (items.tiempo_mtto != null) ? (int)items.tiempo_mtto : 0;
                    dato.tTiempo = (items.tipo_tiempo != null) ? (int)items.tipo_tiempo : 0;
                    _list.Add(dato);
                }

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(_list);
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
        public static ajaxResponse traer_k(int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();

                var query = from k in context.si_kits
                            where k.id_unidad == id
                            select new objK
                            {
                                id = k.id_kit,
                                vaporizador = k.ki_vaporizador,
                                riel = k.ki_riel,
                                centralita = k.ki_centralita,
                                observacion = k.ki_observacion,
                                id_marca = k.si_modelos_kit.id_marca_kit,
                                id_cilindro = (int)k.id_cilindro,
                                id_modelo = (int)k.id_modelo_kit,
                            };

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(query);
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
        public static ajaxResponse traer_t(int id)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objT> list = new List<objT>();
                objT data;

                var query = from t in context.si_tanques
                            where t.id_unidad == id
                            select t;

                foreach (var items in query)
                {
                    data = new objT();

                    data.id = items.id_tanque;
                    data.numero_serie = items.tq_numero_serie;
                    data.fecha_fabricacion = (DateTime)items.tq_fecha_fabricacion;
                    data.multivalvula = items.tq_multivalvulas;
                    data.id_marca_tanque = (int)items.id_marca_tanque;
                    data.id_tipo_tanque = (int)items.id_tipo_tanque;
                    data.id_capacidad = (int)items.id_capasidad;

                    list.Add(data);
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

        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_u(string numero_economico, string numero_serie, DateTime fecha_instalacion, string codigo_barra,
            int anio, string placa, string observacion, int estatus, int estatusInstalacion, int id_empresa, int id_giro,
            int id_marca, int id_modelo, int id_cilindro, string vaporizador, string riel, string centralita, string observacionK,
            int id_modeloK, int id_cilindroKit, string[] numSerieT, DateTime[] fechaFT, string[] multvT, int[] marcaT, int[] tipoT, int[] capacidadT, int id,
            int kilometraje, int id_persona, int tMantenimiento, int tTiempo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                var query = from u in context.si_unidades
                            where u.un_numero_economico == numero_economico
                            select u;
                if (!query.Any())
                {
                    si_unidades uds = new si_unidades();

                    uds.un_numero_economico = !string.IsNullOrEmpty(numero_economico) ? numero_economico : "";
                    uds.un_numero_serie = !string.IsNullOrEmpty(numero_serie) ? numero_serie : "";
                    uds.un_fecha_instalacion = fecha_instalacion;
                    uds.un_codigo_barra = !string.IsNullOrEmpty(codigo_barra) ? codigo_barra : "";
                    uds.un_anio = (anio > 0) ? anio : 0;
                    uds.un_placa = !string.IsNullOrEmpty(placa) ? placa : "";
                    uds.tiempo_mtto = (tMantenimiento < 1) ? 6 : tMantenimiento;
                    uds.tipo_tiempo = (tTiempo < 0) ? 2 : tTiempo;
                    uds.un_observacion = !string.IsNullOrEmpty(observacion) ? observacion : "";
                    uds.estatus_instalacion = estatusInstalacion;
                    uds.un_estatus = false;
                    if (estatus == 1)
                        uds.un_estatus = true;
                    if (id_empresa > 0)
                        uds.id_empresa = id_empresa;
                    if (id_giro > 0)
                        uds.id_giro = id_giro;
                    if (id_marca > 0)
                        uds.id_marca = id_marca;
                    if (id_modelo > 0)
                        uds.id_modelo = id_modelo;
                    if (id_cilindro > 0)
                        uds.id_cilindro = id_cilindro;
                    if (id_persona > 0)
                        uds.id_persona = id_persona;

                    context.si_unidades.InsertOnSubmit(uds);
                    context.SubmitChanges();

                    si_kits kds = new si_kits();

                    kds.ki_vaporizador = !string.IsNullOrEmpty(vaporizador) ? vaporizador : "";
                    kds.ki_riel = !string.IsNullOrEmpty(riel) ? riel : "";
                    kds.ki_centralita = !string.IsNullOrEmpty(centralita) ? centralita : "";
                    kds.ki_observacion = !string.IsNullOrEmpty(observacionK) ? centralita : "";
                    kds.ki_estatus = false;
                    if (estatus == 1)
                        kds.ki_estatus = true;
                    if (id_modelo > 0)
                        kds.id_modelo_kit = id_modeloK;
                    if (id_cilindroKit > 0)
                        kds.id_cilindro = id_cilindroKit;
                    kds.id_unidad = uds.id_unidad;

                    context.si_kits.InsertOnSubmit(kds);
                    context.SubmitChanges();

                    for (int i = 0; i < marcaT.Length; i++)
                    {
                        si_tanques tds = new si_tanques();

                        tds.tq_numero_serie = !string.IsNullOrEmpty(numSerieT[i]) ? numSerieT[i] : "";
                        tds.tq_fecha_fabricacion = fechaFT[i];
                        tds.tq_multivalvulas = !string.IsNullOrEmpty(multvT[i]) ? multvT[i] : "";
                        tds.tq_estatus = kds.ki_estatus;
                        if (marcaT[i] > 0)
                            tds.id_marca_tanque = marcaT[i];
                        if (tipoT[i] > 0)
                            tds.id_tipo_tanque = tipoT[i];
                        if (capacidadT[i] > 0)
                            tds.id_capasidad = capacidadT[i];
                        tds.id_unidad = uds.id_unidad;

                        context.si_tanques.InsertOnSubmit(tds);
                        context.SubmitChanges();
                    }


                    si_mantenimineto mtto = new si_mantenimineto();
                    var last_unidad = (from abc in context.si_unidades orderby abc.id_unidad descending select abc).First();
                    var unidad_last_id = (int)last_unidad.id_unidad;
                    DateTime localDate = DateTime.Now;
                    DateTime fechaProgramada = tTiempo == 1 ? localDate.AddDays(tMantenimiento * 7) : localDate.AddMonths(tMantenimiento);
                    mtto.mtto_fecha_original = fechaProgramada;
                    mtto.mtto_fecha_reprogramada = fechaProgramada;
                    mtto.mtto_estatus = Convert.ToString(1);
                    if (kilometraje >= 0)
                        mtto.mtto_kilometraje = kilometraje;
                    if (id_persona > 0)
                        mtto.id_persona = id_persona;
                    if (unidad_last_id > 0)
                        mtto.id_unidad = unidad_last_id;
                    mtto.id_estatus = 3;
                    mtto.mtto_tipo = 1;
                    context.si_mantenimineto.InsertOnSubmit(mtto);
                    context.SubmitChanges();

                    // Alimentamos Bitacora
                    si_bitacora b = new si_bitacora();
                    b.bit_fecha = DateTime.Now.Date;
                    b.bit_tiempo = DateTime.Now.TimeOfDay;
                    b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                    b.bit_modulo = "Unidades.aspx";
                    b.bit_accion = "Guardar Unidades";
                    b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Usuario Agrego Nueva Unidades = " + numero_economico;
                    ClassBicatora.insertBitacora(b);
                    Response.Result = true;
                    Response.Message = "Agregado Correctamente";
                    Response.Data = null;
                }
                else
                {
                    Response.Result = true;
                    Response.Message = "Ya Existe una unidad con este mismo numero economico. ";
                    Response.Data = null;
                }

                
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error al tratar de guardar la unidad. " + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_u(string numero_economico, string numero_serie, DateTime fecha_instalacion, string codigo_barra,
            int anio, string placa, string observacion, int estatus, int estatusInstalacion, int id_empresa, int id_giro,
            int id_marca, int id_modelo, int id_cilindro, string vaporizador, string riel, string centralita, string observacionK,
            int id_modeloK, int id_cilindroKit, string[] numSerieT, DateTime[] fechaFT, string[] multvT, int[] marcaT, int[] tipoT, int[] capacidadT, int id,
            int kilometraje, int id_persona, int tMantenimiento, int tTiempo)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();

                si_unidades uds = null;

                cambiosUnidades cU = new cambiosUnidades();
                cU.id_unidad = id;
                context.cambiosUnidades.InsertOnSubmit(cU);
                context.SubmitChanges();

                uds = context.si_unidades.Where(x => x.id_unidad.Equals(id)).FirstOrDefault();

                uds.un_numero_economico = string.IsNullOrEmpty(numero_economico) ? "" : numero_economico;
                uds.un_numero_serie = !string.IsNullOrEmpty(numero_serie) ? numero_serie : "";
                uds.un_fecha_instalacion = fecha_instalacion;
                uds.un_codigo_barra = !string.IsNullOrEmpty(codigo_barra) ? codigo_barra : "";
                uds.un_anio = (anio > 0) ? anio : 0;
                uds.un_placa = !string.IsNullOrEmpty(placa) ? placa : "";
                uds.tiempo_mtto = (tMantenimiento < 1) ? 6 : tMantenimiento;
                uds.tipo_tiempo = (tTiempo < 0) ? 2 : tTiempo;
                uds.un_observacion = !string.IsNullOrEmpty(observacion) ? observacion : "";
                uds.estatus_instalacion = estatusInstalacion;
                uds.un_estatus = false;
                if (estatus == 1)
                    uds.un_estatus = true;
                if (id_empresa > 0)
                    uds.id_empresa = id_empresa;
                if (id_giro > 0)
                    uds.id_giro = id_giro;
                if (id_marca > 0)
                    uds.id_marca = id_marca;
                if (id_modelo > 0)
                    uds.id_modelo = id_modelo;
                if (id_cilindro > 0)
                    uds.id_cilindro = id_cilindro;
                if (id_persona > 0)
                    uds.id_persona = id_persona;

                context.SubmitChanges();

                si_kits kds = null;
                kds = context.si_kits.Where(x => x.id_unidad.Equals(id)).FirstOrDefault();

                if (kds == null)
                {
                    kds = new si_kits();

                    kds.ki_vaporizador = !string.IsNullOrEmpty(vaporizador) ? vaporizador : "";
                    kds.ki_riel = !string.IsNullOrEmpty(riel) ? riel : "";
                    kds.ki_centralita = !string.IsNullOrEmpty(centralita) ? centralita : "";
                    kds.ki_observacion = !string.IsNullOrEmpty(observacionK) ? centralita : "";
                    kds.ki_estatus = false;
                    if (estatus == 1)
                        kds.ki_estatus = true;
                    if (id_modelo > 0)
                        kds.id_modelo_kit = id_modeloK;
                    if (id_cilindroKit > 0)
                        kds.id_cilindro = id_cilindroKit;
                    kds.id_unidad = uds.id_unidad;

                    context.si_kits.InsertOnSubmit(kds);
                    context.SubmitChanges();
                }
                else
                {
                    kds.ki_vaporizador = !string.IsNullOrEmpty(vaporizador) ? vaporizador : "";
                    kds.ki_riel = !string.IsNullOrEmpty(riel) ? riel : "";
                    kds.ki_centralita = !string.IsNullOrEmpty(centralita) ? centralita : "";
                    kds.ki_observacion = !string.IsNullOrEmpty(observacionK) ? centralita : "";
                    kds.ki_estatus = false;
                    if (estatus == 1)
                        kds.ki_estatus = true;
                    if (id_modelo > 0)
                        kds.id_modelo_kit = id_modeloK;
                    if (id_cilindroKit > 0)
                        kds.id_cilindro = id_cilindroKit;

                    context.SubmitChanges();
                }

                List<si_tanques> ltds = null;

                ltds = context.si_tanques.Where(x => x.id_unidad.Equals(id)).ToList();

                if (ltds == null)
                {
                    for (int i = 0; i < numSerieT.Length; i++)
                    {
                        si_tanques tds = new si_tanques();

                        tds.tq_numero_serie = !string.IsNullOrEmpty(numSerieT[i]) ? numSerieT[i] : "";
                        tds.tq_fecha_fabricacion = fechaFT[i];
                        tds.tq_multivalvulas = !string.IsNullOrEmpty(multvT[i]) ? multvT[i] : "";
                        tds.tq_estatus = kds.ki_estatus;
                        if (marcaT[i] > 0)
                            tds.id_marca_tanque = marcaT[i];
                        if (tipoT[i] > 0)
                            tds.id_tipo_tanque = tipoT[i];
                        if (capacidadT[i] > 0)
                            tds.id_capasidad = capacidadT[i];
                        tds.id_unidad = uds.id_unidad;

                        context.si_tanques.InsertOnSubmit(tds);
                        context.SubmitChanges();
                    }
                }
                else
                {
                    context.si_tanques.DeleteAllOnSubmit(ltds);
                    context.SubmitChanges();
                    for (int i = 0; i < numSerieT.Length; i++)
                    {
                        si_tanques tds = new si_tanques();

                        tds.tq_numero_serie = !string.IsNullOrEmpty(numSerieT[i]) ? numSerieT[i] : "";
                        tds.tq_fecha_fabricacion = fechaFT[i];
                        tds.tq_multivalvulas = !string.IsNullOrEmpty(multvT[i]) ? multvT[i] : "";
                        tds.tq_estatus = kds.ki_estatus;
                        if (marcaT[i] > 0)
                            tds.id_marca_tanque = marcaT[i];
                        if (tipoT[i] > 0)
                            tds.id_tipo_tanque = tipoT[i];
                        if (capacidadT[i] > 0)
                            tds.id_capasidad = capacidadT[i];
                        tds.id_unidad = uds.id_unidad;

                        context.si_tanques.InsertOnSubmit(tds);
                        context.SubmitChanges();
                    }
                }


                Response.Result = true;
                Response.Message = "Se actualizo correctamente ";
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
    }
}