using System.ComponentModel.DataAnnotations;
using Vendinha.Commons.Exceptions;

namespace Vendinha.Commons.DTOs
{
    public class ClienteDto : BaseDto<int>
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [EmailAddress]
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
        public float DividaCliente { get; set; }

        public bool CpfValido()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            CPF = CPF.Trim();
            CPF = CPF.Replace(".", "").Replace("-", "");
            if (CPF.Length != 11)
                return false;
            tempCpf = CPF.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                if (!int.TryParse(tempCpf[i].ToString(), out int numero)) throw new BusinessRuleException("Formato de CPF inválido");
                soma += numero * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!int.TryParse(tempCpf[i].ToString(), out int numero)) throw new BusinessRuleException("Formato de CPF inválido");
                soma += numero * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return CPF.EndsWith(digito);
        }
    }
}
