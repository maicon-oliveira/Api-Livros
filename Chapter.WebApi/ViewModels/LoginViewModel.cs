using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário")]
        public string senha { get; set; }
    }
}
