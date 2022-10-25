using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Application.Exceptions
{

    public class DbAccessException : Exception
    {
        public DbAccessException()
        {
        }

        public DbAccessException(string message)
            : base(message)
        {
        }
    }

}
