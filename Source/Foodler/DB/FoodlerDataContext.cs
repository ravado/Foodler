using System.Data.Linq;
using Foodler.Models;

namespace Foodler.DB
{
    public class FoodlerDataContext : DataContext
    {
        public const string CONNECTION_STRING = "Data Source=isostore:/Foodler.sdf";
        
        public Table<Participant> Participants;

        public FoodlerDataContext(string connectionString) : base(connectionString)
        {

        }
    }
}
