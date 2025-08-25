using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Interfaces
{
    public interface INewsletterRepository
    {
        void Cadastrar(NewletterEmail newsletter);

        IEnumerable<NewletterEmail> ObterTodasNewsletter();
    }
}
