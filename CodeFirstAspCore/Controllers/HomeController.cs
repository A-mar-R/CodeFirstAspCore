using CodeFirstAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
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
                TempData["success"] = "Student created successfully!";
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public async Task<IActionResult> Details(int? id)
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
            if(id == null || studentDB.Students == null)
            {
                return NotFound();
            }
            var stddata = await studentDB.Students.FindAsync(id);  
            if(stddata == null)
            {
                return NotFound();
            }
            return View(stddata);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student std)
        {
            if(id != std.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                studentDB.Students.Update(std);
                TempData["Update"] = "Student updated successfully!";
                await studentDB.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(std);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || studentDB.Students == null)
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await studentDB.Students.FindAsync(id);
            if (student != null)
            {
                studentDB.Students.Remove(student);
                
            }
            await studentDB.SaveChangesAsync();
            TempData["delete"] = "Student deleted successfully!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Print()
        {
            var data = await studentDB.Students.ToListAsync();

            return new ViewAsPdf("Print", data)
            {
                FileName = "Students.pdf"
            };
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
