using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonMvc.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace PersonMvc.Controllers
{
    public class HomeController : Controller
    {
        string ConStr = "Data Source = localhost;Initial Catalog = Person;Integrated Security=True;";
        public IActionResult Index()
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                var li = db.Query<Person>("Select * from Person").ToList();
                return View(li);
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person model)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                db.Query($"Insert into Person(FirstName,LastName,MiddleName) values('{model.FirstName}','{model.LastName}','{model.MiddleName}')");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult More(int id)
        {
            using (IDbConnection db = new SqlConnection(ConStr))
            {
                var li = db.Query<Person>($"Select * From Person where Id = {id}").ToList();
                var model = li.First();
                return View(model);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
