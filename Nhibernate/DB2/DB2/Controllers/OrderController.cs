using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.Order;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using System.Data;
    using Proxies;
    using Extensions;

    public class OrderController : ApiController
    {
        IOrderRepository orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        // GET: api/Order
        public HttpResponseMessage Get()
        {
            try
            {
                var items = orderRepository.GetAll().Select(x => new OrderResource(x)).ToList();
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

        // GET: api/Order/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderResource(orderRepository.Get(id)));
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

        // POST: api/Order
        public HttpResponseMessage Post([FromBody]OrderResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderResource(orderRepository.Insert(value.ToModel())));
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

        // PUT: api/Order/5
        public HttpResponseMessage Put(int id, [FromBody]OrderResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new OrderResource(orderRepository.Update(id, value.ToModel())));
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

        // DELETE: api/Order/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                orderRepository.Delete(id);
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

        public HttpResponseMessage GetByProcessing(bool processing)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, orderRepository.GetByProcessing(processing).Select(x => new OrderResource(x)));
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
