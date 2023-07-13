using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AdminRepo:IAdminRepo
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection = new SqlConnection();
        private readonly string ConnString;
        public AdminRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnString = configuration.GetConnectionString("EmployeePayrollDB");
            connection.ConnectionString = ConnString;
        }
        //login using empId and EmpName
        public int AdminLogin(LoginModel model)
        {
            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAdminLogin", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Id", model.Id);
                    command.Parameters.AddWithValue("@Name", model.Name);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            int AndminId = Convert.ToInt32(rdr["Id"]);
                            // rdr.GetInt32(0);
                            return AndminId;
                        }
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { connection.Close(); }
        }
    }
}
