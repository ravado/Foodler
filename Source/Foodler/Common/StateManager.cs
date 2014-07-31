using Foodler.Common.Contracts;
using Foodler.ViewModels.Items;
using Microsoft.Phone.Shell;
using System.Collections.Generic;

namespace Foodler.Common
{
    public class StateManager
    {
        public const string KEY_INVOLVED_PARTICIPANTS = "involved_participants";
        public const string KEY_SELECTED_PARTICIPANTS = "add_food_selected_participants";
        public const string KEY_FOOD_CONTAINER = "food_container";
        public const string KEY_SELECTED_ANONYMOUS = "selected_anonymous";
        public const string KEY_FOOD_PRICE = "food_price";

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

        public static IEnumerable<IParticipant> SelectedParticipants
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_SELECTED_PARTICIPANTS))
                {
                    var participantCell = PhoneApplicationService.Current.State[KEY_SELECTED_PARTICIPANTS];
                    if (participantCell is IEnumerable<IParticipant>)
                        return participantCell as IEnumerable<IParticipant>;
                }

                return null;
            }
            set { PhoneApplicationService.Current.State[KEY_SELECTED_PARTICIPANTS] = value; }
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

        public static decimal FoodPrice
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_FOOD_PRICE))
                {
                    var foodPriceCell = PhoneApplicationService.Current.State[KEY_FOOD_PRICE];
                    if (foodPriceCell is decimal)
                        return (decimal)foodPriceCell;
                }

                return default(decimal);
            }
            set { PhoneApplicationService.Current.State[KEY_FOOD_PRICE] = value; }
        }

        internal static void ResetAllData()
        {
            FoodPrice = default(decimal);
            InvolvedParticipants = null;
            FoodContainer = null;
            SelectedAnonymous = null;
            SelectedParticipants = null;
        }
    }
}
