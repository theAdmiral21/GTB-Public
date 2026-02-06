using System;
using Primitives.Menus.Commands;
using Game.UI.Menus.Core.Enums;

namespace Game.Application.UI.Menus.UICommands
{
    public struct LocalNavigationCommand : IUICommand
    {
        public Type CommandType => typeof(LocalNavigationCommand);
        public readonly UIInput Input;
        public LocalNavigationCommand(UIInput input) => Input = input;

    }
}