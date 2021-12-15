using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.CompilerServices;
using ProjectOL.BL.DTOs;
using ProjectOL.BL.Helpers;
using ProjectOL.BL.Services.Implements;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ProjectOL.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<ProjectDTOs>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECTS,
                null,
                ApiService.Method.Get);

            var projects = (List<ProjectDTOs>)responseDTO.Data;
            return View(projects);
        }

       
       
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadData();
            return View(new ProjectDTOs());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectDTOs projectDTO)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<ProjectDTOs>(Endpoints.URL_BASE,
                Endpoints.POST_PROJECTS,
                projectDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            await LoadData();

            var responseDTO = await apiService.RequestAPI<ProjectDTOs>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT + id,
                null,
                ApiService.Method.Get);

            var project = (ProjectDTOs)responseDTO.Data;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectDTOs projectDTO)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTOs>(Endpoints.URL_BASE,
                Endpoints.PUT_PROJECTS + projectDTO.Id,
                projectDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(projectDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTOs>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT + id,
                null,
                ApiService.Method.Get);

            var project = (ProjectDTOs)responseDTO.Data;

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectDTOs projectDTO)
        {
            var responseDTO = await apiService.RequestAPI<ProjectDTOs>(Endpoints.URL_BASE,
                Endpoints.DELETE_PROJECTS + projectDTO.Id,
                null,
                ApiService.Method.Delete);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Language(int id)
        {
            await GetLanguages();

            var responseDTO = await apiService.RequestAPI<List<ProjectLanguageDTOs>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT_LANGUAGES + id,
                null,
                ApiService.Method.Get);

            var projectLanguages = (List<ProjectLanguageDTOs>)responseDTO.Data;
            ViewData["projectLanguages"] = projectLanguages;

            return View(new ProjectLanguageDTOs { ProjectId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Language(ProjectLanguageDTOs projectLanguageDTO)
        {
            await GetLanguages();

            projectLanguageDTO.Id = 0;
            var responseDTO = await apiService.RequestAPI<ProjectLanguageDTOs>(Endpoints.URL_BASE,
                Endpoints.POST_PROJECT_LANGUAGE,
                projectLanguageDTO,
                ApiService.Method.Post);

            return RedirectToAction(nameof(Language), new { id = projectLanguageDTO.ProjectId });
        }

        private async Task GetLanguages()
        {
            var responseDTO = await apiService.RequestAPI<List<LanguageDTOs>>(Endpoints.URL_BASE,
                Endpoints.GET_LANGUAGES,
                null,
                ApiService.Method.Get);

            var languages = (List<LanguageDTOs>)responseDTO.Data;
            ViewData["languages"] = new SelectList(languages, "Id", "Name");
        }

        private async Task LoadData()
        {
            var responseDTO = await apiService.RequestAPI<List<CustomerDTOs>>(Endpoints.URL_BASE,
                Endpoints.GET_CUSTOMERS,
                null,
                ApiService.Method.Get);

            var customers = (List<CustomerDTOs>)responseDTO.Data;

            responseDTO = await apiService.RequestAPI<List<ProjectStateDTOs>>(Endpoints.URL_BASE,
                Endpoints.GET_PROJECT_STATES,
                null,
                ApiService.Method.Get);

            var projectStates = (List<ProjectStateDTOs>)responseDTO.Data;

            ViewData["customers"] = new SelectList(customers, "Id", "FullName");
            ViewData["projectStates"] = new SelectList(projectStates, "Id", "Name");
        }
    }
}
