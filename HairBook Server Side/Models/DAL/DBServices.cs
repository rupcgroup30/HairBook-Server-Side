using System.Data.SqlClient;
using System.Data;
using HairBook_Server_Side.Models;

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
        // Create the insert SqlCommand using a stored procedure
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

            cmd.Parameters.AddWithValue("@birthDate", client.BirthDate);

            cmd.Parameters.AddWithValue("gender", client.Gender);

            return cmd;
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
                    client.BirthDate = dataReader["birthDate"].ToString();
                    client.Image = dataReader["image"].ToString();
                    client.Gender = Convert.ToBoolean(dataReader["gender"]);
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
        // Create the Read SqlCommand
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
        // Create the insert SqlCommand using a stored procedure
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

            cmd.Parameters.AddWithValue("@employeeNum", employee.EmployeeNum);

            cmd.Parameters.AddWithValue("@password", employee.Password);

            cmd.Parameters.AddWithValue("@empolyeeType", employee.EmpolyeeType);

            cmd.Parameters.AddWithValue("@startDate", employee.StartDate);

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

                    emp.FirstName = dataReader["FirstName"].ToString();
                    emp.LastName = dataReader["LastName"].ToString();
                    emp.PhoneNum = dataReader["PhoneNum"].ToString();
                    emp.Image = dataReader["Image"].ToString();
                    emp.EmployeeNum = Convert.ToInt32(dataReader["EmployeeNum"]);
                    emp.EmpolyeeType = dataReader["emplyeeType"].ToString();
                    emp.Password = dataReader["Password"].ToString();
                    emp.StartDate = dataReader["StartDate"].ToString();
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
        // Create the insert SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertProductCommandWithStoredProcedure(String spName, SqlConnection con, Product product)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@productName", product.ProductName);

            cmd.Parameters.AddWithValue("@description", product.Description);

            cmd.Parameters.AddWithValue("@price", product.Price);

            cmd.Parameters.AddWithValue("@seriesName", product.SeriesName);

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
                    product.ProductName = dataReader["productName"].ToString();
                    product.Description = dataReader["description"].ToString();
                    product.Price = Convert.ToDouble(dataReader["price"]);
                    product.SeriesName = dataReader["seriesName"].ToString();
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
        // This method inserts a product to the product table 
        //--------------------------------------------------------------------------------------------------
        public int InsertTreatment(Treatment treatment)
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

            cmd = CreateInsertTreatmentCommandWithStoredProcedure("spInsertTreatment", con, treatment);// create the command

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
        // Create the insert SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertTreatmentCommandWithStoredProcedure(String spName, SqlConnection con, Treatment treatment)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@treatmentType", treatment.TreatmentType);

            cmd.Parameters.AddWithValue("@price", treatment.Price);

            cmd.Parameters.AddWithValue("@treatmentDuration1", treatment.TreatmentDuration1);

            cmd.Parameters.AddWithValue("@treatmentDuration2", treatment.TreatmentDuration2);

            cmd.Parameters.AddWithValue("@treatmentDuration3", treatment.TreatmentDuration3);

            cmd.Parameters.AddWithValue("@break1", treatment.Break1);

            cmd.Parameters.AddWithValue("@break2", treatment.Break2);

            cmd.Parameters.AddWithValue("@break3", treatment.Break3);

            return cmd;
        }


    }
}
