using ListPostDemo.Data;
using ListPostDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListPostDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress; Initial Catalog=FurnitureStore; Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostNumbers(List<int> numbers)
        {
            return View(new NumbersViewModel
            {
                Numbers = numbers
            });
        }

        public IActionResult ShowFurniture()
        {
            var db = new FurnitureManager(_connectionString);
            var vm = new ShowFurnitureViewModel
            {
                FurnitureItems = db.GetAll()
            };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);
        }

        public IActionResult ShowAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFurniture(List<FurnitureItem> items)
        {
            var db = new FurnitureManager(_connectionString);
            db.AddMany(items);
            TempData["message"] = $"{items.Count} added successfully!";
            return Redirect("/home/ShowFurniture");
        }
    }
}