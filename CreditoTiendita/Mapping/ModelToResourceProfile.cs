using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<FeeType, FeeTypeResource>();
            CreateMap<Fee, FeeResource>();
            CreateMap<Period, PeriodResource>();
            CreateMap<TransactionType, TransactionTypeResource>();
            CreateMap<Currency, CurrencyResource>();
            CreateMap<Client, ClientResource>();
            CreateMap<Account, AccountResource>();
        }
    }
}
