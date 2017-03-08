namespace MailValidation.Verify.Validation
{
    public static class Extension
    {
        public static bool MailVerify(this string email)
        {
            MailVerify mailVerify = new MailVerify();
            return mailVerify.Validate(email) == ValidationStatus.OK;
        }


        public static ValidationStatus MailValidate(this string email)
        {
            MailVerify mailVerify = new MailVerify();
            return mailVerify.Validate(email);
        }
    }

}
