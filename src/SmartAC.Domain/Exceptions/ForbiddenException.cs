using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Exceptions
{
    /// <summary>
    /// Represents when the application is refusing to perform the action the user supplied
    /// </summary>
    public class ForbiddenException : ApplicationException
    {
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
