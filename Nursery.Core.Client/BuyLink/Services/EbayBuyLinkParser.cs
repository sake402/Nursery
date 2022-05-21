
// using System.Linq;
// using AngleSharp.Dom;
// using AngleSharp.Html.Dom;
// using LivingThing.Core.Community.BuyLink.Common.Models;

// namespace LivingThing.Core.Community.BuyLink.Common.Services
// {
//     public class EbayBuyLinkParser : IBuyLinkWebsiteParser
//     {
//         public void Populate(CommunityBuyLinkPostModel buyLink, IDocument document)
//         {
//             var images = document.QuerySelectorAll("li.v-pic-item img, div.v-pic-item>img, img.vi-image-gallery__image")
//             .Select(i => ((IHtmlImageElement)i).Source.Replace("s-l64", "s-l500"));
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