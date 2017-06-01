using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.ReportOnStorage;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using System.Data;
    using DB2.Models;
    using Proxies;
    using Extensions;
    using DB2.Repositories.ReportOnStorage;
    using DB2.Resources;

    public class ReportOnStorageController : ApiController
    {
        IReportOnStorageRepository reportOnStorageRepository;

        public ReportOnStorageController(IReportOnStorageRepository reportOnStorageRepository)
        {
            this.reportOnStorageRepository = reportOnStorageRepository;
        }
        // GET: api/ReportOnStorage
        public HttpResponseMessage Get()
        {
            try
            {
                var items = reportOnStorageRepository.GetAll().Select(x => new ReportOnStorageResource(x)).ToList();
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

        // GET: api/ReportOnStorage/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ReportOnStorageResource(reportOnStorageRepository.Get(id)));
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

        // POST: api/ReportOnStorage
        public HttpResponseMessage Post([FromBody]ReportOnStorageResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ReportOnStorageResource(reportOnStorageRepository.Insert(value.ToModel())));
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

        // PUT: api/ReportOnStorage/5
        public HttpResponseMessage Put(int id, [FromBody]ReportOnStorageResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ReportOnStorageResource(reportOnStorageRepository.Update(id, value.ToModel())));
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

        // DELETE: api/ReportOnStorage/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                reportOnStorageRepository.Delete(id);
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
                return Request.CreateResponse(HttpStatusCode.OK, reportOnStorageRepository.GetByCity(city).Select(x => new ReportOnStorageResource(x)));
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

        public HttpResponseMessage GetByProductId(int productId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, reportOnStorageRepository.GetByProductId(productId).Select(x => new ReportOnStorageResource(x)));
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

        public HttpResponseMessage GetByWarehouseId(int warehouseId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, reportOnStorageRepository.GetByWarehouseId(warehouseId).Select(x => new ReportOnStorageResource(x)));
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
