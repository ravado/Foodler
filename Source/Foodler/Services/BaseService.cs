using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
