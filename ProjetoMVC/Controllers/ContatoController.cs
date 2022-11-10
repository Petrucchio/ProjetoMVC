using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        // GET: Contato
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contatos.ToListAsync());
        }

        // GET: Contato/Detalhes/5
        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        // GET: Contato/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Contato/Criar
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more Detalhes, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Id,Nome,Telefone,Ativo")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        // GET: Contato/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        // POST: Contato/Editar/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more Detalhes, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Nome,Telefone,Ativo")] Contato contato)
        {
            if (id != contato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoExists(contato.Id))
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
            return View(contato);
        }

        // GET: Contato/Deletar/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null || _context.Contatos == null)
            {
                return NotFound();
            }

            var contato = await _context.Contatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        // POST: Contato/Deletar/5
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmed(int id)
        {
            if (_context.Contatos == null)
            {
                return Problem("Entity set 'AgendaContext.Contatos'  is null.");
            }
            var contato = await _context.Contatos.FindAsync(id);
            if (contato != null)
            {
                _context.Contatos.Remove(contato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoExists(int id)
        {
          return _context.Contatos.Any(e => e.Id == id);
        }
    }
}
