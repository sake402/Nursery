using LivingThing.Core.Frameworks.Common.Attributes;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandPriceWizard
    {
        [Schema (Description="3,000")]
        public string Price{get; set;}
    }
}