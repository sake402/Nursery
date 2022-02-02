
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LivingThing.Core.Frameworks.Common.Models;
using LivingThing.Core.Frameworks.Client.Interface;
using Nursery.Core.Client.Dispatch.Models;
using System.Linq;

namespace Nursery.Core.Client.Dispatch.Shared
{
    public partial class DispatchImagesWizardStepInput:BaseInputFieldProvider<DispatchImagesWizardStep>
    {
        Task FileSelected(IEnumerable<PickedFileInfo> resources)
        {
            // Value.Images = Value.Images.Concat(resource).ToList();
            return Task.CompletedTask;
        }
    }
}