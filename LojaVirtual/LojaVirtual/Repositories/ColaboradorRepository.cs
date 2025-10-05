using LojaVirtual.Database;
using LojaVirtual.Migrations;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using X.PagedList;
using X.PagedList.Extensions;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        //const int _registroPorPagina = 10;
        private IConfiguration _config; //appsettings.json
        private LojaVirtualContext _banco;

        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _config = configuration;
        }
        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }
        public void Atualizar(Colaborador colaborador)
        {
            //TODO - nome - tipo - email
            _banco.Update(colaborador);
            //comando para nao atualizar a senha
            _banco.Entry(colaborador).Property(a => a.Senha).IsModified = false;
            _banco.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            //TODO - atualizar senha
            _banco.Update(colaborador);
            //comando para nao atualizar o nome - tipo - email
            _banco.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Email).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Tipo).IsModified = false;

            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Colaborador colaborador = ObterColaborador(Id);
            _banco.Remove(colaborador);
            _banco.SaveChanges();
        }

        public Colaborador Login(string Email, string Senha)
        {
            Colaborador colaborador = _banco.Colaboradores.Where(m => m.Email == Email && m.Senha == Senha).FirstOrDefault();
            return colaborador;
        }

        public Colaborador ObterColaborador(int Id)
        {
            return _banco.Colaboradores.Find(Id);
        }

        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            return _banco.Colaboradores.Where(a=>a.Tipo != "G").ToList();
        }

        public IPagedList<Colaborador> ObterTodosColaboradores(int? pagina)
        {
            int NumeroPagina = pagina ?? 1;
            return _banco.Colaboradores.Where(a => a.Tipo != "G").ToPagedList<Colaborador>(NumeroPagina, _config.GetValue<int>("RegistroPorPagina"));

        }

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            //TODO - AsnoTracking -> faz com o entityframework nao acompanhe o objeto, assim nao vai ter conflito na hora de atualizar no banco
            return _banco.Colaboradores.Where(a => a.Email == email).AsNoTracking().ToList();
        }
    }
}
