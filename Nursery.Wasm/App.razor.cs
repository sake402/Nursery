using Nursery.Wasm.Shared;
using LivingThing.Core.Frameworks.Client.App;
using System.Reflection;

namespace Nursery.Wasm
{
    public class App:AppBase<App, MainLayout>
    {
        public override Assembly[] AdditionalAssemblies => new[] { typeof(Nursery.Core.Client.Startup).Assembly };
    }
}
