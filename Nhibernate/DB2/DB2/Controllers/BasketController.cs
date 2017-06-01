using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.Basket;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;

    public class BasketController : ApiController
    {
        private IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        // GET: api/Basket
        public HttpResponseMessage Get(string expand = "")
        {
            try
            {
                var items = basketRepository.GetAll().Select(x => new BasketResource(x)).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, items);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
        }

        // GET: api/Basket/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BasketResource(basketRepository.Get(id)));
                //var item = new BasketResource(basketRepository.Get(id)).ToModel();
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
       
        // POST: api/Basket
        public HttpResponseMessage Post([FromBody]BasketResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BasketResource(basketRepository.Insert(value.ToModel())));
                //value = new BasketResource(basketRepository.Insert(value.ToModel()));
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

        // PUT: api/Basket/5
        public HttpResponseMessage Put(int id, [FromBody]BasketResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BasketResource(basketRepository.Update(id, value.ToModel())));
                //value = new BasketResource(basketRepository.Update(id, value.ToModel()));
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

        // DELETE: api/Basket/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                basketRepository.Delete(id);
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
                return Request.CreateResponse(HttpStatusCode.OK, basketRepository.GetByProcessing(processing).Select(x => new BasketResource(x)));                
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

        public HttpResponseMessage GetByCustomerId(int custId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, basketRepository.GetByCustomerId(custId).Select(x => new BasketResource(x)));
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
