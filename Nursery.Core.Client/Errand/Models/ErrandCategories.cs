using LivingThing.Core.Frameworks.Common.Data;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandCategories : IDisplayable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IconDescriptor Icon { get; set; }
    }
}