using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL: IAdminBL
    {
        private readonly IAdminRepo adminRepo;
        public AdminBL(IAdminRepo adminRepo)
        {
            this.adminRepo = adminRepo;
        }
        public int AdminLogin(LoginModel model)
        {
            return adminRepo.AdminLogin(model);
        }
    }
}
