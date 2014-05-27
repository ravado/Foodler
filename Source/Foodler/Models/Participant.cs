using System;
using System.Windows.Media.Imaging;

namespace Foodler.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BitmapImage Avatar { get; set; }
    }
}
