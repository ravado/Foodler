﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foodler.Annotations;

namespace Expression.Blend.SampleData.SampleDataSource
{
	using System; 

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class SampleDataSource { }
#else

	public class SampleDataSource : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		public SampleDataSource()
		{
			try
			{
				System.Uri resourceUri = new System.Uri("/Foodler;component/SampleData/SampleDataSource/SampleDataSource.xaml", System.UriKind.Relative);
				if (System.Windows.Application.GetResourceStream(resourceUri) != null)
				{
					System.Windows.Application.LoadComponent(this, resourceUri);
				}
			}
			catch (System.Exception)
			{
			}
		}

		private Participants _Participants = new Participants();

		public Participants Participants
		{
			get
			{
				return this._Participants;
			}
		}

		private AvaibleParticipants _AvaibleParticipants = new AvaibleParticipants();

		public AvaibleParticipants AvaibleParticipants
		{
			get
			{
				return this._AvaibleParticipants;
			}
		}

		private ChosenParticipants _ChosenParticipants = new ChosenParticipants();

		public ChosenParticipants ChosenParticipants
		{
			get
			{
				return this._ChosenParticipants;
			}
		}

		private Food _Food = new Food();

		public Food Food
		{
			get
			{
				return this._Food;
			}

			set
			{
				if (this._Food != value)
				{
					this._Food = value;
					this.OnPropertyChanged("Food");
				}
			}
		}

		private ParticipantContainers _ParticipantContainers = new ParticipantContainers();

		public ParticipantContainers ParticipantContainers
		{
			get
			{
				return this._ParticipantContainers;
			}
		}

		private AnonymousMaleParticipantList _AnonymousMaleParticipantList = new AnonymousMaleParticipantList();

		public AnonymousMaleParticipantList AnonymousMaleParticipantList
		{
			get
			{
				return this._AnonymousMaleParticipantList;
			}
		}

		private AnonymousFemaleParticipantList _AnonymousFemaleParticipantList = new AnonymousFemaleParticipantList();

	    public AnonymousFemaleParticipantList AnonymousFemaleParticipantList
	    {
	        get { return this._AnonymousFemaleParticipantList; }
	    }

	    private AvailableFood _AvailableFood = new AvailableFood();

		public AvailableFood AvailableFood
		{
			get
			{
				return this._AvailableFood;
			}
		}

		private string _FoodTotalCost = string.Empty;

		public string FoodTotalCost
		{
			get
			{
				return this._FoodTotalCost;
			}

			set
			{
				if (this._FoodTotalCost != value)
				{
					this._FoodTotalCost = value;
					this.OnPropertyChanged("FoodTotalCost");
				}
			}
		}

		private string _FoodPrice = string.Empty;

		public string FoodPrice
		{
			get
			{
				return this._FoodPrice;
			}

			set
			{
				if (this._FoodPrice != value)
				{
					this._FoodPrice = value;
					this.OnPropertyChanged("FoodPrice");
				}
			}
		}

		private string _CurrencySymbol = string.Empty;

		public string CurrencySymbol
		{
			get
			{
				return this._CurrencySymbol;
			}

			set
			{
				if (this._CurrencySymbol != value)
				{
					this._CurrencySymbol = value;
					this.OnPropertyChanged("CurrencySymbol");
				}
			}
		}
	}

	public class Participants : System.Collections.ObjectModel.ObservableCollection<ParticipantsItem>
	{ 
	}

	public class ParticipantsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}
	}

	public class AvaibleParticipants : System.Collections.ObjectModel.ObservableCollection<AvaibleParticipantsItem>
	{ 
	}

	public class AvaibleParticipantsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private System.Windows.Media.ImageSource _Avatar = null;

		public System.Windows.Media.ImageSource Avatar
		{
			get
			{
				return this._Avatar;
			}

			set
			{
				if (this._Avatar != value)
				{
					this._Avatar = value;
					this.OnPropertyChanged("Avatar");
				}
			}
		}
	}

	public class ChosenParticipants : System.Collections.ObjectModel.ObservableCollection<ChosenParticipantsItem>
	{ 
	}

	public class ChosenParticipantsItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private System.Windows.Media.ImageSource _Avatar = null;

		public System.Windows.Media.ImageSource Avatar
		{
			get
			{
				return this._Avatar;
			}

			set
			{
				if (this._Avatar != value)
				{
					this._Avatar = value;
					this.OnPropertyChanged("Avatar");
				}
			}
		}
	}

	public class Food : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private double _Price = 0;

		public double Price
		{
			get
			{
				return this._Price;
			}

			set
			{
				if (this._Price != value)
				{
					this._Price = value;
					this.OnPropertyChanged("Price");
				}
			}
		}
	}

	public class ParticipantContainers : System.Collections.ObjectModel.ObservableCollection<ParticipantContainersItem>
	{ 
	}

	public class ParticipantContainersItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private Participant _Participant = new Participant();

		public Participant Participant
		{
			get
			{
				return this._Participant;
			}

			set
			{
				if (this._Participant != value)
				{
					this._Participant = value;
					this.OnPropertyChanged("Participant");
				}
			}
		}

		private Food1 _Food = new Food1();

		public Food1 Food
		{
			get
			{
				return this._Food;
			}
		}

		private double _FoodCount = 0;

		public double FoodCount
		{
			get
			{
				return this._FoodCount;
			}

			set
			{
				if (this._FoodCount != value)
				{
					this._FoodCount = value;
					this.OnPropertyChanged("FoodCount");
				}
			}
		}

		private string _TotalCost = string.Empty;

		public string TotalCost
		{
			get
			{
				return this._TotalCost;
			}

			set
			{
				if (this._TotalCost != value)
				{
					this._TotalCost = value;
					this.OnPropertyChanged("TotalCost");
				}
			}
		}
	}

	public class Participant : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}
	}

	public class Food1 : System.Collections.ObjectModel.ObservableCollection<FoodItem>
	{ 
	}

	public class FoodItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private string _Price = string.Empty;

		public string Price
		{
			get
			{
				return this._Price;
			}

			set
			{
				if (this._Price != value)
				{
					this._Price = value;
					this.OnPropertyChanged("Price");
				}
			}
		}
	}

	public class AnonymousMaleParticipantList : System.Collections.ObjectModel.ObservableCollection<AnonymousMaleParticipantListItem>
	{ 
	}

    public class AnonymousMaleParticipantListItem : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AvailableFood : System.Collections.ObjectModel.ObservableCollection<AvailableFoodItem>
	{ 
	}

	public class AvailableFoodItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private System.Windows.Media.ImageSource _Avatar = null;

		public System.Windows.Media.ImageSource Avatar
		{
			get
			{
				return this._Avatar;
			}

			set
			{
				if (this._Avatar != value)
				{
					this._Avatar = value;
					this.OnPropertyChanged("Avatar");
				}
			}
		}
	}

	public class AnonymousFemaleParticipantList : System.Collections.ObjectModel.ObservableCollection<AnonymousFemaleParticipantListItem>
	{ 
	}

	public class AnonymousFemaleParticipantListItem : System.ComponentModel.INotifyPropertyChanged
	{
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		private System.Windows.Media.ImageSource _Avatar = null;

		public System.Windows.Media.ImageSource Avatar
		{
			get
			{
				return this._Avatar;
			}

			set
			{
				if (this._Avatar != value)
				{
					this._Avatar = value;
					this.OnPropertyChanged("Avatar");
				}
			}
		}

		private string _Name = string.Empty;

		public string Name
		{
			get
			{
				return this._Name;
			}

			set
			{
				if (this._Name != value)
				{
					this._Name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}
	}
#endif
}
