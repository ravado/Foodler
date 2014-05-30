using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;

namespace Foodler.ViewModels
{
    public class AddFoodViewModel : BaseViewModel
    {
        private FoodViewModel _food;
        public FoodViewModel Food {
            get { return _food; }
            set
            {
                _food = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ParticipantViewModel> Participants { get; set; }
        public ObservableCollection<ParticipantViewModel> SelectedParticipants { get; set; }
        
        public AddFoodViewModel()
        {
            Participants = new ObservableCollection<ParticipantViewModel>();
            SelectedParticipants = new ObservableCollection<ParticipantViewModel>();
            Food = new FoodViewModel();
        }

        public void Initialize(IEnumerable<ParticipantViewModel> participants, FoodViewModel food)
        {
            if (food != null)
                Food = food;

            if (Participants != null)
            {
                Participants.Clear();
                foreach (var p in participants)
                {
                    Participants.Add(p);
                }
            }
        }

        public FoodContainerViewModel GetFoodContainer()
        {
            var container = new FoodContainerViewModel(Food, SelectedParticipants);
            return container;
        }
    }
}
