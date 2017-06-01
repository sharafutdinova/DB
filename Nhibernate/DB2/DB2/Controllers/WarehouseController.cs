using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.Warehouse;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;
    using System.Data;

    public class WarehouseController : ApiController
    {
        IWarehouseRepository warehouseRepository;

        public WarehouseController(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }
        // GET: api/Warehouse
        public HttpResponseMessage Get()
        {
            try
            {
                var items = warehouseRepository.GetAll().Select(x => new WarehouseResource(x)).ToList();
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

        // GET: api/Warehouse/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new WarehouseResource(warehouseRepository.Get(id)));
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

        // POST: api/Warehouse
        public HttpResponseMessage Post([FromBody]WarehouseResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new WarehouseResource(warehouseRepository.Insert(value.ToModel())));
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

        // PUT: api/Warehouse/5
        public HttpResponseMessage Put(int id, [FromBody]WarehouseResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new WarehouseResource(warehouseRepository.Update(id, value.ToModel())));
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

        // DELETE: api/Warehouse/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                warehouseRepository.Delete(id);
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
                return Request.CreateResponse(HttpStatusCode.OK, warehouseRepository.GetByCity(city).Select(x => new WarehouseResource(x)));
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
