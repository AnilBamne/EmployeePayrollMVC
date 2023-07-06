using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class EmployeeBL: IEmployeeBL
    {
        private readonly IEmployeeRepo employeeRepo;
        public EmployeeBL(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }
        public EmpRegModel Register(EmpRegModel model)
        {
            try
            {
                return employeeRepo.Register(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<EmpRegModel> GetAllEmployees()
        {
            return employeeRepo.GetAllEmployees();
        }
    }
}
