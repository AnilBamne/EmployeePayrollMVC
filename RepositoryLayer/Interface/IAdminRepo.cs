using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepo
    {
        public int AdminLogin(LoginModel model);
    }
}
