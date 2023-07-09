using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EmployeePayrollAppMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Add employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(EmpRegModel model)
        {
            try
            {
                employeeBL.Register(model);
                //return View(model);
                return RedirectToAction("GetAllEmployees");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult GetAllEmployees()
        {
            try
            {
                List<EmpRegModel> list = new List<EmpRegModel>();
                list=employeeBL.GetAllEmployees().ToList();
                return View(list);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update emp details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmpRegModel employee = employeeBL.GetEmpDetails(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] EmpRegModel employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                employeeBL.UpdateEmployee(employee);
                return RedirectToAction("GetAllEmployees");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmpRegModel employee = employeeBL.GetEmpDetails(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmpRegModel employee = employeeBL.GetEmpDetails(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            employeeBL.DeleteEmployee(id);
            return RedirectToAction("GetAllEmployees");
        }
    }
}
