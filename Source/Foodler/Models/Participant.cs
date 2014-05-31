using System;
using System.Windows.Media.Imaging;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    public class Participant:IParticipant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BitmapImage Avatar { get; set; }

        public Participant(){}

        public Participant(string name) : this()
        {
            Name = name;
        }
    }
}
