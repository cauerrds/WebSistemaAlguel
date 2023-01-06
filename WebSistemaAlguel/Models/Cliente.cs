using Microsoft.AspNetCore.Identity;

namespace WebSistemaAlguel.Models {
    public class Cliente : IdentityUser {

        public string? cpf_cliente { get; set; }

        public IEnumerable<Aluguel>? alugueis { get; set; }
    }
}
