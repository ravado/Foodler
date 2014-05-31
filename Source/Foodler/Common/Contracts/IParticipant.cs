using System;
using System.Windows.Media.Imaging;

namespace Foodler.Common.Contracts
{
    public interface IParticipant
    {
        Guid Id { get; set; }
        string Name { get; set; }
        bool IsUserContact { get; set; }
        WriteableBitmap Avatar { get; set; }
    }
}
