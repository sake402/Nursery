using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LivingThing.Core.Frameworks.Common.Attributes;

namespace Nursery.Core.Client.Dispatch.Pages
{
    public enum Cur
    {
        [Schema(Title = "#")]
        Naira,
        [Schema(Title = "$")]
        Dollar
    }
    public enum Weight
    {
        Kg,
        g
    }
    public enum Volume
    {
        m3,
        l
    }
    public enum CancellationReason
    {
        [Schema(Title = "Didn't get any bid")]
        NoBid,
        [Schema(Title = "Bid costs too hight for my budget")]
        BidTooHigh,
        [Schema(Title = "Bidders doesn't have good ratings")]
        BadBiddersRatings,
        [Schema(Title = "Others")]
        Others
    }

    public class Properties
    {
        public DateTime OrderDate { get; set; }

    }
    public partial class Dashboard
    {
bool awarding;
        Properties prop = new Properties
        {
            OrderDate = DateTime.Now
        };

        Dictionary<string, object> dict = new Dictionary<string, object>()
        {
            ["Order Date"] = DateTime.Now,
            ["Number of Packages"] = 3,
            ["Weight Class"] = "Light",
            ["Weight"] = "50kg",
            ["Volume"] = "20cm3",
            ["Declared Value"] = "#50, 000.00",
            ["Send from"] = "24, Ajao Street",
            ["Send to"] = "31, Guangzhou street",
            ["Status"] = "Awaiting Pickup"
        };
        CancellationReason cancellationReason;
        string weight;
        string volume;
        string worth;
        Cur c;
        Weight w;
        Volume v;

    }
}
