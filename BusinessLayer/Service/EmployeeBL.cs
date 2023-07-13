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
            try
            {
                return employeeRepo.GetAllEmployees();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmpRegModel UpdateEmployee(EmpRegModel model)
        {
            try
            {
                return employeeRepo.UpdateEmployee(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public EmpRegModel GetEmpDetails(int? Id)
        {
            return employeeRepo.GetEmpDetails(Id);
        }
        public void DeleteEmployee(int id)
        {
            employeeRepo.DeleteEmployee(id);
        }
        public int EmployeeLogin(LoginModel model)
        {
            return employeeRepo.EmployeeLogin(model);
        }
    }
}
