using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;  // Необходимо за работа с Entity Framework
using StudentDiary.Data;  // Добавено за достъп до AppDbContext
using StudentDiary.Models;
using System.Linq;

namespace StudentDiary.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;  // Променено на AppDbContext

        public StudentsController(ApplicationDbContext context)  // Променено на AppDbContext
        {
            _context = context;
        }

        // Страница за регистрация на ученик
        public IActionResult Create()
        {
            return View();
        }

        // Пост заявка за създаване на ученик
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);  // Добавено към таблицата Students
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // Страница за списък с всички ученици
        public IActionResult Index()
        {
            var students = _context.Students.ToList();  // Взимаме всички студенти
            return View(students);
        }

        // Страница за детайли на ученик
        public IActionResult Details(int id)
        {
            var student = _context.Students.Find(id);  // Намерете ученика по ID
            if (student == null)
            {
                return NotFound();  // Ако не е намерен, върнете 404
            }
            return View(student);
        }
    }
}
