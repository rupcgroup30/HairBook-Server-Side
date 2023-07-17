using System.Data.SqlClient;
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
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Twilio.TwiML.Voice;
using Twilio.Jwt.AccessToken;

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
        // This method read hair salon info by Phone number
        //--------------------------------------------------------------------------------------------------
        public Object ReadAllHairSalon()
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

            cmd = CreateReadAllHairSalonCommandSP("spGetAllHairSalons", con);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                List<Object> hairSalons = new List<Object>();

                while (dataReader.Read())
                {
                    hairSalons.Add(new
                    {
                    SalonName = dataReader["SalonName"].ToString(),
                    Id = Convert.ToInt32(dataReader["HairSalonId"])
                });
                }
                return hairSalons;
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
        private SqlCommand CreateReadAllHairSalonCommandSP(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
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
        public int InsertHairSalonWorkTime(int hairSalonId, string fromHour, string toHour, int day)
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
                if (numEffected == 1)
                {
                    List<Client> clients = ReadAllClients(hairSalonId);
                    foreach (Client client in clients)
                    {
                        if (client.Token != null)
                        {
                        PushNotification pushNotification = new PushNotification();
                        pushNotification.ChangeWorkTimePNAsync(client.Token);
                        }

                    }
                }
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
        private SqlCommand CreateInsertHairSalonWorkTimeCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId, string fromHour, string toHour, int day)
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


        //--------------------------------------------------------------------------------------------------
        // This method Update a Hair color to the HairColor table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateHairColor(int colorNum, bool isActive, int hairSalonId)
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

            cmd = CreateUpdateHairColorCommandWithStoredProcedure("spUpdateColor", con, colorNum, isActive, hairSalonId);// create the command

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
        // Create the update Hair Color SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateHairColorCommandWithStoredProcedure(String spName, SqlConnection con, int colorNum, bool isActive, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@colorNum", colorNum);

            cmd.Parameters.AddWithValue("@isActive", isActive);

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
                        IsActive= dataReader.GetBoolean("IsActive")
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
        // This method inserts a Hair salon image to the hairSalonImages table 
        //--------------------------------------------------------------------------------------------------
        public int InsertHairSalonImages(string image, int hairSalonId)
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


            cmd = CreateInsertHairSalonImagesCommandWithStoredProcedure("spInsertHairSalonImage", con, image, hairSalonId);// create the command

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
        // Create the inserts a Hair salon image SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateInsertHairSalonImagesCommandWithStoredProcedure(String spName, SqlConnection con, string image, int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@image", image);

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);


            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Hair salon image to the hairSalonImages table 
        //--------------------------------------------------------------------------------------------------
        public List<string> ReadHairSalonImages(int hairSalonId)
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

            cmd = CreateReadHairSalonImagesCommandWithStoredProcedure("spGetHairSalonPhotos", con, hairSalonId);// create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<string> images = new List<string>();

                while (dataReader.Read())
                {
                    images.Add(dataReader["Image"].ToString());
                }
                return images;
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
        // Create the inserts a Hair salon image SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadHairSalonImagesCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId)
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

            cmd.Parameters.AddWithValue("@token", client.Token);

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
                int code = rand.Next(1000, 9999);
                Client client = new Client();
                
                while (dataReader.Read())
                {
                    client.Token = dataReader["token"].ToString();
                    if (client.Token != null)
                    {
                        PushNotification pushNotification = new PushNotification();
                        pushNotification.GetCodePNAsync(client.Token,code);
                    }
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
                    client.HairSalonId = Convert.ToInt32(dataReader["HairSalonId"]);
                    client.FirstName = dataReader["firstName"].ToString();
                    client.LastName = dataReader["lastName"].ToString();
                    client.PhoneNum = dataReader["phoneNum"].ToString();
                    client.BirthDate = ((DateTime)dataReader["birthDate"]);
                    client.Image = dataReader["image"].ToString();
                    client.Gender = dataReader["gender"].ToString();
                    client.Token = dataReader["token"].ToString();
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
        // This method read Client hair color by Phone number
        //--------------------------------------------------------------------------------------------------
        public int ReadClientHairColor(string phoneNum, int hairSalonId)
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

            cmd = CreateReadClientHairColorCommandSP("spGetClientColor", con, phoneNum, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);




                int colorNum = 0;

                while (dataReader.Read())
                {
                    colorNum = Convert.ToInt32(dataReader["ColorNum"]);
                }
                return colorNum;
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
        private SqlCommand CreateReadClientHairColorCommandSP(String spName, SqlConnection con, string phoneNum, int hairSalonId)
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
                    client.Token = dataReader["token"].ToString();
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

            cmd.Parameters.AddWithValue("@isActive", employee.IsActive);


            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Employee Specialization to the EmpSpecialize table 
        //--------------------------------------------------------------------------------------------------
        public int SetEmpSpecialize(int hairSalonId, string phoneNum, int numOfCare)
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
            
            cmd = CreateSetEmpecializeCommandWithStoredProcedure("spInsertEmpSpecialize", con, hairSalonId, phoneNum, numOfCare);// create the command

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
        private SqlCommand CreateSetEmpecializeCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId, string phoneNum, int numOfCare)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@empPhoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@numOfCare", numOfCare);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method inserts a Employee to the Employee table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateEmployeeRank(double rank, int employeeNum)
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

            cmd = CreateUpdateEmployeeRankCommandWithStoredProcedure("spUpdateEmployeeRank", con, rank, employeeNum);// create the command

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
        private SqlCommand CreateUpdateEmployeeRankCommandWithStoredProcedure(String spName, SqlConnection con, double rank, int employeeNum)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@employeeNum", employeeNum);

            cmd.Parameters.AddWithValue("@rank", rank);

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
                    emp.Rank = Math.Round(Convert.ToDouble(dataReader["Rank"]), 2);
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
                    emp.Rank =  Math.Round(Convert.ToDouble(dataReader["Rank"]), 2);
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
                    product.Price = Math.Round(Convert.ToDouble(dataReader["price"]), 2);
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
                    product.Price = Math.Round(Convert.ToDouble(dataReader["price"]), 2);
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
                    careKind.Price = Math.Round(Convert.ToDouble(dataReader["price"]), 2);
                    careKind.MinPrice = Math.Round(Convert.ToDouble(dataReader["MinPrice"]), 2);
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
        // This method read all ordered product  by client 
        //--------------------------------------------------------------------------------------------------
        public Object ReadOrderedProdByClient(int hairSalonId,string phoneNum, int flag)
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

            cmd = CreateReadOrderedProdByClientCommandSP("spGetPOrdersByClient", con, hairSalonId,phoneNum,flag);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> orderedProd = new List<Object>();

                while (dataReader.Read())
                {
                    orderedProd.Add(new//op.amount,p.[Name],p.[Description],p.[image],p.price,op.ConfirmNum
                    {
                        DateOfCollection = (DateTime)dataReader["DateOfCollection"],
                        amount = Convert.ToInt32(dataReader["amount"]),
                        Name= dataReader["Name"].ToString(),
                        Description=dataReader["Description"].ToString(),
                        image= dataReader["image"].ToString(),
                        price = Math.Round(Convert.ToDouble(dataReader["price"]), 2),
                        ConfirmNum= Convert.ToInt32(dataReader["ConfirmNum"])
                    });
                }
                return orderedProd;
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
        // Create the Read all ordered product SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadOrderedProdByClientCommandSP(String spName, SqlConnection con, int hairSalonId, string phoneNum, int flag)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNum", phoneNum);

            cmd.Parameters.AddWithValue("@flag", flag);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method update a product in the product table and  inserts a product to the OrderProduct table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateProduct(int id, int amount, float price,bool isActive, int hairSalonId)
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

            cmd = CreateUpdateProductCommandWithStoredProcedure("UpdateProduct", con, id, amount, price, isActive, hairSalonId);// create the command

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
        private SqlCommand CreateUpdateProductCommandWithStoredProcedure(String spName, SqlConnection con, int id, int amount, float price, bool isActive,int hairSalonId)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@productNum", id);

            cmd.Parameters.AddWithValue("@amount", amount);

            cmd.Parameters.AddWithValue("@isActive", isActive);

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
        // This method read employees vacations
        //--------------------------------------------------------------------------------------------------
        public Object ReadEmployeesVacations( int hairSalonId)
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

            cmd = CreateReadEmployeesVacationsCommandSP("spGetEmployeesVacations", con, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> vacations = new List<Object>();

                while (dataReader.Read())
                {
                    vacations.Add(new
                    {
                        phoneNum= dataReader["phoneNum"].ToString(),
                        startDate = (DateTime)dataReader["startDate"],
                        endDate = (DateTime)dataReader["endDate"],
                    });
                }
                return vacations;
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
        // Create the Read all Employee vacations SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateReadEmployeesVacationsCommandSP(String spName, SqlConnection con, int hairSalonId)
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
        public List<string> ReadAvailableTimes(int serviceNum, string phoneNum, DateTime Date, int hairSalonId)
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

                List<string> Times = new List<string>();
                //int count = 1;

                while (dataReader.Read())
                {
                    Times.Add(((TimeSpan)dataReader["TimeValue"]).ToString(@"hh\:mm"));
                }
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
        public int OrderQueue(Queue queue, int hairSalonId,int flag)
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
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                string ClientTreatment = null;
                int confirmNum = 0;
                string token = null;

                while (dataReader.Read())
                {
                    ClientTreatment = dataReader.GetString(1); // Assuming treatmentName is at index 1
                    confirmNum = dataReader.GetInt32(0); // Assuming ConfirmNum is at index 0
                    token = dataReader.GetString(2); // Assuming token is at index 2
                }
                if (flag == 1)
                {
                    PushNotification pushNotification = new PushNotification();
                    pushNotification.OrderQueuePNAsync(token, queue.Date, ClientTreatment, queue.Time);
                }

                return confirmNum;
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
        public Object GetQueuesByClient(int hairSalonId, string phoneNum,int flag)
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

            cmd = CreateGetQueuesByClientCommandSP("spGetAllQueuesByClient", con, phoneNum, hairSalonId,flag);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> queues = new List<Object>();

                while (dataReader.Read())
                {
                    queues.Add(new
                    {
                        Date = (DateTime)dataReader["Date"],
                        StartTime = (TimeSpan)dataReader["MinTime"],
                        FullName = dataReader["fullName"].ToString(),
                        KindCare = dataReader["KindCare"].ToString(),
                        NumQueue = Convert.ToInt32(dataReader["NumQueue"])
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
        private SqlCommand CreateGetQueuesByClientCommandSP(String spName, SqlConnection con, string phoneNum, int hairSalonId,int flag)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@phoneNumber", phoneNum);
            
            cmd.Parameters.AddWithValue("@flag", flag);

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method read all future Queues 
        //--------------------------------------------------------------------------------------------------
        public Object GetAllQueues(int hairSalonId, int flag)
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

            cmd = CreateGetAllQueuesCommandSP("spGetAllQueues", con, hairSalonId, flag);

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
                        ClientName = dataReader["ClientName"].ToString(),
                        ClientPhoneNum = dataReader["phoneNum"].ToString(),
                        EmployeeName = dataReader["EmployeeName"].ToString(),
                        KindCare = dataReader["KindCare"].ToString(),
                        NumQueue = Convert.ToInt32(dataReader["NumQueue"]),
                        phoneNumEMP= dataReader["phoneNumEMP"].ToString()
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
        private SqlCommand CreateGetAllQueuesCommandSP(String spName, SqlConnection con, int hairSalonId, int flag)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@hairSalonId", hairSalonId);

            cmd.Parameters.AddWithValue("@flag", flag);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method read all tommorows products orders 
        //--------------------------------------------------------------------------------------------------
        public List<Object> GetTommorowProductsOrders(int hairSalonId)
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

            cmd = CreateGetTommorowProductsOrdersCommandSP("spOrderedProductReminder", con, hairSalonId);

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> Orders = new List<Object>();

                while (dataReader.Read())
                {
                    Orders.Add(new
                    {
                        productName = dataReader["Name"].ToString(),
                        ConfirmNum = Convert.ToInt32(dataReader["ConfirmNum"]),
                        token = dataReader["token"].ToString()
                    });
                }
                return Orders;
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
        private SqlCommand CreateGetTommorowProductsOrdersCommandSP(String spName, SqlConnection con, int hairSalonId)
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
                    Phones.Add(dataReader["token"].ToString());
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
        public List<Object> QueueReminder(int hairSalonId)
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
                //למצוא דרך לשלוח תזכורת גם לאלה שהזמינו 2 תורים באותו היום שזה לא תור שחולק ל3 כמו החלקה
                List<Object> Phones = new List<Object>();

                while (dataReader.Read())
                {
                    Phones.Add(new
                    {
                        token= dataReader["token"].ToString(),
                        KindCare=dataReader["KindCare"].ToString(),
                        time=dataReader["StartTime"].ToString()
                    });
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
        // This method Reads a today's waiting list 
        //--------------------------------------------------------------------------------------------------
        public List<Object> GetTodayWaiting(int hairSalonId)
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

            cmd = CreateReadPhonesToCreateTenderCommandWithStoredProcedure("spGetTodayWaitingList", con, hairSalonId);// create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Object> waiting = new List<Object>();

                while (dataReader.Read())
                {
                    waiting.Add(new
                    {
                        phoneNum = dataReader["phoneNum"].ToString(),
                        Time= (TimeSpan)dataReader["Time"],
                        NumOfCare = Convert.ToInt32(dataReader["NumOfCare"]),
                        KindCare = dataReader["KindCare"].ToString(),
                        Price = Math.Round(Convert.ToDouble(dataReader["price"]), 2),
                        MinPrice = Math.Round(Convert.ToDouble(dataReader["MinPrice"]), 2),
                        Token = dataReader["Wtoken"].ToString(),
                        hairSalonId= hairSalonId
                    });
                }
                    List<Object> Clients = new List<Object>();
                if (waiting.Count > 0)
                {
                    foreach (var item in waiting)
                    {
                        if (con != null)
                        {
                            // close the db connection
                            con.Close();
                        }
                        try
                        {
                            con = connect("myProjDB"); // create the connection
                        }
                        catch (Exception ex)
                        {
                            // write to log
                            throw new Exception(ex.Message);
                        }
                        SqlCommand cmd2 = CreateReadAvailableTimesCommandSP("spGetAvailableTimes", con, (int)item.GetType().GetProperty("NumOfCare").GetValue(item), "null", DateTime.UtcNow.Date.AddDays(1), hairSalonId); // create the command
                        SqlDataReader dataReader2 = cmd2.ExecuteReader(CommandBehavior.CloseConnection);
                        TimeSpan time = TimeSpan.Zero;
                        int timeDiff = 1000;
                        while (dataReader2.Read())
                        {
                            if (time == TimeSpan.Zero)
                            {
                                time = (TimeSpan)dataReader2["TimeValue"];
                                timeDiff = Math.Abs((int)(((TimeSpan)item.GetType().GetProperty("Time").GetValue(item) - (TimeSpan)dataReader2["TimeValue"]).TotalMinutes));
                            }
                            else if (timeDiff > Math.Abs(((TimeSpan)item.GetType().GetProperty("Time").GetValue(item) - (TimeSpan)dataReader2["TimeValue"]).TotalMinutes))
                            {
                                timeDiff = Math.Abs((int)(((TimeSpan)item.GetType().GetProperty("Time").GetValue(item) - (TimeSpan)dataReader2["TimeValue"]).TotalMinutes));
                                time = (TimeSpan)dataReader2["TimeValue"];
                            }
                        }
                        if (time != TimeSpan.Zero)
                        {
                            Clients.Add(new
                            {
                                careKind = item.GetType().GetProperty("KindCare").GetValue(item),
                                Token = item.GetType().GetProperty("Token").GetValue(item),
                                Time = time,
                                Price = item.GetType().GetProperty("Price").GetValue(item),
                                minPrice = item.GetType().GetProperty("MinPrice").GetValue(item)
                            });
                        }
                    }
                    return Clients;
                }
                else return Clients;
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
        private SqlCommand CreateReadPhonesToCreateTenderCommandWithStoredProcedure(String spName, SqlConnection con, int hairSalonId)
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
        // This method delete queue to the Waiting table 
        //--------------------------------------------------------------------------------------------------
        public Object DeleteQueue(int queueNum, int hairSalonId,int flag)
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
                string ClientToken=null;
                string ClientTreatment = null;
                string WaitingTreatment = null;
                string StartTime = null;

                while (dataReader.Read())
                {
                    queue.EmpPhone = dataReader["phoneNumEMP"].ToString();
                    queue.Clientphone = dataReader["phoneNum"].ToString();
                    queue.Date = (DateTime)dataReader["QueueDate"];
                    queue.ServiceNum = Convert.ToInt32(dataReader["careKind"]);
                    queue.Time = dataReader["Time"].ToString();
                    queue.Token= dataReader["Wtoken"].ToString();
                    ClientToken= dataReader["token"].ToString();
                    ClientTreatment = dataReader["KindCare"].ToString();
                    StartTime = dataReader["StartTime"].ToString();
                    WaitingTreatment= dataReader["careKindName"].ToString();
                }
                if (ClientToken != null)
                {
                    if (flag == 1)
                    {
                        PushNotification pushNotification = new PushNotification();
                        pushNotification.DeleteQueuePNAsync(ClientToken, queue.Date, ClientTreatment, StartTime);
                    }
                }
                if (queue.Clientphone != null)
                {
                    int confirmNum = OrderQueue(queue, hairSalonId,0);
                    PushNotification pushNotification = new PushNotification();
                    pushNotification.EnteredFromWaitingPNAsync(queue.Token, queue.Date, WaitingTreatment, queue.Time);
                    return (new { clientPhone = queue.Clientphone, confirmNum = confirmNum });
                }
                if (flag == 1)
                    return (new { clientPhone = queue.Clientphone });
                else
                    return 1;
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
