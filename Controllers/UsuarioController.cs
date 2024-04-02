using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorios.Interfaces;

namespace SistemaDeCadastro.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }   

        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarUsuarios()
        {
            List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> CadastrarUsuario([FromBody] UsuarioModel usuarioModel)
        {
            UsuarioModel usuario = await _usuarioRepositorio.AdicionarUsuario(usuarioModel);
            return Ok(usuario);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> AtualizarUsuario([FromBody] UsuarioModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.AtualizarUsuario(usuarioModel, id);
            return Ok(usuario);
        }
        [HttpDelete]
        public async Task<ActionResult<UsuarioModel>> RemoverUsuario(int id)
        {
            bool usuarioParaRemover = await _usuarioRepositorio.ApagarUsuario(id);
            return Ok(usuarioParaRemover);
        }
    }
}
