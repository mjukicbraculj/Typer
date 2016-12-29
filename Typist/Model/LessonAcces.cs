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
    
    }
}
