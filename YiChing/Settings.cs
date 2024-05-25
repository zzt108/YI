using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiChing
{
    public class Settings
    {
        public string AnswerLanguage { get; set; }
        public string KeyTwo { get; set; }
        public NestedSettings KeyThree { get; set; }
    }

    public class NestedSettings
    {
        public string Message { get; set; }
    }
}
