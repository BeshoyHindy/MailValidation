using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailValidation.Verify
{
    public static class Extension
    {
        public static bool MailVerify(this string email)
        {
            return true;
        }


        //public static ValidationStatus MailVerify(this string email)
        //{
        //    return ValidationStatus.OK;
        //}
    }

}
