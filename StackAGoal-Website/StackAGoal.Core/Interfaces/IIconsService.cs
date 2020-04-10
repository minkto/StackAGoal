using StackAGoal.Core.Models;
using System.Collections.Generic;

namespace StackAGoal.Core.Interfaces
{
    public interface IIconsService
    {
        IEnumerable<Icon> GetIcons();

    }
}
