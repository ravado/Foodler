using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Models;
using Microsoft.Phone.UserData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Foodler.Services
{
    public sealed class ParticipantService : BaseService
    {
        public EventHandler<IEnumerable<IParticipant>> ContactsLoaded { get; set; }

        public ParticipantService() { }

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
            var contacts = new List<Participant>();
            var watch = new Stopwatch();
            Debug.WriteLine("Start searching...");
            watch.Start();
            foreach (var c in e.Results)
            {
                var participant = new Participant(Guid.Empty, c.DisplayName, true);
                var pic = c.GetPicture();
                if (pic != null)
                {
                    participant.Avatar = pic.ToBytes();
                }
                contacts.Add(participant);
            }
            watch.Stop();
            var str = String.Format("Stop searching. Elapsed: {0}s {1}ms", watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
            Debug.WriteLine(str);
            //var contacts = e.Results.Select(c => new Participant(Guid.Empty, c.DisplayName, true, null)).ToArray();

            SyncContacts(contacts);
        }

        private void SyncContacts(IEnumerable<IParticipant> participantFromContacts)
        {
            using (var context = GetContext())
            {
                var contacts = context.Participants.Where(p => p.IsUserContact).ToList();
                context.Participants.DeleteAllOnSubmit(contacts);
                context.Participants.InsertAllOnSubmit(participantFromContacts.Select(p => new Participant(p)));
                context.SubmitChanges();
                OnContactLoaded(context.Participants.ToArray());
            }
        }

        private void OnContactLoaded(IEnumerable<IParticipant> participants)
        {
            if (ContactsLoaded != null)
                ContactsLoaded(this, participants);
        }
    }
}
