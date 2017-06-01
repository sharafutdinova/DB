using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.Courier;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;

    public class CourierController : ApiController
    {
        private ICourierRepository courierRepository;

        public CourierController(ICourierRepository basketRepository)
        {
            this.courierRepository = courierRepository;
        }
        // GET: api/Courier
        public HttpResponseMessage Get()
        {
            try
            {
                var items = courierRepository.GetAll().Select(x => new CourierResource(x)).ToList();
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

        // GET: api/Courier/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CourierResource(courierRepository.Get(id)));
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

        // POST: api/Courier
        public HttpResponseMessage Post([FromBody]CourierResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CourierResource(courierRepository.Insert(value.ToModel())));
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

        // PUT: api/Courier/5
        public HttpResponseMessage Put(int id, [FromBody]CourierResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CourierResource(courierRepository.Update(id, value.ToModel())));
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

        // DELETE: api/Courier/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                courierRepository.Delete(id);
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
                return Request.CreateResponse(HttpStatusCode.OK, courierRepository.GetByProcessing(processing).Select(x => new CourierResource(x)));
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

        public HttpResponseMessage GetByNumCourier(int num)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, courierRepository.GetByNumCourier(num).Select(x => new CourierResource(x)));
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
