using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodler.Common.Contracts;
using Foodler.Models;
using Microsoft.Phone.UserData;

namespace Foodler
{
    public sealed class ParticipantService
    {
        public EventHandler<IEnumerable<IParticipant>> ContactsLoaded { get; set; }

        public void LoadContactsAsync()
        {
            var cons = new Contacts();
            
            cons.SearchCompleted += ContactsSearchCompleted;
            cons.SearchAsync(String.Empty, FilterKind.None, null);

        }

        private void ContactsSearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            var contacts = e.Results.Select(c => new Participant(Guid.Empty, c.DisplayName, true, null));
            OnContactLoaded(contacts);
            
        }

        private void OnContactLoaded(IEnumerable<IParticipant> participants)
        {
            if(ContactsLoaded != null)
                ContactsLoaded(this, participants);
        }
    }
}
