using System;
using System.Collections.Generic;
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
        public double Speed { get; set; }
        public int Errors { get; set; }
        public string Time { get; set; }
        public string Created { get; set; }

        public LessonDetail(double speed, int errors, string time, string created,
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

    }
}
