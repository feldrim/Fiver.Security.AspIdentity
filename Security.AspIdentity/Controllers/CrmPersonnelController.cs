using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.AspIdentity.Core;
using Security.AspIdentity.Models.Business;

namespace Security.AspIdentity.Controllers
{
    [Authorize]
    public class CrmPersonnelController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public CrmPersonnelController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: CrmPersonnel
        public async Task<IActionResult> Index()
        {
            return View(await _context.CrmPersonnel.ToListAsync());
        }

        // GET: CrmPersonnel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var crmPersonnel = await _context.CrmPersonnel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crmPersonnel == null) return NotFound();

            return View(crmPersonnel);
        }

        // GET: CrmPersonnel/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrmPersonnel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] CrmPersonnel crmPersonnel)
        {
            if (!ModelState.IsValid) return View(crmPersonnel);
            _context.Add(crmPersonnel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: CrmPersonnel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var crmPersonnel = await _context.CrmPersonnel.SingleOrDefaultAsync(m => m.Id == id);
            if (crmPersonnel == null) return NotFound();
            return View(crmPersonnel);
        }

        // POST: CrmPersonnel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] CrmPersonnel crmPersonnel)
        {
            if (id != crmPersonnel.Id) return NotFound();

            if (!ModelState.IsValid) return View(crmPersonnel);
            try
            {
                _context.Update(crmPersonnel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrmPersonnelExists(crmPersonnel.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: CrmPersonnel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var crmPersonnel = await _context.CrmPersonnel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crmPersonnel == null) return NotFound();

            return View(crmPersonnel);
        }

        // POST: CrmPersonnel/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crmPersonnel = await _context.CrmPersonnel.SingleOrDefaultAsync(m => m.Id == id);
            _context.CrmPersonnel.Remove(crmPersonnel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrmPersonnelExists(int id)
        {
            return _context.CrmPersonnel.Any(e => e.Id == id);
        }
    }
}