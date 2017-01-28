using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using System.Data.SQLite;
using Typist.Model;

namespace Typist.Model
{
    class LessonDetailAccess
    {
        /// <summary>
        /// Method finds all lesson details from table lessondetails
        /// that have given lessonId and userId
        /// </summary>
        /// <param name="lessonId">value of column leesonId</param>
        /// <param name="userId">value od columnn userId</param>
        /// <returns>list of lessondetails</returns>
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
                                                                Convert.ToDouble(reader["time"]),
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


        /// <summary>
        /// Method adds lessondetail to table lessondetails.
        /// Id and created columns have triggers (autoincrement and current_timestamp)
        /// </summary>
        /// <param name="detail"></param>
        /// <returns>message about success</returns>
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
                                               Math.Round(detail.Speed, 3) + ", " + detail.Errors + ", '" +
                                               detail.Time + "')";
                    SQLiteCommand command = new SQLiteCommand(insertDetail, connection);
                    command.ExecuteNonQuery();
                    return "Results saved successfully! " + insertDetail;
                }
                catch (Exception e)
                {
                    return "Error in saving data!";
                }
            }
        }

        /// <summary>
        /// Method finds all details that belong to
        /// user with given userId and groups them by 
        /// groupType (Beginner, Intermediate, ...)
        /// </summary>
        /// <param name="userId">value of column userId</param>
        /// <returns>dictionary that maps groupType to list of lesson details</returns>
        public static Dictionary<string, List<LessonDetail>> GetLessonDetailsByUser(int userId)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    Dictionary<string, List<LessonDetail>> groupLessonsDict = new Dictionary<string, List<LessonDetail>>();
                    groupLessonsDict["Beginner"] = new List<LessonDetail>();
                    groupLessonsDict["Intermediate"] = new List<LessonDetail>();
                    groupLessonsDict["Advanced"] = new List<LessonDetail>();
                    groupLessonsDict["Practise"] = new List<LessonDetail>();
                    string selectDetails = @"select lessondetails.id, speed, errors, time, lessondetails.created, parent
                                            from lessondetails inner join lessons
                                            on lessondetails.lessonId=lessons.id
                                            where userId=" + userId +
                                           " order by lessondetails.created desc";
                    SQLiteCommand command = new SQLiteCommand(selectDetails, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        LessonDetail detail = new LessonDetail(Convert.ToDouble(reader["speed"]),
                                                                Convert.ToInt32(reader["errors"]),
                                                                Convert.ToDouble(reader["time"]),
                                                                reader["created"].ToString(),
                                                                Convert.ToInt32(reader["id"]));
                        groupLessonsDict[reader["parent"].ToString()].Add(detail);

                    }
                    return groupLessonsDict;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
