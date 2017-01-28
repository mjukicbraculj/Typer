﻿using System;
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
        private double speed;
        public double Speed 
        {
            get
            {
                return speed;
            }
            set
            {
                string val = value.ToString();
                value = Double.Parse(val.Replace(",", "."), CultureInfo.InvariantCulture);
                this.speed = value;
            }
        }
        public int Errors { get; set; }
        public double Time { get; set; }
        public string Created { get; set; }
        public string Parent { get; set; }
        public LessonDetail(double speed, int errors, double time, string created,
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
