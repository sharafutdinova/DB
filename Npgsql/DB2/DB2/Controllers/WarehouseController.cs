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

    public class WarehouseController : ApiController
    {
        // GET: api/Warehouse
        public HttpResponseMessage Get()
        {
            var items = new List<Warehouse>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM warehouse";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Warehouse
                            {
                                Id = reader.GetInt32(0),
                                City = reader.GetString(1),
                                Address = reader.GetString(2)
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
            return Request.CreateResponse<List<Warehouse>>(HttpStatusCode.OK, items);
        }

        // GET: api/Warehouse/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Warehouse();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM warehouse WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Warehouse
                        {
                            Id = reader.GetInt32(0),
                            City = reader.GetString(1),
                            Address = reader.GetString(2)
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
            return Request.CreateResponse<Warehouse>(HttpStatusCode.OK, item);
        }

        // POST: api/Warehouse
        public HttpResponseMessage Post([FromBody]Warehouse value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO warehouse(city,address) VALUES(@City,@Address) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@City", Convert.ToString(value.City)));
                    command.Parameters.Add(new NpgsqlParameter("@Address", Convert.ToString(value.Address)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Warehouse
                        {
                            Id = reader.GetInt32(0),
                            City = reader.GetString(1),
                            Address = reader.GetString(2)
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
            return Request.CreateResponse<Warehouse>(HttpStatusCode.OK, value);
        }

        // PUT: api/Warehouse/5
        public HttpResponseMessage Put(int id, [FromBody]Warehouse value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE warehouse SET city=@City,address=@Address WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@City", Convert.ToString(value.City)));
                    command.Parameters.Add(new NpgsqlParameter("@Address", Convert.ToString(value.Address)));
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM warehouse WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Warehouse
                        {
                            Id = reader.GetInt32(0),
                            City = reader.GetString(1),
                            Address = reader.GetString(2)
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
            return Request.CreateResponse<Warehouse>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Warehouse/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM warehouse WHERE id =@id";
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

        public HttpResponseMessage GetByCity(string city)
        {
            var items = new List<Warehouse>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@City", city));
                    command.CommandText = "SELECT * FROM warehouse WHERE city=@City";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Warehouse
                            {
                                Id = reader.GetInt32(0),
                                City = reader.GetString(1),
                                Address = reader.GetString(2)
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
            return Request.CreateResponse<List<Warehouse>>(HttpStatusCode.OK, items);
        }
    }
}
