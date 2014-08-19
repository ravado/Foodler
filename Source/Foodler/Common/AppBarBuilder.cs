using System;
using System.Windows;
using Foodler.Models;
using Foodler.Pages;
using Foodler.Resources;
using Microsoft.Phone.Shell;

namespace Foodler.Common
{
    public class AppBarBuilder
    {
        public static class Food
        {
            private static ApplicationBar _food;
            public static ApplicationBar GetAppBarPivotFood(MainPage page)
            {
                if (_food == null)
                {
                    const UriKind uriKind = UriKind.RelativeOrAbsolute;

                    _food = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                    var btnAdd = new ApplicationBarIconButton(new Uri(Images.AppBarAdd, uriKind));
                    btnAdd.Text = AppBarLabels.MainPage_FoodAdd;
                    btnAdd.Click += page.BtnAddFood_OnClick;
                    _food.Buttons.Add(btnAdd);

                    var btnEdit = new ApplicationBarIconButton(new Uri(Images.AppBarListReorder, uriKind));
                    btnEdit.Text = AppBarLabels.MainPage_FoodExpandAll;
                    btnEdit.Click += page.BtnToggleFoodEditMode_OnClick;
                    _food.Buttons.Add(btnEdit);

                    var btnNext = new ApplicationBarIconButton(new Uri(Images.AppBarNext, uriKind));
                    btnNext.Text = AppBarLabels.MainPage_FoodNext;
                    btnNext.Click += page.BtnGoSumTab_OnClick;
                    _food.Buttons.Add(btnNext);

                    SetCommonItems(_food, page.OpenTutorial_Onclick, page.MenuOpenAbout_OnClick);
                }
                return _food;
            }
            public static void ShowExpandAll()
            {
                var btn = _food.Buttons[1] as ApplicationBarIconButton;

                if (btn == null)
                    return;

                btn.IconUri = new Uri(Images.AppBarListReorder, UriKind.RelativeOrAbsolute);
                btn.Text = AppBarLabels.MainPage_FoodExpandAll;
            }

            public static void ShowCollapseAll()
            {
                var btn = _food.Buttons[1] as ApplicationBarIconButton;

                if (btn == null)
                    return;

                btn.IconUri = new Uri(Images.AppBarList, UriKind.RelativeOrAbsolute);
                btn.Text = AppBarLabels.MainPage_FoodCollapseAll;
            }
        }

        public static class Participants
        {
            private static ApplicationBar _participants;
            public static ApplicationBar GetAppBarPivotParticipants(MainPage page)
            {
                if (_participants == null)
                {
                    _participants = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                    var btnAdd = new ApplicationBarIconButton(new Uri(Images.AppBarAdd, UriKind.RelativeOrAbsolute));
                    btnAdd.Text = AppBarLabels.MainPage_ParticipantsAdd;
                    btnAdd.Click += page.BtnAddParticipants_OnClick;
                    _participants.Buttons.Add(btnAdd);

                    var btnNext =
                        new ApplicationBarIconButton(new Uri(Images.AppBarNext, UriKind.RelativeOrAbsolute));
                    btnNext.Text = AppBarLabels.MainPage_ParticipantsNext;
                    btnNext.Click += page.BtnGoFoodTab_OnClick;
                    _participants.Buttons.Add(btnNext);

                    SetCommonItems(_participants, page.OpenTutorial_Onclick, page.MenuOpenAbout_OnClick);
                }
                return _participants;
            }

        }

        public static class Sum
        {
            private static ApplicationBar _sum;

            public static ApplicationBar GetAppBarPivotSum(MainPage page)
            {
                if (_sum == null)
                {
                    _sum = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                    var btnExpandCollapse =
                         new ApplicationBarIconButton(new Uri(Images.AppBarListReorder, UriKind.RelativeOrAbsolute));
                    btnExpandCollapse.Text = AppBarLabels.MainPage_SumExpandAll;
                    btnExpandCollapse.Click += page.BtnExpandCollapseAll_OnClick;
                    _sum.Buttons.Add(btnExpandCollapse);

                    var btnRestart =
                       new ApplicationBarIconButton(new Uri(Images.AppBarRefresh, UriKind.RelativeOrAbsolute));
                    btnRestart.Text = AppBarLabels.MainPage_SumRestart;
                    btnRestart.Click += page.BtnDone_OnClick;
                    _sum.Buttons.Add(btnRestart);

                    SetCommonItems(_sum, page.OpenTutorial_Onclick, page.MenuOpenAbout_OnClick);
                }
                return _sum;
            }
        }

