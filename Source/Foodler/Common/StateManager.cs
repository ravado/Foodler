using Foodler.Models;
using Microsoft.Phone.Shell;
using System.Collections.Generic;

namespace Foodler.Common
{
    public class StateManager
    {
        public const string KEY_INVOLVED_PARTICIPANTS = "involved_participants";
        public const string KEY_FOOD_PRICE = "food_price";

        public static IEnumerable<Participant> InvolvedParticipants
        {
            get
            {
                if (PhoneApplicationService.Current.State.ContainsKey(KEY_INVOLVED_PARTICIPANTS))
                {
                    var participantCell = PhoneApplicationService.Current.State[KEY_INVOLVED_PARTICIPANTS];
                    if (participantCell is IEnumerable<Participant>)
                        return participantCell as IEnumerable<Participant>;
                }

                return null;
            }
            set { PhoneApplicationService.Current.State[KEY_INVOLVED_PARTICIPANTS] = value; }
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
    }
}
