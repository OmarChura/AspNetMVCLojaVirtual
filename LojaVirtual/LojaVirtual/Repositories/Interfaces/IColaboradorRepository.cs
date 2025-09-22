using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IColaboradorRepository
    {
        Colaborador Login(string Email, string Senha);
        void Cadastrar(Colaborador colaborador);
        public void Atualizar(Colaborador colaborador);
        public void Excluir(int Id);
        public Colaborador ObterColaborador(int Id);
        public IEnumerable<Colaborador> ObterTodosColaboradores();
    }
}
