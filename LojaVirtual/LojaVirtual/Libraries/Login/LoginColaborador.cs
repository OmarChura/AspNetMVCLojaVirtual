using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao _sessao;
        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            //armazenar na sessao
            string colaboradorJsonString = JsonConvert.SerializeObject(colaborador);
            _sessao.Cadastrar(Key, colaboradorJsonString);
        }

        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(Key))
            {
                string colaboradorJsonString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJsonString);
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
