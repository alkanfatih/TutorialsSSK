using _1_Pagination.Contexts;
using _1_Pagination.Exceptions;
using _1_Pagination.Extensions;
using _1_Pagination.Models;
using _1_Pagination.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData;
using System.Text.Json;

namespace _1_Pagination.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class ShortingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShortingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonShorting([FromQuery] PersonParametres parametres)
        {
            if (!parametres.ValidAgeRange)
                throw new AgeOutOfRangeBadRequestException();

            var persons = _context.People;
            var result = PersonExtensions.FilterPersons(persons, parametres.MinAge, parametres.MaxAge);
            result = PersonExtensions.Search(result, parametres.SearchTerm);
            result = PersonExtensions.Sort(result, parametres.OrderBy);

            var resul2 = PagedList<Person>.ToPagedList(result, parametres.PageNumber, parametres.PageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(resul2.MetaData));

            return Ok(resul2);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonShorting2(ODataQueryOptions<Person> options)
        { 
            var restul = options.ApplyTo(_context.People.AsQueryable()) as IQueryable<Person>;
            return Ok(restul);
        }
    }
}
