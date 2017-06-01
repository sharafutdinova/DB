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

    public class OrderController : ApiController
    {
        // GET: api/Order
        public HttpResponseMessage Get()
        {
            var items = new List<Order>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM order_";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Order
                            {
                                Id = reader.GetInt32(0),
                                Processing = reader.GetBoolean(1),
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
            return Request.CreateResponse<List<Order>>(HttpStatusCode.OK, items);
        }

        // GET: api/Order/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Order();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM order_ WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Order
                        {
                            Id = reader.GetInt32(0),
                            Processing = reader.GetBoolean(1)
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
            return Request.CreateResponse<Order>(HttpStatusCode.OK, item);
        }

        // POST: api/Order
        public HttpResponseMessage Post([FromBody]Order value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO order_(processing) VALUES(@Processing) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@Processing", Convert.ToBoolean(value.Processing)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Order
                        {
                            Id = reader.GetInt32(0),
                            Processing = reader.GetBoolean(1),
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
            return Request.CreateResponse<Order>(HttpStatusCode.OK, value);
        }

        // PUT: api/Order/5
        public HttpResponseMessage Put(int id, [FromBody]Order value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE order_ SET processing=@Processing WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@Processing ", Convert.ToBoolean(value.Processing)));
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM order_ WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Order
                        {
                            Id = reader.GetInt32(0),
                            Processing = reader.GetBoolean(1),
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
            return Request.CreateResponse<Order>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Order/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM order_ WHERE id =@id";
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

        public HttpResponseMessage GetByProcessing(bool processing)
        {
            var items = new List<Order>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Processing", processing));
                    command.CommandText = "SELECT * FROM order_ WHERE processing=@Processing";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Order
                            {
                                Id = reader.GetInt32(0),
                                Processing = reader.GetBoolean(1),
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
            return Request.CreateResponse<List<Order>>(HttpStatusCode.OK, items);
        }
    }
}
