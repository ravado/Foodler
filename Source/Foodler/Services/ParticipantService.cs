using System;
using System.Collections.Generic;
using System.Linq;
using Foodler.Common.Contracts;
using Foodler.DB;
using Foodler.Models;
using Microsoft.Phone.UserData;

namespace Foodler.Services
{
    public sealed class ParticipantService : BaseService
    {
        public EventHandler<IEnumerable<IParticipant>> ContactsLoaded { get; set; }

        public ParticipantService() {}

        public void LoadContactsToDbAsync()
        {
            var cons = new Contacts();
            
            cons.SearchCompleted += ContactsSearchCompleted;
            cons.SearchAsync(String.Empty, FilterKind.None, null);
        }

        public IEnumerable<IParticipant> GetAllParticipants()
        {
            using (var context = GetContext())
            {
                return context.Participants.ToArray();
            }
        }
        
        private void ContactsSearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            var contacts = e.Results.Select(c => new Participant(Guid.Empty, c.DisplayName, true, null)).ToArray();

            SyncContacts(contacts);
        }

        private void SyncContacts(IEnumerable<IParticipant> participantFromContacts)
        {
            using (var context = GetContext())
            {
                var contacts = context.Participants.Where(p => p.IsUserContact);
                context.Participants.DeleteAllOnSubmit(contacts);
                context.Participants.InsertAllOnSubmit(participantFromContacts.Select(p => new Participant(p)));
                context.SubmitChanges();
                OnContactLoaded(context.Participants.ToArray());
            }
        }

        private void OnContactLoaded(IEnumerable<IParticipant> participants)
        {
            if(ContactsLoaded != null)
                ContactsLoaded(this, participants);
        }
    }
}
