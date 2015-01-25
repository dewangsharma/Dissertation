using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myWebAspClient
{
    public class testOption
    {
        private static List<string> _option = new List<string>();
        public List<string> Option
        {
            get
            {
                return _option;
            }
            set
            {
                _option = value;
            }
        }
    }
}