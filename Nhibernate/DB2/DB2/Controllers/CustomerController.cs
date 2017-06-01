using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using System.Data;
using DB2.Resources;
using DB2.Repositories.Customer;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;

    public class CustomerController : ApiController
    {
        ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        // GET: api/Customer
        public HttpResponseMessage Get()
        {
            try
            {
                var items = customerRepository.GetAll().Select(x => new CustomerResource(x)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        // GET: api/Customer/5
        public HttpResponseMessage Get(int id)
        {            
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CustomerResource(customerRepository.Get(id)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        // POST: api/Customer
        public HttpResponseMessage Post([FromBody]CustomerResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CustomerResource(customerRepository.Insert(value.ToModel())));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        // PUT: api/Customer/5
        public HttpResponseMessage Put(int id, [FromBody]CustomerResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CustomerResource(customerRepository.Update(id, value.ToModel())));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        // DELETE: api/Customer/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                customerRepository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        public HttpResponseMessage GetByCity(string city)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerRepository.GetByCity(city).Select(x => new CustomerResource(x)));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.ToString());
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }
    }
}