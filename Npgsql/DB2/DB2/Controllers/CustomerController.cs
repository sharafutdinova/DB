using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DB2.Models;
using System.Data;

namespace DB2.Controllers
{
    using Middleware;
    using Npgsql;
    using NpgsqlTypes;

    public class CustomerController : ApiController
    {
        // GET: api/Customer
        public HttpResponseMessage Get()
        {
            var items = new List<Customer>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM customer";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                City = reader.GetString(2),
                                Address = reader.GetString(3)
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
            return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, items);
        }

        // GET: api/Customer/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Customer();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM customer WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Customer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            City = reader.GetString(2),
                            Address = reader.GetString(3)
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
            return Request.CreateResponse<Customer>(HttpStatusCode.OK, item);
        }

        // POST: api/Customer
        public HttpResponseMessage Post([FromBody]Customer value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO customer(name,city,address) VALUES(@Name,@City,@Address) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    //command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(value.Id)));
                    command.Parameters.Add(new NpgsqlParameter("@Name", Convert.ToString(value.Name)));
                    command.Parameters.Add(new NpgsqlParameter("@City", Convert.ToString(value.City)));
                    command.Parameters.Add(new NpgsqlParameter("@Address", Convert.ToString(value.Address)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Customer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            City = reader.GetString(2),
                            Address = reader.GetString(3)
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
            return Request.CreateResponse<Customer>(HttpStatusCode.OK, value);
        }

        // PUT: api/Customer/5
        public HttpResponseMessage Put(int id, [FromBody]Customer value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE customer SET name=@Name,city=@City,address=@Address WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.Parameters.Add(new NpgsqlParameter("@Name", Convert.ToString(value.Name)));
                    command.Parameters.Add(new NpgsqlParameter("@City", Convert.ToString(value.City)));
                    command.Parameters.Add(new NpgsqlParameter("@Address", Convert.ToString(value.Address)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM customer WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Customer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            City = reader.GetString(2),
                            Address = reader.GetString(3)
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
            return Request.CreateResponse<Customer>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Customer/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM customer WHERE id =@id";
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
            var items = new List<Customer>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@City", city));
                    command.CommandText = "SELECT * FROM customer WHERE city=@City";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                City = reader.GetString(2),
                                Address = reader.GetString(3)
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
            return Request.CreateResponse<List<Customer>>(HttpStatusCode.OK, items);
        }
    }
}