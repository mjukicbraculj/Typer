using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typist.Objects;
using Typist.Model;

namespace Typist.Contoller
{
    class TextController
    {
        /// <summary>
        /// Method add text to table texts.
        /// Checks length of the text.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public static string AddText(string content, string lesson)
        {
            if (content.Length > 1000)
                return "Text lenght must be less then 1000";
            else if (String.IsNullOrEmpty(content))
                return "Do you want add some text or not?";
            Text text = new Text(content, lesson);
            return TextAccess.AddText(text);
        }

        /// <summary>
        /// Method gets all text contents that 
        /// belong to lesson with given id
        /// </summary>
        /// <param name="lessonId">id of lesson</param>
        /// <returns>list of texts content</returns>
        public static List<string> GetTexts(int lessonId)
        {
            return TextAccess.GetTexts(lessonId);
        }

    }
}
