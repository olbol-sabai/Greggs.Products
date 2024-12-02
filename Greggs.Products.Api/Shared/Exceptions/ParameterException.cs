using System;

namespace Greggs.Products.Api.Shared.Exceptions
{
    public class ParameterException : Exception
    {
        public ParameterException(string message) : base(message)
        {
        }
    }

}
