using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        public Cliente Login(string Email, string Senha);
        public void Cadastrar(Cliente cliente);

        public void Atualizar(Cliente cliente);
        public void Excluir(int Id);
        public Cliente ObterCliente(int Id);
        public IEnumerable<Cliente> ObterTodosClientes();
    }
}
