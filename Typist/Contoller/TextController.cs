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
        public static string AddText(string content, string lesson)
        {
            if (content.Length > 1000)
                return "Text lenght must be less then 1000";
            Text text = new Text(content, lesson);
            return TextAccess.AddText(text);
        }

        public static List<string> GetTexts(int lessonId)
        {
            return TextAccess.GetTexts(lessonId);
        }

    }
}
