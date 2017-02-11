using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typist.Objects
{
    class LessonDetail
    {
        public int Id { get; set; }
        public int LesssonId { get; set; }
        public int UserId { get; set; }
        public string Speed {get;set;}
        public int Errors { get; set; }
        public string Time { get; set; }
        public string Created { get; set; }
        public string Parent { get; set; }
        public LessonDetail(string speed, int errors, string time, string created,
                            int id = -1, int lessonId = -1, int userId = -1)
        {
            Id = id;
            LesssonId = lessonId;
            UserId = userId;
            Speed = speed;
            Errors = errors;
            Time = time;
            Created = created;
        }

        public LessonDetail()
        {
        }
    }
}
