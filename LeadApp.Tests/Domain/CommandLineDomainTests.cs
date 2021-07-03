using System;
using System.Collections.Generic;
using LeadApp.Domain;
using LeadApp.Objects.CommandLineArguments;
using LeadApp.Services.CommandLineService.CommandLineOptions;
using LeadApp.Services.CommandLineService.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LeadApp.Tests.Domain
{
    [TestClass]
    public class CommandLineDomainTests
    {
        [TestMethod]
        public void GetLeadProcessorArguments_ShouldReturnLeadProcessorArguments()
        {
            //arrange
            LeadProcessorOptions options = new()
            {
                FilePath = "Pipe.txt",
                PrimarySortType = "LastNameAsc",
                ExtendedSortTypeList = new List<string>() { "FirstNameDesc" }
            };
            Mock<ICommandLineService> mockCommandLineService = new();
            mockCommandLineService.Setup(cls => cls.ParseArugments<LeadProcessorOptions>(It.IsAny<string[]>())).Returns(new LeadProcessorOptions());
            var sut = new CommandLineDomain(mockCommandLineService.Object);

            //act
            LeadProcessorArguments result = sut.GetLeadProcessorArguments(new string[] { "" });

            //assert
            Assert.IsInstanceOfType(result, typeof(LeadProcessorArguments));
        }
    }
}
