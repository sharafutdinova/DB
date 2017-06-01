using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;
    using System.Data;

    public class ProductInfController : ApiController
    {
        // GET: api/ProductInf
        public HttpResponseMessage Get()
        {
            var items = new List<ProductInf>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM product_inf";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<List<ProductInf>>(HttpStatusCode.OK, items);
        }

        // GET: api/ProductInf/5
        public HttpResponseMessage Get(int id)
        {
            var item = new ProductInf();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM product_inf WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new ProductInf
                        {
                            Id = reader.GetInt32(0),
                            TypeProd = reader.GetString(1),
                            Gender = reader.GetString(2)[0]
                        };
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<ProductInf>(HttpStatusCode.OK, item);
        }

        // POST: api/ProductInf
        public HttpResponseMessage Post([FromBody]ProductInf value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();

                if (value.Gender == 'm' || value.Gender == 'w' || value.Gender == 'u')
                {
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = NpgsqlHelper.Connection;
                        command.CommandText = "INSERT INTO product_inf(type_prod,Gender) VALUES(@TypeProd,@Gender) RETURNING * ";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new NpgsqlParameter("@TypeProd", Convert.ToString(value.TypeProd)));
                        command.Parameters.Add(new NpgsqlParameter("@Gender", Convert.ToChar(value.Gender)));

                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            value = new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            };
                        }
                        command.Dispose();
                    }

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<ProductInf>(HttpStatusCode.OK, value);
        }

        // PUT: api/ProductInf/5
        public HttpResponseMessage Put(int id, [FromBody]ProductInf value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();

                if (value.Gender == 'm' || value.Gender == 'w' || value.Gender == 'u')
                {
                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = NpgsqlHelper.Connection;
                        command.CommandText = "UPDATE product_inf SET type_prod=@TypeProd,Gender=@Gender WHERE id=@Id";
                        command.CommandType = CommandType.Text;
                        //value.Id = id;
                        command.Parameters.Add(new NpgsqlParameter("@TypeProd", Convert.ToString(value.TypeProd)));
                        command.Parameters.Add(new NpgsqlParameter("@Gender", Convert.ToChar(value.Gender)));
                        command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT * FROM product_inf WHERE id=@Id";
                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();
                            value = new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            };
                        }
                        command.Dispose();
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception());
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<ProductInf>(HttpStatusCode.OK, value);
        }

        // DELETE: api/ProductInf/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM product_inf WHERE id =@id";
                    command.Parameters.Add(new NpgsqlParameter("@Id", id));
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage GetByType(string type)
        {
            var items = new List<ProductInf>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@TypeProd", type));
                    command.CommandText = "SELECT * FROM product_inf WHERE type_prod=@TypeProd";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<List<ProductInf>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByGender(char gender)
        {
            var items = new List<ProductInf>();
            try
            { 
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Gender", gender));
                    command.CommandText = "SELECT * FROM product_inf WHERE gender=@Gender";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<List<ProductInf>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByTypeAndGender(string type, char gender)
        {
            var items = new List<ProductInf>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Gender", gender));
                    command.Parameters.Add(new NpgsqlParameter("@TypeProd", type));
                    command.CommandText = "SELECT * FROM product_inf WHERE type_prod=@TypeProd AND gender=@Gender";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ProductInf
                            {
                                Id = reader.GetInt32(0),
                                TypeProd = reader.GetString(1),
                                Gender = reader.GetString(2)[0]
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
            finally
            {
                NpgsqlHelper.Connection.Close();
            }
            return Request.CreateResponse<List<ProductInf>>(HttpStatusCode.OK, items);
        }
    }
}
