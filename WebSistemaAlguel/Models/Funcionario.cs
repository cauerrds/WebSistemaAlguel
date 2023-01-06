using Microsoft.AspNetCore.Identity;

namespace WebSistemaAlguel.Models {
    public class Funcionario : IdentityUser {

        public string? setor_funcionario { get; set; }
    }
}
