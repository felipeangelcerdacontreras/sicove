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

namespace wa_sicove.sicoveadmin
{
    public partial class IndicadoresUnidades : System.Web.UI.Page
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

        public class objE
        {
            public string emp_nombre { get; set; }
            public string zona { get; set; }
            public string emp_contacto { get; set; }
            public string emp_telefono { get; set; }
            public string emp_direccion { get; set; }
            public int Cantidad { get; set; }
            public int Activos { get; set; }
            public int Desintalado { get; set; }
            public int Vendido { get; set; }
            public int Robado { get; set; }
            public int CambioPropietario { get; set; }
            public int ProcesoJuridico { get; set; }
            public int Otro { get; set; }
            public int TActivos { get; set; }
            public decimal? LitrosT { get; set; }
        }

        public class objG
        {
            public string gi_descripcion { get; set; }
            public string gi_autooconsumo { get; set; }
            public int Cantidad { get; set; }
            public int Activos { get; set; }
            public int Desintalado { get; set; }
            public int Vendido { get; set; }
            public int Robado { get; set; }
            public int CambioPropietario { get; set; }
            public int ProcesoJuridico { get; set; }
            public int Otro { get; set; }
            public int TActivos { get; set; }
        }

        public class objM
        {
            public string marca { get; set; }
            public string modelo { get; set; }
            public int Cantidad { get; set; }
            public int Activos { get; set; }
            public int Desintalado { get; set; }
            public int Vendido { get; set; }
            public int Robado { get; set; }
            public int CambioPropietario { get; set; }
            public int ProcesoJuridico { get; set; }
            public int Otro { get; set; }
            public int TActivos { get; set; }
        }


        [WebMethod(EnableSession = true)]
        public static ajaxResponse IndEmpresas(int tipo, DateTime fInicialR, DateTime fFinalR)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                context.CommandTimeout = 4000;
                List<objE> list = new List<objE>();

