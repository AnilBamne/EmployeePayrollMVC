﻿using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public int AdminLogin(LoginModel model);
    }
}
