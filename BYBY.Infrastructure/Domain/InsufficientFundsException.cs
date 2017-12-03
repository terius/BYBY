using System;

namespace BYBY.Infrastructure.Domain
{
    [Serializable]
    public class InsufficientFundsException : ApplicationException
    {
        public InsufficientFundsException(string message)
            : base(message)
        {

        }
    }
}
