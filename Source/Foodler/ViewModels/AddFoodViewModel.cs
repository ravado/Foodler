using System.Linq;
using System.Threading;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Foodler.ViewModels
{
    public class AddFoodViewModel : BaseViewModel
    {
        #region Fields
        
        private IFood _food;
        private string _currencySymbol;
        private bool _isParticipantRemoving;
        private bool _isCanceled;
        private bool _ateRangeActivated;
        private FoodContainerViewModel _foodContainer;

        #endregion

        #region Properties
        
        public IFood Food
        {
            get { return _food; }
            set
            {
                _food = value;
                NotifyPropertyChanged();
            }
        }

        public string CurrencySymbol
        {
            get { return _currencySymbol; }
            set
            {
                _currencySymbol = value;
                NotifyPropertyChanged();
            }
        }

        public bool AteRangeActivated
        {
            get { return _ateRangeActivated; }
            set
            {
                _ateRangeActivated = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<IParticipant> Participants { get; set; }
        public ObservableCollection<IParticipant> SelectedParticipants { get; set; }
        public ObservableCollection<IFood> AvailableFood { get; set; }
        
        #endregion

        public AddFoodViewModel()
        {
            _isCanceled = false;
            Participants = new ObservableCollection<IParticipant>();
            SelectedParticipants = new ObservableCollection<IParticipant>();
            AvailableFood = new ObservableCollection<IFood>();
            FillFood();
            Food = AvailableFood[0];
        }

        public void Initialize(IEnumerable<IParticipant> participants, FoodContainerViewModel foodContainer, bool ignoreFood = false)
        {
            if (Participants != null && participants != null)
            {
                Participants.Clear();
                foreach (var p in participants)
                {
                    Participants.Add(new ParticipantViewModel(p));
                }
            }

            if (foodContainer != null)
            {
                _foodContainer = foodContainer;
                if (foodContainer.Food != null)
                {
                    if (!ignoreFood) // need because food now gets from list picker page and binds directly
                    {
                        Food = foodContainer.Food;
                    }
                    else
                    {
                        Food.Price = foodContainer.Food.Price;
                    }

                    if (foodContainer.Participants != null && Participants != null)
                    {
                        // move ate coefficient from selected participants to a list of available
                        foreach (var par in foodContainer.Participants)
                        {
                            var equal = Participants.FirstOrDefault(p => p.Equals(par));
                            if (equal != null)
                                equal.ParticipantAteCoefficient = par.ParticipantAteCoefficient;
                        }
                    }
                }

                if (foodContainer.Participants != null)
                {
                    var newOnes = foodContainer.Participants.ToArray();
                    SelectedParticipants.Clear();
                    foreach (var p in newOnes)
                    {
                        SelectedParticipants.Add(new ParticipantViewModel(p));
                    }   
                }

                UpdateActivatedAteCoefficientOnParticipants(AteRangeActivated);
            }

            CurrencySymbol = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
        }

        private void UpdateActivatedAteCoefficientOnParticipants(bool ateRangeActivated)
        {
            foreach (var p in SelectedParticipants.Select(sp => sp as ParticipantViewModel))
            {
                p.AteRangeActivated = ateRangeActivated;
            }
            foreach (var p in Participants.Select(sp => sp as ParticipantViewModel))
            {
                p.AteRangeActivated = ateRangeActivated;
            }
        }

        public void AddSelectedParticipantToList(IParticipant participant)
        {
            if (!SelectedParticipants.Contains(participant) && participant != null)
                SelectedParticipants.Add(participant);
        }

        public void RemoveSelectedParticipantFromList(IParticipant participantToRemove)
        {
            if (!_isParticipantRemoving)
            {
                _isParticipantRemoving = true;
                try
                {
                    SelectedParticipants.Remove(participantToRemove);
                }
                finally
                {
                    _isParticipantRemoving = false;
                }
            }
        }

        public void CancelAddingFood()
        {
            _isCanceled = true;
        }

        private void FillFood()
        {
            AvailableFood.Clear();

            AvailableFood.Add(new Food("Фаст фуд", GetImage(Images.FoodFastFood)));
            AvailableFood.Add(new Food("Первые блюда", GetImage(Images.FoodSoup)));
            AvailableFood.Add(new Food("Суши", GetImage(Images.FoodSushi)));
            AvailableFood.Add(new Food("Здоровая пища", GetImage(Images.FoodHealthyFood)));
            AvailableFood.Add(new Food("Пицца", GetImage(Images.FoodPizza)));
            AvailableFood.Add(new Food("Морепродукты", GetImage(Images.FoodSeafood)));
            AvailableFood.Add(new Food("Закуски", GetImage(Images.FoodSnack)));
            
            AvailableFood.Add(new Food("Пиво", GetImage(Images.FoodBeer)));
            AvailableFood.Add(new Food("Шампанское", GetImage(Images.FoodChampain)));
            AvailableFood.Add(new Food("Коктейль", GetImage(Images.FoodCocktail)));
            AvailableFood.Add(new Food("Кофе", GetImage(Images.FoodCoffe)));
            AvailableFood.Add(new Food("Напитки", GetImage(Images.FoodRefreshments)));
            AvailableFood.Add(new Food("Вино", GetImage(Images.FoodVine)));

            AvailableFood.Add(new Food("Пироженое", GetImage(Images.FoodCake)));
            AvailableFood.Add(new Food("Фрукты", GetImage(Images.FoodFruit)));
            AvailableFood.Add(new Food("Мороженое", GetImage(Images.FoodIceCream)));
        }

        private bool Validate()
        {
            var valid = true;

            if (Food.Price <= 0 || SelectedParticipants.Count == 0)
                valid = false;

            return valid;
        }

        //TODO: move to some common place
        public static BitmapImage GetImage(string url)
        {
            return new BitmapImage(new Uri(url, UriKind.Relative));
        }

        public FoodContainerViewModel GetFoodContainer()
        {
            if (_isCanceled) return null;
            var selectedPartisipants = SelectedParticipants;

            // to be ensure that ate coefficient is turned off
            if (!AteRangeActivated)
            {
                foreach (var p in selectedPartisipants)
                {
                    p.ParticipantAteCoefficient = 0;
                }
            }
            
            // cyclic link
            var eaters = selectedPartisipants.ToList();
            var food = Food;

            foreach (var e in eaters)
            {
                if (e.EatenFood == null)
                    e.EatenFood = new List<IFood>();

                if(!e.EatenFood.Contains(food))
                    e.EatenFood.Add(food);
            }
            food.Eaters = eaters;


            //Food.Eaters = selectedPartisipants;
            //foreach (var eater in Food.Eaters)
            //{
            //    if (eater.EatenFood == null)
            //        eater.EatenFood = new List<IFood>();

            //    if(!eater.EatenFood.ToList().Contains(Food))
            //        eater.EatenFood.ToList().Add(Food);
            //}
            //var isEditing = _foodContainer != null ? _foodContainer.IsEditing : false;
            var id = _foodContainer != null ? _foodContainer.Id : Guid.NewGuid();

            var container = new FoodContainerViewModel(id, food, new List<IParticipant>(SelectedParticipants));
            return container;
        }

        public bool IsValid
        {
            get
            {
                return Validate();    
            }
        }

        public void Reset()
        {
            SelectedParticipants.Clear();
            foreach (var participant in Participants)
            {
                participant.ParticipantAteCoefficient = 0;
            }
            Food = AvailableFood[0];
            Food.Price = 0.0m;
        }

        public void ActivateAteRange()
        {
            AteRangeActivated = true;
            UpdateActivatedAteCoefficientOnParticipants(AteRangeActivated);
        }

        public void DeactivateAteRange()
        {
            AteRangeActivated = false;
            UpdateActivatedAteCoefficientOnParticipants(AteRangeActivated);
        }

        public override void InitLabels()
        {
            throw new NotImplementedException();
        }
    }
}
