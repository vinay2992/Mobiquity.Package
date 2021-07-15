using System;
using System.Collections.Generic;
using System.Text;

namespace com.mobiquity.exception
{
    public sealed class APIException : Exception
    {
        public string Msg { get; set; }

        public APIException(string msg): base(msg)
        {
            Msg = msg;
        }
    }
}
