using System.Linq;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Extensions = Foodler.Common.Extensions;

namespace Foodler.ViewModels
{
    public class AddFoodViewModel : BaseViewModel
    {
        #region Fields
        
        private IFood _food;
        private FoodItemViewModel _selectedFoodItem;
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
        public FoodItemViewModel SelectedFoodItem
        {
            get { return _selectedFoodItem; }
            set
            {
                _selectedFoodItem = value;
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
        public ObservableCollection<FoodItemViewModel> AvailableFood { get; set; }

        public string FoodLabel { get; set; }
        public string PriceLabel { get; set; }
        public string ParticipantsLabel { get; set; }
        public string ChooseYourFoodLabel { get; set; }
        #endregion

        public AddFoodViewModel()
        {
            _isCanceled = false;
            Participants = new ObservableCollection<IParticipant>();
            SelectedParticipants = new ObservableCollection<IParticipant>();
            AvailableFood = new ObservableCollection<FoodItemViewModel>();
            FillFood();
            Food = new Food();
            SelectedFoodItem = AvailableFood[0];
            InitLabels();
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
                        SelectedFoodItem = AvailableFood.FirstOrDefault(f => f.Name == Food.Name) ?? AvailableFood.First();
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

            //TODO: please do it more optimal
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Pizza, Images.FoodPizza.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_FastFood, Images.FoodFastFood.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_FirstDishes, Images.FoodSoup.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Sushi, Images.FoodSushi.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_HealthyFood, Images.FoodHealthyFood.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Seafood, Images.FoodSeafood.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Snacks, Images.FoodSnack.GetImage()));

            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Beer, Images.FoodBeer.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Champagne, Images.FoodChampain.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Cocktail, Images.FoodCocktail.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Coffe, Images.FoodCoffe.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Refreshments, Images.FoodRefreshments.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Vine, Images.FoodVine.GetImage()));

            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Cake, Images.FoodCake.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_Fruit, Images.FoodFruit.GetImage()));
            AvailableFood.Add(new FoodItemViewModel(UILabels.FoodName_IceCream, Images.FoodIceCream.GetImage()));
            
        }

        // ReSharper disable once ReplaceWithSingleAssignment.True
        private bool Validate()
        {
            var valid = true;

            if (Food.Price <= 0 || SelectedParticipants.Count == 0)
                valid = false;

            return valid;
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
            Food.Name = SelectedFoodItem.Name;
            (Food as Food).Icon = SelectedFoodItem.Icon; //TODO: bad idea
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
            Food = new Food();
            SelectedFoodItem = AvailableFood[0];
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

        public void InitLabels()
        {
            FoodLabel = UILabels.AddFoodPage_FoodTitle;
            PriceLabel = UILabels.AddFoodPage_PriceTitle;
            ParticipantsLabel = UILabels.AddFoodPage_ParticipantsTitle;
            ChooseYourFoodLabel = UILabels.AddFoodPage_ChooseFoodHeader;
        }
    }
}
