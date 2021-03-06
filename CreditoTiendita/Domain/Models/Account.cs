﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Models
{
    public class Account
    {
        public int Id { get; set; }
        public float UsedCredit { get; set; }
        public float AvailableCredit { get; set; }


        public IList<AccountStatus> AccountStatuses { get; set; } = new List<AccountStatus>();
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public IList<AditionalCost> AditionalCosts { get; set; } = new List<AditionalCost>();

        public Fee Fee { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int PeriodId { get; set; }
        public Period Period { get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }
    }
}
