
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using LivingThing.Core.Frameworks.Common.Attributes;
using LivingThing.Core.Frameworks.Common.Icons;
using LivingThing.Core.Frameworks.Common.ViewModels;
using LivingThing.Core.Frameworks.Common.Data;
using LivingThing.Core.Frameworks.Common.String;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Nursery.Core.Client.BuyLink
{
    public class BuyLinkDashboard
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
    public partial class Dashboard : ILoadableViewModel
    {
        [Parameter] public string Page { get; set; }
        BuyLinkDashboard dashboard = new BuyLinkDashboard();
        public LoadState Loading { get; set; }
        public string LoadErrorMessage { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Colour { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string DeliveryAddress { get; set; }

        public Task Load()
        {
            throw new System.NotImplementedException();
        }

        T RandomizeProperties<T>(T data)
        {
            var properties = typeof(T).GetProperties();
            Random r = new Random();
            foreach (var p in properties)
            {
                if (p.CanWrite)
                {
                    if (p.PropertyType == typeof(string))
                    {
                        var l = r.Next(20);
                        var s = l.GetRandomString();
                        p.SetValue(data, s);
                    }
                    else if (p.PropertyType == typeof(int))
                    {
                        p.SetValue(data, r.Next());
                    }
                }
            }
            return data;
        }
    }
}
