using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        public void Cadastrar(Categoria categoria);
        public void Atualizar(Categoria categoria);
        public void Excluir(int Id);
        public Categoria ObterCategoria(int Id);
        IEnumerable<Categoria> ObterTodasCategorias();
        public IPagedList<Categoria> ObterTodasCategorias(int? pagina);
    }
}
