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

    public class CourierController : ApiController
    {
        // GET: api/Courier
        public HttpResponseMessage Get()
        {
            var items = new List<Courier>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM courier";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Courier
                            {
                                Id = reader.GetInt32(0),
                                NumCourier = reader.GetInt32(1),
                                IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<List<Courier>>(HttpStatusCode.OK, items);
        }

        // GET: api/Courier/5
        public HttpResponseMessage Get(int id)
        {
            var item = new Courier();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM courier WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new Courier
                        {
                            Id = reader.GetInt32(0),
                            NumCourier = reader.GetInt32(1),
                            IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<Courier>(HttpStatusCode.OK, item);
        }

        // POST: api/Courier
        public HttpResponseMessage Post([FromBody]Courier value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO courier(num_courier,id_ord) VALUES(@num_courier,@id_ord) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@num_courier", Convert.ToInt32(value.NumCourier)));
                    command.Parameters.Add(new NpgsqlParameter("@id_ord", Convert.ToInt32(value.IdOrder)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Courier
                        {
                            Id = reader.GetInt32(0),
                            NumCourier = reader.GetInt32(1),
                            IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<Courier>(HttpStatusCode.OK, value);
        }

        // PUT: api/Courier/5
        public HttpResponseMessage Put(int id, [FromBody]Courier value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE courier SET num_courier=@num_courier,id_ord=@id_ord WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@num_courier", Convert.ToInt32(value.NumCourier)));
                    command.Parameters.Add(new NpgsqlParameter("@id_ord", Convert.ToInt32(value.IdOrder)));
                    command.Parameters.Add(new NpgsqlParameter("@id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM courier WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new Courier
                        {
                            Id = reader.GetInt32(0),
                            NumCourier = reader.GetInt32(1),
                            IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<Courier>(HttpStatusCode.OK, value);
        }

        // DELETE: api/Courier/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM courier WHERE id =@id";
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
            var items = new List<Courier>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Processing", processing));
                    command.CommandText = "SELECT courier.id, num_courier, id_ord FROM courier, order_ WHERE id_ord=order_.id AND processing=@Processing";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Courier
                            {
                                Id = reader.GetInt32(0),
                                NumCourier = reader.GetInt32(1),
                                IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<List<Courier>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByNumCourier(int num)
        {
            var items = new List<Courier>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@Num", num));
                    command.CommandText = "SELECT * FROM courier WHERE num_courier=@Num";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new Courier
                            {
                                Id = reader.GetInt32(0),
                                NumCourier = reader.GetInt32(1),
                                IdOrder = reader.GetInt32(2),
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
            return Request.CreateResponse<List<Courier>>(HttpStatusCode.OK, items);
        }
    }
}
