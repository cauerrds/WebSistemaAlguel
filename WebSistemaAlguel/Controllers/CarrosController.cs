using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSistemaAlguel.Data;
using WebSistemaAlguel.Models;

namespace WebSistemaAlguel.Controllers

{
    [Authorize(Roles ="Admin")]
    public class CarrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carros

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.carro.Include(c => c.fabricante);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Carros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.carro == null)
            {
                return NotFound();
            }

            var carro = await _context.carro
                .Include(c => c.fabricante)
                .FirstOrDefaultAsync(m => m.cod_carro == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carros/Create
        public IActionResult Create()
        {
            ViewData["cod_fabricante"] = new SelectList(_context.fabricantes, "cod_fabricante", "nome_fabricante");
            return View();
        }

        // POST: Carros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cod_carro,modelo_carro,valor_carro,ano_carro,alugado_carro,cod_fabricante")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cod_fabricante"] = new SelectList(_context.fabricantes, "cod_fabricante", "nome_fabricante", carro.cod_fabricante);
            return View(carro);
        }

        // GET: Carros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.carro == null)
            {
                return NotFound();
            }

            var carro = await _context.carro.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewData["cod_fabricante"] = new SelectList(_context.fabricantes, "cod_fabricante", "nome_fabricante", carro.cod_fabricante);
            return View(carro);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cod_carro,modelo_carro,valor_carro,ano_carro,alugado_carro,cod_fabricante")] Carro carro)
        {
            if (id != carro.cod_carro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.cod_carro))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["cod_fabricante"] = new SelectList(_context.fabricantes, "cod_fabricante", "nome_fabricante", carro.cod_fabricante);
            return View(carro);
        }

        // GET: Carros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.carro == null)
            {
                return NotFound();
            }

            var carro = await _context.carro
                .Include(c => c.fabricante)
                .FirstOrDefaultAsync(m => m.cod_carro == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.carro == null)
            {
                return Problem("Entity set 'ApplicationDbContext.carro'  is null.");
            }
            var carro = await _context.carro.FindAsync(id);
            if (carro != null)
            {
                _context.carro.Remove(carro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
          return _context.carro.Any(e => e.cod_carro == id);
        }
    }
}
