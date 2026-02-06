using System;
using System.Collections.Generic;
using System.Linq;

using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;

namespace PlayerController.Application.Movement
{
    public sealed class ActionDispatcher
    {
        private List<IActionResult> _actionResults = new List<IActionResult>();
        private Dictionary<Type, IDispatchRequest> _dispatchers;
        public ActionDispatcher(IEnumerable<IDispatchRequest> dispatchers)
        {
            _dispatchers = dispatchers.ToDictionary(
                d => d.DispatchType,
                d => d
            );
        }
        public List<IActionResult> DispatchActions(PlayerActionContext context)
        {
            _actionResults.Clear();
            foreach (IActionRequest req in context.CurrentRequests)
            {
                if (_dispatchers.TryGetValue(req.GetType(), out var dispatcher))
                {
                    IActionResult result = dispatcher.DispatchUntyped(
                       req,
                       context.Facts,
                       context.CurrentGameState,
                       context.InputValues,
                       ref context.RuleState
                   );
                    _actionResults.Add(result);
                }
            }
            return _actionResults;
        }
    }

}