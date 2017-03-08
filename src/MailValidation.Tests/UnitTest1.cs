using System.Collections.Generic;
using MailValidation.Verify.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailValidation.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAvailable()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "mail@zelbike.ru",
                "adoconnection@gmail.com",
                "adoconnection@yandex.ru",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                if (result != ValidationStatus.OK)
                {
                    Assert.Fail("Unable to test " + email);
                }

                Assert.AreEqual(ValidationStatus.OK, result, email);
            }
        }

        [TestMethod]
        public void TestInvalidDomain()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "mail@zelbike1.ru",
                "adoconnection@gmail4411.com",
                "mail@по-кружке.рф",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.NoMailForDomain, result, email);
            }
        }

        [TestMethod]
        public void TestUnableToTest()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "adoconnection@yndx.ru",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.MailServerUnavailable, result, email);
            }
        }

        [TestMethod]
        public void TestNoMailForDomain()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "mail@forum.zelbike.ru",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.NoMailForDomain, result, email);
            }
        }

        [TestMethod]
        public void TestAddressIsEmpty()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "",
                "  ",
                null,
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.AddressIsEmpty, result, email);
            }
        }

        [TestMethod]
        public void TestInvalidFormat()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "mail.zelbike1.ru",
                "adoconnec tion@gmail1.com",
                "adoconnection@yandex1@",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.InvalidFormat, result, email);
            }
        }

        [TestMethod]
        public void TestMailboxUnavailable()
        {
            MailVerify mailVerify = new MailVerify();

            ValidationStatus result = mailVerify.Validate("zxcvbnmasdfghjk112233@gmail.com");

            Assert.AreEqual(ValidationStatus.MailboxUnavailable, result);
        }

        [TestMethod]
        public void TestDisposableMail()
        {
            MailVerify mailVerify = new MailVerify();

            IList<string> emails = new List<string>()
            {
                "bkt96816@psoxs.com",
                "l1385634@mvrht.com",
                "hendy1@wn8c38i.com",
                "tephoslaph@housat.com",
                "6tup1m+bdzhfc1sm34pc@guerrillamail.de",
                "yxqv@e.amav.ro",
                "xare@lillemap.net",
            };

            foreach (string email in emails)
            {
                ValidationStatus result = mailVerify.Validate(email);

                Assert.AreEqual(ValidationStatus.MailboxIsDisposable, result, email);
            }
        }
    }
}
