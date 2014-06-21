using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Shell;
using System.Collections.Generic;

namespace Foodler.Common
{
    public class StateManager
    {
        public const string KEY_INVOLVED_PARTICIPANTS = "involved_participants";
        public const string KEY_FOOD_CONTAINER = "food_container";

        public static IEnumerable<IParticipant> InvolvedParticipants
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_INVOLVED_PARTICIPANTS))
                {
                    var participantCell = PhoneApplicationService.Current.State[KEY_INVOLVED_PARTICIPANTS];
                    if (participantCell is IEnumerable<IParticipant>)
                        return participantCell as IEnumerable<IParticipant>;
                }

                return null;
            }
            set { PhoneApplicationService.Current.State[KEY_INVOLVED_PARTICIPANTS] = value; }
        }

        public static FoodContainerViewModel FoodContainer
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_FOOD_CONTAINER))
                {
                    var participantCell = PhoneApplicationService.Current.State[KEY_FOOD_CONTAINER];
                    if (participantCell is FoodContainerViewModel)
                        return participantCell as FoodContainerViewModel;
                }

                return null;
            }
            set { PhoneApplicationService.Current.State[KEY_FOOD_CONTAINER] = value; }
        }
    }
}
