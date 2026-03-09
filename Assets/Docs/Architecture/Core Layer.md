# Core Layer
The `Core` layer is pure C#. It doesn't rely on anything else. `Core` provides domain logic, state machines, math, rules, constraints, and interfaces (ports). The `Core` layer is where the *rules of my game live* and is completely independent of how the game is built, rendered, or run.

`Core` is responsible for:
1. Game rules and invariants
2. Pure state and state transitions
3. Pure logic (no side effects)
4. Vocabulary of the game
5. Contracts (interfaces) sometimes

## Rules and invariants
When I say rules I mean things like:
- A player can only jump if they have jumps remaining.
- Falling off of the world "kills" the player.
- When the player touches a dairy delight, they get points.
- A player sprints only when the sprint button is held and the player is grounded.

So `Core` defines the conditions and results of actions for the game. Notice how all of those statements gloss over the nitty gritty, of what button was pressed, defining out of bounds, determining contact with a dairy delight etc. This means that `Core` does not care *how* a rule is implemented, just that the rule is followed.

The rule of thumb: if violating a rule would create a bug, then that rule belongs in `Core`.

## Pure state and state transitions
`Core` owns **state**, but NOT **time** or **frames**. For example:
- The player's physical state (grounded, airborne, sliding)
- Health
- Stamina
- Facing direction

`Core` does NOT:
- Check `Time.deltatime`
- Run `Update()`
- Apply forces

Those are all Unity interactions and would be handled in the Unity layer.

Remember: `Core` is **reactive** NOT **proactive**. It must be told to change state.

## Pure logic
Simply put, `Core` takes input data and returns results. `Core` does not know who requested the data and doesn't care.
Example:
```c#
if (!state.CanJump()) return JumpResult.Failed;
return JumpResult.Success(newState);
```
`Core` does not provide functionality for:
- Logging
- Animations
- Audio
- Unity types
- etc

## Vocabulary of the game
`Core` defines the **language** of your game. Instead of 
```c#
bool CanDoThing;
```
`Core` would have:
```c#
bool IsGrounded;
bool IsSwinging;
bool IsFalling;
```
Good `Core` code reads like an actual sentence: "If the player is grounded, they can jump."
## Contracts
If something is essential to the rules of the game, but it is implemented some where else, `Core` provides the contract/interface for that object.
For example:
- `ITimeProvider`
- `IRandomProvider`
- `IInputCommand`
A different layer would implement the interface and `Core` would define it.

# What `Core` is NOT for:
- NO Unity types, if it needs Unity, it isn't `Core`.
- NO "when" logic, `Core` doesn't handle when to update, that belongs in `Application`/`Unity`
- NO presentation, `Core` doesn't render anything and doesn't play audio.
- NO scene knowledge, `Core` is agnostic of the scene and its contents.

# Example:
Say we were to implement jumping. What would the `Core` layer contain?

Rules:
- The player must have a jump available.
- Jumps are reset when the player touches the ground.
- Every time the player jumps, their available jumps is reduced by 1.

State changes:
- Grounded to airborne.
- Airborne to grounded.
- Falling to rising. (If you wanted to be super specific)

Cost:
- Available jumps is reduced by 1.
---
The `Application` layer would handle:

- Incoming jump request from player
- "Ask" `Core` if the player can jump
- If "allowed", perform jump calculations

---
Finally, the `Unity` layer would:
- Detect button presses
- Send request to `Application` layer
- Apply physical jump to player
- Play jump animation
- Play jump sound
