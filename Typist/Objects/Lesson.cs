using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typist.Objects
{
    class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public Lesson(string name, string parent)
        {
            Name = name;
            Parent = parent;
        }

        public Lesson(int id, string name, string parent)
        {
            Id = id;
            name = Name;
            Parent = parent;
        }
    }
}
