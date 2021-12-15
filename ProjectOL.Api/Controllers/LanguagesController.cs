using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectOL.BL.DTOs;
using ProjectOL.BL.Models;
using System.Linq;
using System.Net;

namespace ProjectOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ProjectOLContext context;
        public LanguagesController(ProjectOLContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de objetos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var languages = context.Languages.ToList();
            var languagesDTO = languages.Select(x => mapper.Map<LanguageDTOs>(x)).OrderByDescending(x => x.Id);

            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = languagesDTO });
        }
    }
}
