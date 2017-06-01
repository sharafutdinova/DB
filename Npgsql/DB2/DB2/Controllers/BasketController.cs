using System;
using System.Collections.Generic;
using System.Data;
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

    public class BasketController : ApiController
    {
        // GET: api/Basket
        public HttpResponseMessage Get()//HttpResponseMessage
        {
            var items = new List<Basket>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM basket";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Basket
                            {
                                Id = reader.GetInt32(0),
                                IdCust = reader.GetInt32(1),
                                IdReport = reader.GetInt32(2),
                                IdOrder = reader.GetInt32(3),
                                Count = reader.GetInt32(4)
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
            return Request.CreateResponse<List<Basket>>(HttpStatusCode.OK, items);
        }

        // GET: api/Basket/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Basket();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM basket WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Basket
                        {
                            Id = reader.GetInt32(0),
                            IdCust = reader.GetInt32(1),
                            IdReport = reader.GetInt32(2),
                            IdOrder = reader.GetInt32(3),
                            Count = reader.GetInt32(4)
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
            return Request.CreateResponse<Basket>(HttpStatusCode.OK, item);
        }
       
        // POST: api/Basket
        public HttpResponseMessage Post([FromBody]Basket value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO basket(id_cust,id_report,id_ord,count) VALUES(@IdCust,@IdReport,@IdOrder,@Count) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@IdCust", Convert.ToInt32(value.IdCust)));
                    command.Parameters.Add(new NpgsqlParameter("@IdReport", Convert.ToInt32(value.IdReport)));
                    command.Parameters.Add(new NpgsqlParameter("@IdOrder", Convert.ToInt32(value.IdOrder)));
                    command.Parameters.Add(new NpgsqlParameter("@Count", Convert.ToInt32(value.Count)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Basket
                        {
                            Id = reader.GetInt32(0),
                            IdCust = reader.GetInt32(1),
                            IdReport = reader.GetInt32(2),
                            IdOrder = reader.GetInt32(3),
                            Count = reader.GetInt32(4)
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
            return Request.CreateResponse<Basket>(HttpStatusCode.OK, value);
        }

        // PUT: api/Basket/5
        public HttpResponseMessage Put(int id, [FromBody]Basket value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE basket SET id_cust=@IdCust,id_report=@IdReport,id_ord=@IdOrder,count=@Count WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@IdCust", Convert.ToInt32(value.IdCust)));
                    command.Parameters.Add(new NpgsqlParameter("@IdReport", Convert.ToInt32(value.IdReport)));
                    command.Parameters.Add(new NpgsqlParameter("@IdOrder", Convert.ToInt32(value.IdOrder)));
                    command.Parameters.Add(new NpgsqlParameter("@Count", Convert.ToInt32(value.Count)));
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM basket WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Basket
                        {
                            Id = reader.GetInt32(0),
                            IdCust = reader.GetInt32(1),
                            IdReport = reader.GetInt32(2),
                            IdOrder = reader.GetInt32(3),
                            Count = reader.GetInt32(4)
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
            return Request.CreateResponse<Basket>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Basket/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM basket WHERE id =@id";
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
            var items = new List<Basket>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Processing", processing));
                    command.CommandText = "SELECT basket.id, id_cust, id_report, id_ord, count FROM basket, order_ WHERE id_ord=order_.id AND processing=@Processing";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Basket
                            {
                                Id = reader.GetInt32(0),
                                IdCust = reader.GetInt32(1),
                                IdReport = reader.GetInt32(2),
                                IdOrder = reader.GetInt32(3),
                                Count = reader.GetInt32(4)
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
            return Request.CreateResponse<List<Basket>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByCustomerId(int custId)
        {
            var items = new List<Basket>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@CustId", custId));
                    command.CommandText = "SELECT * FROM basket WHERE id_cust=@CustId";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Basket
                            {
                                Id = reader.GetInt32(0),
                                IdCust = reader.GetInt32(1),
                                IdReport = reader.GetInt32(2),
                                IdOrder = reader.GetInt32(3),
                                Count = reader.GetInt32(4)
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
            return Request.CreateResponse<List<Basket>>(HttpStatusCode.OK, items);
        }
    }
}
