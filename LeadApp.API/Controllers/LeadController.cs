using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadApp.Core.Mappers;
using LeadApp.Domain.Interfaces;
using LeadApp.Objects.DataTransferObjects;
using LeadApp.Objects.Enums;
using LeadApp.Objects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeadApp.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        private static List<LeadDTO> Leads = new();
        private readonly ILogger<LeadController> logger;
        private readonly ILeadDomain leadDomain;

        public LeadController(ILogger<LeadController> logger, ILeadDomain leadDomain)
        {
            this.logger = logger;
            this.leadDomain = leadDomain;
            LoadData();
        }

        /// <summary>
        /// Create Lead
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST /Lead
        ///     {
        ///         "leadData": "Doe | John | House | Paint | 6/30/2021 | +1415550132"
        ///     }
        /// </remarks>
        /// <param name="leadData"></param>
        /// <returns>A newly created Lead</returns>
        /// <response code="201">Returns the newly created lead</response>        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Lead> CreateLead(string leadData)
        {
            try
            {
                LeadDTO lead = leadDomain.ParseLead(leadData);
                Leads.Add(lead);
                return new CreatedResult("/", LeadMapper.ToLeadMap(lead));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message, new { leadData });
                return Problem();
            }

        }

        /// <summary>
        /// Get leads sorted by property type
        /// </summary>
        /// <remarks>
        /// Sample request: /Lead/propertyType/asc
        /// </remarks>
        /// <param name="sort">Direction of sorting (asc | desc)</param>
        /// <returns>A sorted list of Leads</returns>
        [HttpGet("propertyType/{sort}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Lead>))]
        public ActionResult<List<Lead>> GetLeadsSortedByPropertTypeAsync(string sort)
        {
            try
            {
                SortType sortType = sort == "asc" ? SortType.PropertyTypeAsc : SortType.PropertyTypeDesc;
                IList<LeadDTO> leads = leadDomain.SortBy(Leads, sortType).ToList();
                return new OkObjectResult(LeadMapper.ToLeadMap(leads));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Get leads sorted by start date
        /// </summary>
        /// <remarks>
        /// Sample request: /Lead/startDate/asc
        /// </remarks>
        /// <param name="sort">Direction of sorting (asc | desc)</param>
        /// <returns>A sorted list of Leads</returns>
        [HttpGet("startDate/{sort}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Lead>))]
        public ActionResult<List<Lead>> GetLeadsSortedByStartDateAsync(string sort)
        {
            try
            {
                SortType sortType = sort == "asc" ? SortType.StartDateAsc : SortType.StartDateDesc;
                IList<LeadDTO> leads = leadDomain.SortBy(Leads, sortType).ToList();
                return new OkObjectResult(LeadMapper.ToLeadMap(leads));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        /// <summary>
        /// Get leads sorted by project
        /// </summary>
        /// <remarks>
        /// Sample request: /Lead/project/asc
        /// </remarks>
        /// <param name="sort">Direction of sorting (asc | desc)</param>
        /// <returns>A sorted list of leads</returns>
        [HttpGet("project/{sort}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Lead>))]
        public ActionResult<List<Lead>> GetLeadsSortedByProjectsync(string sort)
        {
            try
            {
                SortType sortType = sort == "asc" ? SortType.ProjectAsc : SortType.ProjectDesc;
                IList<LeadDTO> leads = leadDomain.SortBy(Leads, sortType).ToList();
                return new OkObjectResult(LeadMapper.ToLeadMap(leads));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem();
            }

        }

        /// <summary>
        /// Get duplicate leads found in external source
        /// </summary>
        /// <returns>A list of duplicate leads</returns>
        [HttpGet("duplicates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Lead>))]
        public async Task<ActionResult<List<Lead>>> GetDuplicateLeadsAsync()
        {
            try
            {
                IList<LeadDTO> externalLeads = await leadDomain.GetExternalLeads();
                IList<LeadDTO> duplicateLeads = leadDomain.GetDuplicates(Leads, externalLeads.ToList());
                return new OkObjectResult(LeadMapper.ToLeadMap(duplicateLeads));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Problem();
            }
        }

        private void LoadData()
        {
            if(Leads.Count > 0)
            {
                return;
            }
            Leads.AddRange(new List<LeadDTO>()
            {
                new()
                {
                    FirstName = "Summer",
                    LastName = "Emmerich",
                    Phone = "+43177437725",
                    Project = "HVAC",
                    PropertyType = PropertyType.Trailer,
                    StartDate = DateTime.Parse("10/18/2021")
                },
                new()
                {
                    FirstName = "Evette",
                    LastName = "Homenick",
                    Phone = "+50250700115",
                    Project = "Soft Flooring and Base",
                    PropertyType = PropertyType.Trailer,
                    StartDate = DateTime.Parse("11/14/2021")
                },
                new()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    PropertyType = PropertyType.House,
                    Project = "Paint",
                    StartDate = DateTime.Parse("2021-07-01"),
                    Phone = "+14045551234"
                },
                new()
                {
                    FirstName = "John",
                    LastName = "Jackson",
                    PropertyType = PropertyType.Condo,
                    Project = "Roof",
                    StartDate = DateTime.Parse("2021-07-02"),
                    Phone = "+14045551235"
                }
            });
        }
    }
}
