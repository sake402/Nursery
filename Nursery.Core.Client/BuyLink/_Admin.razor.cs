using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LivingThing.Core.Community.BuyLink.Common.Models;
using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common.Data;
using LivingThing.Core.Frameworks.Common.Entry;
using LivingThing.Core.Frameworks.Common.Icons;
using LivingThing.Core.Frameworks.Common.String;
using LivingThing.Core.Frameworks.Common.Types;
using LivingThing.Core.Frameworks.Common.ViewModels;

namespace LivingThing.Core.Community.BuyLink.Client.Pages
{
    public class _BuyLinkDashboard
    {
        [Schema(Description = "New orders since today",
            Options = new[] { "//Icon=" + nameof(MaterialDesignIconNames.NewBox) })]
        public int OrdersToday { get; set; }
        [Schema(Description = "Orders waiting for processing",
            Options = new[] { "//Icon=" + nameof(MaterialDesignIconNames.LanPending) })]
        public int PendingOrders { get; set; }
        [Schema(Description = "Orders waiting for customer payment",
            Options = new[] { "//Icon=" + nameof(MaterialDesignIconNames.LanPending) })]
        public int AwaitingPayments { get; set; }
        [Schema(Description = "Orders currently shipping",
            Options = new[] { "//Icon=" + nameof(MaterialDesignIconNames.Airplane) })]
        public IEnumerable<IChartDataPoint> InShipment { get; set; } = new ChartDataPoint[] { new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1), new ChartDataPoint("1", 1) };
    }
    public partial class _Admin : ILoadableViewModel
    {

        _BuyLinkDashboard dashboard = new _BuyLinkDashboard();
        public LoadState Loading { get; set; }
        public string LoadErrorMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Task Load()
        {
            throw new System.NotImplementedException();
        }

        CommunityBuyLinkPostModel Rand(CommunityBuyLinkPostModel v)
        {
            v.IconDescriptor = "";
            v.Id = new DataId(Guid.NewGuid());
            return v;
        }
        CommunityBuyLinkOrderModel Rand(CommunityBuyLinkOrderModel v)
        {
            v.IconDescriptor = "";
            v.Id = new DataId(Guid.NewGuid());
            return v;
        }
        IEnumerable<CommunityBuyLinkOrderModel> orders;
        IEnumerable<CommunityBuyLinkOrderModel> Orders => orders ??= Enumerable.Range(1, 10).Select(r => Rand(new CommunityBuyLinkOrderModel().RandomizeProperties())).ToList();
        IEnumerable<CommunityBuyLinkPostModel> products;
        IEnumerable<CommunityBuyLinkPostModel> Products => products ??= Enumerable.Range(1, 10).Select(r => Rand(new CommunityBuyLinkPostModel().RandomizeProperties())).ToList();

    }
}
