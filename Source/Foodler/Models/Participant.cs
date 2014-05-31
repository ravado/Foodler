using System;
using System.Windows.Media.Imaging;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    public class Participant : BaseModel, IParticipant
    {
        #region Fields

        private Guid _id;
        private string _name;
        private BitmapImage _avatar;

        #endregion

        #region Properties
        
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        public BitmapImage Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        public Participant() { }

        public Participant(string name)
            : this()
        {
            Name = name;
        }
    }
}
