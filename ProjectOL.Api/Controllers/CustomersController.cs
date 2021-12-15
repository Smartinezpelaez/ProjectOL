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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ProjectOLContext context;
        public CustomersController(ProjectOLContext context, IMapper mapper)

        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de clientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var customers = context.Customers.ToList();
            var customersDTO = customers.Select(x => mapper.Map<CustomerDTOs>(x)).OrderByDescending(x => x.Id);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customersDTO });
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
            var customers = context.Customers.Find(id);
            if (customers == null)
                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

            var customersDTO = mapper.Map<CustomerDTOs>(customers);
            return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customersDTO });
        }

        /// <summary>
        /// Crea un objeto.
        /// </summary>
        /// <param name="customerDTOs">Objeto </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CustomerDTOs customerDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var customers = context.Customers.Add(mapper.Map<Customer>(customerDTOs)).Entity;
                context.SaveChanges();
                customerDTOs.Id = customers.Id;

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customerDTOs });
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
        /// <param name="customerDTOs">Objeto</param>
        /// <returns></returns>
        [HttpPut("{id}")] //    api/Customers/1
        public IActionResult Edit(int id, CustomerDTOs customerDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Ok(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = string.Join(",", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                    });

                var customers = context.Customers.Find(id);
                if (customers == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                context.Entry(customers).State = EntityState.Detached;
                context.Customers.Update(mapper.Map<Customer>(customerDTOs));
                context.SaveChanges();

                return Ok(new ResponseDTO { Code = (int)HttpStatusCode.OK, Data = customerDTOs });
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
        [HttpDelete("{id}")] //    api/Customers/1
        public IActionResult Delete(int id)
        {
            try
            {
                var customers = context.Customers.Find(id);
                if (customers == null)
                    return Ok(new ResponseDTO { Code = (int)HttpStatusCode.NotFound, Message = "NotFound" });

                if (context.Projects.Any(x => x.Id == id))
                    throw new Exception("Dependencies");

                context.Customers.Remove(customers);
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