        public static class AddParticipants
        {
            private static ApplicationBar _addParticipantsBar;

            public static ApplicationBar GetAppBar(AddParticipantPage page)
            {
                //if (_addParticipantsBar == null) //TODO: figure out how does it influence on button actions
                //{
                _addParticipantsBar = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                var btnDone =
                     new ApplicationBarIconButton(new Uri(Images.AppBarDone, UriKind.RelativeOrAbsolute));
                btnDone.Text = AppBarLabels.AddParticipantsPage_AvailableDone;
                btnDone.Click += page.AcceptChangesBtn_OnClick;
                _addParticipantsBar.Buttons.Add(btnDone);

                var btnAnonymous =
                   new ApplicationBarIconButton(new Uri(Images.AppBarAnonymous, UriKind.RelativeOrAbsolute));
                btnAnonymous.Text = AppBarLabels.AddParticipantsPage_AvailableAnonymous;
                btnAnonymous.Click += page.AddAnonymousBtn_OnClick;
                _addParticipantsBar.Buttons.Add(btnAnonymous);

                var btnAddMe =
                      new ApplicationBarIconButton(new Uri(Images.AppBarUserAdd, UriKind.RelativeOrAbsolute));
                btnAddMe.Text = AppBarLabels.AddParticipantsPage_AddMe;
                btnAddMe.Click += page.AddMeBtn_OnClick;
                _addParticipantsBar.Buttons.Add(btnAddMe);

                var btnResetAll =
                   new ApplicationBarIconButton(new Uri(Images.AppBarReset, UriKind.RelativeOrAbsolute));
                btnResetAll.Text = AppBarLabels.AddParticipantsPage_AvailableUnselectAll;
                btnResetAll.Click += page.ClearAllSelectedBtn_OnClick;

                _addParticipantsBar.Buttons.Add(btnResetAll);
                //}
                return _addParticipantsBar;
            }
        }

        public static class AddFood
        {
            private static ApplicationBar _addFoodBar;

            public static ApplicationBar GetAppBar(AddFoodPage page)
            {
                //if (_addFoodBar == null)
                //{
                _addFoodBar = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default };

                var btnDone =
                     new ApplicationBarIconButton(new Uri(Images.AppBarDone, UriKind.RelativeOrAbsolute));
                btnDone.Text = AppBarLabels.AddFoodPage_Done;
                btnDone.Click += page.DoneButtonClick;
                _addFoodBar.Buttons.Add(btnDone);

                //TODO: for the next version
                //var btnAteRange =
                //   new ApplicationBarIconButton(new Uri(Images.AppBarAteRangeOn, UriKind.RelativeOrAbsolute));
                //btnAteRange.Text = AppBarLabels.AddFoodPage_AteRangeOn;
                //btnAteRange.IsEnabled = false;
                //btnAteRange.Click += page.AppBarAddAteRange_OnClick;
                //_addFoodBar.Buttons.Add(btnAteRange);

                var btnSelectAll =
                   new ApplicationBarIconButton(new Uri(Images.AppBarSelectAll, UriKind.RelativeOrAbsolute));
                btnSelectAll.Text = AppBarLabels.AddFoodPage_SelectAll;
                btnSelectAll.Click += page.AppBarSelectAll_OnClick;
                _addFoodBar.Buttons.Add(btnSelectAll);

                var btnResetAll =
                   new ApplicationBarIconButton(new Uri(Images.AppBarReset, UriKind.RelativeOrAbsolute));
                btnResetAll.Text = AppBarLabels.AddFoodPage_ResetAll;
                btnResetAll.Click += page.AppBarResetAll_OnClick;
                _addFoodBar.Buttons.Add(btnResetAll);
                //}
                return _addFoodBar;
            }
        }

        public static void SetCommonItems(ApplicationBar appBar, EventHandler tutorialHandler, EventHandler aboutHandler)
        {
            var tutorialMenu = new ApplicationBarMenuItem(AppBarLabels.MainPage_ShowTutorial);
            tutorialMenu.Click += tutorialHandler;
            appBar.MenuItems.Add(tutorialMenu);

            var aboutMenu = new ApplicationBarMenuItem(AppBarLabels.MainPage_ShowAbout);
            aboutMenu.Click += aboutHandler;
            appBar.MenuItems.Add(aboutMenu);
        }
    }
}
