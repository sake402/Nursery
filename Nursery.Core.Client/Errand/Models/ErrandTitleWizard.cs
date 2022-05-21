using LivingThing.Core.Frameworks.Common.Attributes;
namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandTitleWizard
    {
        [Schema(Description ="e.g Wash clothes, iron dress etc")]
        public string Title{get; set;}
    }
}