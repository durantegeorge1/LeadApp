using System;
using LeadApp.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadApp.Tests.Core
{
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("+14045551234", "+1 404 555 1234")]
        [DataRow("+124045551234", "+12 404 555 1234")]
        [DataRow("4045551234", "+1 404 555 1234")]
        [DataRow("", "")]
        public void ToPhoneNumber_ShouldReturnFormattedPhoneNumberString(string phoneNumber, string expectedFormattedPhoneNumber)
        {
            //act
            string formattedPhoneNumber = phoneNumber.ToPhoneNumber();

            //assert
            Assert.AreEqual(expectedFormattedPhoneNumber, formattedPhoneNumber);
        }
    }
}
