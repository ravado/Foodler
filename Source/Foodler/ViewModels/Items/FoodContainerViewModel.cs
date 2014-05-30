using System.Collections.Generic;
using System.Linq;

namespace Foodler.ViewModels.Items
{
    public class FoodContainerViewModel
    {
        public FoodViewModel Food { get; set; }
        public IEnumerable<ParticipantViewModel> Participants { get; set; }

        public int ParticipantCount
        {
            get { return Participants == null ? 0 : Participants.Count(); }
        }

        public FoodContainerViewModel() {}

        public FoodContainerViewModel(FoodViewModel food, IEnumerable<ParticipantViewModel> participants)
        {
            Food = food;
            Participants = participants;
        }
    }
}
