using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Model;
namespace Typist.Contoller
{
    class LessonController
    {
        public static string AddLesson(string name, string parent)
        {
            Lesson lesson = new Lesson(name, parent);
            return LessonAcces.AddLesson(lesson);
        }

        public static List<string> GetLessonsName(string parent)
        {
            return LessonAcces.GetLessonsName(parent);
        }

        public static int GetLessonId(string name)
        {
            return LessonAcces.getLessonId(name);
        }
    }
}
