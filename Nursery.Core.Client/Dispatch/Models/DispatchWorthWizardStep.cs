using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common.Data.Measurements;

namespace Nursery.Core.Client.Dispatch.Models
{
    public class DispatchWorthWizardStep
    {
        [Schema(Description = "Please leave blank if you do not want to declare", Options = new[] { "//Unit=₦" })]
        public Money PackageWorth { get; set; }
    }
}
