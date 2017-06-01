using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using DB2.Repositories.Product;
using DB2.Resources;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using Proxies;
    using Extensions;
    using System.Data;    
    
    public class ProductController : ApiController
    {
        IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        // GET: api/Product
        public HttpResponseMessage Get()
        {
            try
            {
                var items = productRepository.GetAll().Select(x => new ProductResource(x)).ToList();
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

        // GET: api/Product/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ProductResource(productRepository.Get(id)));
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

        // POST: api/Product
        public HttpResponseMessage Post([FromBody]ProductResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ProductResource(productRepository.Insert(value.ToModel())));
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

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody]ProductResource value)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ProductResource(productRepository.Update(id, value.ToModel())));
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

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                productRepository.Delete(id);
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

        public HttpResponseMessage GetByType(string type)//nullable
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productRepository.GetByType(type).Select(x => new ProductResource(x)));
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

        public HttpResponseMessage GetByGender(string gender)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productRepository.GetByGender(gender).Select(x => new ProductResource(x)));
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

        public HttpResponseMessage GetByPrice(int max, int min = 0)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productRepository.GetByPrice(max,  min).Select(x => new ProductResource(x)));
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

        public HttpResponseMessage GetByTypeAndGender(string gender, string type)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, productRepository.GetByTypeAndGender(gender, type).Select(x => new ProductResource(x)));
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
