using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_sicove.core
{
    public class ClassBicatora
    {
        public static void insertBitacora(si_bitacora bitacora)
        {
            try
            {
                si_bitacora b = new si_bitacora();
                b = bitacora;
                sicoveDBDataContext context = new sicoveDBDataContext();
                context.si_bitacora.InsertOnSubmit(b);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Problema de Bitacora. " + ex.Message);
            }
        }
    }
}