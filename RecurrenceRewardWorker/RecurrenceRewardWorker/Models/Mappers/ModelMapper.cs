﻿using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Models.Mappers
{
    public static class ModelMapper
    {
        public static CustomerModel.CustomerVersion Map(CustomerModel.Customer customer)
        {
            var model = customer;
            return new CustomerModel.CustomerVersion()
            {
                MobileNumber = model.MobileNumber,
                Type = model.Type,
                TypeId = model.TypeId,
                SignUpDate = model.SignUpDate,
                Wallet = model.Wallet,
                WalletBalance = model.WalletBalance,
                KYC = model.KYC,
                Flags = model.Flags,
                Install = model.Install,
                Segments = model.Segments,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                //InternalCustomerId = model.LoyaltyId,
                ExternalCustomerId=model.CustomerId,
                UniqueCustomerId = model.UniqueCustomerId,
            };
        }       

        public static TransactionModel.Transaction Map(TransactionModel.Transaction transactionRequest)
        {
            var model = transactionRequest;
            
            return new TransactionModel.Transaction()
            {
                TransactionId = model.TransactionId,
                MobileNumber = model.MobileNumber,
                LOB = model.LOB,
                EventId = model.EventId,
                ChannelCode = model.ChannelCode,
                //ProductCode = model.ProductCode,
                CustomerDetail = GetCustomerDetail(),
                Wallet = model.Wallet, 
                Campaign = model.Campaign,
                UTM = model.UTM,
                TransactionDetail = model.TransactionDetail,
                InternalCustomerId=model.InternalCustomerId,
                ExternalCustomerId=model.ExternalCustomerId
            };

            Common.TransactionModel.CustomerDetail GetCustomerDetail() => new Common.TransactionModel.CustomerDetail { LoyaltyId = string.Empty, CustomerVersionId = string.Empty };
        }        

        public static Common.TransactionModel.CustomerDetail MapCustomerDetail(CustomerModel.Customer customer)
        {
            var model = customer;
            return new Common.TransactionModel.CustomerDetail() 
            {
                 LoyaltyId = customer.LoyaltyId,
                 CustomerVersionId = customer.CustomerVersionId
            };
        }

        
    }
}
