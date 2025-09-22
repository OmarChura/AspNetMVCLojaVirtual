using LojaVirtual.Libraries.Lang;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.Models
{
    public class Categoria
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        //TODO - criar validaçao = nome categoria unico no banco de dados
        public string Nome { get; set; }

        /*
         * url normal -> www.lojavirtual.com.br/catgoria/5
         * url amigavel com slug -> www.lojavirtual.com.br/catgoria/informatica (url amigavel)
         * slug:
         */
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]

        public string Slug { get; set; }
        /*
         * Auto-relacionamento
         * -informatica
         * --mause
         * ---mause sem fio
         * ---mause gamer
         */
        [Display(Name = "Categoria Pai")]
        public int? CategoriaPaiId { get; set;}
        /*
         * ORM - EntityFrameworkCore
         */
        //[ForeignKey("CategoriaPaiId")]
        [ForeignKey(nameof(CategoriaPaiId))]
        public virtual Categoria CategoriaPai { get; set; }
    }
}
