using _1_Pagination.Contexts;
using _1_Pagination.Exceptions;
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
    [ApiExplorerSettings(GroupName = "v2")]
    public class FilterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilterController(AppDbContext context)
        {
            _context = context;
        }

        //filer?minAge=20&max=30
        //filter?minAg>20
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsByFilter([FromQuery] PersonParametres parametres)
        {
            try
            {
                if (!parametres.ValidAgeRange)
                    throw new AgeOutOfRangeBadRequestException();

                var person2 = PagedList<Person>.ToPagedList(_context.People.Where(p => p.Age >= parametres.MinAge && p.Age <= parametres.MaxAge).ToList(), parametres.PageNumber, parametres.PageSize);

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(person2.MetaData));

                return Ok(person2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("[action]")]
        //https://localhost:7073/api/Filters/GetAllPersonsByFilter2?$filter=age%20lt%2025
        /*
         $filter=Age gt 30 -> Yaşı dan büyük olanlar
         $filter=Age lt 30 -> Yaşı dan küçük olanlar
         $filter=Age ge 30 -> veya Yaşı dan büyük olanlar
         $filter=Age le 30 -> veya Yaşı dan küçük olanlar
         */
        public IActionResult GetAllPersonsByFilter2(ODataQueryOptions<Person> options)
        {
            var results = options.ApplyTo(_context.People.AsQueryable()) as IQueryable<Person>;
            return Ok(results);
        }
    }
}
