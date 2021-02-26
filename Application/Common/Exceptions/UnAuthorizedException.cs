using System;

namespace Application.Common.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string userName) : base("User is unauthorized")
        {

        }
    }
}