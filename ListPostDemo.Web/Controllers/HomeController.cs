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

//create an mvc application that displays a list of people. On top of the list
//have a link that takes you to a page that allows the user to enter multiple
//people. When that page loads, there should be one row of textboxes (first name,
//lastname and age) with a button on top that says "Add". When Add is clicked,
//another row of textboxes should appear. Beneath those textboxes, there should
//be a submit button, that when clicked, adds all those people to the database
//and then redirects the user back to the home page.