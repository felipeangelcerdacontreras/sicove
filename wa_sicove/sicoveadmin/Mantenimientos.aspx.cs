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
    public partial class Mantenimientos : System.Web.UI.Page
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
            ///// mtto
            public int id { get; set; }
            public DateTime fecha_oiginal { get; set; }
            public DateTime fecha_reprogramada { get; set; }
            public DateTime? fecha_ultima { get; set; }
            public float kilometraje { get; set; }
            public int id_estatus { get; set; }
            public string estatus { get; set; }
            public int mtto_tipo { get; set; }
            public int id_tecnico { get; set; }
            public string observaciones { get; set; }
            public string refacciones { get; set; }
            ///// unidad
            public int id_unidad { get; set; }
            public string num_economico { get; set; }
            public string num_contrato { get; set; }
            public string marcaV { get; set; }
            public string modeloV { get; set; }
            public int anioV { get; set; }
            ///// kit
            public string marcaK { get; set; }
            public string modeloK { get; set; }
            public string riel { get; set; }
            public string centralita { get; set; }
            ///// empresa
            public string empresa { get; set; }
            public string giro { get; set; }
            public string Comodatario { get; set; }
            public string Base { get; set; }
        }

        public class objC
        {
            public int id { get; set; }
            public string usuario { get; set; }
            public DateTime? fecha_comentario { get; set; }
            public string comentario { get; set; }
            public int mantenimiento { get; set; }
        }

        public class uM
        {
            public string unidad { get; set; }
            public DateTime? fecha_prox { get; set; }
        }

        /* INICIO empresa**
         *traer los mantenimientos por empresa
         */
        [WebMethod(EnableSession = true)]
        public static ajaxResponse mtto_Em(string id_empresa, string mtto_Est, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                if (mtto_Est == "1")
                {
                    var query =
                    from mtto in context.si_mantenimineto
                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                    from como in comos.DefaultIfEmpty()
                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                    from bas in bass.DefaultIfEmpty()
                    where em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                          u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                }
                else
                { 
                    if(tipoMtto > 0 && tipoMtto < 4)
                {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                    else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                  
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
        public static ajaxResponse mtto_EmF(string id_empresa, string mtto_Est, DateTime inicioF, DateTime finalF,int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();

                    if (mtto_Est == "1") {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }

                    } else {
                        if (tipoMtto > 0 && tipoMtto < 4)
                        {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                            em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                            u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                            em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                            u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
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
        public static ajaxResponse mtto_EmGi(string id_empresa, int id_giro, string mtto_Est, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                    if (mtto_Est == "1")
                    {  
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est && gi.id_giro == id_giro
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                    else
                    {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && gi.id_giro == id_giro
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                    else {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && gi.id_giro == id_giro
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                        
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
        public static ajaxResponse mtto_EmGiF(string id_empresa, int id_giro, string mtto_Est, DateTime inicioF, DateTime finalF, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();

                if (mtto_Est == "1") {

                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                      em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est && gi.id_giro == id_giro
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }
                } else {
                    if (tipoMtto > 0 && tipoMtto < 4 ) {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && gi.id_giro == id_giro
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          em.emp_nombre == id_empresa && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && gi.id_giro == id_giro
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                    
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
        /* FINAL empresa**
         *traer los mantenimientos por empresa
         */



        /* INICIO Numero Economico**
         *traer los mantenimientos por Numero Economico
         */
        [WebMethod(EnableSession = true)]
        public static ajaxResponse mtto_NumEco(string numeroEconomico, string mtto_Est, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();

                if (mtto_Est == "1")
                {
                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }
                }
                else
                {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where mtto.mtto_tipo == tipoMtto && u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
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
        public static ajaxResponse mtto_NumEcoF(string numeroEconomico, string mtto_Est, DateTime inicioF, DateTime finalF, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
               
                    if (mtto_Est == "1") {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }

                    } else {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.un_numero_economico.StartsWith(numeroEconomico) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
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
        /* INICIO Numero Economico**
         *traer los mantenimientos por Numero Economico
         */



        /* INICIO Numero Contrato**
    *traer los mantenimientos por Numero Contrato
    */
        [WebMethod(EnableSession = true)]
        public static ajaxResponse mtto_NumCon(string numeroContrato, string mtto_Est, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                if (mtto_Est == "1")
                {
                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }
                }
                else
                {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
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
        public static ajaxResponse mtto_NumConF(string numeroContrato, string mtto_Est, DateTime inicioF, DateTime finalF, int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                
                if (mtto_Est == "1") {
                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                      u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }
                } else {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.un_codigo_barra.StartsWith(numeroContrato) && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
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
        /* INICIO Numero Contrato**
         *traer los mantenimientos por Numero Contrato
         */



        /* INICIO Comodatario**
*traer los mantenimientos Comodatario
*/
        [WebMethod(EnableSession = true)]
        public static ajaxResponse mtto_Comoda(int Comodatario, string mtto_Est,int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                if (mtto_Est == "1") {
                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }

                } else
                {
                    if (tipoMtto > 0 && tipoMtto < 4)
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                    else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }

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
        public static ajaxResponse mtto_ComodaF(int Comodatario, string mtto_Est, DateTime inicioF, DateTime finalF,int tipoMtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();

                if (mtto_Est == "1")
                {
                    var query = from mtto in context.si_mantenimineto
                                join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                join gi in context.si_giros on u.id_giro equals gi.id_giro
                                join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                from como in comos.DefaultIfEmpty()
                                join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                from bas in bass.DefaultIfEmpty()
                                where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                      u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                      u.estatus_instalacion != 4 && mtto.mtto_estatus == mtto_Est
                                select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                    foreach (var items in query)
                    {
                        objM dato = new objM();
                        ///// mtto
                        dato.id = items.mtto.id_mantenimiento;
                        dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                        dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                        dato.fecha_ultima = (items.mtto.mtto_fecha_ultimo != null) ? (DateTime)items.mtto.mtto_fecha_ultimo : DateTime.Now;
                        dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                        dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                        dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                        dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                        dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                        dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                        dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                        ///// unidad
                        dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                        dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                        dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                        dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                        dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                        dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                        ///// kit
                        dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                        dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                        dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                        dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                        ///// empresa
                        dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                        dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                        dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                        dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                        list.Add(dato);
                    }
                }
                else
                {
                    if (tipoMtto > 0 && tipoMtto < 4) {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_tipo == tipoMtto && mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = (items.mtto.mtto_fecha_ultimo != null) ? (DateTime)items.mtto.mtto_fecha_ultimo : DateTime.Now;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    } else
                    {
                        var query = from mtto in context.si_mantenimineto
                                    join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                                    join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                                    join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                                    join ma in context.si_marcas on u.id_marca equals ma.id_marca
                                    join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                                    join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                                    join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                                    join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                                    join em in context.si_empresas on u.id_empresa equals em.id_empresa
                                    join gi in context.si_giros on u.id_giro equals gi.id_giro
                                    join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                                    from como in comos.DefaultIfEmpty()
                                    join bas in context.si_bases on u.id_base equals bas.id_base into bass
                                    from bas in bass.DefaultIfEmpty()
                                    where mtto.mtto_fecha_reprogramada >= inicioF && mtto.mtto_fecha_reprogramada < finalF &&
                                          u.id_comodatario == Comodatario && u.un_estatus == true && u.estatus_instalacion != 2 &&
                                          u.estatus_instalacion != 4
                                    select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };

                        foreach (var items in query)
                        {
                            objM dato = new objM();
                            ///// mtto
                            dato.id = items.mtto.id_mantenimiento;
                            dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                            dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                            dato.fecha_ultima = (items.mtto.mtto_fecha_ultimo != null) ? (DateTime)items.mtto.mtto_fecha_ultimo : DateTime.Now;
                            dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                            dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                            dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                            dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                            dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                            dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                            dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                            ///// unidad
                            dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                            dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                            dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                            dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                            dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                            dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                            ///// kit
                            dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                            dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                            dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                            dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                            ///// empresa
                            dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                            dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                            dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                            dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
                            list.Add(dato);
                        }
                    }
                   
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
        /* INICIO Comodatario**
         *traer los mantenimientos Comodatario
         */




        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_m_Repro(int id_mtto, int kilometraje, int tipoMtto, DateTime fechaInstalacion, int tecnico, string Observaciones)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_mantenimineto ma = null;
                DateTime localDate = DateTime.Now;

                ma = context.si_mantenimineto.Where(x => x.id_mantenimiento.Equals(id_mtto)).FirstOrDefault();
                ma.mtto_bitacora = localDate.Date;
                ma.mtto_fecha_reprogramada = fechaInstalacion;
                if (kilometraje >= 0)
                    ma.mtto_kilometraje = kilometraje;
                ma.observacion = !string.IsNullOrEmpty(Observaciones) ? Observaciones : "";
                if (tipoMtto > 0)
                    ma.mtto_tipo = tipoMtto;
                if (tecnico > 0)
                    ma.id_persona = tecnico;
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Mantenimientos.aspx";
                b.bit_accion = "Actualizar Mantenimiento";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Se actualizo el mtto = " + id_mtto;
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

        [WebMethod(EnableSession = true)]
        public static ajaxResponse actualizar_m_Reali(DateTime fecha_originall, int kilometraje, int id_persona, int unidad, int id_estatus, int mtto_tipo, DateTime fechaRealizacion, int id_mtto, string Observaciones, string refacciones)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_mantenimineto ma = null;
                ma = context.si_mantenimineto.Where(x => x.id_mantenimiento.Equals(id_mtto)).FirstOrDefault();
                ma.mtto_estatus = Convert.ToString(2);
                ma.mtto_tipo = mtto_tipo;
                ma.id_estatus = id_estatus;
                ma.observacion = Observaciones;
                ma.mtto_refacciones = refacciones;
                ma.mtto_fecha_ultimo = fechaRealizacion;
                context.SubmitChanges();

                si_unidades uni = null;
                uni = context.si_unidades.Where(x => x.id_unidad.Equals(unidad)).FirstOrDefault();


                si_mantenimineto mtto = new si_mantenimineto();
                DateTime localDate = DateTime.Now;
                // DateTime fechaProgramada = localDate.AddMonths(6);
                DateTime fechaProgramada;
                if (mtto_tipo == 1)
                {
                    fechaProgramada = uni.tipo_tiempo == 1 ? fechaRealizacion.AddDays((double)uni.tiempo_mtto * 7) : fechaRealizacion.AddMonths((int)uni.tiempo_mtto);
                }
                else
                {
                    fechaProgramada = fecha_originall;
                }

                mtto.mtto_fecha_original = fechaProgramada;
                // mtto.mtto_fecha_ultimo = localDate;
                mtto.mtto_fecha_ultimo = fechaRealizacion;
                mtto.mtto_fecha_reprogramada = fechaProgramada;
                mtto.mtto_estatus = Convert.ToString(1);
                if (kilometraje >= 0)
                    mtto.mtto_kilometraje = kilometraje;
                if (id_persona > 0)
                    mtto.id_persona = id_persona;
                if (unidad > 0)
                    mtto.id_unidad = unidad;
                if (mtto_tipo > 0)
                    mtto.mtto_tipo = 1;
                mtto.id_estatus = 3;
                context.si_mantenimineto.InsertOnSubmit(mtto);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Mantenimientos.aspx";
                b.bit_accion = "Actualizar Mantenimiento como realizado";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Se actualizo el mtto como realizado = " + id_mtto;
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

        [WebMethod(EnableSession = true)]
        public static ajaxResponse mtto_proxi()
        {

            ajaxResponse Response = new ajaxResponse();
            try
            {
                DateTime localdate = DateTime.Now;
                DateTime limit = localdate.AddDays(10);
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();
                var query = from mtto in context.si_mantenimineto
                            join u in context.si_unidades on mtto.id_unidad equals u.id_unidad
                            join pe in context.si_personal on mtto.id_persona equals pe.id_persona
                            join es in context.si_estatus_mttos on mtto.id_estatus equals es.id_estatus
                            join ma in context.si_marcas on u.id_marca equals ma.id_marca
                            join mo in context.si_modelos on u.id_modelo equals mo.id_modelo
                            join ki in context.si_kits on u.id_unidad equals ki.id_unidad
                            join kimo in context.si_modelos_kit on ki.id_modelo_kit equals kimo.id_modelo_kit
                            join kima in context.si_marcas_kit on kimo.id_marca_kit equals kima.id_marca_kit
                            join em in context.si_empresas on u.id_empresa equals em.id_empresa
                            join gi in context.si_giros on u.id_giro equals gi.id_giro
                            join como in context.si_comodatarios on u.id_comodatario equals como.id_comodatario into comos
                            from como in comos.DefaultIfEmpty()
                            join bas in context.si_bases on u.id_base equals bas.id_base into bass
                            from bas in bass.DefaultIfEmpty()
                            where mtto.mtto_fecha_reprogramada <= limit && u.un_estatus == true &&
                                  mtto.mtto_estatus == Convert.ToString(1) && u.estatus_instalacion != 2 &&
                                  u.estatus_instalacion != 4
                            select new { mtto = mtto, es = es, u = u, ma = ma, mo = mo, ki = ki, kimo = kimo, kima = kima, em = em, gi = gi, como = como, bas = bas };
                foreach (var items in query)
                {
                    objM dato = new objM();
                    ///// mtto
                    dato.id = items.mtto.id_mantenimiento;
                    dato.fecha_oiginal = (items.mtto.mtto_fecha_original != null) ? (DateTime)items.mtto.mtto_fecha_original : DateTime.Now;
                    dato.fecha_reprogramada = (items.mtto.mtto_fecha_reprogramada != null) ? (DateTime)items.mtto.mtto_fecha_reprogramada : DateTime.Now;
                    dato.fecha_ultima = items.mtto.mtto_fecha_ultimo;
                    dato.kilometraje = (items.mtto.mtto_kilometraje != null) ? (float)items.mtto.mtto_kilometraje : -1;
                    dato.id_estatus = (items.mtto.id_estatus != null) ? (int)items.mtto.id_estatus : -1;
                    dato.estatus = (items.es.e_descripcion != null) ? items.es.e_descripcion : string.Empty;
                    dato.mtto_tipo = (items.mtto.mtto_tipo != null) ? (int)items.mtto.mtto_tipo : -1;
                    dato.id_tecnico = (items.mtto.id_persona != null) ? (int)items.mtto.id_persona : -1;
                    dato.observaciones = (items.mtto.observacion != null) ? items.mtto.observacion : string.Empty;
                    dato.refacciones = (items.mtto.mtto_refacciones != null) ? items.mtto.mtto_refacciones : string.Empty;
                    ///// unidad
                    dato.id_unidad = (items.mtto.id_unidad != null) ? (int)items.mtto.id_unidad : -1;
                    dato.num_economico = (items.u.un_numero_economico != null) ? items.u.un_numero_economico : string.Empty;
                    dato.num_contrato = (items.u.un_codigo_barra != null) ? items.u.un_codigo_barra : string.Empty;
                    dato.marcaV = (items.ma.ma_descripcion != null) ? items.ma.ma_descripcion : string.Empty;
                    dato.modeloV = (items.mo.mod_descripcion != null) ? items.mo.mod_descripcion : string.Empty;
                    dato.anioV = (items.u.un_anio != null) ? (int)items.u.un_anio : -1;
                    ///// kit
                    dato.marcaK = (items.kima.mak_descripcion != null) ? items.kima.mak_descripcion : string.Empty;
                    dato.modeloK = (items.kimo.mok_descripcion != null) ? items.kimo.mok_descripcion : string.Empty;
                    dato.riel = (items.ki.ki_riel != null) ? items.ki.ki_riel : string.Empty;
                    dato.centralita = (items.ki.ki_centralita != null) ? items.ki.ki_centralita : string.Empty;
                    ///// empresa
                    dato.empresa = (items.em.emp_nombre != null) ? items.em.emp_nombre : string.Empty;
                    dato.giro = (items.gi.gi_descripcion != null) ? items.gi.gi_descripcion : string.Empty;
                    dato.Comodatario = (items.como != null) ? items.como.co_nombre : string.Empty;
                    dato.Base = (items.bas != null) ? items.bas.ba_direccion : string.Empty;
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
                Response.Message = "Ha ocurrido un error al iniciar la sesión del usuario. " + ex.Message;
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse guardar_comentario(string comentario, int id_mtto)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                si_comentarios_mantenimiento cm = new si_comentarios_mantenimiento();

                cm.cm_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                cm.cm_comentario = comentario;
                cm.cm_fecha_comentario = DateTime.Now.Date;
                cm.id_mantenimiento = id_mtto;
                context.si_comentarios_mantenimiento.InsertOnSubmit(cm);
                context.SubmitChanges();

                // Alimentamos Bitacora
                si_bitacora b = new si_bitacora();
                b.bit_fecha = DateTime.Now.Date;
                b.bit_tiempo = DateTime.Now.TimeOfDay;
                b.id_usuario = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).id_usuario;
                b.bit_modulo = "Mantenimientos.aspx";
                b.bit_accion = "Agregar comentario de mantenimiento";
                b.bit_observaciones = ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_nombre + "(" + ((si_usuarios)HttpContext.Current.Session["sesionUsuario"]).us_user + ") - Se Agrego comentario a el mtto  = " + id_mtto;
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

        [WebMethod(EnableSession = true)]
        public static ajaxResponse traer_comentarios(int id_mtto)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objC> lista = new List<objC>();

                var consulta = from cm in context.si_comentarios_mantenimiento
                               join gi in context.si_usuarios on cm.cm_usuario equals gi.id_usuario
                               where cm.id_mantenimiento == id_mtto
                               select new
                               {
                                   id = cm.id_comentario,
                                   usuario = gi.us_nombre,
                                   fecha_com = cm.cm_fecha_comentario,
                                   comentario = cm.cm_comentario
                               };

                foreach (var item in consulta)
                {
                    objC cm = new objC();

                    cm.id = item.id;
                    cm.usuario= item.usuario;
                    cm.fecha_comentario = item.fecha_com;
                    cm.comentario = item.comentario;

                    lista.Add(cm);
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
        public static ajaxResponse infoUltimo(string unidad)
        {
            ajaxResponse Response = new ajaxResponse();

            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<uM> lista = new List<uM>();

                var consulta = from mt in context.si_mantenimineto
                               join u in context.si_unidades on mt.id_unidad equals u.id_unidad
                               where u.un_codigo_barra == unidad && mt.mtto_estatus == "1"
                               select new
                               {
                                   unidad = u.un_codigo_barra,
                                   fecha_com = mt.mtto_fecha_original
                               };

                foreach (var item in consulta)
                {
                    uM cm = new uM();

                    cm.unidad = item.unidad;
                    cm.fecha_prox = item.fecha_com;

                    lista.Add(cm);
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
