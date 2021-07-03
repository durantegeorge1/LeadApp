using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LeadApp.CLI.Processors;
using LeadApp.Domain.Interfaces;
using LeadApp.Objects.CommandLineArguments;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LeadApp.Tests.CLI
{
    [TestClass]
    public class LeadProcessorTests
    {
        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void Execute_ShouldExecuteLeadParsingAndSorting(bool IsExtendedSortTypes)
        {
            //arrange
            LeadProcessorArguments args = new()
            {
                FilePath = "",
                PrimarySortType = SortType.LastNameAsc,
                ExtendedSortTypeList = IsExtendedSortTypes ? new SortType[] { SortType.FirstNameDesc } : new SortType[] { }
            };
            Mock<ICommandLineDomain> mockCommandLineDomain = new();
            Mock<ILeadDomain> mockLeadDomain = new();

            mockCommandLineDomain.Setup(cld => cld.GetLeadProcessorArguments(It.IsAny<string[]>())).Returns(args);
            mockLeadDomain.Setup(ld => ld.ParseFile(It.IsAny<string>())).Returns(MockData.InternalLeads);
            mockLeadDomain.Setup(ld => ld.SortBy(It.IsAny<List<LeadDTO>>(), It.IsAny<SortType>())).Returns(MockData.InternalLeads.OrderBy(l => l.LastName));
            mockLeadDomain.Setup(ld => ld.ThenSortBy(It.IsAny<IOrderedEnumerable<LeadDTO>>(), It.IsAny<SortType[]>())).Returns(MockData.InternalLeads.OrderBy(l => l.LastName));

            LeadProcessor sut = new(mockLeadDomain.Object, mockCommandLineDomain.Object);

            //act
            sut.Execute(new string[] { });

            //assert
            mockCommandLineDomain.Verify(m => m.GetLeadProcessorArguments(It.IsAny<string[]>()), Times.Once);
            if (IsExtendedSortTypes)
            {
                mockLeadDomain.Verify(m => m.ParseFile(It.IsAny<string>()), Times.Once);
                mockLeadDomain.Verify(m => m.SortBy(It.IsAny<List<LeadDTO>>(), It.IsAny<SortType>()), Times.Once);
                mockLeadDomain.Verify(m => m.ThenSortBy(It.IsAny<IOrderedEnumerable<LeadDTO>>(), It.IsAny<SortType[]>()), Times.Once);
            }
            else
            {
                mockLeadDomain.Verify(m => m.ParseFile(It.IsAny<string>()), Times.Once);
                mockLeadDomain.Verify(m => m.SortBy(It.IsAny<List<LeadDTO>>(), It.IsAny<SortType>()), Times.Once);
                mockLeadDomain.Verify(m => m.ThenSortBy(It.IsAny<IOrderedEnumerable<LeadDTO>>(), It.IsAny<SortType[]>()), Times.Never);
            }
        }      
    }
}
