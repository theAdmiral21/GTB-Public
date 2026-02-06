using Primitives.Common.State.Enums;

namespace Game.Application.Scenes.DataStructures
{
    public struct GameStateChangeRequest
    {
        public readonly GameState TargetState;

        public GameStateChangeRequest(GameState target)
        {
            TargetState = target;
        }
    }
}