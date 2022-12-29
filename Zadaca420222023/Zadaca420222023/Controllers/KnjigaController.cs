using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zadaca420212022.Data;
using Zadaca420212022.Models;

namespace Zadaca420212022.Controllers
{
    public class KnjigaController : Controller
    {
        static List<Knjiga> knjige = new List<Knjiga>();

        public KnjigaController(DataContext context)
        {
        }

        // GET: Knjiga
        public IActionResult Index()
        {
            return View(knjige);
        }

        // GET: Knjiga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Knjiga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ISBN,NazivKnjige,ImeAutora,GodinaIzdavanja,EdicijaKnjige,OpisKnjige")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                knjige.Add(knjiga);
                return RedirectToAction(nameof(Index));
            }
            return View(knjiga);
        }
    }
}
