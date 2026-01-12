using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            using var con = new OracleConnection(
                _config.GetConnectionString("OracleDB"));

            con.Open();

            string sql =
                "SELECT COUNT(*) FROM USERS WHERE USERNAME=:u AND PASSWORD=:p";

            using var cmd = new OracleCommand(sql, con);
            cmd.Parameters.Add(new OracleParameter("u", login.Username));
            cmd.Parameters.Add(new OracleParameter("p", login.Password));

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
                return Ok("Login Successful");
            else
                return Unauthorized("Invalid Credentials");
        }
    }
}
