using System;
using Foodler.Pages;
using Microsoft.Phone.Shell;

namespace Foodler.Common
{
    public static class AppBarBuilder
    {
        private static ApplicationBar _participants;
        private static ApplicationBar _food;
        private static ApplicationBar _sum;

        public static ApplicationBar GetAppBarPivotParticipants(MainPage page)
        {
            if (_participants == null)
            {
                _participants = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                var btnAdd = new ApplicationBarIconButton(new Uri("/Assets/AppBar/add.png", UriKind.RelativeOrAbsolute));
                btnAdd.Text = "add";
                btnAdd.Click += page.BtnAddParticipants_OnClick;
                _participants.Buttons.Add(btnAdd);

                var btnNext =
                    new ApplicationBarIconButton(new Uri("/Assets/AppBar/next.png", UriKind.RelativeOrAbsolute));
                btnNext.Text = "next";
                btnNext.Click += page.BtnGoFoodTab_OnClick;
                _participants.Buttons.Add(btnNext);
            }
            return _participants;
        }

        public static ApplicationBar GetAppBarPivotFood(MainPage page)
        {
            if (_food == null)
            {
                _food = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                var btnAdd = new ApplicationBarIconButton(new Uri("/Assets/AppBar/add.png", UriKind.RelativeOrAbsolute));
                btnAdd.Text = "add";
                btnAdd.Click += page.BtnAddFood_OnClick;
                _food.Buttons.Add(btnAdd);

                var btnNext =
                    new ApplicationBarIconButton(new Uri("/Assets/AppBar/next.png", UriKind.RelativeOrAbsolute));
                btnNext.Text = "next";
                btnNext.Click += page.BtnGoSumTab_OnClick;
                _food.Buttons.Add(btnNext);
            }
            return _food;
        }

        public static ApplicationBar GetAppBarPivotSum(MainPage page)
        {
            if (_sum == null)
            {
                _sum = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                var btnRestart =
                    new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.RelativeOrAbsolute));
                btnRestart.Text = "restart";
                btnRestart.Click += page.BtnDone_OnClick;
                _sum.Buttons.Add(btnRestart);
            }
            return _sum;
        }
    }
}
