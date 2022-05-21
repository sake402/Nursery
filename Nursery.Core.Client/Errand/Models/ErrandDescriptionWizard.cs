using LivingThing.Core.Frameworks.Common.Attributes;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandDescription
    {
        [Schema (Title=" Describe the errand ", Description="Describe the errand")]
        public string Description{get; set;}
    }
}