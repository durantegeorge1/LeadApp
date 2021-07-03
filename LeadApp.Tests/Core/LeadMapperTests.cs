using System;
using System.Collections.Generic;
using System.Reflection;
using LeadApp.Core.Mappers;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using LeadApp.Objects.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadApp.Tests.Core
{
    [TestClass]
    public class LeadMapperTests
    {
        [TestMethod]
        public void ToLeadDTOMap_ShouldMapALeadToALeadDTOAndReturnALeadDTO()
        {
            //arrange
            Lead sut = new()
            {
                FirstName = "John",
                LastName = "Doe",
                PropertyType = "House",
                Project = "Roof",
                PhoneNumber = "+1 404 555 0671",
                StartDate = "7/1/2021"
            };

            //act
            LeadDTO result = sut.ToLeadDTOMap();

            //assert
            Assert.IsInstanceOfType(result, typeof(LeadDTO));
            Type type = result.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                Assert.IsNotNull(property.GetValue(result));
            }
        }

        [TestMethod]
        public void ToLeadDTOMap_ShouldMapAListOfLeadsToAListOfLeadDTOsAndReturnAListOfLeadDTOs()
        {
            //arrange
            List<Lead> sut = new()
            {
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PropertyType = "House",
                    Project = "Roof",
                    PhoneNumber = "+1 404 555 0671",
                    StartDate = "7/1/2021"
                },
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PropertyType = "House",
                    Project = "Roof",
                    PhoneNumber = "+1 404 555 0671",
                    StartDate = "7/1/2021"
                }
            };

            //act
            IList<LeadDTO> result = sut.ToLeadDTOMap();

            //assert
            Assert.IsInstanceOfType(result, typeof(IList<LeadDTO>));
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void ToLeadMap_ShouldMapALeadDTOToALeadAndReturnALead()
        {
            //arrange
            LeadDTO sut = new()
            {
                FirstName = "John",
                LastName = "Doe",
                PropertyType = PropertyType.House,
                Project = "Roof",
                Phone = "+14045550671",
                StartDate = DateTime.Parse("07/01/2021")
            };

            //act
            Lead result = sut.ToLeadMap();

            //assert
            Assert.IsInstanceOfType(result, typeof(Lead));
            Type type = result.GetType();
            PropertyInfo[] propertyInfo = type.GetProperties();
            foreach (PropertyInfo property in propertyInfo)
            {
                Assert.IsNotNull(property.GetValue(result));
            }
        }

        [TestMethod]
        public void ToLeadMap_ShouldMapAListOfLeadDTOsToAListOfLeadsAndReturnAListOfLeads()
        {
            //arrange
            List<LeadDTO> sut = new()
            {
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PropertyType = PropertyType.House,
                    Project = "Roof",
                    Phone = "+14045550671",
                    StartDate = DateTime.Parse("07/01/2021")
                },
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PropertyType = PropertyType.House,
                    Project = "Roof",
                    Phone = "+14045550671",
                    StartDate = DateTime.Parse("07/01/2021")
                }
            };

            //act
            IList<Lead> result = sut.ToLeadMap();

            //assert
            Assert.IsInstanceOfType(result, typeof(IList<Lead>));
            Assert.AreEqual(2, result.Count);
        }
    }
}
