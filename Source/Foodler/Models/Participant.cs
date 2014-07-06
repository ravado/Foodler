using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Foodler.Common.Contracts;

namespace Foodler.Models
{
    [Table(Name = "Participants")]
    public class Participant : BaseModel, IParticipant, IEquatable<Participant>
    {
        #region Fields

        private Guid _id;
        private string _name;
        private bool _isUserContact;
        private byte[] _avatar;
        private int _participantAteCoefficient;
        private IList<IFood> _eatenFood;

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
        [Column(DbType = "image")]
        public byte[] Avatar
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

        public int ParticipantAteCoefficient
        {
            get
            {
                if (_participantAteCoefficient <= 0)
                    _participantAteCoefficient = 1;

                return _participantAteCoefficient; }
            set
            {
                _participantAteCoefficient = value;
                NotifyPropertyChanged();
            }
        }

        public IList<IFood> EatenFood
        {
            get
            {
                if (_participantAteCoefficient <= 0)
                    _participantAteCoefficient = 1;

                return _eatenFood;
            }
            set
            {
                _eatenFood = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        public Participant() { }

        public Participant(Guid id, string name, bool isUserContact = false, byte[] avatar = null) : this()
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

        public override string ToString()
        {
            return String.Format("{0}, ate coeff: {1}", Name, ParticipantAteCoefficient);
        }

        public bool Equals(Participant other)
        {
            if (other != null && other.Id == Id)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Participant);
        }

        public override int GetHashCode()
        {
            var nameHash = (Name == null) ? 0 : Name.GetHashCode();

            return Id.GetHashCode() + nameHash;
        }
    }
}
