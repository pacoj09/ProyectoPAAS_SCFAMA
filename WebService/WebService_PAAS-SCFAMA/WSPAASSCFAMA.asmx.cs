using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService_PAAS_SCFAMA
{
    /// <summary>
    /// Descripción breve de WSPAASSCFAMA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSPAASSCFAMA : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] UserAuthentication(string[] _Campos)
        {
            clsValidarDatosUsuario objValidarDatosUsuario = new clsValidarDatosUsuario();
            return objValidarDatosUsuario.validar(_Campos);
        }
    }
}
