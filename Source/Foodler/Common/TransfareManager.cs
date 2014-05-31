using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foodler.Common.Contracts;
using Foodler.ViewModels.Items;

namespace Foodler.Common
{
    public static class TransfareManager
    {
        public static List<IParticipant> SelectedParticipants { get; set; }
        public static FoodContainerViewModel FoodContainer { get; set; }
    }
}
