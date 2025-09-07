//instalar newtonsoft.json
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries
{
    public class LoginCliente
    {
        private string Key = "Login.Cliente";
        private Sessao.Sessao _sessao;
        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            //armazenar na sessao
            string clienteJsonString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(Key, clienteJsonString);
        }

        public Cliente GetCliente()
        {
            if (_sessao.Existe(Key))
            {
                string clienteJsonString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJsonString);
            }
            else
            {
                return null;
            }
            
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
