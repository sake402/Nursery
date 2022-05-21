
using LivingThing.Core.Frameworks.Common.Attributes;
using System;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandExpiry
    {
        [Schema(Title="Choose Expiry Time")]
        public DateTime ExpiryTime { get; set; }
    }
}
