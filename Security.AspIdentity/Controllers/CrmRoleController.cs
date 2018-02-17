using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.AspIdentity.Core;
using Security.AspIdentity.Models.Core;

namespace Security.AspIdentity.Controllers
{
    public class CrmRoleController : Controller
    {
        private readonly RoleManager<CrmRole> _roleManager;

        public CrmRoleController(AppIdentityDbContext context, RoleManager<CrmRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: CrmRole
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: CrmRole/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmRole = await _roleManager.Roles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crmRole == null)
            {
                return NotFound();
            }

            return View(crmRole);
        }

        // GET: CrmRole/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CrmRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] CrmRole crmRole)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(crmRole);
                return RedirectToAction(nameof(Index));
            }
            return View(crmRole);
        }

        // GET: CrmRole/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmRole = await _roleManager.Roles.SingleOrDefaultAsync(m => m.Id == id);
            if (crmRole == null)
            {
                return NotFound();
            }
            return View(crmRole);
        }

        // POST: CrmRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,Id,Name")] CrmRole crmRole)
        {
            if (id != crmRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roleManager.UpdateAsync(crmRole);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CrmRoleExists(crmRole.Id))
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
            return View(crmRole);
        }

        // GET: CrmRole/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crmRole = await _roleManager.Roles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (crmRole == null)
            {
                return NotFound();
            }

            return View(crmRole);
        }

        // POST: CrmRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var crmRole = await _roleManager.Roles.SingleOrDefaultAsync(m => m.Id == id);
            await _roleManager.DeleteAsync(crmRole);
            return RedirectToAction(nameof(Index));
        }

        private bool CrmRoleExists(string id)
        {
            return _roleManager.Roles.Any(e => e.Id == id);
        }
    }
}
