using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.OutputResponce
{
        public class Responce<T>
        {
            public bool Suceess { get; set; } = false;
            public string Message { get; set; }
            public T? Data { get; set; }
        }
}
