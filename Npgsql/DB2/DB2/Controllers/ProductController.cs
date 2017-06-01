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
    
    
    public class ProductController : ApiController
    {
        // GET: api/Product
        public HttpResponseMessage Get()
        {
            var items = new List<Product>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM product";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                Id = reader.GetInt32(1),
                                Price = reader.GetInt32(3),
                                IdInf = reader.GetInt32(0),
                                Title = reader.GetString(2),
                                Description = reader.GetString(4)
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
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, items);
        }

        // GET: api/Product/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Product();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM product WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Product
                        {
                            Id = reader.GetInt32(1),
                            Price = reader.GetInt32(3),
                            IdInf = reader.GetInt32(0),
                            Title = reader.GetString(2),
                            Description = reader.GetString(4)
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
            return Request.CreateResponse<Product>(HttpStatusCode.OK, item);
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody]Product value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO product(id_inf,title,price,description) VALUES(@Id_inf,@Title,@Price,@Description) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@Id_inf", Convert.ToInt32(value.IdInf)));
                    command.Parameters.Add(new NpgsqlParameter("@Title", Convert.ToString(value.Title)));
                    command.Parameters.Add(new NpgsqlParameter("@Price", Convert.ToInt32(value.Price)));
                    command.Parameters.Add(new NpgsqlParameter("@Description", Convert.ToString(value.Description)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Product
                        {
                            Id = reader.GetInt32(1),
                            Price = reader.GetInt32(3),
                            IdInf = reader.GetInt32(0),
                            Title = reader.GetString(2),
                            Description = reader.GetString(4)
                        };
                    }
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
            return Request.CreateResponse<Product>(HttpStatusCode.OK, value);
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put(int id, [FromBody]Product value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE product SET id_inf=@Id_inf,title=@Title,price=@Price,description=@Description WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@Id_inf", Convert.ToInt32(value.IdInf)));
                    command.Parameters.Add(new NpgsqlParameter("@Title", Convert.ToString(value.Title)));
                    command.Parameters.Add(new NpgsqlParameter("@Price", Convert.ToInt32(value.Price)));
                    command.Parameters.Add(new NpgsqlParameter("@Description", Convert.ToString(value.Description)));
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM product WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Product
                        {
                            Id = reader.GetInt32(1),
                            Price = reader.GetInt32(3),
                            IdInf = reader.GetInt32(0),
                            Title = reader.GetString(2),
                            Description = reader.GetString(4)
                        };
                    }
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
            return Request.CreateResponse<Product>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM product WHERE id =@id";
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

        public HttpResponseMessage GetByType(string type)//nullable
        {
            var items = new List<Product>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection; command.Parameters.Add(new NpgsqlParameter("@Type_prod", type));
                    command.CommandText = "SELECT id_inf, product.id, title, price, description FROM product, product_inf WHERE (product_inf.type_prod =@Type_prod AND product_inf.id = product.id_inf)";
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                Id = reader.GetInt32(1),
                                Price = reader.GetInt32(3),
                                IdInf = reader.GetInt32(0),
                                Title = reader.GetString(2),
                                Description = reader.GetString(4)
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
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByGender(string gender)
        {
            var items = new List<Product>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;

                    command.Parameters.Add(new NpgsqlParameter("@Gender", gender));
                    command.CommandText = "SELECT id_inf, product.id, title, price, description FROM product, product_inf WHERE (product_inf.gender=@Gender AND product_inf.id=product.id_inf)";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                Id = reader.GetInt32(1),
                                Price = reader.GetInt32(3),
                                IdInf = reader.GetInt32(0),
                                Title = reader.GetString(2),
                                Description = reader.GetString(4)
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
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByPrice(int max, int min = 0)
        {
            var items = new List<Product>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                        command.Parameters.Add(new NpgsqlParameter("@Max", max));
                        command.Parameters.Add(new NpgsqlParameter("@Min", min));
                    command.CommandText = "SELECT * FROM product WHERE price BETWEEN @Min AND @Max";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                Id = reader.GetInt32(1),
                                Price = reader.GetInt32(3),
                                IdInf = reader.GetInt32(0),
                                Title = reader.GetString(2),
                                Description = reader.GetString(4)
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
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByTypeAndGender(string gender, string type)
        {
            var items = new List<Product>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT product.id, id_inf, title, description, price FROM product, product_inf WHERE id_inf=product_inf.id AND type_prod=@Type_Prod AND gender=@Gender";
                    command.Parameters.Add(new NpgsqlParameter("@Type_prod", type));
                    command.Parameters.Add(new NpgsqlParameter("@Gender", gender));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                IdInf = reader.GetInt32(1),
                                Price = reader.GetInt32(4),
                                Title = reader.GetString(2),
                                Description = reader.GetString(3)                             
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
            return Request.CreateResponse<List<Product>>(HttpStatusCode.OK, items);
        }

    }
}
