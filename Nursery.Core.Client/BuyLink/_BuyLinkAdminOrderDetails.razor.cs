
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivingThing.Core.Community.BuyLink.Common.Models;
using LivingThing.Core.Frameworks.Common.Entry;
using LivingThing.Core.Frameworks.Common.RPC;
using LivingThing.Core.Frameworks.Common.Types;
using Microsoft.AspNetCore.Components;

namespace LivingThing.Core.Community.BuyLink.Client.Shared
{
    public partial class _BuyLinkAdminOrderDetails //: ILoadableViewModel
    {
        [Inject] public IServerRemoteService Server { get; set; }
        CommunityBuyLinkQueryPostModel query = new CommunityBuyLinkQueryPostModel();
        Task Query(CommunityBuyLinkQueryPostModel query)
        {
            return Server.Create(ViewModel.Id.Id.ToString(), query);
        }
        IEnumerable<CommunityBuyLinkPostModel> products;
        CommunityBuyLinkPostModel Rand(CommunityBuyLinkPostModel v)
        {
            v.IconDescriptor = "";
            v.Id = new DataId(Guid.NewGuid());
            return v;
        }
        IEnumerable<CommunityBuyLinkPostModel> Products => products ??= Enumerable.Range(1, 10).Select(r => Rand(new CommunityBuyLinkPostModel().RandomizeProperties())).ToList();

    }
}