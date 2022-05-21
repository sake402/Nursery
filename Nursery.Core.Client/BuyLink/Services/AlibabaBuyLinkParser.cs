
// using System.Linq;
// using AngleSharp.Dom;
// using AngleSharp.Html.Dom;
// using LivingThing.Core.Community.BuyLink.Common.Models;

// namespace LivingThing.Core.Community.BuyLink.Common.Services
// {
//     public class AlibabaBuyLinkParser : IBuyLinkWebsiteParser
//     {
//         public void Populate(CommunityBuyLinkPostModel buyLink, IDocument document)
//         {
//             var images = document.QuerySelectorAll("img.J-slider-cover-item")
//             .Select(i => ((IHtmlImageElement)i).Source.Replace(".jpg_50x50", ""));
//             if (images.Any())
//             {
//                 if (buyLink.ProductIcons == null)
//                 {
//                     buyLink.ProductIcons = images;
//                 }
//                 else
//                 {
//                     buyLink.ProductIcons = buyLink.ProductIcons.Concat(images).Distinct();
//                 }
//             }
//         }
//     }
// }