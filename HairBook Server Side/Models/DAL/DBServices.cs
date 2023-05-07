﻿using System.Data.SqlClient;
using System.Data;
using HairBook_Server_Side.Models;
using Microsoft.VisualBasic;
using System.Globalization;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Data.Common;

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
        // This method inserts a Hair Salon Info to the HairSalon table 
        //--------------------------------------------------------------------------------------------------
        public int InsertHairSalonInfo(int hairSalonId, HairSalon hairSalonInfo)
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

            cmd = CreateInsertHairSalonCommandWithStoredProcedure("spInsertHairSalon", con, hairSalonId, hairSalonInfo);// create the command

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
        // Create the insert Hair Salon SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertHairSalonCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId, HairSalon hairSalonInfo)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@salonPhoneNum", hairSalonInfo.SalonPhoneNum);

            cmd.Parameters.AddWithValue("@address", hairSalonInfo.Address);

            cmd.Parameters.AddWithValue("@city", hairSalonInfo.City);

            cmd.Parameters.AddWithValue("@facebook", hairSalonInfo.Facebook);

            cmd.Parameters.AddWithValue("@instagram", hairSalonInfo.Instagram);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read hair salon info by Phone number
        //--------------------------------------------------------------------------------------------------
        public HairSalon ReadHairSalonInfo(int hairSalonId)
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

            cmd = CreateReadHairSalonInfoCommandSP("spGetHairSalonInfo", con, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                HairSalon hairSalon = new HairSalon();

                while (dataReader.Read())
                {
                    hairSalon.SalonName = dataReader["SalonName"].ToString();
                    hairSalon.Address = dataReader["Address"].ToString();
                    hairSalon.City = dataReader["City"].ToString();
                    hairSalon.SalonPhoneNum = dataReader["SalonPhoneNum"].ToString();
                    hairSalon.Facebook = dataReader["Facebook"].ToString();
                    hairSalon.Instagram = dataReader["Instagram"].ToString();
                }
                return hairSalon;
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
        // Create the Read hair salon info SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadHairSalonInfoCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method inserts a Hair Salon work time to the HairSalon table 
        //--------------------------------------------------------------------------------------------------
        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, string day)
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

            cmd = CreateInsertHairSalonWorkTimeCommandWithStoredProcedure("spInsertWorkTimes", con, hairSalonId, fromHour, toHour, day);// create the command

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
        // Create the insert Hair Salon work time SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertHairSalonWorkTimeCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId, string fromHour, string toHour, string day)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@day", day);

            cmd.Parameters.AddWithValue("@fromHour", fromHour);

            cmd.Parameters.AddWithValue("@toHour", toHour);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read hair salon info by Phone number
        //--------------------------------------------------------------------------------------------------
        public Object ReadHairSalonWorkTime(int hairSalonId)
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

            cmd = CreateGetHairSalonWorkTimeCommandSP("spGetHairSalonWorkTime", con, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                List<Object> hairSalonWT = new List<Object>();

                while (dataReader.Read())
                {
                    hairSalonWT.Add(new
                    {
                        Day = dataReader["DayOfWeek"].ToString(),
                        FromHour = (TimeSpan)dataReader["fromHour"],
                        ToHour = (TimeSpan)dataReader["toHour"]
                    });
                }
                return hairSalonWT;
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
        // Create the Read hair salon info SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateGetHairSalonWorkTimeCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Hair color to the HairColor table 
        //--------------------------------------------------------------------------------------------------
        public int InsertHairColor(int colorNum, string colorName, int hairSalonId)
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

            cmd = CreateInsertHairColorCommandWithStoredProcedure("spInsertHairColor", con, colorNum, colorName, hairSalonId);// create the command

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
        // Create the insert Hair Color SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertHairColorCommandWithStoredProcedure(String spName, SqlConnection con, int colorNum, string colorName, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@colorNum", colorNum);

            cmd.Parameters.AddWithValue("@colorName", colorName);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);


            return cmd;
        }

        public Object ReadHairColors(bool flag, int HairSalonId)
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

            cmd = CreateGetHairColorsCommandSP("spGetHairColors", con, flag, HairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                List<Object> HairColors = new List<Object>();

                while (dataReader.Read())
                {
                    HairColors.Add(new
                    {
                        ColorNum = Convert.ToInt32(dataReader["ColorNum"]),
                        ColorName = (dataReader["ColorName"]).ToString(),
                    });
                }
                return HairColors;
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
        // Create the Read hair colors info SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateGetHairColorsCommandSP(String spName, SqlConnection con, bool flag, int HairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@flag", flag);

            cmd.Parameters.AddWithValue("@HairSalonId", HairSalonId);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Hair color to the HairColor table 
        //--------------------------------------------------------------------------------------------------
        public int InsertClientHairColor(string phoneNum, int colorNum, int hairSalonId)
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

            cmd = CreateInsertClientHairColorCommandWithStoredProcedure("spInsertClientHairColor", con, phoneNum, colorNum, hairSalonId);// create the command

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
        // Create the insert Hair Color SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertClientHairColorCommandWithStoredProcedure(String spName, SqlConnection con, string phoneNum, int colorNum, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@colorNum", colorNum);

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);


            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Client to the client table 
        //--------------------------------------------------------------------------------------------------
        public int InsertClient(Client client, int hairSalonId)
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

            cmd = CreateInsertClientCommandWithStoredProcedure("spInsertClient", con, client, hairSalonId);// create the command

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
        private SqlCommand CreateInsertClientCommandWithStoredProcedure(String spName, SqlConnection con, Client client, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@firstName", client.FirstName);

            cmd.Parameters.AddWithValue("@lastName", client.LastName);

            cmd.Parameters.AddWithValue("@phoneNum", client.PhoneNum);

            cmd.Parameters.AddWithValue("@image", client.Image);

            cmd.Parameters.AddWithValue("@birthDate", client.BirthDate);

            cmd.Parameters.AddWithValue("@gender", client.Gender);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Clients by Phone number
        //--------------------------------------------------------------------------------------------------
        public int GetCode(string phoneNum, int hairSalonId)
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

            cmd = CreateReadClientCommandSP("spGetClient", con, phoneNum, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Random rand = new Random();
                int code = rand.Next(1000, 9999); ;
                while (dataReader.Read())
                {

                    const string accountSid = "AC19a8ff3d22acff9a3caa5900baa0e56f";
                    const string authToken = "1b160ae8bd1402b596cd91c7e619bdae";

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
        public Client ReadClient(string phoneNum, int hairSalonId)
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

            cmd = CreateReadClientCommandSP("spGetClient", con, phoneNum, hairSalonId);

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
        private SqlCommand CreateReadClientCommandSP(String spName, SqlConnection con, string phoneNum, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all Clients by Phone number
        //--------------------------------------------------------------------------------------------------
        public List<Client> ReadAllClients(int hairSalonId)
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

            cmd = CreateReadAllClientCommandSP("spGetAllClients", con, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                List<Client> clients = new List<Client>();
                while (dataReader.Read())
                {
                    Client client = new Client();
                    client.FirstName = dataReader["firstName"].ToString();
                    client.LastName = dataReader["lastName"].ToString();
                    client.PhoneNum = dataReader["phoneNum"].ToString();
                    client.BirthDate = ((DateTime)dataReader["birthDate"]);
                    client.Image = dataReader["image"].ToString();
                    client.Gender = dataReader["gender"].ToString();
                    clients.Add(client);
                }
                return clients;
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
        // Create the Read all Client SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadAllClientCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method inserts a Employee to the Employee table 
        //--------------------------------------------------------------------------------------------------
        public int InsertEmployee(Employee employee, int hairSalonId)
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

            cmd = CreateInsertEmployeeCommandWithStoredProcedure("spInsertEmployee", con, employee, hairSalonId);// create the command

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
        private SqlCommand CreateInsertEmployeeCommandWithStoredProcedure(String spName, SqlConnection con, Employee employee, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@firstName", employee.FirstName);

            cmd.Parameters.AddWithValue("@lastName", employee.LastName);

            cmd.Parameters.AddWithValue("@phoneNum", employee.PhoneNum);

            cmd.Parameters.AddWithValue("@image", employee.Image);

            cmd.Parameters.AddWithValue("@password", employee.Password);

            cmd.Parameters.AddWithValue("@empolyeeType", employee.EmpolyeeType);

            cmd.Parameters.AddWithValue("@startDate", (employee.StartDate).Date);

            cmd.Parameters.AddWithValue("@rank", employee.Rank);

            cmd.Parameters.AddWithValue("@isActive", employee.IsActive);


            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Employee by Phone number and Password
        //--------------------------------------------------------------------------------------------------
        public Employee ReadEmployee(string phoneNum, string password, int hairSalonId)
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

            cmd = CreateReadEmployeeCommandSP("spGetEmployee", con, phoneNum, password, hairSalonId);

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
                    emp.Rank = Convert.ToInt32(dataReader["Rank"]);
                    emp.EmpolyeeType = dataReader["employeeType"].ToString();
                    emp.Password = dataReader["password"].ToString();
                    emp.StartDate = ((DateTime)dataReader["startDate"]);
                    emp.IsActive = dataReader.GetBoolean("IsActive");
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
        private SqlCommand CreateReadEmployeeCommandSP(String spName, SqlConnection con, string phoneNum, string password, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@password", password);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method update a Employee to the client table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateEmployee(string phoneNum, string password, string type, string isActive, int hairSalonId)
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

            cmd = CreateUpdateEmployeeCommandWithStoredProcedure("spUpdateEmployee", con, phoneNum, password, type, isActive, hairSalonId);// create the command

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
        // Create the update Employee SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateEmployeeCommandWithStoredProcedure(String spName, SqlConnection con, string phoneNum, string password, string type, string isActive, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@password", password);

            cmd.Parameters.AddWithValue("@empolyeeType", type);

            cmd.Parameters.AddWithValue("@isActive", isActive);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read all Employee by Phone number and Password
        //--------------------------------------------------------------------------------------------------
        public List<Employee> ReadAllEmployees(int hairSalonId)
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

            cmd = CreateReadAllEmployeesCommandSP("GetAllEmployees", con, hairSalonId);

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
                    emp.Rank = Convert.ToInt32(dataReader["Rank"]);
                    emp.EmpolyeeType = dataReader["employeeType"].ToString();
                    emp.Password = dataReader["password"].ToString();
                    emp.StartDate = ((DateTime)dataReader["startDate"]);
                    emp.IsActive = dataReader.GetBoolean("IsActive");
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
        private SqlCommand CreateReadAllEmployeesCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Employee vacation to the Employee vacation table 
        //--------------------------------------------------------------------------------------------------
        public int InsertEmployeeVacation(int hairSalonId,string phoneNum, DateTime startDate,DateTime endDate,string fromHour,string toHour)
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

            cmd = CreateInsertEmployeeVacationCommandWithStoredProcedure("spInsertEmpVaction", con, hairSalonId, phoneNum, startDate, endDate, fromHour, toHour);// create the command

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
        // Create the insert Employee vacation SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertEmployeeVacationCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId, string phoneNum, DateTime startDate, DateTime endDate, string fromHour, string toHour)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@startDate", startDate);

            cmd.Parameters.AddWithValue("@endDate", endDate);

            cmd.Parameters.AddWithValue("@empPhone", phoneNum);

            cmd.Parameters.AddWithValue("@fromHour", fromHour);

            cmd.Parameters.AddWithValue("@toHour", toHour);

            return cmd;
        }





        //--------------------------------------------------------------------------------------------------
        // This method inserts a product to the product table 
        //--------------------------------------------------------------------------------------------------
        public int InsertProduct(Product product, int hairSalonId)
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

            cmd = CreateInsertProductCommandWithStoredProcedure("spInsertProduct", con, product, hairSalonId);// create the command

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
        private SqlCommand CreateInsertProductCommandWithStoredProcedure(String spName, SqlConnection con, Product product, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@name", product.ProductName);

            cmd.Parameters.AddWithValue("@description", product.Description);

            cmd.Parameters.AddWithValue("@price", product.Price);

            cmd.Parameters.AddWithValue("@seriesName", product.SeriesName);

            cmd.Parameters.AddWithValue("@careKind", product.CareKind);

            cmd.Parameters.AddWithValue("@amount", product.Amount);

            cmd.Parameters.AddWithValue("@image", product.Image);

            cmd.Parameters.AddWithValue("@isActive", product.IsActive);


            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all Products
        //--------------------------------------------------------------------------------------------------
        public List<Product> ReadProducts(int hairSalonId)
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

            cmd = CreateReadProductsCommandSP("spGetProducts", con, hairSalonId);

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
                    product.IsActive = dataReader.GetBoolean("IsActive");
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
        private SqlCommand CreateReadProductsCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Related Products by series name
        //--------------------------------------------------------------------------------------------------
        public List<Product> ReadRelatedProducts(string seriesName, int hairSalonId)
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

            cmd = CreateReadRelatedProductsCommandSP("spGetRelatedProducts", con, seriesName, hairSalonId);

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
        // Create the Read Related products SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadRelatedProductsCommandSP(String spName, SqlConnection con, string seriesName, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@seriesName", seriesName);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Treatment to the Treatment table 
        //--------------------------------------------------------------------------------------------------
        public int InsertService(Service service, int hairSalonId)
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

            cmd = CreateInsertServiceCommandWithStoredProcedure("spInsertCareKind", con, service, hairSalonId);// create the command

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
        private SqlCommand CreateInsertServiceCommandWithStoredProcedure(String spName, SqlConnection con, Service service, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

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
        // This method inserts a Treatment to the Treatment table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateService(Service service, int hairSalonId)
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

            cmd = CreateUpdateServiceCommandWithStoredProcedure("spUpdateCareKind", con, service, hairSalonId);// create the command

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
        private SqlCommand CreateUpdateServiceCommandWithStoredProcedure(String spName, SqlConnection con, Service service, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@careKindNum", service.TreatmentNum);

            cmd.Parameters.AddWithValue("@price", service.Price);

            cmd.Parameters.AddWithValue("@minPrice", service.MinPrice);

            cmd.Parameters.AddWithValue("@treatmentDuration1", service.TreatmentDuration1);

            cmd.Parameters.AddWithValue("@treatmentDuration2", service.TreatmentDuration2);

            cmd.Parameters.AddWithValue("@treatmentDuration3", service.TreatmentDuration3);

            cmd.Parameters.AddWithValue("@break1", service.Break1);

            cmd.Parameters.AddWithValue("@break2", service.Break2);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read all services 
        //--------------------------------------------------------------------------------------------------
        public List<Service> ReadService(int hairSalonId)
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

            cmd = CreateReadServiceCommandSP("spGetCareKind", con, hairSalonId);

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
                    careKind.MinPrice = Convert.ToDouble(dataReader["MinPrice"]);
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
        private SqlCommand CreateReadServiceCommandSP(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method update a product in the product table and  inserts a product to the OrderProduct table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateNOrdetProduct(int id, string phoneNum, int amount, DateTime date, int hairSalonId)
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

            cmd = CreateUpdateNOrderProductCommandWithStoredProcedure("spOrderPoduct", con, id, phoneNum, amount, date, hairSalonId);// create the command

            try
            {
                //int numEffected = cmd.ExecuteNonQuery(); // execute the command
                //return numEffected;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception(ex.Message + " Phone not found");
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
        // Create the order Product SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateNOrderProductCommandWithStoredProcedure(String spName, SqlConnection con, int id, string phoneNum, int amount, DateTime date, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@productNum", id);

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@amount", amount);

            cmd.Parameters.AddWithValue("@dateOfCollection", date);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method update a product in the product table and  inserts a product to the OrderProduct table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateProduct(int id, int amount, float price, int hairSalonId)
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

            cmd = CreateUpdateProductCommandWithStoredProcedure("UpdateProduct", con, id, amount, price, hairSalonId);// create the command

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
        // Create the update Product SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateProductCommandWithStoredProcedure(String spName, SqlConnection con, int id, int amount, float price, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@productNum", id);

            cmd.Parameters.AddWithValue("@amount", amount);

            cmd.Parameters.AddWithValue("@price", price);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read Employee by service
        //--------------------------------------------------------------------------------------------------
        public Object ReadByService(int service, int hairSalonId)
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

            cmd = CreateReadEmployeeByServiceCommandSP("spGetEmployeeByService", con, service, hairSalonId);

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
        private SqlCommand CreateReadEmployeeByServiceCommandSP(String spName, SqlConnection con, int service, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@numOfCare", service);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read Available dates by employee
        //--------------------------------------------------------------------------------------------------
        public Object ReadDatesByEmployee(string EmpPhone, int hairSalonId)
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

            cmd = CreateReadDateByEmployeeCommandSP("spGetDatesByEmployee", con, EmpPhone, hairSalonId);

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
                        // dayOfWeek = dataReader["DayOfWeek"].ToString()
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
        private SqlCommand CreateReadDateByEmployeeCommandSP(String spName, SqlConnection con, string EmpPhone, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNumber", EmpPhone);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method read Available times by employee,service and date
        //--------------------------------------------------------------------------------------------------
        public List<TimeSpan> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date, int hairSalonId)
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

            cmd = CreateReadAvailableTimesCommandSP("spGetAvailableTimes", con, serviceNum, phoneNum, Date, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<TimeSpan> Times = new List<TimeSpan>();
                //int count = 1;

                while (dataReader.Read())
                {
                    Times.Add((TimeSpan)dataReader["TimeValue"]);
                }
                //    //לולאה שבודקת את זהמנים הפנויים
                return Times;
                //}
                //else return null;
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
        // Create the Read Available times by employee,service and date SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadAvailableTimesCommandSP(String spName, SqlConnection con, int serviceNum, string phoneNum, DateTime Date, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@numOfCare", serviceNum);

            cmd.Parameters.AddWithValue("@phoneNumber", phoneNum);

            cmd.Parameters.AddWithValue("@date", Date);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method Order queue in the QueueClient table and  inserts a queue to the OrderQueue table 
        //--------------------------------------------------------------------------------------------------
        public int OrderQueue(Queue queue, int hairSalonId)
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

            cmd = CreateOrderQueueCommandWithStoredProcedure("spOrderQueue", con, queue, hairSalonId);// create the command

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
        // Create the order Queue SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateOrderQueueCommandWithStoredProcedure(String spName, SqlConnection con, Queue queue, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNumClient", queue.Clientphone);

            cmd.Parameters.AddWithValue("@phoneNumEmp", queue.EmpPhone);

            cmd.Parameters.AddWithValue("@Date", queue.Date);

            cmd.Parameters.AddWithValue("@Time", queue.Time);

            cmd.Parameters.AddWithValue("@NumOfCare", queue.ServiceNum);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read all future Queues 
        //--------------------------------------------------------------------------------------------------
        public Object GetFutureQueues(int hairSalonId, string phoneNum)
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

            cmd = CreateGetFutureQueuesCommandSP("spGetFutureQueues", con, phoneNum, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> queues = new List<Object>();

                while (dataReader.Read())
                {
                    queues.Add(new
                    {
                        Date = (DateTime)dataReader["Date"],
                        StartTime = (TimeSpan)dataReader["StartTime"],
                        EndTime = (TimeSpan)dataReader["EndTime"],
                        Price = Convert.ToDouble(dataReader["Price"]),
                        FullName = dataReader["fullName"].ToString(),
                        PhoneNum = dataReader["phoneNum"].ToString(),
                        KindCare = dataReader["KindCare"].ToString()
                    });
                }
                return queues;
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
        // Create the Read Future Queues and date SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateGetFutureQueuesCommandSP(String spName, SqlConnection con, string phoneNum, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNumber", phoneNum);

            return cmd;
        }




        //--------------------------------------------------------------------------------------------------
        // This method inserts a Waitingqueue to the Waiting table 
        //--------------------------------------------------------------------------------------------------
        public int InsertToWaitingList(Queue queue, int hairSalonId)
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

            cmd = CreateInsertToWaitingListCommandWithStoredProcedure("spInsertToWait", con, queue, hairSalonId);// create the command

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
        private SqlCommand CreateInsertToWaitingListCommandWithStoredProcedure(String spName, SqlConnection con, Queue queue, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@careKind", queue.ServiceNum);

            cmd.Parameters.AddWithValue("@phoneNumEMP", queue.EmpPhone);

            cmd.Parameters.AddWithValue("@phoneNumClient", queue.Clientphone);

            cmd.Parameters.AddWithValue("@QueueDate", queue.Date);

            cmd.Parameters.AddWithValue("@Time", queue.Time);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method Reads a Client phones to remind client to order a queue 
        //--------------------------------------------------------------------------------------------------
        public List<string> ReadPhonesToRemind(int hairSalonId)
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

            cmd = CreateReadPhonesToRemindCommandWithStoredProcedure("OrderQueueReminder", con, hairSalonId);// create the command

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
        private SqlCommand CreateReadPhonesToRemindCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Reads a Client phones to remind tommorow queue 
        //--------------------------------------------------------------------------------------------------
        public List<string> QueueReminder(int hairSalonId)
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

            cmd = CreateReadPhonesToRemindCommandWithStoredProcedure("spQueueReminder", con, hairSalonId);// create the command

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


        //--------------------------------------------------------------------------------------------------
        // This method delete queue to the Waiting table 
        //--------------------------------------------------------------------------------------------------
        public Object DeleteQueue(int queueNum, int hairSalonId)
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

            cmd = CreateDeleteQueueCommandWithStoredProcedure("spCancelQueue", con, queueNum, hairSalonId);// create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Queue queue = new Queue();

                while (dataReader.Read())
                {
                    queue.EmpPhone = dataReader["phoneNumEMP"].ToString();
                    queue.Clientphone = dataReader["phoneNum"].ToString();
                    queue.Date = (DateTime)dataReader["QueueDate"];
                    queue.ServiceNum = Convert.ToInt32(dataReader["careKind"]);
                    queue.Time = dataReader["Time"].ToString();
                }
                if (queue.Clientphone != null)
                {
                    int conformNum = OrderQueue(queue, hairSalonId);
                    return (new { clientPhone = queue.Clientphone, conformNum = conformNum });
                }
                return (new { clientPhone = queue.Clientphone });
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
        private SqlCommand CreateDeleteQueueCommandWithStoredProcedure(String spName, SqlConnection con, int queueNum, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@queueNum", queueNum);

            return cmd;
        }





    }


}
