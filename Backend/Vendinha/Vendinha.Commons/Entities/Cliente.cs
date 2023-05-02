namespace Vendinha.Commons.Entities
{
    public class Cliente : BaseEntity<int>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public int Idade
        {
            get
            {
                DateTime now = DateTime.UtcNow.AddHours(-3);
                var age = now.Year - DataNascimento.Year;
                //Se a data de nascimento for maior que a data atual
                if (DataNascimento.Date > now.AddYears(-age))
                    age--;
                return age;
            }
        }


        public IEnumerable<Divida> Dividas { get; set; }
    }
}
