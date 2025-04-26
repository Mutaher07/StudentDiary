using Microsoft.AspNetCore.Mvc;
using StudentDiary.Data;  // Добавено за достъп до AppDbContext
using StudentDiary.Models;
using System.Linq;

namespace StudentDiary.Controllers
{
    public class GradesController : Controller
    {
        private readonly AppDbContext _context;  // Променено на AppDbContext

        public GradesController(AppDbContext context)  // Променено на AppDbContext
        {
            _context = context;
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
                _context.Grades.Add(grade);  // Добавено към таблицата Grades
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // Страница за всички оценки
        public IActionResult Index()
        {
            var grades = _context.Grades.ToList();  // Вземане на всички оценки
            return View(grades);
        }

        // Страница за редактиране на оценка
        public IActionResult Edit(int id)
        {
            var grade = _context.Grades.Find(id);  // Намерете оценката по ID
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
                _context.Grades.Update(grade);  // Актуализиране на оценката в базата данни
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }
    }
}
