using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typist.Objects
{
    class Text
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Lesson { get; set; }

        public Text(string content, string lesson)
        {
            Content = content;
            Lesson = lesson;
        }

        public Text(int id, string content, string lesson)
        {
            Id = id;
            Content = content;
            Lesson = lesson;
        }

    }
}
