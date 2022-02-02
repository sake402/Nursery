using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common;
using LivingThing.Core.Frameworks.Common.Data.Geographic;

namespace Nursery.Core.Client.Dispatch.Models
{
    public class DispatchCreateWizard
    {
        [Schema(Order = 1, InputType = InputType.Custom,
            Description = "Please take one or more pictures of the package or pasckages you are dispatching")]
        public DispatchImagesWizardStep Images { get; set; } = new DispatchImagesWizardStep();

        [Schema(Order = 2, Description = "Please specify a close appropriate size of the package")]
        public DispatchWeightWizardStep Weight { get; set; } = new DispatchWeightWizardStep();

        [Schema(Order = 3, Description = "Please declare the value of this package. This information is not shared with anybody, but can prove useful in case of package loss or dispute.")]
        public DispatchWorthWizardStep Worth { get; set; } = new DispatchWorthWizardStep();
        [Schema(Order = 4, Description = "Where will you be sending these packages from?")]
        public DispatchPickupLocationWizardStep PickupLocation { get; set; } = new DispatchPickupLocationWizardStep();
        [Schema(Order = 5, Description = "Where will you be sending these packages to?\r\nPick a location on the map")]
        public DispatchDeliveryLocationWizardStep DeliveryLocation { get; set; } = new DispatchDeliveryLocationWizardStep();
    }
}
