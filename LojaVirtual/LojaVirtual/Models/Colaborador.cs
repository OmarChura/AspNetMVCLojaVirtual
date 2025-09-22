namespace LojaVirtual.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        //tipo 
        //C - comun
        //G - gerente
        public string Tipo { get; set; }


    }
}
