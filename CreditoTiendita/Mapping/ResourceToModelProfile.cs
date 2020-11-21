using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Resources;
using CreditoTiendita.Resources.SaveResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveFeeTypeResource, FeeType>();
            CreateMap<SaveFeeResource, Fee>();
            CreateMap<SavePeriodResource, Period>();
            CreateMap<SaveTransactionTypeResource, TransactionType>();
            CreateMap<SaveCurrencyResource, Currency>();
            CreateMap<SaveClientResource, Client>();
            CreateMap<SaveAccountResource, Account>();
        }
    }
}
