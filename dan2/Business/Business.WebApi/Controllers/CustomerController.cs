using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace Business.WebApi.Controllers
{
    public class CustomerController : ApiController
    {
        public static List<Customer> customers = new List<Customer> { new Customer("Marko", "Cicek", 1),
            new Customer("Ivan", "Ivanic", 2), 
            new Customer("Kristijan", "Kristijanic", 3) };

        [HttpGet]
        [Route("api/customer/all")]
        public HttpResponseMessage ListCustomers()
        {
            if(customers == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "There are no customers!");
            }
            return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, customers);
        }
        [HttpGet]
        public HttpResponseMessage Find(int id)
        {
            Customer localCustomer = customers.Find(customer => customer.Id == id);
            if(localCustomer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            return Request.CreateResponse(HttpStatusCode.OK, localCustomer);
        }
        [HttpPost]
        public HttpResponseMessage Add([FromBody]Customer customer)
        {
            if(customer.Fname == null || customer.Lname == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, customer.Id);
            }
            customer.Id = customers.Last().Id + 1;
            customers.Add(customer);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Update(int id, [FromBody]Customer updatedCustomer)
        {
            Customer localCustomer = customers.Find(customer => customer.Id == id);
            if (localCustomer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            else if (updatedCustomer.Fname == null || updatedCustomer.Lname == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, updatedCustomer.Id);
            }
            customers.ElementAt(id-1).Fname = updatedCustomer.Fname;
            customers.ElementAt(id-1).Lname = updatedCustomer.Lname;

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (!customers.Remove(customers.Find(customer => customer.Id == id)))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}
