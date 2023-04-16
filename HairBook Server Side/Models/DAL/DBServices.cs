using System.Data.SqlClient;
using System.Data;
using HairBook_Server_Side.Models;
using Microsoft.VisualBasic;
using System.Globalization;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HairBook_Server_Side.Models.DAL
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBServices()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }


        //--------------------------------------------------------------------------------------------------
        // This method inserts a Client to the client table 
        //--------------------------------------------------------------------------------------------------
        public int InsertClient(Client client)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateInsertClientCommandWithStoredProcedure("spInsertClient", con, client);// create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Client SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertClientCommandWithStoredProcedure(String spName, SqlConnection con, Client client)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@firstName", client.FirstName);

            cmd.Parameters.AddWithValue("@lastName", client.LastName);

            cmd.Parameters.AddWithValue("@phoneNum", client.PhoneNum);

            cmd.Parameters.AddWithValue("@image", client.Image);

            cmd.Parameters.AddWithValue("@birthDate", client.BirthDate );

            cmd.Parameters.AddWithValue("@gender", client.Gender);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Clients by Phone number
        //--------------------------------------------------------------------------------------------------
        public int GetCode(string phoneNum)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadClientCommandSP("spGetClient", con, phoneNum);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Random rand = new Random();
                int code = rand.Next(1000, 9999); ;
                while (dataReader.Read())
                {

                    const string accountSid = "AC19a8ff3d22acff9a3caa5900baa0e56f";
                    const string authToken = "46dc1d84da3c03c33b852fd63e0aeaf6";

                    TwilioClient.Init(accountSid, authToken);
                    var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("whatsapp:+972" + phoneNum.Substring(1)));
                    messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
                    messageOptions.Body = "Your HairBook code is " + code;
                    var message = MessageResource.Create(messageOptions);
                    Console.WriteLine(message.Body);
                }
                    return code;

            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // This method read Clients by Phone number
        //--------------------------------------------------------------------------------------------------
        public Client ReadClient(string phoneNum)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadClientCommandSP("spGetClient", con, phoneNum);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                Client client = new Client();

                while (dataReader.Read())
                {
                    client.FirstName = dataReader["firstName"].ToString();
                    client.LastName = dataReader["lastName"].ToString();
                    client.PhoneNum = dataReader["phoneNum"].ToString();
                    client.BirthDate = ((DateTime)dataReader["birthDate"]);
                    client.Image = dataReader["image"].ToString();
                    client.Gender = dataReader["gender"].ToString();
                }
                return client;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }



        //---------------------------------------------------------------------------------
        // Create the Read Client SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadClientCommandSP(String spName, SqlConnection con, string phoneNum)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method inserts a Client to the client table 
        //--------------------------------------------------------------------------------------------------
        public int InsertEmployee(Employee employee)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateInsertEmployeeCommandWithStoredProcedure("spInsertEmployee", con, employee);// create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Employee SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertEmployeeCommandWithStoredProcedure(String spName, SqlConnection con, Employee employee)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@firstName", employee.FirstName);

            cmd.Parameters.AddWithValue("@lastName", employee.LastName);

            cmd.Parameters.AddWithValue("@phoneNum", employee.PhoneNum);

            cmd.Parameters.AddWithValue("@image", employee.Image);

            cmd.Parameters.AddWithValue("@password", employee.Password);

            cmd.Parameters.AddWithValue("@empolyeeType", employee.EmpolyeeType);

            cmd.Parameters.AddWithValue("@startDate", (employee.StartDate).Date);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Employee by Phone number and Password
        //--------------------------------------------------------------------------------------------------
        public Employee ReadEmployee(string phoneNum,string password)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadEmployeeCommandSP("spGetEmployee", con, phoneNum, password);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Employee emp = new Employee();

                while (dataReader.Read())
                {

                    emp.FirstName = dataReader["firstName"].ToString();
                    emp.LastName = dataReader["lastName"].ToString();
                    emp.PhoneNum = dataReader["phoneNum"].ToString();
                    emp.Image = dataReader["image"].ToString();
                    emp.EmployeeNum = Convert.ToInt32(dataReader["employeeNum"]);
                    emp.EmpolyeeType = dataReader["employeeType"].ToString();
                    emp.Password = dataReader["password"].ToString();
                    emp.StartDate = ((DateTime)dataReader["startDate"]);
                }
                return emp;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the Read Employee SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadEmployeeCommandSP(String spName, SqlConnection con, string phoneNum, string password)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@password", password);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all Employee by Phone number and Password
        //--------------------------------------------------------------------------------------------------
        public List<Employee> ReadAllEmployees()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadAllEmployeesCommandSP("GetAllEmployees", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Employee> employees = new List<Employee>();

                while (dataReader.Read())
                {
                    Employee emp = new Employee();
                    emp.FirstName = dataReader["firstName"].ToString();
                    emp.LastName = dataReader["lastName"].ToString();
                    emp.PhoneNum = dataReader["phoneNum"].ToString();
                    emp.Image = dataReader["image"].ToString();
                    emp.EmployeeNum = Convert.ToInt32(dataReader["employeeNum"]);
                    emp.EmpolyeeType = dataReader["employeeType"].ToString();
                    emp.Password = dataReader["password"].ToString();
                    emp.StartDate = ((DateTime)dataReader["startDate"]);
                    employees.Add(emp);
                }
                return employees;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the Read Employee SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadAllEmployeesCommandSP(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a product to the product table 
        //--------------------------------------------------------------------------------------------------
        public int InsertProduct(Product product)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateInsertProductCommandWithStoredProcedure("spInsertProduct", con, product);// create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Product SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertProductCommandWithStoredProcedure(String spName, SqlConnection con, Product product)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@name", product.ProductName);

            cmd.Parameters.AddWithValue("@description", product.Description);

            cmd.Parameters.AddWithValue("@price", product.Price);

            cmd.Parameters.AddWithValue("@seriesName", product.SeriesName);

            cmd.Parameters.AddWithValue("@careKind", product.CareKind);

            cmd.Parameters.AddWithValue("@amount", product.Amount);

            cmd.Parameters.AddWithValue("@image", product.Image);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all Products
        //--------------------------------------------------------------------------------------------------
        public List<Product> ReadProducts()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadProductsCommandSP("spGetProducts", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Product> products = new List<Product>();

                while (dataReader.Read())
                {
                    Product product = new Product();
                    product.ProductName = dataReader["Name"].ToString();
                    product.Description = dataReader["Description"].ToString();
                    product.Price = Convert.ToDouble(dataReader["price"]);
                    product.SeriesName = dataReader["seriesName"].ToString();
                    product.CareKind = dataReader["CareKind"].ToString();
                    product.Amount = Convert.ToInt32(dataReader["Amount"]);
                    product.Image = dataReader["image"].ToString();
                    product.ProductNum = Convert.ToInt32(dataReader["NumProduct"]);
                    products.Add(product);
                }
                return products;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the Read products SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadProductsCommandSP(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method inserts a Treatment to the Treatment table 
        //--------------------------------------------------------------------------------------------------
        public int InsertService(Service service)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateInsertServiceCommandWithStoredProcedure("spInsertCareKind", con, service);// create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Service SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertServiceCommandWithStoredProcedure(String spName, SqlConnection con, Service service)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@careKind", service.TreatmentType);

            cmd.Parameters.AddWithValue("@price", service.Price);

            cmd.Parameters.AddWithValue("@minPrice", service.MinPrice);

            cmd.Parameters.AddWithValue("@treatmentDuration1", service.TreatmentDuration1);

            cmd.Parameters.AddWithValue("@treatmentDuration2", service.TreatmentDuration2);

            cmd.Parameters.AddWithValue("@treatmentDuration3", service.TreatmentDuration3);

            cmd.Parameters.AddWithValue("@break1", service.Break1);

            cmd.Parameters.AddWithValue("@break2", service.Break2);

            cmd.Parameters.AddWithValue("@break3", service.Break3);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all services 
        //--------------------------------------------------------------------------------------------------
        public List<Service> ReadService()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadServiceCommandSP("spGetCareKind", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Service> careKindList = new List<Service>();

                while (dataReader.Read())
                {
                    Service careKind = new Service();
                    careKind.TreatmentType = dataReader["kindCare"].ToString();
                    careKind.TreatmentNum = Convert.ToInt32(dataReader["NumOfCare"]);
                    careKind.Price = Convert.ToDouble(dataReader["price"]);
                    careKind.MinPrice= Convert.ToDouble(dataReader["MinPrice"]);
                    careKind.TreatmentDuration1 = Convert.ToInt32(dataReader["TreatmentDuration1"]);
                    careKind.TreatmentDuration2 = Convert.ToInt32(dataReader["TreatmentDuration2"]);
                    careKind.TreatmentDuration3 = Convert.ToInt32(dataReader["TreatmentDuration3"]);
                    careKind.Break1 = Convert.ToInt32(dataReader["Break1"]);
                    careKind.Break2 = Convert.ToInt32(dataReader["Break2"]);
                    careKind.Break3 = Convert.ToInt32(dataReader["Break3"]);
                    careKindList.Add(careKind);
                }
                return careKindList;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the Read service SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadServiceCommandSP(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method update a product in the product table and  inserts a product to the OrderProduct table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateProduct(int id, string phoneNum, int amount, DateTime date)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateUpdateProductCommandWithStoredProcedure("spUpdateAmountPoduct", con, id,phoneNum,amount ,date);// create the command

            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                //return numEffected;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the update Product SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateProductCommandWithStoredProcedure(String spName, SqlConnection con, int id, string phoneNum, int amount, DateTime date)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@productNum", id);

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@amount", amount);

            cmd.Parameters.AddWithValue("@dateOfCollection", date);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read Employee by service
        //--------------------------------------------------------------------------------------------------
        public Object ReadByService(int service)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadEmployeeByServiceCommandSP("spGetEmployeeByService", con, service);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> Employees = new List<Object>();

                while (dataReader.Read())
                {
                    Employees.Add(new
                    {
                        FirstName = dataReader["firstName"].ToString(),
                        LastName = dataReader["lastName"].ToString(),
                        PhoneNum = dataReader["phoneNum"].ToString()
                    });
                }
                return Employees;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the Read Employee by service SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadEmployeeByServiceCommandSP(String spName, SqlConnection con, int service)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@numOfCare", service);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read Available dates by employee
        //--------------------------------------------------------------------------------------------------
        public Object ReadDatesByEmployee(string EmpPhone)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadDateByEmployeeCommandSP("spGetDatesByEmployee", con, EmpPhone);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> Dates = new List<Object>();

                while (dataReader.Read())
                {
                    Dates.Add(new
                    {
                        startDate = (DateTime)dataReader["startDate"],
                        endDate = (DateTime)dataReader["endDate"],
                        dayOfWeek = dataReader["DayOfWeek"].ToString()
                    });
                }
                return Dates;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //---------------------------------------------------------------------------------
        // Create the Read Available Dates by Employee SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadDateByEmployeeCommandSP(String spName, SqlConnection con, string EmpPhone)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@phoneNumber", EmpPhone);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method read Available dates by employee
        //--------------------------------------------------------------------------------------------------
        public List<string> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadAvailableTimesCommandSP("spGetAvailableTimes", con, serviceNum,phoneNum,Date);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<string> Times = new List<string>();

                while (dataReader.Read())
                {
                    Times.Add(dataReader["DayOfWeek"].ToString());
                }
                return Times;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //---------------------------------------------------------------------------------
        // Create the Read Available Dates by Employee SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadAvailableTimesCommandSP(String spName, SqlConnection con, int serviceNum, string phoneNum, DateTime Date)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@numOfCare", serviceNum);

            cmd.Parameters.AddWithValue("@phoneNumber", phoneNum);

            cmd.Parameters.AddWithValue("@date", Date);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Waitingqueue to the Waiting table 
        //--------------------------------------------------------------------------------------------------
        public int InsertToWaitingList(Queue queue)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateInsertToWaitingListCommandWithStoredProcedure("spInsertToWait", con, queue);// create the command

            try
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Service SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertToWaitingListCommandWithStoredProcedure(String spName, SqlConnection con, Queue queue)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@careKind", queue.ServiceNum);

            cmd.Parameters.AddWithValue("@phoneNumClient", queue.Clientphone);

            cmd.Parameters.AddWithValue("@QueueDate", queue.Date);

            cmd.Parameters.AddWithValue("@Time", queue.Time);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method Reads a Client phones to remind client to order a queue 
        //--------------------------------------------------------------------------------------------------
        public List<string> ReadPhonesToRemind()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            cmd = CreateReadPhonesToRemindCommandWithStoredProcedure("OrderQueueReminder", con);// create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<string> Phones = new List<string>();

                while (dataReader.Read())
                {
                    Phones.Add(dataReader["phoneNumClient"].ToString());
                }
                return Phones;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the insert Employee SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadPhonesToRemindCommandWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }


    }


}
