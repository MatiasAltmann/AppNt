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
    public class AsignatureController : Controller
    {
        private readonly RankingDataBaseContext _context;

        public AsignatureController(RankingDataBaseContext context)
        {
            _context = context;
        }

        // GET: Asignature



        [Authorize(Roles = "ESTUDIANTES")]
        public IActionResult IndexUnSemestre(int id)
        {
            TempData["indexAsignature"] = id;
            var asignatures = _context.Asignatures.Where(a => a.Semester.Id == id).ToList();
            return View(asignatures);
        }

        // GET: Asignature
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Index()
        {
            var rankingDataBaseContext = _context.Asignatures.Include(a => a.Semester);
            return View(await rankingDataBaseContext.ToListAsync());
        }

        // GET: Asignature/Details/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignature = await _context.Asignatures
                .Include(a => a.Semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignature == null)
            {
                return NotFound();
            }

            return View(asignature);
        }

        // GET: Asignature/Create
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult Create()
        {
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name");
            return View();
        }

        // POST: Asignature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SemesterId")] Asignature asignature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name", asignature.SemesterId);
            return View(asignature);
        }

        // GET: Asignature/Edit/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignature = await _context.Asignatures.FindAsync(id);
            if (asignature == null)
            {
                return NotFound();
            }
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name", asignature.SemesterId);
            return View(asignature);
        }

        // POST: Asignature/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SemesterId")] Asignature asignature)
        {
            if (id != asignature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignatureExists(asignature.Id))
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
            ViewData["SemesterId"] = new SelectList(_context.Semesters, "Id", "Name", asignature.SemesterId);
            return View(asignature);
        }

        // GET: Asignature/Delete/5
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignature = await _context.Asignatures
                .Include(a => a.Semester)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignature == null)
            {
                return NotFound();
            }

            return View(asignature);
        }

        // POST: Asignature/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignature = await _context.Asignatures.FindAsync(id);
            _context.Asignatures.Remove(asignature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignatureExists(int id)
        {
            return _context.Asignatures.Any(e => e.Id == id);
        }
    }
}
