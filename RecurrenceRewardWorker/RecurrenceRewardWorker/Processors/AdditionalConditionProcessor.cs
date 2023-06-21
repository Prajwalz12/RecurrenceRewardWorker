using Domain.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using CampaignModel = Domain.Models.CampaignModel;
using TransactionModel = Domain.Models.TransactionModel;

namespace Domain.Processors;

public class AdditionalConditionProcessor
{
    private readonly ILogger<AdditionalConditionProcessor> _logger;
    private readonly ITransactionService _transactionService;

    public AdditionalConditionProcessor(ILogger<AdditionalConditionProcessor> logger, ITransactionService transactionService)
    {
        _logger = logger;
        _transactionService = transactionService;
    }
    public async Task<(bool status, AdditionalConditionParserResponse additionalConditionParserResponse)> ProcessAsync(TransactionModel.ProcessedTransaction processedTransaction, CampaignModel.EarnCampaign earnCampaign)
    {
        var status = true;
        AdditionalConditionParserResponse additionalConditionParserResponse = null;
        var isAdditionalCondition = earnCampaign.Filter.IsAdditionalCondition;
        if (!isAdditionalCondition)
        {
            status = true;
        }
        else
        {
            var customer = processedTransaction.Customer;
            var accountOpeningDate = customer.AccountOpeningDate;
            if (accountOpeningDate == null)
            {
                status = false;
            }
            else
            {
                var additionalCondition = earnCampaign.RewardCriteria.AdditionalCondition;
                var referenceEventCode = additionalCondition.ReferenceEvent.Code;
                var referenceEventType = additionalCondition.ReferenceEvent.Type;
                var additionalConditionDuration = additionalCondition.AdditionalConditionDuration;
                var campaignOfferType = earnCampaign.OfferType;
                var campaignRewardIssuance = earnCampaign.RewardCriteria.RewardIssuance;              

                if (String.Equals(campaignOfferType, "PAYMENT", StringComparison.OrdinalIgnoreCase))
                {
                    if (String.Equals(campaignRewardIssuance, "DIRECT", StringComparison.OrdinalIgnoreCase))
                    {
                        string transactionEventCode = String.Empty;
                        string campaignEventCode = String.Empty;
                        if (String.Equals(referenceEventType, "EventType", StringComparison.OrdinalIgnoreCase))
                        {
                            transactionEventCode = processedTransaction.TransactionRequest.EventId;
                            campaignEventCode = "PAYMENT";                           
                        }
                        else if (String.Equals(referenceEventType, "PaymentType", StringComparison.OrdinalIgnoreCase))
                        {
                            transactionEventCode = processedTransaction.TransactionRequest.TransactionDetail.Type;
                            campaignEventCode = earnCampaign.RewardCriteria.Direct.PaymentDirect.PaymentCategories;
                            
                        }
                        if (!String.Equals(campaignEventCode, transactionEventCode, StringComparison.OrdinalIgnoreCase))
                        {
                            // TransactionEventCode Does not Matched With CampaignEventCode.
                            status = false;
                        }
                        else
                        {
                            switch (referenceEventCode)
                            {
                                case "AO":
                                    {
                                        var transactionFilterDefinition = Builders<TransactionModel.Transaction>.Filter.Where(o => o.TransactionDetail.Customer.CustomerId == customer.CustomerId && o.LOB == customer.Lob && o.EventId == processedTransaction.TransactionRequest.EventId);
                                        var transactions = _transactionService.Get(transactionFilterDefinition).FirstOrDefault();
                                        if (transactions == null)
                                        {
                                            status = false;
                                        }
                                        else
                                        {
                                            if (!String.Equals(transactionEventCode, campaignEventCode, StringComparison.OrdinalIgnoreCase))
                                            {
                                                status = false;
                                            }
                                            else
                                            {
                                                if (additionalConditionDuration == null)
                                                {
                                                    status = false;
                                                }
                                                else
                                                {
                                                    var transactionDate = processedTransaction.TransactionRequest.TransactionDetail.DateTime;
                                                    var additionalConditionDurationValue = additionalConditionDuration.Value;

                                                    if (!(((DateTime)accountOpeningDate).AddDays(additionalConditionDurationValue) >= transactionDate))
                                                    {
                                                        status = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                else if (String.Equals(campaignOfferType, "ACTIVITY", StringComparison.OrdinalIgnoreCase))
                {
                    if (String.Equals(campaignRewardIssuance, "DIRECT", StringComparison.OrdinalIgnoreCase))
                    {
                        string transactionEventCode = String.Empty;
                        string campaignEventCode = earnCampaign.RewardCriteria.Direct.ActivityDirect.Event.EventName;
                        if (String.Equals(referenceEventType, "EventType", StringComparison.OrdinalIgnoreCase))
                        {
                            transactionEventCode = processedTransaction.TransactionRequest.EventId;                            
                        }
                        else
                        {
                            transactionEventCode = processedTransaction.TransactionRequest.TransactionDetail.Type;
                        }
                        if (!String.Equals(campaignEventCode, transactionEventCode, StringComparison.OrdinalIgnoreCase))
                        {
                            // TransactionEventCode Does not Matched With CampaignEventCode.
                            status = false;
                        }
                        else
                        {
                            switch (referenceEventCode)
                            {
                                case "AO":
                                    {
                                        var transactionFilterDefinition = Builders<TransactionModel.Transaction>.Filter.Where(o => o.TransactionDetail.Customer.CustomerId == customer.CustomerId && o.LOB == customer.Lob && o.EventId ==processedTransaction.TransactionRequest.EventId);
                                        var transactions = _transactionService.Get(transactionFilterDefinition).FirstOrDefault();
                                        if (transactions == null)
                                        {
                                            status = false;
                                        }
                                        else
                                        {
                                            if (!String.Equals(transactionEventCode, campaignEventCode, StringComparison.OrdinalIgnoreCase))
                                            {
                                                status = false;
                                            }
                                            else
                                            {
                                                if (additionalConditionDuration == null)
                                                {
                                                    status = false;
                                                }
                                                else
                                                {
                                                    var transactionDate = processedTransaction.TransactionRequest.TransactionDetail.DateTime;
                                                    var additionalConditionDurationValue = additionalConditionDuration.Value;

                                                    if (!(((DateTime)accountOpeningDate).AddDays(additionalConditionDurationValue) >= transactionDate))
                                                    {
                                                        status = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                } 
            }
        }
        if(status)
        {
            additionalConditionParserResponse = new AdditionalConditionParserResponse() 
            {
                 StatusCode = 200,
                 StatusMessage = "SUCCESS"
            };
        }
        else
        {
            additionalConditionParserResponse = new AdditionalConditionParserResponse()
            {
                StatusCode = 500,
                StatusMessage = "NOT SUCCESS"
            };
        }
        return await Task.FromResult((status, additionalConditionParserResponse)).ConfigureAwait(false);
    }
}
public class AdditionalConditionParserResponse
{
    public int StatusCode { get; set; }
    public string StatusMessage { get; set; }
}