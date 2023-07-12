using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeStyle;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;

namespace EmployeePayrollAppMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var result = adminBL.AdminLogin(model);
            HttpContext.Session.SetInt32("UseerId", result);
            if (result == 1)
            {
                Console.WriteLine("Logged in");
                return RedirectToAction("GetAllEmployees","Employee");
            }
            else
            {
                return View(model);
            }
        }
    }
}
