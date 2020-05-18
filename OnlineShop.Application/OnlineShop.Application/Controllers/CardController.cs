using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Backend.Models;
using OnlineShop.Application.Data;

namespace OnlineShop.Application.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class CardController : Controller
    {
        private readonly ETDatabaseContext _context;

        public CardController(ETDatabaseContext context)
        {
            _context = context;
        }

        // GET: Card
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cards.ToListAsync());
        }

        // GET: Card/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cards = await _context.Cards
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cards == null)
            {
                return NotFound();
            }

            return View(cards);
        }

        // GET: Card/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Card/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardId,CardNumber,NameOnCard,ExpirationDate,Cvv")] Cards cards)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cards);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cards);
        }

        // GET: Card/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cards = await _context.Cards.FindAsync(id);
            if (cards == null)
            {
                return NotFound();
            }
            return View(cards);
        }

        // POST: Card/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardId,CardNumber,NameOnCard,ExpirationDate,Cvv")] Cards cards)
        {
            if (id != cards.CardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardsExists(cards.CardId))
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
            return View(cards);
        }

        // GET: Card/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cards = await _context.Cards
                .FirstOrDefaultAsync(m => m.CardId == id);
            if (cards == null)
            {
                return NotFound();
            }

            return View(cards);
        }

        // POST: Card/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cards = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(cards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardsExists(int id)
        {
            return _context.Cards.Any(e => e.CardId == id);
        }
    }
}
