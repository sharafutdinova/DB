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

    public class ReportOnStorageController : ApiController
    {
        // GET: api/ReportOnStorage
        public HttpResponseMessage Get()
        {
            var items = new List<ReportOnStorage>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM report_on_storage";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ReportOnStorage
                            {
                                Id = reader.GetInt32(0),
                                IdProd = reader.GetInt32(1),
                                IdWarehouse = reader.GetInt32(2),
                                Count = reader.GetInt32(3)
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
            return Request.CreateResponse<List<ReportOnStorage>>(HttpStatusCode.OK, items);
        }

        // GET: api/ReportOnStorage/5
        public HttpResponseMessage Get(int id)
        {
            var item = new ReportOnStorage();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "SELECT * FROM report_on_storage WHERE id=" + id;
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        item = new ReportOnStorage
                        {
                            Id = reader.GetInt32(0),
                            IdProd = reader.GetInt32(1),
                            IdWarehouse = reader.GetInt32(2),
                            Count = reader.GetInt32(3)
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
            return Request.CreateResponse<ReportOnStorage>(HttpStatusCode.OK, item);
        }

        // POST: api/ReportOnStorage
        public HttpResponseMessage Post([FromBody]ReportOnStorage value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "INSERT INTO report_on_storage(prod_id,id_warehouse,count) VALUES(@ProdId,@idWarehouse,@Count) RETURNING * ";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("@ProdId", Convert.ToInt32(value.IdProd)));
                    command.Parameters.Add(new NpgsqlParameter("@IdWarehouse", Convert.ToInt32(value.IdWarehouse)));
                    command.Parameters.Add(new NpgsqlParameter("@Count", Convert.ToInt32(value.Count)));

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new ReportOnStorage
                        {
                            Id = reader.GetInt32(0),
                            IdProd = reader.GetInt32(1),
                            IdWarehouse = reader.GetInt32(2),
                            Count = reader.GetInt32(3)
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
            return Request.CreateResponse<ReportOnStorage>(HttpStatusCode.OK, value);
        }

        // PUT: api/ReportOnStorage/5
        public HttpResponseMessage Put(int id, [FromBody]ReportOnStorage value)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "UPDATE report_on_storage SET prod_id=@ProdId,id_warehouse=@IdWarehouse,count=@Count WHERE id=@Id";
                    command.CommandType = CommandType.Text;
                    //value.Id = id;
                    command.Parameters.Add(new NpgsqlParameter("@ProdId", Convert.ToInt32(value.IdProd)));
                    command.Parameters.Add(new NpgsqlParameter("@IdWarehouse", Convert.ToInt32(value.IdWarehouse)));
                    command.Parameters.Add(new NpgsqlParameter("@Count", Convert.ToInt32(value.Count)));
                    command.Parameters.Add(new NpgsqlParameter("@Id", Convert.ToInt32(id)));
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT * FROM report_on_storage WHERE id=@Id";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        value = new ReportOnStorage
                        {
                            Id = reader.GetInt32(0),
                            IdProd = reader.GetInt32(1),
                            IdWarehouse = reader.GetInt32(2),
                            Count = reader.GetInt32(3)
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
            return Request.CreateResponse<ReportOnStorage>(HttpStatusCode.OK, value);
        }

        // DELETE: api/ReportOnStorage/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.CommandText = "DELETE FROM report_on_storage WHERE id =@id";
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
            var items = new List<ReportOnStorage>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@City", city));
                    command.CommandText = "SELECT report_on_storage.id, prod_id, id_warehouse, count FROM report_on_storage, warehouse WHERE id_warehouse=warehouse.id AND city=@City";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ReportOnStorage
                            {
                                Id = reader.GetInt32(0),
                                IdProd = reader.GetInt32(1),
                                IdWarehouse = reader.GetInt32(2),
                                Count = reader.GetInt32(3)
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
            return Request.CreateResponse<List<ReportOnStorage>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByProductId(int productId)
        {
            var items = new List<ReportOnStorage>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@IdProd", productId));
                    command.CommandText = "SELECT * FROM report_on_storage WHERE prod_id=@IdProd";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ReportOnStorage
                            {
                                Id = reader.GetInt32(0),
                                IdProd = reader.GetInt32(1),
                                IdWarehouse = reader.GetInt32(2),
                                Count = reader.GetInt32(3)
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
            return Request.CreateResponse<List<ReportOnStorage>>(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage GetByWarehouseId(int warehouseId)
        {
            var items = new List<ReportOnStorage>();
            try
            {
                NpgsqlHelper.Connection.Open();
                using (var command = new NpgsqlCommand())
                {
                    command.Connection = NpgsqlHelper.Connection;
                    command.Parameters.Add(new NpgsqlParameter("@IdWarehouse", warehouseId));
                    command.CommandText = "SELECT * FROM report_on_storage WHERE id_warehouse=@IdWarehouse";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ReportOnStorage
                            {
                                Id = reader.GetInt32(0),
                                IdProd = reader.GetInt32(1),
                                IdWarehouse = reader.GetInt32(2),
                                Count = reader.GetInt32(3)
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
            return Request.CreateResponse<List<ReportOnStorage>>(HttpStatusCode.OK, items);
        }
    }
}
