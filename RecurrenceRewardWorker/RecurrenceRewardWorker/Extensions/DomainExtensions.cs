﻿using Domain.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using RewardModel = Domain.Models.RewardModel;
using CampaignModel = Domain.Models.CampaignModel;
using CustomerModel = Domain.Models.CustomerModel;
using TransactionModel = Domain.Models.TransactionModel;
using System.Linq;

namespace Extensions
{
    public static class DomainExtensions
    {
        public static bool HasProperty(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName) != null;
        }
        public static bool RMSCheckIgnoreCase(this Domain.Models.CampaignModel.RMSAttribute rmsAttribute, string key)
        {
            return string.Equals(rmsAttribute.AttributeType, key, StringComparison.OrdinalIgnoreCase);
        }
        public static bool RMSParameterCheckIgnoreCase(this Domain.Models.CampaignModel.RMSAttribute rmsAttribute, string key)
        {
            return string.Equals(rmsAttribute.Parameter, key, StringComparison.OrdinalIgnoreCase);
        }
        // public static bool CustomerSegmentCheckIgnoreCase(this Domain.Models.CustomerModel.Customer rmsAttribute, string key)
        //{
        //    return string.Equals(rmsAttribute.AttributeType, key, StringComparison.OrdinalIgnoreCase);
        //}
        public static UpdateDefinition<TEntity> CombineWith<TEntity>(this UpdateDefinition<TEntity> leftDefinition, UpdateDefinition<TEntity> rightDefinition)
        {
            return Builders<TEntity>.Update.Combine(leftDefinition, rightDefinition);
        }
        public static FilterDefinition<TEntity> CombineWithAnd<TEntity>(this FilterDefinition<TEntity> leftDefinition, FilterDefinition<TEntity> rightDefinition)
        {
            return Builders<TEntity>.Filter.And(leftDefinition, rightDefinition);
        }
        public static FilterDefinition<TEntity> CombineWithOr<TEntity>(this FilterDefinition<TEntity> leftDefinition, FilterDefinition<TEntity> rightDefinition)
        {
            return Builders<TEntity>.Filter.Or(leftDefinition, rightDefinition);
        }
       
        public static List<T> GetResults<T>(this string message)
        {
            return JsonConvert.DeserializeObject<List<T>>(message);
        }
        public static T GetResult<T>(this string message)
        {
            return JsonConvert.DeserializeObject<T>(message);
        }
        public static dynamic GetDynamicResult(this string message)
        {
            return JsonConvert.DeserializeObject<dynamic>(message);
        }

        public static FilterDefinition<T> PrepareFilterDefinition<T>(this FilterDefinitionBuilder<T> filterDefinitionBuilder, Expression<Func<T, bool>> expression)
        {
            return filterDefinitionBuilder.Where(expression);
        }


        #region Filter Section
        //static FilterDefinition<CustomerModel.CustomerEvent> PrepareMobileNumberFilter(FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, string mobileNumber)
        //{
        //    return filterBuilder.Where(o => o.MobileNumber == mobileNumber);
        //}
        //static FilterDefinition<RewardModel.TransactionReward> PrepareMobileNumberFilter(FilterDefinitionBuilder<RewardModel.TransactionReward> filterBuilder, string mobileNumber)
        //{
        //    return filterBuilder.Where(o => o.MobileNumber == mobileNumber);
        //}
        //static FilterDefinition<CustomerModel.CustomerEvent> PrepareEventCodeFilter(FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, string eventCode)
        //{
        //    return filterBuilder.Where(o => o.EventCode == eventCode);
        //}
        //static FilterDefinition<CustomerModel.CustomerEvent> PrepareCampaignMinimumAmountFilter(FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, double campaignMinimumAmount)
        //{
        //    return filterBuilder.Where(o => o.Amount >= campaignMinimumAmount);
        //}
        //static FilterDefinition<CustomerModel.CustomerEvent> PrepareCampaignStartDateFilter(FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, DateTime startDate)
        //{
        //    return filterBuilder.Where(o => o.TxnDateTime >= startDate);
        //}
        private static FilterDefinition<CustomerModel.CustomerEvent> PreparePaymentInstrumentFilter(this FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, List<string> paymentInstrument)
        {
            return filterBuilder.Where(o => o.PaymentInstrument.Intersect(paymentInstrument).Any());
        }        
        public static FilterDefinition<CustomerModel.CustomerEvent> PrepareFilter(this FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, string mobileNumber, string eventCode = null)
        {
            var filterDefinition = filterBuilder.PrepareFilterDefinition(o => o.MobileNumber == mobileNumber);
            if (!String.IsNullOrEmpty(eventCode))
            {
                filterDefinition &= filterBuilder.PrepareFilterDefinition(o => o.EventCode == eventCode);
            }
            return filterDefinition;
        }
        public static FilterDefinition<RewardModel.TransactionReward> PrepareFilter(this FilterDefinitionBuilder<RewardModel.TransactionReward> filterBuilder, string mobileNumber, string eventCode = null)
        {
            var filterDefinition = filterBuilder.PrepareFilterDefinition(o => o.MobileNumber == mobileNumber);
            if (!String.IsNullOrEmpty(eventCode))
            {
                filterDefinition &= filterBuilder.PrepareFilterDefinition(o => o.EventId == eventCode);
            }
            return filterDefinition;
        }

        public static FilterDefinition<RewardModel.TransactionReward> PrepareWalletCreationFilter(this FilterDefinitionBuilder<RewardModel.TransactionReward> filterBuilder, string mobileNumber)
        {
            return filterBuilder.PrepareFilter(mobileNumber, "WalletCreation");
        }
        public static FilterDefinition<CustomerModel.CustomerEvent> PrepareWalletCreationFilter(this FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, string mobileNumber)
        {
            return filterBuilder.PrepareFilter(mobileNumber, "WalletCreation");
        }
        public static FilterDefinition<CustomerModel.CustomerEvent> PrepareWalletLoadFilter(this FilterDefinitionBuilder<CustomerModel.CustomerEvent> filterBuilder, string mobileNumber, DateTime campaignStartDate, CampaignModel.WalletLoad walletLoad)
        {
            var filter = filterBuilder.PrepareFilter(mobileNumber, "WalletLoad");
            filter &= filterBuilder.PrepareFilterDefinition(o=> o.TxnDateTime >= campaignStartDate);
            if (walletLoad.IsMinimumLoadAmount)
            {
                filter &= filterBuilder.PrepareFilterDefinition(o=> o.Amount >= (double)walletLoad.MinimumLoadAmount);
            }
            //if (walletLoad.PaymentInstruments != null)
            //{
            //    filter &= filterBuilder.PreparePaymentInstrumentFilter(walletLoad.PaymentInstruments);
            //}
            return filter;
        }        
        #endregion
    }
}
