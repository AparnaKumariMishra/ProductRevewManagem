using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EmployeeManagment;

namespace EmployeeManagement
{
    public class Salary
    {
        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=payroll_service;Integrated Security=True");

        }

        public int UpdateEmployeeSalary(SalaryUpdateModel salaryUpdateModel)
        {
            SqlConnection salaryConnection = ConnectionSetup();
            int salary = 0;
            try
            {

                using (salaryConnection)
                {
                    SalaryDetailModel dispalymodel = new SalaryDetailModel();
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", salaryConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", salaryUpdateModel.SalaryId);
                    command.Parameters.AddWithValue("@month", salaryUpdateModel.Month);
                    command.Parameters.AddWithValue("@salary", salaryUpdateModel.EmployeeSalary);
                    command.Parameters.AddWithValue("@empId", salaryUpdateModel.EmployeeId);
                    salaryConnection.Open();
                    SqlDataReader dr = command.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dispalymodel.EmployeeId = dr.GetInt32(0);
                            dispalymodel.EmployeeName = dr.GetString(1);
                            dispalymodel.JobDescription = dr.GetString(2);
                            dispalymodel.EmployeeSalary = dr.GetInt32(3);
                            dispalymodel.Month = dr.GetString(4);
                            dispalymodel.SalaryId = dr.GetInt32(5);

                            Console.WriteLine(dispalymodel.EmployeeName + " - " + dispalymodel.JobDescription + " - " + dispalymodel.EmployeeSalary);
                            salary = dispalymodel.EmployeeSalary;
                        }

                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dr.Close();
                    salaryConnection.Close();



                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                salaryConnection.Close();
            }
            return salary;
        }
    }
}