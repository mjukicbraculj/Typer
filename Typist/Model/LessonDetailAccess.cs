using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using System.Data.SQLite;

namespace Typist.Model
{
    class LessonDetailAccess
    {
        public static List<LessonDetail> GetLessonDetails(int lessonId, int userId)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    List<LessonDetail> details = new List<LessonDetail>();
                    connection.Open();
                    string selectDetails = @"select id, speed, errors, time, created 
                                            from lessondetails
                                            where lessonId=" + lessonId + @" and
                                            userId=" + userId + 
                                            " order by created desc";
                    SQLiteCommand command = new SQLiteCommand(selectDetails, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LessonDetail detail = new LessonDetail(Convert.ToDouble(reader["speed"]),
                                                                Convert.ToInt32(reader["errors"]),
                                                                reader["time"].ToString(),
                                                                reader["created"].ToString(),
                                                                Convert.ToInt32(reader["id"]));
                        details.Add(detail);
                    }
                    return details;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static string AddLessonDetail(LessonDetail detail)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    List<LessonDetail> details = new List<LessonDetail>();
                    connection.Open();
                    string insertDetail = @"insert into lessondetails(lessonId, userId, speed, 
                                            errors, time) values
                                            (" + detail.LesssonId + ", " + detail.UserId + ", " +
                                               detail.Speed + ", " + detail.Errors + ", '" +
                                               detail.Time + "')";
                    SQLiteCommand command = new SQLiteCommand(insertDetail, connection);
                    command.ExecuteNonQuery();
                    return "Results saved successfully!";
                }
                catch (Exception e)
                {
                    return "Error in saving data!";
                }
            }
        }
    }
}
