using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppNt.Models;
using RankingProfesores.Context;
using Microsoft.AspNetCore.Authorization;

namespace AppNt.Controllers
{
    public class Profesors1Controller : Controller
    {
        private readonly RankingDataBaseContext _context;

        public Profesors1Controller(RankingDataBaseContext context)
        {
            _context = context;
        }

        // GET: Profesors1

        [HttpGet]
        public IActionResult IndexProfesoresMateria(int id)
        {
            TempData["indexMateria"] = id; //Faltaria ver q pasa cuando vuelve para atras
            var profesores = _context.Profesors.Where(x => x.AsignatureId == id).ToList();


            return View(profesores);
        }


        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Index()
        {
            var rankingDataBaseContext = _context.Profesors.Include(p => p.Asignature);
            return View(await rankingDataBaseContext.ToListAsync());
        }

        // GET: Profesors1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors
                .Include(p => p.Asignature)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

      [Authorize(Roles = "ADMINISTRADOR")]
        // GET: Profesors1/Create
        public IActionResult Create()
        {
            ViewData["AsignatureId"] = new SelectList(_context.Asignatures, "Id", "Name");
            return View();
        }

        // POST: Profesors1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Lastname,Age,Photo,AsignatureId")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profesor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignatureId"] = new SelectList(_context.Asignatures, "Id", "Name", profesor.AsignatureId);
            return View(profesor);
        }
        [Authorize(Roles = "ADMINISTRADOR")]
        // GET: Profesors1/Edit/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            ViewData["AsignatureId"] = new SelectList(_context.Asignatures, "Id", "Name", profesor.AsignatureId);
            return View(profesor);
        }

        // POST: Profesors1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Lastname,Age,Photo,AsignatureId")] Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profesor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["AsignatureId"] = new SelectList(_context.Asignatures, "Id", "Name", profesor.AsignatureId);
            return View(profesor);
        }

        // GET: Profesors1/Delete/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors
                .Include(p => p.Asignature)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesors1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesor = await _context.Profesors.FindAsync(id);
            _context.Profesors.Remove(profesor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesors.Any(e => e.Id == id);
        }
    }
}
