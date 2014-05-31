using System.Collections.Generic;
using System.Linq;
using Foodler.Common.Contracts;

namespace Foodler.ViewModels.Items
{
    public class FoodContainerViewModel
    {
        public FoodViewModel Food { get; set; }
        public IEnumerable<IParticipant> Participants { get; set; }

        public int ParticipantCount
        {
            get { return Participants == null ? 0 : Participants.Count(); }
        }

        public FoodContainerViewModel() {}

        public FoodContainerViewModel(FoodViewModel food, IEnumerable<IParticipant> participants)
        {
            Food = food;
            Participants = participants;
        }
    }
}
