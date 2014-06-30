using System;
using System.Collections.Generic;

namespace Foodler.Common.Contracts
{
    public interface IParticipant
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool IsUserContact { get; set; }
        byte[] Avatar { get; set; }
        int ParticipantAteCoefficient { get; set; }
        IList<IFood> EatenFood { get; set; }
    }
}
