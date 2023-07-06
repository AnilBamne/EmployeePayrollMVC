using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmployeeRepo: IEmployeeRepo
    {
        private readonly IConfiguration configuration;
        private readonly SqlConnection connection = new SqlConnection();
        private readonly string ConnectionString;
        public EmployeeRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetConnectionString("EmployeePayrollDB");
            connection.ConnectionString = ConnectionString;
        }

        //Register employee
        public EmpRegModel Register(EmpRegModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("spAddEmployee", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@ProfileImage", model.ProfileImage);
                    cmd.Parameters.AddWithValue("@Gender", model.Gender);
                    cmd.Parameters.AddWithValue("@Department", model.Department);
                    cmd.Parameters.AddWithValue("@Salary", model.Salary);
                    cmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                    cmd.Parameters.AddWithValue("@Notes", model.Notes);
                    connection.Open();
                    var count = cmd.ExecuteNonQuery();
                    if (count != 0)
                    {
                        return model;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.Close();
            }
        }

        //GetAllEmployees
        public IEnumerable<EmpRegModel> GetAllEmployees()
        {
            try
            {
                List<EmpRegModel> list = new List<EmpRegModel>();

                using (connection)
                {
                    SqlCommand cmd = new SqlCommand("spGetAllEmployees", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        EmpRegModel employee = new EmpRegModel();

                        employee.Id = Convert.ToInt32(rdr["Id"]);
                        employee.Name = rdr["Name"].ToString();
                        employee.ProfileImage = rdr["ProfileImage"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Department = rdr["Department"].ToString();
                        employee.Salary = Convert.ToInt32(rdr["Salary"]);
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = rdr["Notes"].ToString();

                        list.Add(employee);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}
