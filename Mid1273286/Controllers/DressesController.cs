using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mid1273286.Models;
using Mid1273286.ViewModels.Input;

namespace Mid1273286.Controllers
{
    public class DressesController : Controller
    {
        private readonly DressDbContext db;
        private readonly IWebHostEnvironment env;
        public DressesController(DressDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Dresses.Include(x => x.Sales).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DressInputModel model)
        {
            if (ModelState.IsValid)
            {



                await db.Database.ExecuteSqlInterpolatedAsync($"EXEC InsertProduct {model.DressName}, {model.Price}, {(int)model.Size}, {model.Picture}, {(model.OnSale ? 1 : 0)}");
                var id = await db.Dresses.MaxAsync(x => x.DressId);
                foreach (var s in model.Sales)
                {
                    await db.Database.ExecuteSqlInterpolatedAsync($"EXEC InsertSale {s.Date}, {s.Quantity}, {id}");
                }
                return Json(new { success = true });

            }
            return Json(new { success = true });
        }
        public IActionResult GetSalesForm()
        {
            return PartialView("_SalesForm");
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(env.WebRootPath, "Pictures", fileName);
            FileStream fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return Json(new { name = fileName });
        }
    }
}
