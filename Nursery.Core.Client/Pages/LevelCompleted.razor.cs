﻿using Microsoft.AspNetCore.Components;
using Sudoku.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Core.Pages
{
    public partial class LevelCompleted
    {
        [Inject] SudokuEngine engine { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        bool render = true;
        public void Continue()
        {
            engine.Level++;
            render = false;
            Navigation.NavigateTo("/Game");
        }

        protected override bool ShouldRender()
        {
            return render;
        }
        void GoBackHome()
        {
            engine.StopGame();
            Navigation.NavigateTo("");
        }
    }
}
