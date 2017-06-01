using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.ProductInf;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;
    using System.Data;

    public class ProductInfController : ApiController
    {
        IProductInfRepository productInfRepository;

        public ProductInfController (IProductInfRepository productInfRepository)
        {
            this.productInfRepository = productInfRepository;
        }
        // GET: api/ProductInf
        public HttpResponseMessage Get()
        {
            try
            {
                var items = productInfRepository.GetAll().Select(x => new ProductInfResource(x)).ToList();
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

        // GET: api/ProductInf/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ProductInfResource(productInfRepository.Get(id)));
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

        // POST: api/ProductInf
        public HttpResponseMessage Post([FromBody]ProductInfResource value)
        {
            try
            {
                if (value.Gender == 'm' || value.Gender == 'w' || value.Gender == 'u')
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ProductInfResource(productInfRepository.Insert(value.ToModel())));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                }
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

        // PUT: api/ProductInf/5
        public HttpResponseMessage Put(int id, [FromBody]ProductInfResource value)
        {
            try
            {
                if (value.Gender == 'm' || value.Gender == 'w' || value.Gender == 'u')
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ProductInfResource(productInfRepository.Update(id, value.ToModel())));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception());
                }
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

        // DELETE: api/ProductInf/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                productInfRepository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
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

        public HttpResponseMessage GetByType(string type)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productInfRepository.GetByType(type).Select(x => new ProductInfResource(x)));
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

        public HttpResponseMessage GetByGender(char gender)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productInfRepository.GetByGender(gender).Select(x => new ProductInfResource(x)));
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

        public HttpResponseMessage GetByTypeAndGender(string type, char gender)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productInfRepository.GetByTypeAndGender(type, gender).Select(x => new ProductInfResource(x)));
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
