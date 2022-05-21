using Nursery.Core.Client.Errand.Models;
using LivingThing.Core.Frameworks.Common.Icons;

namespace Nursery.Core.Client.Errand.Pages
{
    public partial class Home
    {
        ErrandCategories[] Categories => new ErrandCategories[]{
            new ErrandCategories{
                Title="Cooking",
                Icon= MaterialDesignIconNames.FoodForkDrink,

            },
            new ErrandCategories{
                Title="Cleaning",
                Icon= MaterialDesignIconNames.Food,
            },
            new ErrandCategories{
                Title="Shopping",
                Icon= MaterialDesignIconNames.Shopping,
            },
            new ErrandCategories{
                Title="Entertainment",
                Icon= MaterialDesignIconNames.Music
            },
            new ErrandCategories{
                Title="Assist",
                Icon= MaterialDesignIconNames.Shovel
            },
            new ErrandCategories{
                Title="More",
                Icon= MaterialDesignIconNames.More
            }
        };
        public string AskingPrice{get; set;}
        public string Pitch{get; set;}
        public string Price{get; set;}
        public string BidPrice{get; set;}
    }
}