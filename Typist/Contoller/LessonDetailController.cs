using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Model;
using System.Globalization;

namespace Typist.Contoller
{
    class LessonDetailController
    {

        /// <summary>
        /// Method gets all details about lessons
        /// with given lessonId and userId
        /// </summary>
        /// <param name="lessonId">id of lesson</param>
        /// <param name="userId">id of user</param>
        /// <returns>list of details</returns>
        public static List<LessonDetail> GetLessonDetails(int lessonId, int userId)
        {
            return LessonDetailAccess.GetLessonDetails(lessonId, userId);
        }

        /// <summary>
        /// Mehod adds lessondetail to database
        /// </summary>
        /// <param name="detail">detail to add</param>
        /// <returns></returns>
        public static string AddLessonDetail(LessonDetail detail)
        {
            return LessonDetailAccess.AddLessonDetail(detail);
        }

        /// <summary>
        /// Finds user id.
        /// Gets dictionary that group details by groupTypes (beginner, ...).
        /// Calculates average lessonDetail for every groupType.
        /// Puts the in list.
        /// </summary>
        /// <param name="username">Username of current user</param>
        /// <returns>list of average lessonDetails</returns>
        public static List<LessonDetail> GetLessonDetailsByUser(string username)
        {
            int userId = UserContoller.GetUserId(username);
            Dictionary<string, List<LessonDetail>> groupLessonsDict = LessonDetailAccess.GetLessonDetailsByUser(userId);
            List<LessonDetail> list = new List<LessonDetail>();
            foreach (string groupType in LessonController.GetGroupTypes())
            {
                LessonDetail averageDetail = MakeAverageLessonDetail(groupType, groupLessonsDict);
                list.Add(averageDetail);
            }
           
            list.RemoveAll(item => item == null);
            return list;
        }


        /// <summary>
        /// Method calculates average lesson detail
        /// </summary>
        /// <param name="key">groupType</param>
        /// <param name="dict">binds groupTypes and lesson details</param>
        /// <returns>average lesson detail</returns>
        private static LessonDetail MakeAverageLessonDetail(string key, Dictionary<string, List<LessonDetail>> dict)
        {
            LessonDetail averageDetail = new LessonDetail();
            double seconds = 0;
            foreach(LessonDetail detail in dict[key]) 
            {
                averageDetail.Speed += detail.Speed;
                averageDetail.Errors += detail.Errors;
                seconds += TimeSpan.Parse(detail.Time).TotalSeconds;

            }
            if (dict[key].Count() > 0)
            {
                averageDetail.Speed /= dict[key].Count();
                averageDetail.Speed = Math.Round(averageDetail.Speed, 3);
                averageDetail.Errors = Convert.ToInt32((double)averageDetail.Errors / dict[key].Count());
                averageDetail.Time = TimeSpan.FromSeconds((seconds / dict[key].Count())).ToString();
                averageDetail.Created = dict[key][dict[key].Count()-1].Created;
                averageDetail.Parent = key;
                return averageDetail;
            }
            return null;
        }
    }
}
