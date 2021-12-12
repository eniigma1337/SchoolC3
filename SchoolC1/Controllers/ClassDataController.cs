using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolC1.Models;
using MySql.Data.MySqlClient;

namespace SchoolC1.Controllers
{
    public class ClassDataController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns> A list of all class names and class codes</returns>
        [HttpGet]
        public List<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int ClassId = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];
                DateTime ClassStart = Convert.ToDateTime(ResultSet["startdate"].ToString());
                DateTime ClassFinish = Convert.ToDateTime(ResultSet["finishdate"].ToString());


                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassStart = ClassStart;
                NewClass.ClassFinish = ClassFinish;


                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Class names
            return Classes;

        }
        /// <summary>
        /// Returns selected Class of the system
        /// </summary>
        /// <example>api/ClassData/ShowClass/1</example>
        /// <example>api/ClassData/ShowClass/2</example>
        /// <example>api/ClassData/ShowClass/3</example>
        /// <param name="ClassId">The database id of the class </param>
        /// <returns>Returns the selected class</returns>
        [HttpGet]
        public Class ShowClass(int ClassId)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid=" + ClassId;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            Class SelectedClass = new Class();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int Id = (int)ResultSet["classid"];
                string ClassName = (string)ResultSet["classname"];
                string ClassCode = (string)ResultSet["classcode"];
                DateTime ClassStart = Convert.ToDateTime(ResultSet["startdate"].ToString());
                DateTime ClassFinish = Convert.ToDateTime(ResultSet["finishdate"].ToString());


                SelectedClass.ClassId = Id;
                SelectedClass.ClassName = ClassName;
                SelectedClass.ClassCode = ClassCode;
                SelectedClass.ClassStart = ClassStart;
                SelectedClass.ClassFinish = ClassFinish;

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Returns the selected Class
            return SelectedClass;

        }

    }
}
