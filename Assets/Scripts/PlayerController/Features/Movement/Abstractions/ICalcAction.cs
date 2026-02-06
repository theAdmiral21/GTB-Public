using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Stats;

namespace PlayerController.Features.Movement.Abstractions
{
    public interface ICalcAction
    {
        public PlayerStats Stats { get; }
        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult);
    }
}