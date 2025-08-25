using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;

namespace LojaVirtual.Repositories
{
    public class NewsletterRepository : INewsletterRepository
    {
        private LojaVirtualContext _banco;
        public NewsletterRepository(LojaVirtualContext banco) 
        {
            _banco = banco;
        }
        public void Cadastrar(NewletterEmail newsletter)
        {
            _banco.NewletterEmails.Add(newsletter);
            _banco.SaveChanges();
        }

        public IEnumerable<NewletterEmail> ObterTodasNewsletter()
        {
            return _banco.NewletterEmails.ToList();
        }
    }
}
