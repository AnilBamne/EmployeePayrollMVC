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
    }
}
