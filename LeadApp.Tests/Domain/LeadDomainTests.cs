using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LeadApp.Domain;
using LeadApp.Objects;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using LeadApp.Services.FileService.Interfaces;
using LeadApp.Services.HttpClientService.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace LeadApp.Tests.Domain
{
    [TestClass]
    public class LeadDomainTests
    {
        private const string PipeDelimintedMockFileContents =
            "Doe|John|House|Paint|6/30/2021|+14045551234\r\n" +
            "Doe|Jane|Condo|Plumbing|2021-07-01|+14045551235\r\n" +
            "Bar|Foo|Trailer|Roof|2021-07-02|+17705551234";

        private const string CommaDelimintedMockFileContents =
            "Doe,John,House,Paint,6/30/2021,+14045551234\r\n" +
            "Doe,Jane,Condo,Plumbing,2021-07-01,+14045551235\r\n" +
            "Bar,Foo,Trailer,Roof,2021-07-02,+17705551234";

        private const string SpaceDelimintedMockFileContents =
            "Doe John House Paint 6/30/2021 +14045551234\r\n" +
            "Doe Jane Condo Plumbing 2021-07-01 +14045551235\r\n" +
            "Bar Foo Trailer Roof 2021-07-02 +17705551234";

        private LeadDomain leadDomain;
        private Mock<IOptions<AppSettings>> mockOptions;
        private Mock<IFileService> mockFileService;
        private Mock<IHttpClientService> mockHtppClientService;

        [TestInitialize]
        public void Init()
        {
            MemoryStream pipeMemoryStream = new(Encoding.UTF8.GetBytes(PipeDelimintedMockFileContents));
            MemoryStream commaMemoryStream = new(Encoding.UTF8.GetBytes(CommaDelimintedMockFileContents));
            MemoryStream spaceMemoryStream = new(Encoding.UTF8.GetBytes(SpaceDelimintedMockFileContents));

            string serializedLeads = JsonConvert.SerializeObject(MockData.ExternalLeads);
            HttpContent mockContent = new StringContent(serializedLeads);

            mockOptions = new Mock<IOptions<AppSettings>>();
            mockFileService = new Mock<IFileService>();
            mockHtppClientService = new Mock<IHttpClientService>();

            mockOptions.Setup(o => o.Value).Returns(new AppSettings());

            mockFileService.Setup(fs => fs.StreamReader(It.Is<string>(p => p == "pipe"))).Returns(new StreamReader(pipeMemoryStream));
            mockFileService.Setup(fs => fs.StreamReader(It.Is<string>(p => p == "comma"))).Returns(new StreamReader(commaMemoryStream));
            mockFileService.Setup(fs => fs.StreamReader(It.Is<string>(p => p == "space"))).Returns(new StreamReader(spaceMemoryStream));

            mockHtppClientService.Setup(hcs => hcs.SendAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK, Content = mockContent });

            leadDomain = new LeadDomain(mockOptions.Object, mockFileService.Object, mockHtppClientService.Object);
        }

        [TestMethod]
        public void GetDuplicates_ShouldFindDuplicateLeadsAndReturnListOfLeads()
        {
            //act
            IList<LeadDTO> result = leadDomain.GetDuplicates(MockData.InternalLeads, MockData.ExternalLeads);

            //assert
            Assert.IsInstanceOfType(result, typeof(IList<LeadDTO>));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetExternalLeads_ShouldReturnAListOfLeads()
        {
            //act
            IList<LeadDTO> result = await leadDomain.GetExternalLeads();

            //assert
            Assert.IsInstanceOfType(result, typeof(IList<LeadDTO>));
            Assert.AreEqual(4, result.Count);
        }

        [DataTestMethod]
        [DataRow("pipe")]
        [DataRow("comma")]
        [DataRow("space")]
        public void ParseFileAysnc_ShouldParseAFileAndReturnAListOfLeads(string file)
        {
            //act
            IList<LeadDTO> result = leadDomain.ParseFile(file);

            //assertˆ
            Assert.IsInstanceOfType(result, typeof(IList<LeadDTO>));
            Assert.AreEqual(3, result.Count);
        }

        [DataTestMethod]
        [DataRow("Doe|John|House|Paint|7/01/2021|+14045551234")]
        [DataRow("Doe,John,House,Paint,7/01/2021,+14045551234")]
        [DataRow("Doe John House Paint 7/01/2021 +14045551234")]
        public void ParseLeadAsync_ShouldParseAStringAndReturnALead(string lead)
        {
            //act
            LeadDTO result = leadDomain.ParseLead(lead);

            //assert
            Assert.IsInstanceOfType(result, typeof(LeadDTO));
            Assert.AreEqual("Doe", result.LastName);
            Assert.AreEqual("John", result.LastName);
            Assert.AreEqual("House", result.LastName);
            Assert.AreEqual("Paint", result.LastName);
            Assert.AreEqual("07/01/2021", result.LastName);
            Assert.AreEqual("+14045551234", result.LastName);
        }

        [DataTestMethod]
        [DataRow(SortType.LastNameAsc)]
        [DataRow(SortType.LastNameDesc)]
        [DataRow(SortType.PropertyTypeAsc)]
        [DataRow(SortType.PropertyTypeDesc)]
        [DataRow(SortType.ProjectAsc)]
        [DataRow(SortType.ProjectDesc)]
        [DataRow(SortType.StartDateAsc)]
        [DataRow(SortType.StartDateDesc)]
        public void SortByAsync_ShouldSortLeadsAndReturnAListOfLeads(SortType sortType)
        {
            //act
            IOrderedEnumerable<LeadDTO> result = leadDomain.SortBy(MockData.InternalLeads, sortType);

            //assert
            Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<LeadDTO>));
            List<LeadDTO> sortedList = result.ToList();
            switch (sortType)
            {
                case SortType.LastNameAsc:
                    Assert.AreEqual("Banks", sortedList[0].LastName);
                    Assert.AreEqual("Smith", sortedList[3].LastName);
                    break;
                case SortType.LastNameDesc:
                    Assert.AreEqual("Smith", sortedList[0].LastName);
                    Assert.AreEqual("Banks", sortedList[3].LastName);
                    break;
                case SortType.PropertyTypeAsc:
                    Assert.AreEqual((int)PropertyType.Condo, (int)sortedList[0].PropertyType);
                    Assert.AreEqual(PropertyType.Trailer, sortedList[3].PropertyType);
                    break;
                case SortType.PropertyTypeDesc:
                    Assert.AreEqual(PropertyType.Trailer, sortedList[0].PropertyType);
                    Assert.AreEqual(PropertyType.Condo, sortedList[3].PropertyType);
                    break;
                case SortType.ProjectAsc:
                    Assert.AreEqual("Lawncare", sortedList[0].Project);
                    Assert.AreEqual("Roof", sortedList[3].Project);
                    break;
                case SortType.ProjectDesc:
                    Assert.AreEqual("Roof", sortedList[0].Project);
                    Assert.AreEqual("Lawncare", sortedList[3].Project);
                    break;
                case SortType.StartDateAsc:
                    Assert.AreEqual(DateTime.Parse("2021-07-01"), sortedList[0].StartDate);
                    Assert.AreEqual(DateTime.Parse("2021-07-04"), sortedList[3].StartDate);
                    break;
                case SortType.StartDateDesc:
                    Assert.AreEqual(DateTime.Parse("2021-07-04"), sortedList[0].StartDate);
                    Assert.AreEqual(DateTime.Parse("2021-07-01"), sortedList[3].StartDate);
                    break;
                default:
                    break;
            }
        }

        [DataTestMethod]
        [DataRow(SortType.LastNameAsc)]
        [DataRow(SortType.LastNameDesc)]
        [DataRow(SortType.PropertyTypeAsc)]
        [DataRow(SortType.PropertyTypeDesc)]
        [DataRow(SortType.ProjectAsc)]
        [DataRow(SortType.ProjectDesc)]
        [DataRow(SortType.StartDateAsc)]
        [DataRow(SortType.StartDateDesc)]
        public void ThenSortByAsync_ShouldSortLeadsAndReturnAListOfLeads(SortType sortType)
        {
            //arrange
            var list = MockData.InternalLeads.OrderBy(l => l.FirstName);
            var sortTypes = new SortType[] { sortType };
            //act
            IOrderedEnumerable<LeadDTO> result = leadDomain.ThenSortBy(list, sortTypes);

            //assert
            Assert.IsInstanceOfType(result, typeof(IOrderedEnumerable<LeadDTO>));
            List<LeadDTO> sortedList = result.ToList();
            switch (sortType)
            {
                case SortType.LastNameAsc:
                    Assert.AreEqual("Banks", sortedList[0].LastName);
                    Assert.AreEqual("Smith", sortedList[3].LastName);
                    break;
                case SortType.LastNameDesc:
                    Assert.AreEqual("Smith", sortedList[0].LastName);
                    Assert.AreEqual("Banks", sortedList[3].LastName);
                    break;
                case SortType.PropertyTypeAsc:
                    Assert.AreEqual((int)PropertyType.Condo, (int)sortedList[0].PropertyType);
                    Assert.AreEqual(PropertyType.Trailer, sortedList[3].PropertyType);
                    break;
                case SortType.PropertyTypeDesc:
                    Assert.AreEqual(PropertyType.Trailer, sortedList[0].PropertyType);
                    Assert.AreEqual(PropertyType.Condo, sortedList[3].PropertyType);
                    break;
                case SortType.ProjectAsc:
                    Assert.AreEqual("Lawncare", sortedList[0].Project);
                    Assert.AreEqual("Roof", sortedList[3].Project);
                    break;
                case SortType.ProjectDesc:
                    Assert.AreEqual("Roof", sortedList[0].Project);
                    Assert.AreEqual("Lawncare", sortedList[3].Project);
                    break;
                case SortType.StartDateAsc:
                    Assert.AreEqual(DateTime.Parse("2021-07-01"), sortedList[0].StartDate);
                    Assert.AreEqual(DateTime.Parse("2021-07-04"), sortedList[3].StartDate);
                    break;
                case SortType.StartDateDesc:
                    Assert.AreEqual(DateTime.Parse("2021-07-04"), sortedList[0].StartDate);
                    Assert.AreEqual(DateTime.Parse("2021-07-01"), sortedList[3].StartDate);
                    break;
                default:
                    break;
            }
        }
    }
}
