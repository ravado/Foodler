using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodler.Common.Contracts;
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

        public ObservableCollection<IParticipant> Participants { get; set; }
        public ObservableCollection<IParticipant> SelectedParticipants { get; set; }
        
        public AddFoodViewModel()
        {
            Participants = new ObservableCollection<IParticipant>();
            SelectedParticipants = new ObservableCollection<IParticipant>();
            Food = new FoodViewModel();
        }

        public void Initialize(IEnumerable<IParticipant> participants, FoodViewModel food)
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
