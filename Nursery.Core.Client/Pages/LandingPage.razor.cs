using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sudoku.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Core.Pages
{
    public partial class LandingPage
    {
        [Inject] SudokuEngine engine { get; set; }
        
        public void Continue()
        {
            //showOtherContent = true;
            if (engine.CurrentPlayerName != null)
                showOtherContent = true;
        }
        public int Level
        {
            get => level;
            set
            {

                if (value == 1)
                    level = value;
                if (value == 2)
                    level = 3;
                if (value == 3)
                    level = 5;
                if (value == 4)
                    level = 7;
                //else
                //    level = value;
                engine.Level = level;
            }

        }
        public void Clear()
        {

            engine.CurrentPlayerName = null;
            showOtherContent = false;
        }

        bool showOtherContent = false;
        int level = 1;


    }
}
