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
using System.Security.Claims;

namespace AppNt.Controllers
{
    public class VotesController : Controller
    {
        private readonly RankingDataBaseContext _context;

        public VotesController(RankingDataBaseContext context)
        {
            _context = context;
        }

        
         
        public IActionResult mostrarRanking()
        {
            int id = (int)TempData["indexMateria"];

            var votos = _context.Votes.Include(x => x.Profesor).Where(x => x.valueVote == true).Where(x => x.Profesor.AsignatureId == (int)TempData["indexMateria"]).ToList();
            var agruparPorProfesor = votos.GroupBy(x => x.Profesor).ToList();

            var mayores = agruparPorProfesor.OrderByDescending(x => x.Key.Vote.Count);
            int pos = 1;
            var listaVotosProfesor = new List<VotoProfesor>();
            foreach (var item in mayores)
            {
                listaVotosProfesor.Add(new VotoProfesor
                {
                    Id = pos,
                    Name = item.Key.Name,
                    QtyVotes = item.Key.Vote.Count()
                });

                pos++;
            }
            /*
             
            var votosProfesor = mayores.Select(x => new VotoProfesor {
                    Name = x.Key.Name,
                    QtyVotes = x.Key.Vote.Count(),

                })
                .ToList();
            */
            TempData["indexMateria"] = id;

            return View(listaVotosProfesor);
        }

        // GET: Votes
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<IActionResult> Index()
        {
            var rankingDataBaseContext = _context.Votes.Include(v => v.Profesor).Include(v => v.User);
            return View(await rankingDataBaseContext.ToListAsync());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Profesor)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

     [Authorize(Roles = "ESTUDIANTES")]
        // GET: Votes/Create
        public IActionResult Create()
        {
           int  id =(int) TempData["indexMateria"];

            var claimsUser = User.Claims;
            var usernameClaim = claimsUser.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;

            ViewData["ProfesorId"] = new SelectList(_context.Profesors.Where(x => x.AsignatureId == id), "Id", "Lastname");
            // ViewData["ProfesorId"] = new SelectList(_context.Profesors.Where(x=> x.AsignatureId == i), "Id", "Lastname");
           // ViewData["UserId"] = new SelectList(_context.Users.Where(x => x.Email == usernameClaim), "Id", "Email").FirstOrDefault();
          //  ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            TempData["indexMateria"] = id;
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [Authorize(Roles = "ESTUDIANTE")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProfesorId,valueVote")] Vote vote)
        {
            var claimsUser = User.Claims;
            var usernameClaim = claimsUser.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;

            User user = _context.Users.Where(x => x.Email == usernameClaim).FirstOrDefault();
            int userId = user.Id;
            vote.User = user;
            vote.UserId = userId;
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                   
                return RedirectToAction("IndexForStudents","Semester");
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesors, "Id", "Lastname", vote.ProfesorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", vote.UserId);
            return Redirect("/Semester/IndexForStudents");
        }

        // GET: Votes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["ProfesorId"] = new SelectList(_context.Profesors, "Id", "Lastname", vote.ProfesorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", vote.UserId);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProfesorId,valueVote")] Vote vote)
        {
            if (id != vote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.Id))
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
            ViewData["ProfesorId"] = new SelectList(_context.Profesors, "Id", "Lastname", vote.ProfesorId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", vote.UserId);
            return View(vote);
        }

        [Authorize(Roles = "ESTUDIANTE")]
        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Profesor)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.Id == id);
        }
    }
}
