using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mid1273286.Models;

namespace R54_M10_Class_09_Work_01.Controllers
{
    public class SalesController : Controller
    {
        private readonly DressDbContext db;
        public SalesController(DressDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sale model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlInterpolated($"EXEC InsertSale {model.Date}, {model.Quantity}, {model.ProductId}");
                return RedirectToAction("Index", "Products");

            }
            ViewBag.Products = db.Dresses.ToList();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var data = db.Sales.FirstOrDefault(x => x.SaleId == id);
            if (data == null) { return NotFound(); }
            ViewBag.Products = db.Dresses.ToList();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Sale model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlInterpolated($"EXEC UpdateSale {model.SaleId}, {model.Date}, {model.Quantity}, {model.ProductId}");
                return RedirectToAction("Index", "Products");

            }
            ViewBag.Products = db.Dresses.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteSale {id}");
            return Json(new { success = true, id });
        }

    }
}
