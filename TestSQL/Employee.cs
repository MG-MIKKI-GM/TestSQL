using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQL
{
    public class Employee
    {
        /// <summary> ФИО </summary>
        public string Name { get; set; }
        /// <summary> Дата рождения </summary>
        public DateTime Birthday { get; set; }
        /// <summary> Пол </summary>
        public string Gender { get; set; }

        public Employee(string name, DateTime birthday, string gender) 
        {
            Name = name;
            Birthday = birthday;
            Gender = gender;
        }

        public override string ToString()
        {
            return Name.PadRight(40) + Birthday.ToShortDateString().PadRight(25) + GetAge();
        }

        /// <summary>
        /// Возвращает возраст сотрудника
        /// </summary>
        /// <returns>Полный возраст</returns>
        public int GetAge()
        {
            return DateTime.Now.Year - Birthday.Year;
        }

        /// <summary>
        /// SQL код для добавления сотрудника в таблицу БД
        /// </summary>
        /// <returns>SQL код</returns>
        public string GetSQL_AddEmployee()
        {
            return $"INSERT Employee VALUES ('{Name}','{Birthday.Month}/{Birthday.Day}/{Birthday.Year}','{Gender}')";
        }

        /// <summary>
        /// SQL код для добавления сотрудников в таблицу БД
        /// </summary>
        /// <param name="employees">Сотрудники</param>
        /// <returns>SQL код</returns>
        public static string GetSQL_AddEmployees(Employee[] employees)
        {
            string request = "INSERT Employee VALUES";
            foreach (Employee employee in employees)
            {
                request += $" ('{employee.Name}','{employee.Birthday.Month}/{employee.Birthday.Day}/{employee.Birthday.Year}','{employee.Gender}'),";
            }

            return request.TrimEnd(',');
        }

        /// <summary>
        /// SQL код для добавления сотрудника в таблицу БД
        /// </summary>
        /// <returns>SQL код</returns>
        public static string GetSQL_SelectEmployees(string Filter = "")
        {
            return "SELECT DISTINCT Name, Birthday, Gender FROM Employee " + Filter;
        }

        /// <summary>
        /// SQL код для создания таблицы в БД
        /// </summary>
        /// <returns>SQL код</returns>
        public static string GetSQL_CreatTableEmployee()
        {
            return "DROP TABLE IF EXISTS [dbo].[Employee];CREATE TABLE [dbo].[Employee]([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,[Name] NVARCHAR(45) NOT NULL,[Birthday] DATE NOT NULL,[Gender] NVARCHAR(6) NOT NULL)";
        }
    }
}
