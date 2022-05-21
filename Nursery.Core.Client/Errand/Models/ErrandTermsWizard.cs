using LivingThing.Core.Frameworks.Common.Attributes;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandTerms
    {
        [Schema (Title=" Describe the errand ", Description="Describe the errand...")]
        public string Terms{get; set;}
    }
}