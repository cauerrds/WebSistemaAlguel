using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSistemaAlguel.Models {
    public class Carro {
        [Key]
        public int cod_carro { get; set; }

        public string? modelo_carro { get; set; }

        public decimal valor_carro { get; set; }

        public int ano_carro { get; set; }

        public bool alugado_carro { get; set; }

        [ForeignKey("cod_fabricante")]
        public Fabricante? fabricante { get; set; }

        public int cod_fabricante { get; set; }


    }
}
