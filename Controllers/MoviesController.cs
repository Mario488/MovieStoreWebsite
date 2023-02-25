using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using move_store_app.Models;
using move_store_app.data;
using movestoreapp.Migrations;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using FluentAssertions.Equivalency;

namespace move_store_app.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesDbContext _context;

        public MoviesController(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Movies.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult PaymentConfirmation(string card_number, string expiration_date,
            string security_code, string name_on_card, string billing_address, string email,
            string phone, string Price)
        {
            bool[] arr = new bool[7];
            if (card_number?.Length == 16) { arr[0] = true; }
            if (expiration_date?.Length == 5){ arr[1] = true; }
            if(security_code?.Length == 3) { arr[2] = true; }
            if(name_on_card?.Length > 1) { arr[3] = true; }
            if(billing_address?.Length > 1) { arr[4] = true; }
            if(email != null)
            {
                if (email.Contains('@') && email.Length > 1) { arr[5] = true; }
            }
            if(phone?.Length == 10) { arr[6] = true; }
            ViewBag.Check = true;
            ViewBag.Price = Price;
            ViewBag.ED = expiration_date;
            ViewBag.TransactionID = new Random(Guid.NewGuid().GetHashCode()).Next(100000000, 999999999).ToString();
            foreach (var item in arr)
            {
                if(item != true)
                {
                    ViewBag.Check = false;
                    break;
                }
            }

            return View();
        }
        public IActionResult Purchase(string price)
        {
            ViewBag.Price = price;
            return View();
        }
        

        public IActionResult ShopCart(string movieTitle)
        {
            var data = new MoviesRepo(_context);

            var movies = data.GetMovies().Where(x => x.Title == movieTitle);
            ViewBag.List = movies;
            return View();
        }
        public IActionResult Movies(string movieName)
        {
            var data = new MoviesRepo(_context);
            if(movieName == null)
            {
                ViewBag.Key = data.GetMovies();
            }
            else
            {
                ViewBag.Key = data.GetMovies().Where(x => x.Title.ToLower().Contains($"{movieName.ToLower()}"));
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,RelDate,Genre,Description,Rating,Image,Price")] Models.Movies movies)
        {
            if (ModelState.IsValid)
            {
                movies.Id = Guid.NewGuid();
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,RelDate,Genre,Description,Rating,Image,Price")] Models.Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
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
            return View(movies);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MoviesDbContext.Movies'  is null.");
            }
            var movies = await _context.Movies.FindAsync(id);
            if (movies != null)
            {
                _context.Movies.Remove(movies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(Guid id)
        {
          return _context.Movies.Any(e => e.Id == id);
        }
    }
}
