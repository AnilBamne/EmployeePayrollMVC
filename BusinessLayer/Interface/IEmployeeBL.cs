using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IEmployeeBL
    {
        public EmpRegModel Register(EmpRegModel model);
        public IEnumerable<EmpRegModel> GetAllEmployees();
        public EmpRegModel UpdateEmployee(EmpRegModel model);
        public EmpRegModel GetEmpDetails(int? Id);
        public void DeleteEmployee(int id);
        public int EmployeeLogin(LoginModel model);
    }
}
