using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectOL.BL.DTOs;
using ProjectOL.BL.Models;
using System;
using System.Linq;
using System.Net;

namespace ProjectOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectLanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ProjectOLContext context;
        public ProjectLanguagesController(ProjectOLContext context,
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
        [Route("GetAll/{id}")]
        public IActionResult GetAll(int id)
        {
            var projectLanguages = context.ProjectLanguages
                .Include(x => x.Language)
                .Include(x => x.Project)
                .Where(x => x.ProjectId == id).ToList();
            var projectLanguagesDTO = projectLanguages.Select(x => mapper.Map<ProjectLanguageDTOs>(x)).OrderByDescending(x => x.Id).ToList();

            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectLanguagesDTO });
        }

        /// <summary>
        /// Crea un objeto.
        /// </summary>
        /// <param name="customerDTO">Objeto</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProjectLanguageDTOs projectLanguageDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var projectLanguage = context.ProjectLanguages.Add(mapper.Map<ProjectLanguage>(projectLanguageDTO)).Entity;
                context.SaveChanges();
                projectLanguage.Id = projectLanguage.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectLanguageDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
