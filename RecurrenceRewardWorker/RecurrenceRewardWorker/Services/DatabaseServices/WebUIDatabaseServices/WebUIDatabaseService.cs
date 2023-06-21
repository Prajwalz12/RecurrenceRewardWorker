using Domain.Models;
using Domain.Models.CustomerTimelineEventMasterModel;
using Domain.Models.EnumMaster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class WebUIDBContext : IDisposable
    {
        public MySqlConnection Connection { get; }

        public WebUIDBContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        public void Dispose() => Connection.Dispose();
    }

    public class WebUIDatabaseService
    {
        private readonly ILogger<WebUIDatabaseService> _logger;
        internal WebUIDBContext _db { get; set; }
        private readonly IConfiguration _configuration;
        public WebUIDatabaseService(IConfiguration configuration, WebUIDBContext db, ILogger<WebUIDatabaseService> logger)
        {
            _db = db;
            _logger = logger;
            _configuration = configuration;
        }
        public WebUIDatabaseService(WebUIDBContext db, ILogger<WebUIDatabaseService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public LapsePolicy GetLapsePolicyByCode(string code)
        {
            LapsePolicy lapsePolicy = null;
            try
            {
                DataSet ds = new DataSet();
                //if (_db.Connection.State != ConnectionState.Open) 
                //{ 
                //    await _db.Connection.OpenAsync().ConfigureAwait(false); 
                //}                
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "bfl_Get_LapsePolicyByCode";
                cmd.Parameters.AddWithValue("P_Code", code);

                var adaptor = new MySqlDataAdapter(cmd);
                adaptor.Fill(ds);

                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    lapsePolicy = new LapsePolicy()
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Name = row["PolicyName"].ToString(),
                        Code = row["PolicyCode"].ToString(),
                        DurationType = row["DurationType"].ToString(),
                        DurationValue = Convert.ToInt32(row["DurationValue"])
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //if (_db.Connection.State == ConnectionState.Open)
                //{
                //    await _db.Connection.CloseAsync().ConfigureAwait(false);
                //}
            }
            return lapsePolicy;
        }
        public int GetCustomerSegmentCount(string query)
        {
            int segmentCount = 0;
            try
            {
                DataSet ds = new DataSet();
                //if (_db.Connection.State != ConnectionState.Open) 
                //{ 
                //    await _db.Connection.OpenAsync().ConfigureAwait(false); 
                //}                
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Get_CustomerSegmentCount";
                cmd.Parameters.AddWithValue("P_Query", query);

                var adaptor = new MySqlDataAdapter(cmd);
                adaptor.Fill(ds);

                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    segmentCount = Convert.ToInt32(row["Count"]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //if (_db.Connection.State == ConnectionState.Open)
                //{
                //    await _db.Connection.CloseAsync().ConfigureAwait(false);
                //}
            }
            return segmentCount;
        }
        public async Task<List<DBEnumValue>> GetDBEnumValuesAsync(string masterCode = null)
        {
            List<DBEnumValue> enumValues = new List<DBEnumValue>();
            #region ABC
            try
            {
                DataSet ds = new DataSet();
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_BFL_GetEnumValues";
                cmd.Parameters.AddWithValue("P_MasterCode", masterCode);
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    await _db.Connection.OpenAsync().ConfigureAwait(false);
                }
                enumValues = await ReadEnumMasterValueAsync(await ExecuteCommandAsync(cmd).ConfigureAwait(false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_db.Connection.State == ConnectionState.Open)
                {
                    await _db.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return enumValues;
            #endregion
            async Task<DbDataReader> ExecuteCommandAsync(MySqlCommand cmd) => await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        }
        public async Task<List<DBEnumValue>> GetMerchantDBEnumValues(string category, string merchantGroupId, string merchantId, int? isTripleReward = null)
        {
            List<DBEnumValue> enumValues = new List<DBEnumValue>();
            #region ABC
            try
            {
                DataSet ds = new DataSet();
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_BFL_GetMerchantEnumValues";
                cmd.Parameters.AddWithValue("P_Category", String.IsNullOrEmpty(category) ? null : category);
                cmd.Parameters.AddWithValue("P_MerchantGroupId", String.IsNullOrEmpty(merchantGroupId) ? null : merchantGroupId);
                cmd.Parameters.AddWithValue("P_MerchantId", merchantId);
                cmd.Parameters.AddWithValue("P_IsTripleReward", isTripleReward);
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    await _db.Connection.OpenAsync().ConfigureAwait(false);
                }
                enumValues = await ReadEnumMasterValueAsync(await ExecuteCommandAsync(cmd).ConfigureAwait(false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_db.Connection.State == ConnectionState.Open)
                {
                    await _db.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return enumValues;
            #endregion
            async Task<DbDataReader> ExecuteCommandAsync(MySqlCommand cmd) => await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        }
        public bool IsMerchantExist(string segmentCodes, string merchantId)
        {
            var flag = false;
            try
            {
                DataSet ds = new DataSet();
                //if (_db.Connection.State != ConnectionState.Open) 
                //{ 
                //    await _db.Connection.OpenAsync().ConfigureAwait(false); 
                //}                
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_BFL_MerchantSegmentValidation";
                cmd.Parameters.AddWithValue("P_SegmentCodes", segmentCodes);
                cmd.Parameters.AddWithValue("P_MerchantId", merchantId);

                var adaptor = new MySqlDataAdapter(cmd);
                adaptor.Fill(ds);
                _logger.LogInformation($"FROM Database : DS : {JsonConvert.SerializeObject(ds)}");
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    var merchantStatus = Convert.ToInt32(row["MerchantStatus"]);
                    if (merchantStatus == 2)
                    {
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return flag;
        }
        private async Task<List<DBEnumValue>> ReadEnumMasterValueAsync(DbDataReader reader)
        {
            var models = new List<DBEnumValue>();
            using (reader)
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    var model = new DBEnumValue()
                    {
                        MasterId = reader.IsDBNull("MasterId") ? 0 : reader.GetInt32("MasterId"),
                        MasterCode = reader.IsDBNull("MasterCode") ? string.Empty : reader.GetString("MasterCode"),
                        MasterName = reader.IsDBNull("MasterName") ? string.Empty : reader.GetString("MasterName"),

                        Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id"),
                        Code = reader.IsDBNull("Code") ? string.Empty : reader.GetString("Code"),
                        Name = reader.IsDBNull("Name") ? string.Empty : reader.GetString("Name"),

                        GroupMerchantCode = reader.IsDBNull("GroupMerchantCode") ? string.Empty : reader.GetString("GroupMerchantCode"),
                        GroupMerchantName = reader.IsDBNull("GroupMerchantName") ? string.Empty : reader.GetString("GroupMerchantName"),
                        BrandCode = reader.IsDBNull("BrandCode") ? string.Empty : reader.GetString("BrandCode")

                    };
                    models.Add(model);
                }
            }
            return models;
        }
        public async Task<List<DBEnumValue>> GetBillerDBEnumValues(string category, string billerId)
        {
            List<DBEnumValue> responseModels = new List<DBEnumValue>();

            try
            {
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_BFL_GetBillerEnumValues";
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    await _db.Connection.OpenAsync().ConfigureAwait(false);
                }
                AttachParameters(cmd);
                responseModels = await ReadEnumMasterValueAsync(await cmd.ExecuteReaderAsync()).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
                throw;
            }
            finally
            {
                if (_db.Connection.State == ConnectionState.Open)
                {
                    await _db.Connection.CloseAsync().ConfigureAwait(false);
                }
            }

            _logger.LogInformation($"GetBillerDBEnumValues For category : {category}, billerId : {billerId},  Response : {JsonConvert.SerializeObject(responseModels)}");
            return responseModels;

            #region Local Methods  

            void AttachParameters(MySqlCommand cmd)
            {
                cmd.Parameters.AddWithValue("P_Category", category.Trim());
                cmd.Parameters.AddWithValue("P_BillerId", billerId.Trim());
            };
            #endregion
        }
        public async Task<List<ProductMaster>> GetProductMasters(string productCode = null)
        {
            List<ProductMaster> enumValues = new List<ProductMaster>();
            #region ABC
            try
            {
                DataSet ds = new DataSet();
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Get_ProductCodeMaster";
                cmd.Parameters.AddWithValue("P_ProductCode", productCode);
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    await _db.Connection.OpenAsync().ConfigureAwait(false);
                }
                enumValues = await ReadProductMasterValueAsync(await ExecuteCommandAsync(cmd).ConfigureAwait(false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_db.Connection.State == ConnectionState.Open)
                {
                    await _db.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return enumValues;
            #endregion
            async Task<DbDataReader> ExecuteCommandAsync(MySqlCommand cmd) => await cmd.ExecuteReaderAsync().ConfigureAwait(false);
        }

        private async Task<List<ProductMaster>> ReadProductMasterValueAsync(DbDataReader reader)
        {
            var models = new List<ProductMaster>();
            using (reader)
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    var model = new ProductMaster()
                    {
                        LobEnumId = reader.IsDBNull("LobEnumId") ? 0 : reader.GetInt32("LobEnumId"),
                        ProductCode = reader.IsDBNull("ProductCode") ? string.Empty : reader.GetString("ProductCode"),
                        ProductName = reader.IsDBNull("ProductName") ? string.Empty : reader.GetString("ProductName")
                    };
                    models.Add(model);
                }
            }
            return models;
        }

        #region Read customer Timeline Event master Data
        public async Task<CustomerTimelineEventMaster> GetCustomerTimelineEventMaster(CustomerTimelineEventRequest customerTimelineEventRequest)
        {
            CustomerTimelineEventMaster customerTimelineEventMaster = new CustomerTimelineEventMaster();
            try
            {
                DataSet ds = new DataSet();
                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Get_CustomerTimelineMaster";
                cmd.Parameters.AddWithValue("P_LobCode", customerTimelineEventRequest.LobCode);
                cmd.Parameters.AddWithValue("P_TimelineEventCode", String.IsNullOrEmpty(customerTimelineEventRequest.TimelineEventCode) ? null : customerTimelineEventRequest.TimelineEventCode);
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    await _db.Connection.OpenAsync().ConfigureAwait(false);
                }

                customerTimelineEventMaster.CustomerTimelineEvents = await ReadCustomerTimelineEventAsync(await ExecuteCommandAsync(cmd).ConfigureAwait(false)).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_db.Connection.State == ConnectionState.Open)
                {
                    await _db.Connection.CloseAsync().ConfigureAwait(false);
                }
            }
            return customerTimelineEventMaster;
            async Task<DbDataReader> ExecuteCommandAsync(MySqlCommand cmd) => await cmd.ExecuteReaderAsync().ConfigureAwait(false);

        }

        private async Task<List<CustomerTimelineEvent>> ReadCustomerTimelineEventAsync(DbDataReader reader)
        {
            var models = new List<CustomerTimelineEvent>();
            using (reader)
            {
                while (await reader.ReadAsync().ConfigureAwait(false))
                {
                    var model = new CustomerTimelineEvent()
                    {
                        Id = reader.IsDBNull("Id") ? 0 : reader.GetInt32("Id"),
                        LobName = reader.IsDBNull("LobName") ? string.Empty : reader.GetString("LobName"),
                        LobCode = reader.IsDBNull("LobCode") ? string.Empty : reader.GetString("LobCode"),
                        TimelineEventName = reader.IsDBNull("TimelineEventName") ? string.Empty : reader.GetString("TimelineEventName"),
                        TimelineEventCode = reader.IsDBNull("TimelineEventCode") ? string.Empty : reader.GetString("TimelineEventCode"),
                        Status = reader.IsDBNull("Status") ? 0 : reader.GetInt32("Status"),
                        Priority = reader.IsDBNull("Priority") ? 0 : reader.GetInt32("Priority"),
                        IsDisplayOnPortal = reader.IsDBNull("IsDisplayOnPortal") ? false : reader.GetBoolean("IsDisplayOnPortal"),
                    };
                    models.Add(model);
                }
            }
            return models;
        }

        #endregion
    }
}
