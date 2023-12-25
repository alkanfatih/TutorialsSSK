using _1_Pagination.Contexts;
using _1_Pagination.Exceptions;
using _1_Pagination.Extensions;
using _1_Pagination.Models;
using _1_Pagination.RequestFeatures;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Text.Json;

namespace _1_Pagination.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        //searchTerm=Araba - Lucene.net
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsBySearch([FromQuery] PersonParametres parametres)
        {
            if (!parametres.ValidAgeRange)
                throw new AgeOutOfRangeBadRequestException();

            var persons = PersonExtensions.FilterPersons(_context.People, parametres.MinAge, parametres.MaxAge);
            persons = PersonExtensions.Search(persons, parametres.SearchTerm);

            var persons2 = PagedList<Person>.ToPagedList(persons, parametres.PageNumber, parametres.PageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(persons2.MetaData));

            return Ok(persons2);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsBySearch2(ODataQueryOptions<Person> options)
        { 
            var results = options.ApplyTo(_context.People.AsQueryable()) as IQueryable<Person>;
            return Ok(results);
        }
    }
}
