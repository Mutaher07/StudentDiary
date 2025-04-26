using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDiary.Data;  // Добавено за достъп до AppDbContext
using StudentDiary.Data.Common;
using StudentDiary.Models;
using System.Linq;

namespace StudentDiary.Controllers
{
    public class GradesController : Controller
    {
        private readonly ApplicationDbContext context;  // Променено на AppDbContext
        private readonly IRepository repo;

        public GradesController(ApplicationDbContext _context,
                                IRepository _repo)  // Променено на AppDbContext
        {
            context = _context;
            repo = _repo;
        }

        // Страница за въвеждане на оценка
        public IActionResult Create(int studentId)
        {
            ViewBag.StudentId = studentId;
            return View();
        }

        // Пост заявка за създаване на оценка
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                context.Grades.Add(grade);  // Добавено към таблицата Grades
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // Страница за всички оценки
        public async Task<IActionResult> Index()
        {
            var model = await repo.AllReadonly<Grade>().ToListAsync();
            return View(model);
        }

        // Страница за редактиране на оценка
        public IActionResult Edit(int id)
        {
            var grade = context.Grades.Find(id);  // Намерете оценката по ID
            if (grade == null)
            {
                return NotFound();  // Ако не е намерена, върнете 404
            }
            return View(grade);
        }

        // Пост заявка за редактиране на оценка
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                context.Grades.Update(grade);  // Актуализиране на оценката в базата данни
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }
    }
}
