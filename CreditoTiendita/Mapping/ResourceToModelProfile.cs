﻿using AutoMapper;
using CreditoTiendita.Domain.Models;
using CreditoTiendita.Resources;
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
        }
    }
}