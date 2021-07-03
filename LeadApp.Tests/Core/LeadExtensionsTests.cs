using System;
using LeadApp.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadApp.Tests.Core
{
    [TestClass]
    public class LeadExtensionsTests
    {
        [TestMethod]
        public void ToDisplayText_ShouldReturnFormattedTextOfLeads()
        {
            //arrange
            var osVersion = Environment.OSVersion;
            string expectedText;
            if(osVersion.Platform == PlatformID.Win32NT)
            {
                expectedText = "Last Name       | First Name      | Property Type   | Project         | Start Date      | Phone          \r\nDoe             | John            | House           | Paint           | 7/1/2021        | +14045551234   \r\nJackson         | John            | Condo           | Roof            | 7/2/2021        | +14045551235   \r\nSmith           | John            | Trailer         | Plumbing        | 7/3/2021        | +16785551234   \r\nBanks           | John            | House           | Lawncare        | 7/4/2021        | +17705551234   \r\n";
            }
            else
            {
                expectedText = "Last Name       | First Name      | Property Type   | Project         | Start Date      | Phone          \nDoe             | John            | House           | Paint           | 7/1/2021        | +14045551234   \nJackson         | John            | Condo           | Roof            | 7/2/2021        | +14045551235   \nSmith           | John            | Trailer         | Plumbing        | 7/3/2021        | +16785551234   \nBanks           | John            | House           | Lawncare        | 7/4/2021        | +17705551234   \n";
            }

            //act
            var result = MockData.Leads.ToDisplayText();

            //assert
            Assert.AreEqual(expectedText, result);
        }
    }
}
