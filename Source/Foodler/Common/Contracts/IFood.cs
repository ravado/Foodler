using System.Collections.Generic;

namespace Foodler.Common.Contracts
{
    public interface IFood
    {
        string Name { get; set; }
        decimal Price { get; set; }
        IList<IParticipant> Eaters { get; set; }
    }
}
