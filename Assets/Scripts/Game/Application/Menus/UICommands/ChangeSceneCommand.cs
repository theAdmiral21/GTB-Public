using System;
using Primitives.Menus.Commands;

namespace Game.Application.UI.Menus.UICommands
{
    public struct ChangeSceneCommand : IUICommand
    {
        public Type CommandType => typeof(ChangeSceneCommand);
    }
}