using BusinessLayer.Interface;
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
    }
}
