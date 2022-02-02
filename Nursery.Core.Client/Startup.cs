using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivingThing.Core.Frameworks.Common;
using LivingThing.Core.Frameworks.Client;
using LivingThing.Core.IdentityManager.Common;
using LivingThing.Core.IdentityManager.Client;
using Microsoft.Extensions.DependencyInjection;
using Nursery.Core.Client.Dispatch.Shared;
using Nursery.Core.Client.Dispatch.Models;
using LivingThing.Core.Frameworks.Client.Interface;

namespace Nursery.Core.Client
{
    public static class Startup
    {
        public static void AddGidiClient(this IServiceCollection services)
        {
            services.AddInputField<DispatchImagesWizardStepInput, DispatchImagesWizardStep>();
            services.AddInputField<DispatchPickupLocationWizardStepInput, DispatchPickupLocationWizardStep>();
            services.AddInputField<DispatchDeliveryLocationWizardStepInput, DispatchDeliveryLocationWizardStep>();
            services.AddCommonServices();
            services.AddClientFrameworks();
            services.AddIdentityCommon();
            services.AddIdentityClient();
        }
    }
}
