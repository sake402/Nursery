using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common;
using LivingThing.Core.Frameworks.Common.Data.Measurements;

namespace Nursery.Core.Client.Dispatch.Models
{
    public class DispatchWeightWizardStep
    {
        [Schema(Order = 1, InputType = InputType.ButtonSet)]
        public DispatchWeightClass WeightClass { get; set; }
        [Schema(Order = 2, Description = "Please leave blank if you do not know")]
        public Weight ExactWeight { get; set; }
        [Schema(Order = 3, Description = "Please leave blank if you do not know")]
        public Volume ExactVolume { get; set; }
    }
}
