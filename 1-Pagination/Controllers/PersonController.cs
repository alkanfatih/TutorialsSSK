using _1_Pagination.Contexts;
using _1_Pagination.Models;
using _1_Pagination.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace _1_Pagination.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class PersonController : ControllerBase
    {
       private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersons()
        {
            var person = _context.People.ToList();
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsByPage1(int page=1, int pageSize=5)
        {
            //Toplam data sayısını bul.
            var totalCount = _context.People.Count();

            //Toplam sayfas sayısını bul.
            var totalPages = (int)Math.Ceiling(totalCount / (decimal)pageSize);

            var person = _context.People
                .OrderBy(x => x.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = page,
                PageSize = pageSize,
                Data = person
            };

            if (person == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsByPage2([FromQuery] PersonParametres parametres)
        {
            var persons = _context.People
                .OrderBy(x => x.Id)
                .Skip((parametres.PageNumber-1)*parametres.PageSize)
                .Take(parametres.PageSize)
                .ToList();

            return Ok(persons);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsByPage3([FromQuery] PersonParametres parametres)
        {
            var persons = PagedList<Person>.ToPagedList(_context.People.ToList(), parametres.PageNumber, parametres.PageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(persons.MetaData));

            return Ok(persons);
        }
    }
}
