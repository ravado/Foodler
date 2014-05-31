using Foodler.Models;
using Microsoft.Phone.Shell;
using System.Collections.Generic;

namespace Foodler.Common
{
    public class StateManager
    {
        public const string KEY_INVOLVED_PARTICIPANTS = "involved_participants";

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
    }
}
