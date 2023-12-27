using _1_Pagination.ActionFilters;
using _1_Pagination.Contexts;
using _1_Pagination.Loggers;
using _1_Pagination.Models;
using _1_Pagination.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace _1_Pagination.Controllers
{
    //[ValidationFilterAttribute]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;
        public ValuesController(AppDbContext context, IMapper mapper, ILoggerService loggerService)
        {
            _context = context;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersons()
        { 
            var result = _context.People.ToList();
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsForOnlyUser()
        {
            var result = _context.People.ToList();
            return Ok(result);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllPersonsForOnlyEditor()
        {
            var result = _context.People.ToList();
            return Ok(result);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidationFilterAttribute]
        //[ServiceFilter(typeof(ValidationFilterAttribute), Order = 1)]
        public IActionResult CreatePerson(PersonDTO model)
        {
            //Person person = new Person();
            //person.Name = model.Name;
            //person.Age = model.Age;

            //if (model is null)
            //    return BadRequest(); // 400

            //if(!ModelState.IsValid)
            //    return UnprocessableEntity(model);

            var person = _mapper.Map<Person>(model);

            _context.People.Add(person);
            if (_context.SaveChanges()>0)
            {
                return Ok(person);
            }
            else
            {
                _loggerService.LogWarning("Dikkat");
                return BadRequest();
                
            }
            
        }
    }
}
