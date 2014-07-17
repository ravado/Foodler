using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;
using Foodler.Common;
using Foodler.Resources;
using Foodler.ViewModels.Common;

namespace Foodler.ViewModels
{
    public class TutorialViewModel : BaseViewModel
    {
        private int _currentIndex = 0;
        private List<string> _tutorialImages;
        private string _currentTutorial;

        public List<string> TutorialImages
        {
            get { return _tutorialImages; }
            set { _tutorialImages = value; NotifyPropertyChanged(); }
        }

        public string CurrentTutorial
        {
            get { return _currentTutorial; }
            set { _currentTutorial = value; NotifyPropertyChanged(); }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; NotifyPropertyChanged(); }
        }

        public Action TutorialFinished;

        public TutorialViewModel()
        {
            LoadTutorialImages();
            CurrentTutorial = TutorialImages.First();
        }

        public void LoadTutorialImages()
        {
            TutorialImages = Images.Tutorials.Split(Constants.SPLITTER_CHARACTER, StringSplitOptions.None).ToList();
        }

        public void NextTutorial()
        {
            CurrentIndex++;
            if (CurrentIndex < TutorialImages.Count())
            {
                CurrentTutorial = TutorialImages[_currentIndex];
            }
            else
            {
                TutorialFinished();
            }
        }

        public void PrevTutorial()
        {
            CurrentIndex--;
            if (CurrentIndex >= 0)
            {
                CurrentTutorial = TutorialImages[_currentIndex];
            }
            else
            {
                TutorialFinished();
            }
        }
    }
}
