using LojaVirtual.Models;
using LojaVirtual.Repositories.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Libraries.Validacao
{
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        //sobre escrever o IsValid com 2 parametros
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //TODO - pegar valor do campo email, obter repositorio do colaborador, fazer verificação

            string Email = value as string;
            //TODO -  obter repositorio do colaborador
            IColaboradorRepository _colaboradorRepository = (IColaboradorRepository)validationContext.GetService(typeof(IColaboradorRepository));

            //TODO - fazer verificação
            List<Colaborador> colaboradores = _colaboradorRepository.ObterColaboradorPorEmail(Email);

            Colaborador objColaborador = (Colaborador)validationContext.ObjectInstance;

            //TODO - cadastrar -> colaboradores > 1 === rejeitar
            if(colaboradores.Count > 1)
            {
                return new ValidationResult("E-mail já existente!");
            }

            //TODO - atualizar -> (colaboradores == 1 && objColaborador.ID != colaboradores[0].Id) === rejeitar
            if(colaboradores.Count == 1 && objColaborador.Id != colaboradores[0].Id)
            {
                return new ValidationResult("E-mail já existente!");
            }

            return ValidationResult.Success;
        }
    }
}
