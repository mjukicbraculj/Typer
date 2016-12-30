using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using System.Data.SQLite;

namespace Typist.Model
{
    class TextAccess
    {

        /// <summary>
        /// Method adds text to table texts
        /// </summary>
        /// <param name="text"></param>
        /// <returns>message about success</returns>
        public static string AddText(Text text)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    connection.Open();
                    int lessonId = LessonAcces.getLessonId(text.Lesson);
                    if (lessonId == -1)
                        return "Unkonwn lesson!";
                    else
                    {
                        string insertText = @"insert into texts(lessonId, text)
                                                values (" + lessonId + ", '" + text.Content + "')";
                        SQLiteCommand command1 = new SQLiteCommand(insertText, connection);
                        command1.ExecuteNonQuery();
                        return "Done successfully! :)";
                    }
                }
                catch (Exception e)
                {
                    return "Error in adding text";
                }
            }
        }

        /// <summary>
        /// Method finds rows of table texts with given lessonId
        /// Makes list of column text.
        /// </summary>
        /// <param name="lesssonId"></param>
        /// <returns>list of texts</returns>
        public static List<string> GetTexts(int lesssonId)
        {
            using (SQLiteConnection connection = DB.GetConnection())
            {
                try
                {
                    List<string> texts = new List<string>();
                    connection.Open();
                    string selectTexts = @"select text from texts 
                                            where lessonId=" + lesssonId;
                    SQLiteCommand command = new SQLiteCommand(selectTexts, connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                        texts.Add(reader["text"].ToString());
                    return texts;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
