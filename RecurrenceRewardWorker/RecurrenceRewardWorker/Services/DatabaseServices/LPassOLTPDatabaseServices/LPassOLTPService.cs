using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ReferralModel = Domain.Models.ReferralModel;
namespace Domain.Services
{
    public class LPassOLTPDBContext : IDisposable
    {
        public MySqlConnection Connection { get; }

        public LPassOLTPDBContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        public void Dispose() => Connection.Dispose();
    }
    public class LPassOLTPDatabaseService
    {
        private readonly ILogger<LPassOLTPDatabaseService> _logger;
        internal LPassOLTPDBContext _db { get; set; }
        private readonly IConfiguration _configuration;
        public LPassOLTPDatabaseService(IConfiguration configuration, LPassOLTPDBContext db, ILogger<LPassOLTPDatabaseService> logger)
        {
            _db = db;
            _logger = logger;
            _configuration = configuration;
        }
        public LPassOLTPDatabaseService(LPassOLTPDBContext db, ILogger<LPassOLTPDatabaseService> logger)
        {
            _db = db;
            _logger = logger;
        }
        public List<ReferralModel.Referral> GetReferrals(ReferralModel.ReferralRequest referralRequest)
        {
            List<ReferralModel.Referral> referrals = new List<ReferralModel.Referral>();
            try
            {
                DataSet ds = new DataSet();

                using var cmd = _db.Connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Get_ReferralByCustomerIdAndLob";

                cmd.Parameters.AddWithValue("P_Referee_Lob", String.IsNullOrEmpty(referralRequest.Referee?.Lob) ? null : referralRequest.Referee?.Lob);
                cmd.Parameters.AddWithValue("P_Referee_CustomerId", String.IsNullOrEmpty(referralRequest.Referee?.CustomerId) ? null : referralRequest.Referee?.CustomerId);
                cmd.Parameters.AddWithValue("P_Referer_Lob", String.IsNullOrEmpty(referralRequest.Referrer?.Lob) ? null : referralRequest.Referrer?.Lob);
                cmd.Parameters.AddWithValue("P_Referer_CustomerId", String.IsNullOrEmpty(referralRequest.Referrer?.CustomerId) ? null : referralRequest.Referrer?.CustomerId);
                cmd.Parameters.AddWithValue("P_Referer_ReferalCode", String.IsNullOrEmpty(referralRequest?.ReferralCode) ? null : referralRequest?.ReferralCode);
                cmd.Parameters.AddWithValue("P_ReferralRuleId", String.IsNullOrEmpty(referralRequest?.ReferralRuleId) ? null : referralRequest?.ReferralRuleId);
                var adaptor = new MySqlDataAdapter(cmd);
                adaptor.Fill(ds);

                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var dataTable = ds.Tables[0];
                    foreach (DataRow row in dataTable.Rows)
                    {
                        referrals.Add(MapReferral(row));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return referrals;
        }
        public int UpdateReferral(ReferralModel.UpdateReferralRequest updateReferralRequest)
        {
            var queryResponse = 0;
            using var cmd = _db.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Update_ReferralStatus";
            try
            {
                if (_db.Connection.State == ConnectionState.Closed)
                {
                    _db.Connection.Open();
                }
                cmd.Parameters.AddWithValue("P_Referral_Id", updateReferralRequest.Id == null ? 0 : (int)updateReferralRequest.Id);
                cmd.Parameters.AddWithValue("P_Referee_Lob", String.IsNullOrEmpty(updateReferralRequest.Referee?.Lob) ? null : updateReferralRequest.Referee?.Lob);
                cmd.Parameters.AddWithValue("P_Referee_CustomerId", String.IsNullOrEmpty(updateReferralRequest.Referee?.CustomerId) ? null : updateReferralRequest.Referee?.CustomerId);
                cmd.Parameters.AddWithValue("P_Referer_Lob", String.IsNullOrEmpty(updateReferralRequest.Referrer?.Lob) ? null : updateReferralRequest.Referrer?.Lob);
                cmd.Parameters.AddWithValue("P_Referer_CustomerId", String.IsNullOrEmpty(updateReferralRequest.Referrer?.CustomerId) ? null : updateReferralRequest.Referrer?.CustomerId);
                cmd.Parameters.AddWithValue("P_Referer_ReferalCode", String.IsNullOrEmpty(updateReferralRequest.ReferralCode) ? null : updateReferralRequest.ReferralCode);

                cmd.Parameters.AddWithValue("P_IsExpired", updateReferralRequest.IsExpired == null ? null : Convert.ToBoolean(updateReferralRequest.IsExpired));
                cmd.Parameters.AddWithValue("P_ReferralStatus", updateReferralRequest.ReferralStatus == null ? null : Convert.ToInt32(updateReferralRequest.ReferralStatus));

                queryResponse = cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                throw;
            }
            finally
            {
                if(_db.Connection.State == ConnectionState.Open)
                {
                    _db.Connection.Close();
                }
            }
            return queryResponse;
        }
        private ReferralModel.Referral MapReferral(DataRow dataRow)
        {
            var referal = new ReferralModel.Referral
            {
                Id = dataRow.IsNull("Id") ? 0 : (int)dataRow["Id"],
                Referrer = MapReferer(dataRow),
                Referee = MapReferee(dataRow),
                BranchCode = dataRow.IsNull("BranchCode") ? String.Empty : (string)dataRow["BranchCode"],
                EmployeeCode = dataRow.IsNull("EmployeeCode") ? String.Empty : (string)dataRow["EmployeeCode"],
                ReferenceId = dataRow.IsNull("ReferenceId") ? String.Empty : (string)dataRow["ReferenceId"],
                ReferralCode = dataRow.IsNull("ReferalCode") ? String.Empty : (string)dataRow["ReferalCode"],
                Source = dataRow.IsNull("Source") ? String.Empty : (string)dataRow["Source"],
                Status = dataRow.IsNull("Status") ? 0 : (int)dataRow["Status"],
                ReferralStatus = dataRow.IsNull("ReferralStatus") ? 0 : (int)dataRow["ReferralStatus"],
                IsExpired = dataRow.IsNull("IsExpired") ? false : Convert.ToBoolean(dataRow["IsExpired"]),
                IsNew = dataRow.IsNull("IsNew") ? false : Convert.ToBoolean(dataRow["IsNew"]),
                FranchiseCode = dataRow.IsNull("FranchiseCode") ? String.Empty : (string)dataRow["FranchiseCode"],
                ReferralRuleId = dataRow.IsNull("ReferralRuleId") ? String.Empty : (string)dataRow["ReferralRuleId"],
                CampaignId = dataRow.IsNull("CampaignId") ? String.Empty : (string)dataRow["CampaignId"]
            };
            if(!dataRow.IsNull("ExpiryDate"))
            {
                referal.ExpiryDate = (DateTime)dataRow["ExpiryDate"];
            }
            if (!dataRow.IsNull("ReferedDate"))
            {
                referal.ReferredDate = (DateTime)dataRow["ReferedDate"];
            }
            if (!dataRow.IsNull("StatusChangedDate"))
            {
                referal.StatusChangedDate = (DateTime)dataRow["StatusChangedDate"];
            }
            if (!dataRow.IsNull("CreatedDate"))
            {
                referal.CreatedDate = (DateTime)dataRow["CreatedDate"];
            }
            if (!dataRow.IsNull("UpdatedDate"))
            {
                referal.UpdatedDate = (DateTime)dataRow["UpdatedDate"];
            }
            return referal;
        }
        private ReferralModel.Referee MapReferee(DataRow dataRow)
        {
            return new ReferralModel.Referee()
            {
                CustomerId = dataRow.IsNull("RefereeCustomerId") ? String.Empty : (string)dataRow["RefereeCustomerId"],
                Email = dataRow.IsNull("RefereeEmail") ? String.Empty : (string)dataRow["RefereeEmail"],
                Lob = dataRow.IsNull("RefereeLob") ? String.Empty : (string)dataRow["RefereeLob"],
                MobileNumber = dataRow.IsNull("RefereeMobileNumber") ? String.Empty : (string)dataRow["RefereeMobileNumber"],
            };

        }
        private ReferralModel.Referrer MapReferer(DataRow dataRow)
        {
            return new ReferralModel.Referrer()
            {
                CustomerId = dataRow.IsNull("RefererCustomerId") ? String.Empty : (string)dataRow["RefererCustomerId"],
                Email = dataRow.IsNull("RefererEmail") ? String.Empty : (string)dataRow["RefererEmail"],
                Lob = dataRow.IsNull("RefererLob") ? String.Empty : (string)dataRow["RefererLob"],
                MobileNumber = dataRow.IsNull("RefererMobileNumber") ? String.Empty : (string)dataRow["RefererMobileNumber"],
            };
        }

    }

}
