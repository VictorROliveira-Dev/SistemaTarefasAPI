using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeCadastro.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaDeCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Login == "admin" && login.Senha == "admin")
            {
                var token = GerarToken();
                return Ok(new { token });
            }

            return BadRequest(new { mensagem = "Credenciais inválidas, verifique seu nome de usuário e senha" });
        }

        private string GerarToken()
        {
            string chaveToken = "40955bf5-2d9e-47c5-829d-969c166058fd";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveToken));
            var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var clains = new[]
            {
                new Claim("login", "admin"),
                new Claim("nome", "Administrador do Sistema")
            };

            var token = new JwtSecurityToken(
                issuer: "sua_empresa",
                audience: "sua_aplicacao",
                claims: clains,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencial
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
