using Microsoft.AspNetCore.Mvc;
using ProjectOL.BL.DTOs;
using ProjectOL.BL.Helpers;
using ProjectOL.BL.Services.Implements;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProjectOL.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApiService apiService = new ApiService();

        public async Task<IActionResult> Index()
        {
            var responseDTO = await apiService.RequestAPI<List<CustomerDTOs>>(BL.Helpers.Endpoints.URL_BASE,
               Endpoints.GET_CUSTOMERS,
                null,
                ApiService.Method.Get);

            var customers = (List<CustomerDTOs>)responseDTO.Data;
            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CustomerDTOs());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTOs customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTOs>(BL.Helpers.Endpoints.URL_BASE,
                Endpoints.POST_CUSTOMERS,
                customerDTO,
                ApiService.Method.Post);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(customerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTOs>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_CUSTOMER + id,
              null,
              ApiService.Method.Get);

            var customer = (CustomerDTOs)responseDTO.Data;

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDTOs customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTOs>(BL.Helpers.Endpoints.URL_BASE,
              Endpoints.POST_CUSTOMERS + customerDTO.Id,
                customerDTO,
                ApiService.Method.Put);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(customerDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTOs>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.GET_CUSTOMER + id,
              null,
              ApiService.Method.Get);

            var customer = (CustomerDTOs)responseDTO.Data;

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CustomerDTOs customerDTO)
        {
            var responseDTO = await apiService.RequestAPI<CustomerDTOs>(BL.Helpers.Endpoints.URL_BASE,
             Endpoints.DELETE_CUSTOMERS + customerDTO.Id,
              null,
              ApiService.Method.Delete);

            if (responseDTO.Code == (int)HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(customerDTO);
        }

    }
}
