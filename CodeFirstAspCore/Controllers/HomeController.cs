using CodeFirstAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeFirstAspCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDB;

        public HomeController(StudentDBContext studentDB )
        {
            this.studentDB = studentDB;
        }

        public async Task<IActionResult> Index()
        {
            var data = await studentDB.Students.ToListAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student std)
        {
            if(ModelState.IsValid)
            {
               await studentDB.Students.AddAsync(std);
                await studentDB.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id== null  || studentDB.Students == null)
            {
                return NotFound();
            }
            var stddata = await studentDB.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (stddata == null)
            {
                return NotFound();
            }
            return View(stddata);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            var stddata = await studentDB.Students.FindAsync(id);
            return View(stddata);
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
