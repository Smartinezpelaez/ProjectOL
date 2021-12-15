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
    public class ProjectStates : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ProjectOLContext context;
        public ProjectStates(ProjectOLContext context, IMapper mapper)

        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de proyectos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var projects = context.ProjectStates.ToList();
            var projectStateDTO = projects.Select(x => mapper.Map<ProjectStateDTOs>(x)).OrderByDescending(x => x.Id);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectStateDTO });
        }

    }
}
