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
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }   

        [HttpGet]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTodasTarefas()
        {
            List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();
            return Ok(tarefas);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TarefaModel>>> BuscarTarefaPorId(int id)
        {
            TarefaModel tarefa = await _tarefaRepositorio.BuscarTarefaPorId(id);
            return Ok(tarefa);
        }
        [HttpPost]
        public async Task<ActionResult<TarefaModel>> CadastrarTarefa([FromBody] TarefaModel tarefaModel)
        {
            TarefaModel tarefa = await _tarefaRepositorio.AdicionarTarefa(tarefaModel);
            return Ok(tarefa);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TarefaModel>> AtualizarTarefa([FromBody] TarefaModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefaModel tarefa = await _tarefaRepositorio.AtualizarTarefa(tarefaModel, id);
            return Ok(tarefa);
        }
        [HttpDelete]
        public async Task<ActionResult<TarefaModel>> RemoverTarefa(int id)
        {
            bool tarefaParaRemover = await _tarefaRepositorio.ApagarTarefa(id);
            return Ok(tarefaParaRemover);
        }
    }
}
