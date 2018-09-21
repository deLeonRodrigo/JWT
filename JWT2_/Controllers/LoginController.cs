using DB;
using JWT2_.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace JWT2_.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            Conexion _conexion = new Conexion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            DataTableReader dtr = null;
            LoginRequest p = null;
            _conexion.Conectar();
            _conexion.PrepararProcedimiento("LoginUser", _Parametros);
            _Parametros.Add(new SqlParameter("@User", login.Username));
            dtr = _conexion.EjecutarTableReader();
            while (dtr.Read())
            {
                p = new LoginRequest();
                p.Password = dtr["Password"].ToString();
            }
            _conexion.Desconectar();
            _conexion = null;
            if (dtr != null) { dtr.Close(); dtr.Dispose(); }


            bool isCredentialValid = (login.Password == p.Password);
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
