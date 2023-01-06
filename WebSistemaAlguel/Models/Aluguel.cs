using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSistemaAlguel.Models {
    public class Aluguel {
        [Key]
        public int cod_aluguel { get; set; }

        public decimal preco_final_aluguel { get; set; }

        public DateTime data_inicio_alguel { get; set; }

        public DateTime data_fim_aluguel { get; set; }

        public bool ativo_aluguel { get; set; }

        [ForeignKey("Id")]
        public Cliente? cliente { get; set; }
        public string Id { get; set; }

        [ForeignKey("cod_carro")]
        public Carro? carro { get; set; }
        public int cod_carro { get; set; }
    }
}
