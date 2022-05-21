using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Sudoku.Core.Models;
using Sudoku.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Core.Pages
{
    public partial class Index:IDisposable
    {
        public string Title => "Sudoku";
        public bool focused = false;
        public int row;
        public int col;
        [Inject] public SudokuEngine engine { get; set; }
        [Inject ] public NavigationManager Navigation { get; set; }
        public void Focused(int _row, int _col)
        {
            focused = true;
            row = _row;
            col = _col;
        }
        public void Keypress(int key)
        {
            if (focused)
            {
                if(engine.GameState==GameState.Playing)
                {
                    if (key == 0)
                        engine[row, col] = null;
                    else
                    {
                        engine[row, col] = key;

                        focused = false;
                    }
                } 
            }
        }

        int count;
        bool disposed;
        bool confirm = false;

        void LevelCompleted(object sender, int level)
        {
            Navigation.NavigateTo("/Completed");
        }
        void LevelTimeOut(object sender, int level)
        {
            Navigation.NavigateTo("/GameOver");
        }

        protected override void OnInitialized()
        {
            engine.OnLevelCompleted += LevelCompleted;
            engine.OnLevelTimeOut += LevelTimeOut;
            Task.Run(async () =>
            {
                while (!disposed)
                {
                    count++;
                    await InvokeAsync(StateHasChanged);
                    await Task.Delay(1000);
                }
            });
            base.OnInitialized();
        }
        public void Dispose()
        {
            engine.OnLevelCompleted -= LevelCompleted;
            engine.OnLevelTimeOut -= LevelTimeOut;
            disposed = true;
        }
        void Reload()
        {
            engine.Reload();
        }
        void PlayPause()
        {
            engine.ToggleState();
        }
        void TryEnd()
        {
            confirm = true;
            wait=true;
            engine.ToggleState();
        }
        void End()
        {
            engine.StopGame();
            Navigation.NavigateTo("");
        }
        bool wait;
        void DonNotEnd()
        {
            wait = false;
            //StateHasChanged();
            //await Task.Delay(400);
            //StateHasChanged();
            confirm = false;
            
            engine.Play();
        }
       
    }
    
}
