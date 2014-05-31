using System;
using System.Data.Linq.Mapping;
using System.Windows.Media.Imaging;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    [Table(Name = "Participants")]
    public class Participant : BaseModel, IParticipant
    {
        #region Fields

        private Guid _id;
        private string _name;
        private bool _isUserContact;
        private object _avatar;

        #endregion

        #region Properties

        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }
        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }
        [Column(DbType = "varbinary")]
        public object Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
                NotifyPropertyChanged();
            }
        }
        [Column]
        public bool IsUserContact
        {
            get { return _isUserContact; }
            set
            {
                _isUserContact = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public Participant() { }

        public Participant(Guid id, string name, bool isUserContact = false, WriteableBitmap avatar = null) : this()
        {
            Id = id;
            Name = name;
            IsUserContact = isUserContact;
            Avatar = avatar;
        }

        public Participant(IParticipant participant) : this()
        {
            Id = participant.Id;
            Name = participant.Name;
            IsUserContact = participant.IsUserContact;
            Avatar = participant.Avatar;
        }

        #endregion
    }
}
