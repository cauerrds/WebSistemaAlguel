using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebSistemaAlguel.Data;
using WebSistemaAlguel.Models;

namespace WebSistemaAlguel.Controllers {

    [Authorize]
    public class AluguelController : Controller {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AluguelController(ApplicationDbContext context, UserManager<IdentityUser> _userManager) {
            _context = context;
            this._userManager = _userManager;
        }

        public async Task<IActionResult> CarrosDisponiveis(string? txtPesquisarCarros) {
            if(string.IsNullOrEmpty(txtPesquisarCarros)) {
                txtPesquisarCarros = "";
            }

            var lista_de_carros = _context.carro.Include(f => f.fabricante)
                .Where(c => c.alugado_carro == false)
                .Where(c => c.modelo_carro.Contains(txtPesquisarCarros));
            return View(await lista_de_carros.ToListAsync());
        }

        public async Task<IActionResult> AlugarCarro(int? id) { 
            var carro = await _context.carro.Include(c => c.fabricante)
                .FirstOrDefaultAsync(m => m.cod_carro== id);
            if(carro==null) {
                return NotFound();
            }

            return View(carro);
        }

        public async Task<IActionResult> ConfirmarAluguel(int cod_carro) {
                var clienteLogado = await _userManager.GetUserAsync(User);

            _context.alguel.Add(new Aluguel() {
                cliente = (Cliente)clienteLogado,
                cod_carro = cod_carro,
                ativo_aluguel = true
            });
            
            await _context.SaveChangesAsync();

            var carro = await _context.carro.FirstOrDefaultAsync(c => c.cod_carro == cod_carro);

            carro.alugado_carro = true;

            _context.Update(carro);

            await _context.SaveChangesAsync();

            return RedirectToAction("CarrosDisponiveis");
        }

        public async Task<IActionResult> MeusCarrosAlugados () {
            var alugueis = _context.alguel.Include(cli => cli.cliente)
                .Include(car => car.carro)
                .Where(c => c.ativo_aluguel == true && c.cliente.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(await alugueis.ToListAsync());
        }

        public async Task<IActionResult> DevolverCarro(int cod_aluguel) {
            var aluguel = await _context.alguel
                .Include(car => car.carro).FirstOrDefaultAsync(m => m.cod_aluguel == cod_aluguel);

            DateTime dataEntrega =  DateTime.Now;
            var dias = (dataEntrega - aluguel.data_inicio_alguel).TotalDays;
            aluguel.preco_final_aluguel = (decimal)dias * aluguel.carro.valor_carro;

            return View(aluguel);
        }

        public async Task<IActionResult> ConfirmarDevolucao(int cod_aluguel) {
            var aluguel = await _context.alguel
                .Include(car => car.carro).FirstOrDefaultAsync(m => m.cod_aluguel == cod_aluguel);

            DateTime dataEntrega = DateTime.Now;
            var dias = (dataEntrega - aluguel.data_inicio_alguel).TotalDays;
            aluguel.preco_final_aluguel = (decimal)dias * aluguel.carro.valor_carro;

            aluguel.ativo_aluguel = false;
            _context.Update(aluguel);
            await _context.SaveChangesAsync();

            var carro = await _context.carro.FirstOrDefaultAsync(c => c.cod_carro == aluguel.carro.cod_carro);
            carro.alugado_carro = false;
            _context.Update(carro);
            await _context.SaveChangesAsync();




            return RedirectToAction("CarrosDisponiveis");
        }
    }
}
