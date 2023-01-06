using System.ComponentModel.DataAnnotations;

namespace WebSistemaAlguel.Models {
    public class Fabricante {
        [Key]
        public int cod_fabricante { get; set; }

        public string? nome_fabricante { get; set; }

        public string matriz_fabricante { get; set; }

        public IEnumerable<Carro>? carros { get; set; }

    }
}
