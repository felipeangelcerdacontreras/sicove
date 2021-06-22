using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using wa_sicove.core;

namespace wa_sicove.sicovegas
{
    public partial class ServiciosComodatarios : System.Web.UI.Page
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
            public string tVenta { get; set; }
            public string unidad { get; set; }
            public string nEco { get; set; }
            public string despachador { get; set; }
            public string empresa { get; set; }
            public DateTime? fechaVenta { get; set; }
            public int? fCortesia { get; set; }
            public decimal? cCortesia { get; set; }
            public int? kilometraje { get; set; }
            public string mPago { get; set; }
            public string estacion { get; set; }
            public string medidor { get; set; }
            public int? nServicio { get; set; }
            public decimal? litros { get; set; }
            public decimal? pGas { get; set; }
            public decimal? total { get; set; }
        }

        public class objR
        {
            public int idUnidad { get; set; }
            public string un_codigo_barra { get; set; }
            public string un_numero_economico { get; set; }
            public decimal? volumen { get; set; }
            public int? servicios { get; set; }
            public string emp_nombre { get; set; }
            public int id_empresa { get; set; }
            public int? id_comodatario { get; set; }
            public string co_nombre { get; set; }
            public string un_direccion { get; set; }
            public string un_telefono { get; set; }
            public int estatus_instalacion { get; set; }
            public DateTime un_fecha_instalacion { get; set; }
            public int dias { get; set; }
            public decimal? promedio { get; set; }
            public int gi_autoconsumo { get; set; }
            public DateTime? ultima_carga { get; set; }
        }

        public class objU
        {
            public int idVenta { get; set; }
            public int? idUnidad { get; set; }
            public string un_codigo_barra { get; set; }
            public string un_numero_economico { get; set; }
            public string ma_descripcion { get; set; }
            public string mod_descripcion { get; set; }
            public int? anio { get; set; }
            public string gi_descripcion { get; set; }
            public string un_placa { get; set; }
            public string co_nombre { get; set; }
            public string emp_nombre { get; set; }
            public DateTime? ve_fecha_venta { get; set; }
            public decimal? ve_precio_gas { get; set; }
            public int? ve_kilometros { get; set; }
            public int? ve_kilometros_ant { get; set; }
            public int? ve_servicio { get; set; }
            public decimal? ve_litros { get; set; }
            public decimal? ve_total { get; set; }
            public string medidor { get; set; }
            public string esta_descripcion { get; set; }
            public string Despachador { get; set; }
            public decimal? litrosAnterior { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse serviciosXFecha(DateTime fInicial, DateTime fFinal, int idEstacion, int modulo, string tipo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objV> list = new List<objV>();

                    var query = context.SPServiciosXFecha(fInicial, fFinal, idEstacion, modulo, tipo, Convert.ToString(((si_comodatarios)HttpContext.Current.Session["sesionUsuario"]).id_comodatario));
                    foreach (var item in query)
                    {
                        objV dato = new objV();

                        dato.tVenta = item.tVenta;
                        dato.unidad = item.unidad;
                        dato.nEco = item.nEconomico;
                        dato.despachador = item.despachador;
                        dato.empresa = item.empresa;
                        dato.fechaVenta = item.fechaVenta;
                        dato.fCortesia = item.fCortesia;
                        dato.cCortesia = item.cCortesia;
                        dato.kilometraje = item.kilometraje;
                        dato.mPago = item.mPago;
                        dato.estacion = item.estacion;
                        dato.medidor = item.medidor;
                        dato.nServicio = item.nServicio;
                        dato.litros = item.litros;
                        dato.pGas = item.pGas;
                        dato.total = item.total;

                        list.Add(dato);
                    }
                    var jsonSerialiser = new JavaScriptSerializer();
                    jsonSerialiser.MaxJsonLength = int.MaxValue;
                    var json = jsonSerialiser.Serialize(list);
                    Response.Result = true;
                    Response.Message = "Correctamente";
                    Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = "Ha ocurrido un error. " + ex.Message + " " + ex.HResult + " " + ex.ToString();
                Response.Data = null;
            }
            return Response;
        }

        
        [WebMethod(EnableSession = true)]
        public static ajaxResponse serviciosResumen(DateTime fInicial, DateTime fFinal, int zonas, string[] estatus, int consumos, int tipo)
        {
            ajaxResponse Response = new ajaxResponse();
            string senten = "";
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objR> list = new List<objR>();

                int diasDiff = (fFinal - fInicial).Days;

                if(diasDiff < 1)
                {
                    diasDiff = 1;
                }

                string status = string.Join(",", estatus);

                string diaI = fInicial.Day.ToString().Length < 2 ? "0" + fInicial.Day.ToString() : fInicial.Day.ToString();
                string mesI = fInicial.Month.ToString().Length < 2 ? "0" + fInicial.Month.ToString() : fInicial.Month.ToString();
                string horaI = fInicial.Hour.ToString().Length < 2 ? "0" + fInicial.Hour.ToString() : fInicial.Hour.ToString();
                string minutoI = fInicial.Minute.ToString().Length < 2 ? "0" + fInicial.Minute.ToString() : fInicial.Minute.ToString();

                string diaF = fFinal.Day.ToString().Length < 2 ? "0" + fFinal.Day.ToString() : fFinal.Day.ToString(); ;
                string mesF = fFinal.Month.ToString().Length < 2 ? "0" + fFinal.Month.ToString() : fFinal.Month.ToString();
                string horaF = fFinal.Hour.ToString().Length < 2 ? "0" + fFinal.Hour.ToString() : fFinal.Hour.ToString();
                string minutoF = fFinal.Minute.ToString().Length < 2 ? "0" + fFinal.Minute.ToString() : fFinal.Minute.ToString();

                // Sentencia SQL 
                string sentencia = "SELECT " +
                    "u.id_unidad, " +
                    "u.un_codigo_barra, " +
                    "u.un_numero_economico, " +
                     "(select SUM(ve_litros) from si_ventas WHERE id_unidad = u.id_unidad AND ve_fecha_venta BETWEEN '" + fInicial.Year + "-" + mesI + "-" + diaI + "T" + horaI + ":" + minutoI + ":00.000' AND '" + fFinal.Year + "-" + mesF + "-" + diaF + "T" + horaF + ":" + minutoF + ":00.000') as volumen, " +
                     "(select COUNT(id_venta) from si_ventas WHERE id_unidad = u.id_unidad AND ve_fecha_venta BETWEEN '" + fInicial.Year + "-" + mesI + "-" + diaI + "T" + horaI + ":" + minutoI + ":00.000' AND '" + fFinal.Year + "-" + mesF + "-" + diaF + "T" + horaF + ":" + minutoF + ":00.000') as servicios, " +
                     "e.emp_nombre, " +
                     "u.id_empresa, " +
                     "c.id_comodatario, " +
                     "c.co_nombre, " +
                     "u.un_direccion, " +
                     "u.un_telefono, " +
                     "u.estatus_instalacion, " +
                     "u.un_fecha_instalacion, " +
                     "g.gi_autoconsumo, " +
                     "(SELECT TOP 1 ve_fecha_venta from si_ventas where id_unidad=u.id_unidad ORDER BY ve_fecha_venta DESC) as ultimaCarga " +
                "FROM si_unidades as u " +
                "INNER JOIN si_giros as g ON u.id_giro = g.id_giro " +
                "INNER JOIN si_empresas as e ON u.id_empresa = e.id_empresa " +
                "LEFT JOIN si_comodatarios as c ON u.id_comodatario = c.id_comodatario " +
                "LEFT JOIN si_ventas as v ON v.id_unidad = u.id_unidad " +
                "WHERE u.estatus_instalacion IN(" + status + ") ";

                if( zonas != -1)
                {
                    sentencia += " AND e.id_zona = " + zonas + " ";
                } 

                if(tipo == 0 || tipo == 1)
                {
                    sentencia += " AND g.gi_autoconsumo = " + tipo + " ";
                }

                if(tipo == 3)
                {
                    sentencia += "AND u.un_codigo_barra like 'EXT%' ";
                }


                sentencia +=  " GROUP BY u.id_unidad,  " +
                    "u.un_codigo_barra,  " +
                    "u.id_empresa,  " +
                    "e.emp_nombre,  " +
                    "u.un_numero_economico, " +
                    "c.id_comodatario,  " +
                    "c.co_nombre,  " +
                    "u.un_direccion, " +
                    "u.un_telefono, " +
                    "u.estatus_instalacion, " +
                    "u.un_fecha_instalacion, " +
                    "g.gi_autoconsumo " +
                "ORDER BY u.id_unidad ";

                senten = sentencia;
                
                var query = context.SPServiciosR(sentencia);

                foreach (var item in query)
                {
                    objR dato = new objR();

                    dato.idUnidad = item.id_unidad;
                    dato.un_codigo_barra = item.un_codigo_barra;
                    dato.un_numero_economico = item.un_numero_economico;
                    dato.volumen = item.volumen;
                    dato.servicios = item.servicios;
                    dato.emp_nombre = item.emp_nombre;
                    dato.id_empresa = item.id_empresa;
                    dato.id_comodatario = item.id_comodatario;
                    dato.co_nombre = item.co_nombre;
                    dato.un_direccion = item.un_direccion;
                    dato.un_telefono = item.un_telefono;
                    dato.estatus_instalacion = item.estatus_instalacion;
                    dato.un_fecha_instalacion = (DateTime)item.un_fecha_instalacion;
                    dato.ultima_carga = item.ultimaCarga;

                    if( dato.un_fecha_instalacion > fInicial)
                    {
                        int diasDiff2 = (fFinal - dato.un_fecha_instalacion).Days;

                        if (diasDiff2 < 1)
                            diasDiff2 = 1;


                        dato.dias = diasDiff2;
                        dato.promedio = dato.volumen / diasDiff;
                        dato.gi_autoconsumo = item.gi_autoconsumo;
                        
                    } else
                    {
                        dato.dias = diasDiff;
                        dato.promedio = dato.volumen / diasDiff;
                        dato.gi_autoconsumo = item.gi_autoconsumo;
                    }



                    switch (consumos)
                    {
                        case -1:
                            list.Add(dato);
                            break;

                        case 1:
                            if(dato.volumen <= 100)
                                list.Add(dato);
                            break;

                        case 2:
                            if (dato.volumen >= 100 && dato.volumen <= 200)
                                list.Add(dato);
                            break;

                        case 3:
                            if (dato.volumen >= 200 && dato.volumen <= 400)
                                list.Add(dato);
                            break;

                        case 4:
                            if (dato.volumen >= 400 && dato.volumen <= 600)
                                list.Add(dato);
                            break;

                        case 5:
                            if (dato.volumen >= 595)
                                list.Add(dato);
                            break;

                    }
                    //list.Add(dato);
                }
                var jsonSerialiser = new JavaScriptSerializer();
                jsonSerialiser.MaxJsonLength = int.MaxValue;
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = senten + " Ha ocurrido un error al iniciar la sesión del usuario. " + ex.ToString();
                Response.Data = null;
            }
            return Response;
        }
        
        [WebMethod(EnableSession = true)]
        public static ajaxResponse serviciosUnidad(DateTime fInicial, DateTime fFinal, int idUnidadU, int idEstacionU, int? idModuloU)
        {
            ajaxResponse Response = new ajaxResponse();
            string senten = "";
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();

                var query = context.SPServiciosXUnidad(fInicial, fFinal, idUnidadU, idEstacionU, idModuloU);

                foreach (var item in query)
                {
                    objU dato = new objU();

                    dato.idVenta = item.id_venta;
                    dato.idUnidad = item.id_unidad;
                    dato.un_codigo_barra = item.un_codigo_barra;
                    dato.un_numero_economico = item.un_numero_economico;
                    dato.ma_descripcion = item.ma_descripcion;
                    dato.mod_descripcion = item.mod_descripcion;
                    dato.gi_descripcion = item.gi_descripcion;
                    dato.un_placa = item.un_placa;
                    dato.co_nombre = item.co_nombre;
                    dato.emp_nombre = item.emp_nombre;
                    dato.ve_fecha_venta = item.ve_fecha_venta;
                    dato.ve_precio_gas = item.ve_precio_gas;
                    dato.ve_kilometros = item.ve_kilometros;
                    dato.ve_kilometros_ant = item.kil_anterior;
                    dato.ve_servicio = item.ve_servicio;
                    dato.ve_litros = item.ve_litros;
                    dato.ve_total = item.ve_total;
                    dato.medidor = item.medidor;
                    dato.esta_descripcion = item.esta_descripcion;
                    dato.Despachador = item.Despachador;
                    dato.litrosAnterior = item.litros_anterior;
                    dato.anio = item.un_anio;
                    list.Add(dato);
                }
                var jsonSerialiser = new JavaScriptSerializer();
                jsonSerialiser.MaxJsonLength = int.MaxValue;
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = senten + " Ha ocurrido un error al iniciar la sesión del usuario. " + ex.ToString();
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse serviciosEmpresa(DateTime fInicial, DateTime fFinal, int? idEmpresa, int? idComodatario, int? idEstacion, int? idModulo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();

                var query = context.SPServiciosXEmpresa(fInicial, fFinal, idEmpresa, idComodatario, idEstacion, idModulo);

                foreach (var item in query)
                {
                    objU dato = new objU();

                    dato.idVenta = item.id_venta;
                    dato.idUnidad = item.id_unidad;
                    dato.un_codigo_barra = item.un_codigo_barra;
                    dato.un_numero_economico = item.un_numero_economico;
                    dato.ma_descripcion = item.ma_descripcion;
                    dato.mod_descripcion = item.mod_descripcion;
                    dato.anio = item.un_anio;
                    dato.gi_descripcion = item.gi_descripcion;
                    dato.un_placa = item.un_placa;
                    dato.co_nombre = item.co_nombre;
                    dato.emp_nombre = item.emp_nombre;
                    dato.ve_fecha_venta = item.ve_fecha_venta;
                    dato.ve_precio_gas = item.ve_precio_gas;
                    dato.ve_kilometros = item.ve_kilometros;
                    dato.ve_kilometros_ant = item.kil_anterior;
                    dato.ve_servicio = item.ve_servicio;
                    dato.ve_litros = item.ve_litros;
                    dato.ve_total = item.ve_total;
                    dato.medidor = item.medidor;
                    dato.esta_descripcion = item.esta_descripcion;
                    dato.Despachador = item.Despachador;
                    dato.litrosAnterior = item.litros_anterior;
                    list.Add(dato);
                }
                var jsonSerialiser = new JavaScriptSerializer();
                jsonSerialiser.MaxJsonLength = int.MaxValue;
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = " Ha ocurrido un error al iniciar la sesión del usuario. " + ex.ToString();
                Response.Data = null;
            }
            return Response;
        }

        [WebMethod(EnableSession = true)]
        public static ajaxResponse serviciosEmpresaAUTO(DateTime fInicial, DateTime fFinal, int? idEmpresa, int? idComodatario, int? idEstacion, int? idModulo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objU> list = new List<objU>();

                var query = context.SPServiciosXEmpresaAUTO(fInicial, fFinal, idEmpresa, idComodatario, idEstacion, idModulo);

                foreach (var item in query)
                {
                    objU dato = new objU();

                    dato.idVenta = item.id_venta;
                    dato.idUnidad = item.id_unidad;
                    dato.un_codigo_barra = item.un_codigo_barra;
                    dato.un_numero_economico = item.un_numero_economico;
                    dato.ma_descripcion = item.ma_descripcion;
                    dato.mod_descripcion = item.mod_descripcion;
                    dato.anio = item.un_anio;
                    dato.gi_descripcion = item.gi_descripcion;
                    dato.un_placa = item.un_placa;
                    dato.co_nombre = item.co_nombre;
                    dato.emp_nombre = item.emp_nombre;
                    dato.ve_fecha_venta = item.ve_fecha_venta;
                    dato.ve_precio_gas = item.ve_precio_gas;
                    dato.ve_kilometros = item.ve_kilometros;
                    dato.ve_kilometros_ant = item.kil_anterior;
                    dato.ve_servicio = item.ve_servicio;
                    dato.ve_litros = item.ve_litros;
                    dato.ve_total = item.ve_total;
                    dato.medidor = item.medidor;
                    dato.esta_descripcion = item.esta_descripcion;
                    dato.Despachador = item.Despachador;
                    dato.litrosAnterior = item.litros_anterior;
                    list.Add(dato);
                }
                var jsonSerialiser = new JavaScriptSerializer();
                jsonSerialiser.MaxJsonLength = int.MaxValue;
                var json = jsonSerialiser.Serialize(list);
                Response.Result = true;
                Response.Message = "Correctamente";
                Response.Data = json;
            }
            catch (Exception ex)
            {
                Response.Result = false;
                Response.Message = " Ha ocurrido un error al iniciar la sesión del usuario. " + ex.ToString();
                Response.Data = null;
            }
            return Response;
        }
    }
}