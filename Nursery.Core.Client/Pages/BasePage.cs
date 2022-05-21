using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Core.Pages
{
    public class BasePage:ComponentBase, IAppPage
    {
        [Inject] IJSRuntime js { get; set; }
        protected override void OnInitialized()
        {
            Sudoku.Core.Page.Current = this;
            base.OnInitialized();
        }

        bool? back;
        public bool Back()
        {
            Task.Run(async () =>
            {
                back = await js.InvokeAsync<bool>("BackButtonPressed");
            });
            return back ?? true;
        }

    }
}
