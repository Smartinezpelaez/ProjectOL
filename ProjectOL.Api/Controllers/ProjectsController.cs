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
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ProjectOLContext context;
        public ProjectsController(ProjectOLContext context, IMapper mapper)

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
            var projects = context.Projects.Include(x => x.Customer).Include(x=> x.ProjectState).ToList();
            var projectsDTO = projects.Select(x => mapper.Map<ProjectDTOs>(x)).OrderByDescending(x => x.Id);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectsDTO });
        }

        /// <summary>
        /// Obtiene un cliente  por su id.
        /// </summary>
        /// <param name="id">Id del Customers</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")] //  api/Customers/GetById/1
        public IActionResult GetById(int id)
        {
            var projects = context.Projects.Include(x => x.Customer).Include(x => x.ProjectState).FirstOrDefault(x => x.Id == id);
            if (projects == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var projectsDTO = mapper.Map<ProjectDTOs>(projects);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectsDTO });
        }

        /// <summary>
        /// Crea un objeto.
        /// </summary>
        /// <param name="projectsDTO">Objeto </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProjectDTOs projectsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var projects = context.Projects.Add(mapper.Map<Project>(projectsDTO)).Entity;
                context.SaveChanges();
                projectsDTO.Id = projects.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectsDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Edita un objeto
        /// </summary>
        /// <param name="id">Id del customer</param>
        /// <param name="projectsDTO">Objeto</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Customers/1
        public IActionResult Edit(int id, ProjectDTOs projectsDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var projects = context.Projects.Find(id);
                if (projects == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(projects).State = EntityState.Detached;
                context.Projects.Update(mapper.Map<Project>(projectsDTO));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = projectsDTO });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un objeto
        /// </summary>
        /// <param name="id">Id del customer</param>
        /// <returns></returns>
        /// 
       
        [HttpDelete("{id}")]  //api/Customers/1
        public IActionResult Delete(int id)
        {
            try
            {
                var project = context.Projects.Find(id);
                if (project == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.ProjectLanguages.Any(x => x.ProjectId == id))
                    context.ProjectLanguages.RemoveRange(context.ProjectLanguages.Where(x => x.ProjectId == id));

                context.Projects.Remove(project);
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Message = "Se ha realizado el proceso con exito." });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.InternalServerError, Message = ex.Message });
            }
        }
    }
}
