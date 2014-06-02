using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Foodler.Common;
using Foodler.Common.Contracts;
using Foodler.Models;
using Foodler.Resources;
using Foodler.ViewModels.Common;
using Foodler.ViewModels.Items;

namespace Foodler.ViewModels
{
    public class AddFoodViewModel : BaseViewModel
    {
        private IFood _food;
        public IFood Food
        {
            get { return _food; }
            set
            {
                _food = value;
                NotifyPropertyChanged();
            }
        }
        
        public ObservableCollection<IParticipant> Participants { get; set; }
        public ObservableCollection<IParticipant> SelectedParticipants { get; set; }
        public ObservableCollection<IFood> AvailableFood { get; set; }
        //public IFood SelectedFood { get; set; }
        

        public AddFoodViewModel()
        {
            Participants = new ObservableCollection<IParticipant>();
            SelectedParticipants = new ObservableCollection<IParticipant>();
            AvailableFood = new ObservableCollection<IFood>();
            FillFood();
            Food = AvailableFood[0];
        }

        public void Initialize(IEnumerable<IParticipant> participants, IFood food)
        {
            if (food != null)
                Food = food;

            if (Participants != null && participants != null)
            {
                Participants.Clear();
                foreach (var p in participants)
                {
                    Participants.Add(p);
                }
            }
        }

        private void FillFood()
        {
            AvailableFood.Clear();
            AvailableFood.Add(new Food("Пироженое", GetImage(Images.FoodCake)));
            AvailableFood.Add(new Food("Пиво", GetImage(Images.FoodBeer)));
            AvailableFood.Add(new Food("Шампанское", GetImage(Images.FoodChampain)));
            AvailableFood.Add(new Food("Коктейль", GetImage(Images.FoodCocktail)));
            AvailableFood.Add(new Food("Кофе", GetImage(Images.FoodCoffe)));
            AvailableFood.Add(new Food("Фаст фуд", GetImage(Images.FoodFastFood)));
            AvailableFood.Add(new Food("Фрукты", GetImage(Images.FoodFruit)));
            AvailableFood.Add(new Food("Здоровая пища", GetImage(Images.FoodHealthyFood)));
            AvailableFood.Add(new Food("Мороженое", GetImage(Images.FoodIceCream)));
            AvailableFood.Add(new Food("Пицца", GetImage(Images.FoodPizza)));
            AvailableFood.Add(new Food("Напитки", GetImage(Images.FoodRefreshments)));
            AvailableFood.Add(new Food("Морепродукты", GetImage(Images.FoodSeafood)));
            AvailableFood.Add(new Food("Закуски", GetImage(Images.FoodSnack)));
            AvailableFood.Add(new Food("Первые блюда", GetImage(Images.FoodSoup)));
            AvailableFood.Add(new Food("Вино", GetImage(Images.FoodVine)));
            AvailableFood.Add(new Food("Суши", GetImage(Images.FoodSushi)));
        }

        public static BitmapImage GetImage(string url)
        {
            return new BitmapImage(new Uri(url, UriKind.Relative));
        }

        public FoodContainerViewModel GetFoodContainer()
        {
            var container = new FoodContainerViewModel(Food, SelectedParticipants);
            return container;
        }
    }
}
