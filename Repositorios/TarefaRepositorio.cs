using Microsoft.EntityFrameworkCore;
using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorios.Interfaces;

namespace SistemaDeCadastro.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;

        public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }
        public async Task<TarefaModel> BuscarTarefaPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(usuario => usuario.Usuario)
                .FirstOrDefaultAsync(tarefaID => tarefaID.Id == id);
        }

        public async Task<List<TarefaModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include(usuario => usuario.Usuario)
                .ToListAsync();
        }

        public async Task<TarefaModel> AdicionarTarefa(TarefaModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefaModel> AtualizarTarefa(TarefaModel tarefa, int id)
        {
            TarefaModel tarefaPorId = await BuscarTarefaPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa de ID: {id}, não foi encontrado na base de dados.");
            }

            tarefaPorId.Nome = tarefa.Nome;
            tarefaPorId.Descricao = tarefa.Descricao;
            tarefaPorId.Status = tarefa.Status;
            tarefaPorId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Update(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return tarefaPorId;
        }

        public async Task<bool> ApagarTarefa(int id)
        {
            TarefaModel tarefaPorId = await BuscarTarefaPorId(id);

            if (tarefaPorId == null)
            {
                throw new Exception($"Tarefa de ID: {id}, não foi encontrado na base de dados.");
            }

            _dbContext.Tarefas.Remove(tarefaPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
