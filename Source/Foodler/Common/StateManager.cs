using Foodler.Common.Contracts;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Shell;
using System.Collections.Generic;

namespace Foodler.Common
{
    public class StateManager
    {
        public const string KEY_INVOLVED_PARTICIPANTS = "involved_participants";
        public const string KEY_FOOD_CONTAINER = "food_container";
        public const string KEY_SELECTED_ANONYMOUS = "selected_anonymous";

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

        public static IParticipant SelectedAnonymous
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_SELECTED_ANONYMOUS))
                {
                    var participantCell = PhoneApplicationService.Current.State[KEY_SELECTED_ANONYMOUS];
                    if (participantCell is IParticipant)
                        return participantCell as IParticipant;
                }

                return null;
            }
            set { PhoneApplicationService.Current.State[KEY_SELECTED_ANONYMOUS] = value; }
        }
    }
}
