using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Evaluacion_Parcial_2.Data;
using Evaluacion_Parcial_2.Models;

namespace Evaluacion_Parcial_2.Controllers
{
    public class TalleresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TalleresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Talleres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Talleres.ToListAsync());
        }

        // GET: Talleres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller = await _context.Talleres
                .FirstOrDefaultAsync(m => m.TallerId == id);
            if (taller == null)
            {
                return NotFound();
            }

            return View(taller);
        }

        // GET: Talleres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Talleres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TallerId,Nombre,Descripcion,Fecha,Ubicacion")] Taller taller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taller);
        }

        // GET: Talleres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller = await _context.Talleres.FindAsync(id);
            if (taller == null)
            {
                return NotFound();
            }
            return View(taller);
        }

        // POST: Talleres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TallerId,Nombre,Descripcion,Fecha,Ubicacion")] Taller taller)
        {
            if (id != taller.TallerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TallerExists(taller.TallerId))
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
            return View(taller);
        }

        // GET: Talleres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taller = await _context.Talleres
                .FirstOrDefaultAsync(m => m.TallerId == id);
            if (taller == null)
            {
                return NotFound();
            }

            return View(taller);
        }

        // POST: Talleres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);
            if (taller != null)
            {
                _context.Talleres.Remove(taller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TallerExists(int id)
        {
            return _context.Talleres.Any(e => e.TallerId == id);
        }
    }
}
