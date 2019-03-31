using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProperArch01.Contracts.Constants
{
    public class ClassAttendanceResponse
    {
        public const int Success = 0;
        public const int ClassNotFound = 1;
        public const int ClassCancelled = 2;
        public const int UnspecifiedError = 3;
    }
}