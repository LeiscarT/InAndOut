using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

            public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;
            foreach(var obj in objList)
            {
                obj.ExpenseType = _db.ExpenseTypes.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }
            return View(objList);
        }

        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString()});
            //ViewBag.TypeDropDown = TypeDropDown;

            ExpenseViewModel expenseViewModel = new ExpenseViewModel()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseTypes.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }),
            };



            return View(expenseViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ExpenseViewModel obj)
        {
            if (ModelState.IsValid)
            {
                
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(obj);
         
        }

     
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //get update 

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(Expense obj)
        {
            if(obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }









































    }
}
