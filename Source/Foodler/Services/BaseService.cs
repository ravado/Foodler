using Foodler.DB;

namespace Foodler.Services
{
    public abstract class BaseService 
    {
        public FoodlerDataContext GetContext(string connectionString = FoodlerDataContext.CONNECTION_STRING)
        {
            return new FoodlerDataContext(connectionString);
        }
    }
}
