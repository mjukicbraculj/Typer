using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using System.Data.SQLite;

namespace Typist.Model
{
    class LessonAcces
    {

        /// <summary>
        /// Method adds new lesson to table lessons.
        /// </summary>
        /// <param name="lesson">lesson to add</param>
        /// <returns>string about success</returns>
        public static string AddLesson(Lesson lesson)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    string insertLesson = @"insert into lessons(name, parent)
                                            values ('" + lesson.Name + "', '" + lesson.Parent + "')";
                    SQLiteCommand command = new SQLiteCommand(insertLesson, connection);
                    command.ExecuteNonQuery();
                }
                catch(Exception)
                {
                    return "Error: insert to table Lessons";
                }
                return null;
            }
        }

        /// <summary>
        /// Method finds all lesson's names
        /// that have given parent
        /// </summary>
        /// <param name="parent">lesson's parent</param>
        /// <returns>string of lesson's names</returns>
        public static List<string> GetLessonsName(string parent)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    List<string> lessons = new List<string>();
                    connection.Open();
                    string selectLesson = @"select name from lessons
                                                where parent like '" + parent + "'";
                    SQLiteCommand command = new SQLiteCommand(selectLesson, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        lessons.Add(reader["name"].ToString());
                    return lessons;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Method finds id of lesson with 
        /// given name
        /// </summary>
        /// <param name="name">lesson name</param>
        /// <returns>lesson id</returns>
        public static int getLessonId(string name)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    List<string> lessons = new List<string>();
                    string selectLessonId = @"select id from lessons
                                            where name like '" + name + "'";
                    SQLiteCommand command = new SQLiteCommand(selectLessonId, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                        return -1;
                    return Convert.ToInt32(reader["id"]);
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Method finds all distinct groupTypes (Beginner, ...)
        /// </summary>
        /// <returns>List of groupTypes</returns>
        public static List<string> GetGroupTypes()
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    List<string> parents = new List<string>();
                    string selectLessonId = @"select distinct parent from lessons";
                    SQLiteCommand command = new SQLiteCommand(selectLessonId, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        parents.Add(reader["parent"].ToString());
                    }
                    return parents;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    
    }
}
