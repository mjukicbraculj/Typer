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
        /// <summary>
        /// Method adds lesson with given name and parent 
        /// to table lessons using LessonAcess class
        /// </summary>
        /// <param name="name">name of the lesson</param>
        /// <param name="parent">group it belongs to</param>
        /// <returns>message about success</returns>
        public static string AddLesson(string name, string parent)
        {
            Lesson lesson = new Lesson(name, parent);
            return LessonAcces.AddLesson(lesson);
        }

        /// <summary>
        /// Using LessonAcess class method gets all lesson's names 
        /// which have given parent
        /// </summary>
        /// <param name="parent">parent name</param>
        /// <returns>list of lesson's names</returns>
        public static List<string> GetLessonsName(string parent)
        {
            return LessonAcces.GetLessonsName(parent);
        }

        /// <summary>
        /// Using LessonAcess class gets id
        /// of the lesson with given name
        /// </summary>
        /// <param name="name">name of the lesson</param>
        /// <returns>id of lesson</returns>
        public static int GetLessonId(string name)
        {
            return LessonAcces.getLessonId(name);
        }

        /// <summary>
        /// Mehod gets all group types 
        /// of the lessons (beginning, intermediate...)
        /// </summary>
        /// <returns>list of group names</returns>
        public static List<string> GetGroupTypes()
        {
            return LessonAcces.GetGroupTypes();
        }
    }
}
