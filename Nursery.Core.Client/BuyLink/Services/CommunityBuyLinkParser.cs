
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using AngleSharp;
// using AngleSharp.Css;
// using AngleSharp.Dom;
// using AngleSharp.Scripting;
// using LivingThing.Core.Community.BuyLink.Common.Models;

// namespace LivingThing.Core.Community.BuyLink.Common.Services
// {
//     public class CommunityBuyLinkParser
//     {
//         IBrowsingContext context;
//         static Dictionary<string, IBuyLinkWebsiteParser> parsers = new Dictionary<string, IBuyLinkWebsiteParser>()
//         {
//             ["alibaba.com"] = new AlibabaBuyLinkParser(),
//             ["www.alibaba.com"] = new AlibabaBuyLinkParser(),
//             ["ebay.com"] = new EbayBuyLinkParser(),
//             ["www.ebay.com"] = new EbayBuyLinkParser(),
//         };
//         public CommunityBuyLinkParser()
//         {
//             var config = Configuration.Default
//             .WithDefaultLoader();
//             // .WithJs()
//             // .WithCss();
//             // .WithCookies();
//             // .WithCookies()
//             // .WithJavaScript()
//             // .WithCss();
//             context = BrowsingContext.New(config);
//         }


//         IElement GetOpenGraphMeta(IDocument document, IHtmlCollection<IElement> metas, string tagName)
//         {
//             return metas.FirstOrDefault(m => m.Attributes.Any(a => a.Name == "property" && a.Value == tagName));
//         }

//         string GetMetaValue(IDocument document, IHtmlCollection<IElement> metas, string tagName)
//         {
//             return GetOpenGraphMeta(document, metas, tagName)?.Attributes?.SingleOrDefault(a => a.Name == "content")?.Value;
//         }

//         string GetSiteName(IDocument document, IHtmlCollection<IElement> metas)
//         {
//             return GetMetaValue(document, metas, "og:site_name");
//         }

//         string GetPageTitle(IDocument document, IHtmlCollection<IElement> metas)
//         {
//             return GetMetaValue(document, metas, "og:title")
//             ?? document.QuerySelector("head title")?.InnerHtml;
//         }

//         string GetPageDescription(IDocument document, IHtmlCollection<IElement> metas)
//         {
//             return GetMetaValue(document, metas, "og:description");
//         }

//         string GetPageImage(IDocument document, IHtmlCollection<IElement> metas)
//         {
//             return GetMetaValue(document, metas, "og:image");
//         }

//         string GetSiteIcon(IDocument document)
//         {
//             var link = document.QuerySelector("link[rel=apple-touch-icon]");
//             return link?.Attributes["href"]?.Value;
//         }

//         public async Task<CommunityBuyLinkPostModel> Parse(string url)
//         {
//             var uri = new Uri(url);
//             var document = await context.OpenAsync(url);
//             var metas = document.QuerySelectorAll("meta");
//             string title = GetPageTitle(document, metas);
//             if (title != null)
//             {
//                 var img = GetPageImage(document, metas);
//                 var buyLink = new CommunityBuyLinkPostModel
//                 {
//                     Name = title.Substring(0, Math.Min(256, title.Length)),
//                     Summary = GetPageDescription(document, metas) ?? title,
//                     ProductIcons = img != null ? new[] { img } : null,
//                     Url = url,
//                     SiteName = GetSiteName(document, metas),
//                     SiteIcon = GetSiteIcon(document)
//                 };
//                 if (parsers.ContainsKey(uri.Host))
//                 {
//                     parsers[uri.Host].Populate(buyLink, document);
//                 }
//                 return buyLink;
//             }
//             return null;
//         }
//     }
// }