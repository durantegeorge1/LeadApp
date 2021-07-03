using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadApp.API.Controllers;
using LeadApp.Domain.Interfaces;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LeadApp.Tests.API
{
    [TestClass]
    public class LeadControllerTests
    {
        Mock<ILogger<LeadController>> mockLogger;
        Mock<ILeadDomain> mockLeadDomain;
        LeadController leadController;

        [TestInitialize]
        public void Init()
        {
            mockLogger = new();
            mockLeadDomain = new();
            mockLeadDomain.Setup(ld => ld.ParseLead(It.IsAny<string>())).Returns(MockData.InternalLeads[0]);
            mockLeadDomain.Setup(ld => ld.SortBy(It.IsAny<List<LeadDTO>>(), It.IsAny<SortType>())).Returns(MockData.InternalLeads.OrderBy(l => l.FirstName));
            mockLeadDomain.Setup(ld => ld.GetExternalLeads()).Returns(Task.FromResult((IList<LeadDTO>)MockData.InternalLeads));
            mockLeadDomain.Setup(ld => ld.GetDuplicates(It.IsAny<List<LeadDTO>>(), It.IsAny<List<LeadDTO>>())).Returns(MockData.InternalLeads);
            leadController = new(mockLogger.Object, mockLeadDomain.Object);
        }

        [TestMethod]
        public void CreateLead_ShouldReturnCreatedResult()
        {
            //act
            var result = leadController.CreateLead("");

            //assert
            Assert.IsInstanceOfType(result.Result, typeof(CreatedResult));
        }

        [TestMethod]
        public void GetLeadsSortedByPropertTypeAsync_ShouldReturnOkObjectResult()
        {
            //act
            var result = leadController.GetLeadsSortedByPropertTypeAsync("");

            //assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetLeadsSortedByStartDateAsync_ShouldReturnOkObjectResult()
        {
            //act
            var result = leadController.GetLeadsSortedByStartDateAsync("");

            //assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetLeadsSortedByProjectsync_ShouldReturnOkObjectResult()
        {
            //act
            var result = leadController.GetLeadsSortedByProjectsync("");

            //assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetDuplicateLeadsAsync_ShouldReturnOkObjectResult()
        {
            //act
            var result = await leadController.GetDuplicateLeadsAsync();

            //assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
