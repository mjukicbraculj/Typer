using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Model;

namespace Typist.Contoller
{
    class LessonDetailContoller
    {
        public static List<LessonDetail> GetLessonDetails(int lessonId, int userId)
        {
            return LessonDetailAccess.GetLessonDetails(lessonId, userId);
        }

        public static string AddLessonDetail(LessonDetail detail)
        {
            return LessonDetailAccess.AddLessonDetail(detail);
        }
    }
}
