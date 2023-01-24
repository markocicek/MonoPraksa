using Business.Model;
using Business.Service;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Business.WebApi.Models;
using System.Threading.Tasks;
using Business.Service.Common;

namespace Business.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        protected ICustomerService CustomerService { get; private set; }
        public CustomerController(ICustomerService customerService)
        {
            this.CustomerService = customerService;
        }

        [HttpGet]
        [Route("api/customer/all")]
        public async Task<HttpResponseMessage> ListCustomersAsync()
        {
            List<Customer> customers = new List<Customer>();
            customers = await CustomerService.GetCustomerListAsync();
            List<CustomerRest> customersRest = new List<CustomerRest>();

            foreach(Customer customer in customers)
            {
                customersRest.Add(new CustomerRest(customer));
            }
            if(customersRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There are no customers!");
            }
            return Request.CreateResponse<List<CustomerRest>>(HttpStatusCode.OK, customersRest); 
        }
        [HttpGet]
        public async Task<HttpResponseMessage> FindAsync(Guid id)
        {
            Customer localCustomer = new Customer();
            localCustomer = await CustomerService.FindCustomerAsync(id);
            CustomerRest customerRest = new CustomerRest(localCustomer);
            if(customerRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
            }
            return Request.CreateResponse(HttpStatusCode.OK, customerRest);
        }
        [HttpPost]
        public async Task<HttpResponseMessage> AddAsync([FromBody] CustomerRest customerRest)
        {
            if (customerRest.FirstNameOrLastNameNotNull(customerRest))
            {
                Customer customer = new Customer(customerRest.Id, customerRest.FirstName, customerRest.LastName, customerRest.DateOfBirth, customerRest.EmployeeId);
                await CustomerService.AddCustomerAsync(customer);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");

        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAsync(Guid id, [FromBody] CustomerRest updatedCustomer)
        {
            if (updatedCustomer.FirstNameOrLastNameNotNull(updatedCustomer))
            {
                Customer customer = new Customer(updatedCustomer.Id, updatedCustomer.FirstName, updatedCustomer.LastName, updatedCustomer.DateOfBirth, updatedCustomer.EmployeeId);
                if (await CustomerService.UpdateCustomerAsync(id, customer))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteAsync(Guid id)
        {
            if (await CustomerService.DeleteCustomerAsync(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
        }

    }
}
