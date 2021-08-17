using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class IncorrectCredentialException:Exception
    {
        public IncorrectCredentialException(string message):base(message)
        {

        }
    }
}
