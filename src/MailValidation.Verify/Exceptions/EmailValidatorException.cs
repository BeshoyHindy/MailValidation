using System;
using System.Runtime.Serialization;

namespace MailValidation.Verify.Exceptions
{
    public class MailValidationException :  Exception
    {
        public MailValidationException()
        {
        }

        public MailValidationException(string message) : base(message)
        {
        }

        public MailValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MailValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}