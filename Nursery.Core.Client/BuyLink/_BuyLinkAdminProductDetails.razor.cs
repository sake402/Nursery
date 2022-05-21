
using LivingThing.Core.Frameworks.Common.RPC;
using LivingThing.Core.Community.BuyLink.Common.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace LivingThing.Core.Community.BuyLink.Client.Shared
{
    public partial class _BuyLinkAdminProductDetails //: ILoadableViewModel
    {
        [Inject] public IServerRemoteService Server { get; set; }
        CommunityBuyLinkQueryPostModel query = new CommunityBuyLinkQueryPostModel();
        Task Query(CommunityBuyLinkQueryPostModel query)
        {
            return Server.Create(ViewModel.Id.Id.ToString(), query);
        }

        Task SetPrice(double amount)
        {
            return Task.CompletedTask;
        }
    }
}
