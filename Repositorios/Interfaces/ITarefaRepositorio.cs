using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorios.Interfaces
{
    public interface ITarefaRepositorio
    {
        Task<List<TarefaModel>> BuscarTodasTarefas();
        Task<TarefaModel> BuscarTarefaPorId(int id);
        Task<TarefaModel> AdicionarTarefa(TarefaModel tarefa);
        Task<TarefaModel> AtualizarTarefa(TarefaModel tarefa, int id);
        Task<bool> ApagarTarefa(int id);
    }
}
