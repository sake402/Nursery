
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using LivingThing.Core.Frameworks.Common.Data;
using LivingThing.Core.Frameworks.Common.Storage;
using LivingThing.Core.Frameworks.Common.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Nursery.Core.Client.BuyLink
{
    class CommunityBuyLinkPostModel : IDisplayable
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IconDescriptor Icon { get; set; }
    }
    public partial class Index : ILoadableViewModel
    {
        [Inject] public ILocalStorageService StorageService { get; set; }
        string url;
        List<CommunityBuyLinkPostModel> cart = new List<CommunityBuyLinkPostModel>();

        IBrowsingContext context;

        public LoadState Loading { get; set; }
        public string LoadErrorMessage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Task Load()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnInitialized()
        {
            var config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            StorageService.GetItem<List<CommunityBuyLinkPostModel>>("cart")
                .ContinueWith(t =>
                {
                    cart = t.Result ?? new List<CommunityBuyLinkPostModel>();
                    _ = InvokeAsync(StateHasChanged);
                });
            base.OnInitialized();
        }
        Task SubmitUrl()
        {
            if (!string.IsNullOrEmpty(url))
            {
                return this.Load(async () =>
                {
                    var document = await context.OpenAsync(url);
                    var metas = document.QuerySelectorAll("meta");
                    string title = null;
                    string description = null;
                    string image = null;
                    var metaTitle = metas.FirstOrDefault(m => m.Attributes.Any(a => a.Name == "property" && a.Value == "og:title"));
                    var metaDescription = metas.FirstOrDefault(m => m.Attributes.Any(a => a.Name == "property" && a.Value == "og:description"));
                    var metaImge = metas.FirstOrDefault(m => m.Attributes.Any(a => a.Name == "property" && a.Value == "og:image"));
                    title = metaTitle?.Attributes?.SingleOrDefault(a => a.Name == "content")?.Value;
                    description = metaDescription?.Attributes?.SingleOrDefault(a => a.Name == "content")?.Value;
                    image = metaImge?.Attributes?.SingleOrDefault(a => a.Name == "content")?.Value;
                    if (title == null)
                    {
                        title = document.QuerySelector("head title").InnerHtml;
                    }
                    var buyLink = new CommunityBuyLinkPostModel
                    {
                        Title = title,
                        Description = description,
                        Icon = image,
                        Url = url
                    };
                    cart.Add(buyLink);
                    url = null;
                    await StorageService.SetItem("cart", cart);
                }, true, (pn) => PropertyChanged?.Invoke(this, pn));
            }
            return Task.CompletedTask;
        }
    }
}