using System.Collections.Generic;
using Game.UI.Menus.Core.Enums;

namespace Game.Core.UI.Menus.Abstractions
{
    public interface IUINavigationModel
    {
        public void Initialize(IReadOnlyList<IUIElement> elements);
        public int GetNextIndex(int currentNdx, UIInput input);
    }
}