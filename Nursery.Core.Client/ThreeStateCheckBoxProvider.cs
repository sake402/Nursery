
using LivingThing.Core.Frameworks.Client;
using LivingThing.Core.Frameworks.Client.Interface;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Nursery.Core.Client
{
    public class ABC{

    }
    public class ThreeStateCheckBoxProvider : IInputFieldProvider<ABC>
    {
        public RenderFragment GetField(ABC value, Func<ABC, Task> onChanged)
        {
            return FragmentView.From<Shared.Three>();
        }
    }
}