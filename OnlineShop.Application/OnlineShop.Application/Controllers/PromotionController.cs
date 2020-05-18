﻿using System;
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
    public class PromotionController : Controller
    {
        private readonly ETDatabaseContext _context;

        public PromotionController(ETDatabaseContext context)
        {
            _context = context;
        }

        // GET: Promotion
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promotions.ToListAsync());
        }

        // GET: Promotion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions
                .FirstOrDefaultAsync(m => m.PromoId == id);
            if (promotions == null)
            {
                return NotFound();
            }

            return View(promotions);
        }

        // GET: Promotion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promotion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromoId,DiscountValue")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promotions);
        }

        // GET: Promotion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions.FindAsync(id);
            if (promotions == null)
            {
                return NotFound();
            }
            return View(promotions);
        }

        // POST: Promotion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromoId,DiscountValue")] Promotions promotions)
        {
            if (id != promotions.PromoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionsExists(promotions.PromoId))
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
            return View(promotions);
        }

        // GET: Promotion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions
                .FirstOrDefaultAsync(m => m.PromoId == id);
            if (promotions == null)
            {
                return NotFound();
            }

            return View(promotions);
        }

        // POST: Promotion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotions = await _context.Promotions.FindAsync(id);
            _context.Promotions.Remove(promotions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotionsExists(int id)
        {
            return _context.Promotions.Any(e => e.PromoId == id);
        }
    }
}
