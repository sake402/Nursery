using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursery.Core.Client.Errand.Models
{
    public class ErrandDetailsWizardStep
    {
        public ErrandDetailsWizardStep(ErrandCreateWizard root)
        {
            Root = root;
        }

        public ErrandCreateWizard Root { get; set; }
    }
}
