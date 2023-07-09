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
        /// <summary>
        /// Update records of a perticular employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EmpRegModel UpdateEmployee(EmpRegModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("EmpId", model.Id);
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

        public EmpRegModel GetEmpDetails(int? Id)
        {
            try
            {
                EmpRegModel employee = new EmpRegModel();
                using (connection)
                {
                    string sqlQuery = "SELECT * FROM EmployeeTable WHERE Id= " + Id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                    connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        employee.Id = Convert.ToInt32(rdr["Id"]);
                        employee.Name = rdr["Name"].ToString();
                        employee.ProfileImage = rdr["ProfileImage"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Department = rdr["Department"].ToString();
                        employee.Salary = Convert.ToInt32(rdr["Salary"]);
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = rdr["Notes"].ToString();

                    }
                }
                return employee;
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

        //Write a method to add and update in mvc. If empid exist update the data else add the data.
        public EmpRegModel GetById(int id)
        {
            EmpRegModel employee = new EmpRegModel();
            using (connection)
            {
                string sqlQuery = "SELECT * FROM EmployeeTable WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        employee.Id = Convert.ToInt32(rdr["Id"]);
                        employee.Name = rdr["Name"].ToString();
                        employee.ProfileImage = rdr["ProfileImage"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Department = rdr["Department"].ToString();
                        employee.Salary = Convert.ToInt32(rdr["Salary"]);
                        employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                        employee.Notes = rdr["Notes"].ToString();
                        //If employee id is present then update the info
                        if(employee != null)
                        {
                            SqlCommand command = new SqlCommand("spUpdateEmployee", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("EmpId", employee.Id);
                            command.Parameters.AddWithValue("@Name", employee.Name);
                            command.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                            command.Parameters.AddWithValue("@Gender", employee.Gender);
                            command.Parameters.AddWithValue("@Department", employee.Department);
                            command.Parameters.AddWithValue("@Salary", employee.Salary);
                            command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                            command.Parameters.AddWithValue("@Notes", employee.Notes);
                            connection.Open();
                            var count = cmd.ExecuteNonQuery();
                            if (count != 0)
                            {
                                return employee;
                            }
                            return null;
                        }
                    }
                }
                else
                {
                    //records not found then create new record
                    SqlCommand command = new SqlCommand("spAddEmployee", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@ProfileImage", employee.ProfileImage);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    command.Parameters.AddWithValue("@Notes", employee.Notes);
                    connection.Open();
                    var count = cmd.ExecuteNonQuery();
                    if (count != 0)
                    {
                        return employee;
                    }
                    return null;
                }
            }
            connection.Close();
            return employee;
        }
        //To Delete the record on a particular employee    
        public void DeleteEmployee(int id)
        {
            using (connection)
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpId", id);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
