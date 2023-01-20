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

namespace Business.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        CustomerService customerService = new CustomerService();

        [HttpGet]
        [Route("api/customer/all")]
        public HttpResponseMessage ListCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers = customerService.GetCustomerList();
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
        public HttpResponseMessage Find(Guid id)
        {
            Customer localCustomer = new Customer();
            localCustomer = customerService.FindCustomer(id);
            CustomerRest customerRest = new CustomerRest(localCustomer);
            if(customerRest == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
            }
            return Request.CreateResponse(HttpStatusCode.OK, customerRest);
        }
        [HttpPost]
        public HttpResponseMessage Add([FromBody] CustomerRest customerRest)
        {
            if (customerRest.FirstNameOrLastNameNotNull(customerRest))
            {
                Customer customer = new Customer(customerRest.Id, customerRest.FirstName, customerRest.LastName, customerRest.EmployeeId);
                customerService.AddCustomer(customer);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");

        }

        [HttpPut]
        public HttpResponseMessage Update(Guid id, [FromBody] CustomerRest updatedCustomer)
        {
            if (updatedCustomer.FirstNameOrLastNameNotNull(updatedCustomer))
            {
                Customer customer = new Customer(updatedCustomer.Id, updatedCustomer.FirstName, updatedCustomer.LastName, updatedCustomer.EmployeeId);
                if (customerService.UpdateCustomer(id, customer))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Update successful");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no employee with that id");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Check first and last name");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(Guid id)
        {
            if (customerService.DeleteCustomer(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Delete successful");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There is no customer with that id");
        }

    }
}