                var query = from e in context.si_empresas
                            join z in context.si_zonas on e.id_zona equals z.id_zona
                            where e.emp_autoconsumo == tipo
                            select new
                            {
                                nombre = e.emp_nombre,
                                zona = z.cd_descripcion,
                                contacto = e.emp_contacto,
                                telefono = e.emp_telefono,
                                direccion = e.emp_direccion,
                                Cantidad = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa select u.id_unidad).Count(),
                                Activos = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 1 select u.id_unidad).Count(),
                                Desinstalado = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 2 select u.id_unidad).Count(),
                                Vendido = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 3 select u.id_unidad).Count(),
                                Robado = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 4 select u.id_unidad).Count(),
                                CambioPropietario = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 5 select u.id_unidad).Count(),
                                ProcesoJuridico = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 6 select u.id_unidad).Count(),
                                Otro = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.estatus_instalacion == 7 select u.id_unidad).Count(),
                                TActivos = (int)(from u in context.si_unidades where e.id_empresa == u.id_empresa && u.un_estatus == true select u.id_unidad).Count(),
                                LitrosT = (from v in context.si_ventas join u in context.si_unidades on v.id_unidad equals u.id_unidad join em in context.si_empresas on u.id_empresa equals em.id_empresa where em.id_empresa == e.id_empresa && v.ve_fecha_venta >= fInicialR && v.ve_fecha_venta < fFinalR select v.ve_litros).Sum() ?? 0

                            };
                


                foreach (var item in query)
                {
                    objE dato = new objE();

                    dato.emp_nombre = item.nombre;
                    dato.zona = item.zona;
                    dato.emp_contacto = item.contacto;
                    dato.emp_telefono = item.telefono;
                    dato.emp_direccion = item.direccion;
                    dato.Cantidad = item.Cantidad;
                    dato.Activos = item.Activos;
                    dato.Desintalado = item.Desinstalado;
                    dato.Vendido = item.Vendido;
                    dato.Robado = item.Robado;
                    dato.CambioPropietario = item.CambioPropietario;
                    dato.ProcesoJuridico = item.ProcesoJuridico;
                    dato.Otro = item.Otro;
                    dato.TActivos = item.TActivos;
                    dato.LitrosT = item.LitrosT;

                    list.Add(dato);

                }


                var query2 = (from v in context.si_ventas
                             where v.ve_observacion == "DOMESTICO" && v.ve_litros > 3
                             && v.ve_fecha_venta >= fInicialR && v.ve_fecha_venta < fFinalR
                             select v.ve_litros).Sum();


                objE dato2 = new objE();

                dato2.emp_nombre = "DOMESTICO";
                dato2.LitrosT = query2;

                list.Add(dato2);


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
        public static ajaxResponse IndGiros(int tipo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objG> list = new List<objG>();

                var query = from g in context.si_giros
                            where g.gi_autoconsumo == tipo
                            select new
                            {
                                nombre = g.gi_descripcion,
                                autoconsumo = g.gi_autoconsumo,
                                Cantidad = (int)(from u in context.si_unidades where g.id_giro == u.id_giro select u.id_unidad).Count(),
                                Activos = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 1 select u.id_unidad).Count(),
                                Desinstalado = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 2 select u.id_unidad).Count(),
                                Vendido = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 3 select u.id_unidad).Count(),
                                Robado = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 4 select u.id_unidad).Count(),
                                CambioPropietario = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 5 select u.id_unidad).Count(),
                                ProcesoJuridico = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 6 select u.id_unidad).Count(),
                                Otro = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.estatus_instalacion == 7 select u.id_unidad).Count(),
                                TActivos = (int)(from u in context.si_unidades where g.id_giro == u.id_giro && u.un_estatus == true select u.id_unidad).Count()

                            };


                foreach (var item in query)
                {
                    objG dato = new objG();

                    dato.gi_descripcion = item.nombre;
                    dato.gi_autooconsumo = item.autoconsumo.ToString();
                    dato.Cantidad = item.Cantidad;
                    dato.Activos = item.Activos;
                    dato.Desintalado = item.Desinstalado;
                    dato.Vendido = item.Vendido;
                    dato.Robado = item.Robado;
                    dato.CambioPropietario = item.CambioPropietario;
                    dato.ProcesoJuridico = item.ProcesoJuridico;
                    dato.Otro = item.Otro;
                    dato.TActivos = item.TActivos;

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
        public static ajaxResponse indMarca(int tipo)
        {
            ajaxResponse Response = new ajaxResponse();
            try
            {
                sicoveDBDataContext context = new sicoveDBDataContext();
                List<objM> list = new List<objM>();

                var query = from m in context.si_modelos
                            join ma in context.si_marcas on m.id_marca equals ma.id_marca
                            select new
                            {
                                marca = ma.ma_descripcion,
                                modelo = m.mod_descripcion,
                                Cantidad = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                Activos = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 1 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                Desinstalado = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 2 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                Vendido = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 3 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                Robado = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 4 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                CambioPropietario = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 5 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                ProcesoJuridico = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 6 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                Otro = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.estatus_instalacion == 7 && g.gi_autoconsumo == tipo select u.id_unidad).Count(),
                                TActivos = (int)(from u in context.si_unidades join g in context.si_giros on u.id_giro equals g.id_giro where m.id_marca == u.id_marca && m.id_modelo == u.id_modelo && u.un_estatus == true && g.gi_autoconsumo == tipo select u.id_unidad).Count()

                            };


                foreach (var item in query)
                {
                    objM dato = new objM();

                    dato.marca = item.marca;
                    dato.modelo = item.modelo;
                    dato.Cantidad = item.Cantidad;
                    dato.Activos = item.Activos;
                    dato.Desintalado = item.Desinstalado;
                    dato.Vendido = item.Vendido;
                    dato.Robado = item.Robado;
                    dato.CambioPropietario = item.CambioPropietario;
                    dato.ProcesoJuridico = item.ProcesoJuridico;
                    dato.Otro = item.Otro;
                    dato.TActivos = item.TActivos;

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

    }
}