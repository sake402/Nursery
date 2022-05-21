using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common.Icons; 
namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandCreateWizard
    {
        public ErrandCreateWizard()
        {
            Details = new ErrandDetailsWizardStep(this);
        }

        [Schema(Options = new []{"//Icon="+MaterialDesignIconNames.MessageQuestionOutline}, 
        Description ="What help would you want?")]
        public ErrandTitleWizard Title {get; set;} = new ErrandTitleWizard();

        [Schema(Options = new []{"//Icon="+MaterialDesignIconNames.CurrencyNgn},
        Description ="How much would you pay?")]
        public ErrandPriceWizard Price {get; set;} = new ErrandPriceWizard();

        [Schema(Options = new []{"//Icon="+MaterialDesignIconNames.NotebookEdit},
        Description ="Describe the natureof the errand?")]
        public ErrandDescription Description {get; set; } = new ErrandDescription();
        [Schema(Description = "Where will you be sending these packages from?")]
        public ErrandPickupLocationWizardStep PickupLocation { get; set; } = new ErrandPickupLocationWizardStep();
        [Schema( Description = "Where will you be sending these packages to?\r\nPick a location on the map")]
        public ErrandDeliveryLocationWizardStep DeliveryLocation { get; set; } = new ErrandDeliveryLocationWizardStep();
        [Schema(Options = new []{"//Icon="+MaterialDesignIconNames.Mace},
        Description ="Please, declare your terms for this service?")]
        public ErrandTerms Terms {get; set; } = new ErrandTerms();
        [Schema(Options = new[] { "//Icon=" + MaterialDesignIconNames.ClockEnd },
        Description = "How long would you want this work done?")]
        public ErrandExpiry Expiry { get; set; } = new ErrandExpiry();
        public ErrandDetailsWizardStep Details { get; set; }

    }
}