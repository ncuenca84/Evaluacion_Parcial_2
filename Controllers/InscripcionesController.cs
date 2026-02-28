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
    public class InscripcionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InscripcionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inscripciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Inscripciones.Include(i => i.Participante).Include(i => i.Taller);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Inscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones
                .Include(i => i.Participante)
                .Include(i => i.Taller)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // GET: Inscripciones/Create
        public IActionResult Create()
        {
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido");
            ViewData["TallerId"] = new SelectList(_context.Talleres, "TallerId", "Nombre");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscripcionId,TallerId,ParticipanteId,FechaInscripcion")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcion.ParticipanteId);
            ViewData["TallerId"] = new SelectList(_context.Talleres, "TallerId", "Nombre", inscripcion.TallerId);
            return View(inscripcion);
        }

        // GET: Inscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcion.ParticipanteId);
            ViewData["TallerId"] = new SelectList(_context.Talleres, "TallerId", "Nombre", inscripcion.TallerId);
            return View(inscripcion);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,TallerId,ParticipanteId,FechaInscripcion")] Inscripcion inscripcion)
        {
            if (id != inscripcion.InscripcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionExists(inscripcion.InscripcionId))
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
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcion.ParticipanteId);
            ViewData["TallerId"] = new SelectList(_context.Talleres, "TallerId", "Nombre", inscripcion.TallerId);
            return View(inscripcion);
        }

        // GET: Inscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripciones
                .Include(i => i.Participante)
                .Include(i => i.Taller)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);
            if (inscripcion != null)
            {
                _context.Inscripciones.Remove(inscripcion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}
