using Expression.Blend.SampleData.SampleDataSource;
using System.Data.Linq;
using Foodler.Common.Contracts;

namespace Foodler.DB
{
    public class FoodlerDataContext : DataContext
    {
        public const string CONNECTION_STRING = "Data Source=isostore:/Foodler.sdf";

        public Table<IParticipant> Participants;

        public FoodlerDataContext(string connectionString) : base(connectionString)
        {

        }
    }
}
