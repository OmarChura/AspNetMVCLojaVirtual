namespace LojaVirtual.Libraries.Sessao
{
    public class Sessao
    {
        private IHttpContextAccessor _httpContext;
        public Sessao( IHttpContextAccessor httpContext)
        { 
            _httpContext = httpContext;
        }

        //crud cadastrar/atualizar/consultar/remover - removertodos/verificar se existe
        public void Cadastrar(string Key, string Valor)
        {
            _httpContext.HttpContext.Session.SetString(Key, Valor);
        }

        public void Atualizar(string Key, string Valor)
        {
            if (Existe(Key))
            {
                _httpContext.HttpContext.Session.Remove(Key);
            }
            _httpContext.HttpContext.Session.SetString(Key, Valor);
        }

        public void Remover(string Key)
        {
            _httpContext.HttpContext.Session.Remove(Key);
        }

        public string Consultar(string Key)
        {
            return _httpContext.HttpContext.Session.GetString(Key);
        }

        public bool Existe(string Key) 
        {
           if(_httpContext.HttpContext.Session.GetString(Key) == null)
            {
                return false;
            }
           return true;
        }

        public void RemoverTodos()
        {
            _httpContext.HttpContext.Session.Clear();
        }
    }
}
