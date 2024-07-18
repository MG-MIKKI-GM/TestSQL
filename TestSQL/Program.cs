using System.Data;
using Microsoft.Data.SqlClient;
using TestSQL;

public class Program
{
    private SqlConnection sqlConnection;
    private SqlCommand sqlCommand;

    public Program(string[] args)
    {
        OpenSQL();

        DateTime startTime = DateTime.Now;
        switch (args[0])
        {
            case "1":
                SendRequest(Employee.GetSQL_CreatTableEmployee());
                Console.WriteLine("Таблица \"Employee\" была успешно создана!");
                break;
            case "2":
                if(args.Length < 4)
                {
                    Console.WriteLine("Вы не указали данные Сотрудника");
                    return;
                }
                try
                {
                    Employee employee = new Employee(args[1], DateTime.Parse(args[2]), args[3]);
                    SendRequest(employee.GetSQL_AddEmployee());
                    Console.WriteLine("Строчка добавлена в таблицу базы данных!");
                }
                catch
                {
                    Console.WriteLine("Ошибка при добавлении строчки в таблицу базы данных!");
                }
                break;
            case "3":
                SqlDataReader sqlData = SendRequestReader(Employee.GetSQL_SelectEmployees("ORDER BY Name"));
                Console.WriteLine("Name".PadRight(40) + "Birthday".PadRight(25) + "Gender");
                while (sqlData.Read())
                {
                    Employee employee1 = new Employee(sqlData.GetString(0), sqlData.GetDateTime(1), sqlData.GetString(2));
                    Console.WriteLine(employee1);
                }
                break;
            case "4":
                for(int i = 1; i <= 10000; i++)
                {
                    string name;
                    DateTime birthday;

                    if (i % 100 == 0)
                    {
                        Console.Clear();
                        Console.WriteLine((int)(i / 100) + "%");
                    }

                    try
                    {
                        name = Faker.Name.FullName(Faker.NameFormats.StandardWithMiddle).Replace("'", "");
                        birthday = new DateTime(1970, 1, 1).AddDays(new Random().Next(365 * 37));
                        Employee employee2 = new Employee(name, birthday, i % 2 == 0 ? "Male" : "Female");
                        SendRequest(employee2.GetSQL_AddEmployee());
                    }
                    catch
                    {
                    }
                }
                Console.WriteLine("Данные добавлены");
                break;
            case "5":
                SqlDataReader sqlData1 = SendRequestReader(Employee.GetSQL_SelectEmployees("WHERE Name LIKE 'F%' AND Gender = 'Male'"));
                Console.WriteLine("Name".PadRight(40) + "Birthday".PadRight(25) + "Gender");
                while (sqlData1.Read())
                {
                    Employee employee1 = new Employee(sqlData1.GetString(0), sqlData1.GetDateTime(1), sqlData1.GetString(2));
                    Console.WriteLine(employee1);
                }
                break;
        }

        DateTime endTime = DateTime.Now;
        Console.WriteLine("\n" + (endTime - startTime).TotalSeconds + " cекунд");
    }

    private void SendRequest(string sqlText)
    {
        sqlCommand.CommandText = sqlText;
        sqlCommand.ExecuteNonQuery();
    }

    private SqlDataReader SendRequestReader(string sqlText)
    {
        sqlCommand.CommandText = sqlText;
        return sqlCommand.ExecuteReader();
    }

    private void OpenSQL()
    {
        string connectString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\source\\repos\\TestSQL\\TestSQL\\DatabaseTest.mdf;Integrated Security=True";
        sqlConnection = new SqlConnection(connectString);
        sqlConnection.Open();

        sqlCommand = new SqlCommand()
        {
            Connection = sqlConnection,
        };
    }

    private static void Main(string[] args)
    {
        //args = new string[] { "1" };

        if (args.Length == 0)
        {
           Console.Write("Вы не указали параметр!");
           return;
        }

        new Program(args);
    }
}
