# Player Controller Architecture Reference

This document is a **living reference** for how your player controller is structured, why each layer exists, and how data flows between them. The goal is not academic purity, but **clarity, debuggability, and long-term sanity**.

---

## High-Level Philosophy

Your controller is built around a few core ideas:

* **Decisions are separate from motion**
* **Rules decide *what may happen***
* **Simulation decides *what actually happens***
* **Unity only applies results, never decides rules**

Think of the system as:

```
INPUT → REQUESTS → RULES → RESULTS → SIMULATION → UNITY
```

---

## Layer Overview

| Layer       | Assembly                       | Responsibility                                       |
| ----------- | ------------------------------ | ---------------------------------------------------- |
| Unity       | `PlayerController.Unity`       | Talks to Unity (MonoBehaviours, Transform, Raycasts) |
| Application | `PlayerController.Application` | Orchestration & data flow                            |
| Features    | `PlayerController.Features`    | Calculations that change kinematics                  |
| Core        | `PlayerController.Core`        | Rules, state, facts, and intent                      |

---

## Core Layer (The Brain)

**Contains NO Unity code.**

### What lives here

* `PlayerRuleState`
* `PhysicsContext`
* `ActionRules` (JumpRules, FallRules, etc)
* Requests (`JumpRequest`, `MoveRequest`)
* Results (`JumpResult`, `FallResult`)
* Enums & structs that describe *intent*

### What it does

The Core layer answers questions like:

* *Is the player allowed to jump?*
* *Are we grounded?*
* *Is this a coyote jump or a wall jump?*
* *What type of fall should occur?*

It **never moves anything**.

### PlayerRuleState

* Long-lived, persistent
* Tracks things like:

  * Remaining jumps
  * Cooldowns
  * Buffs / debuffs

> ⚠️ Important: **This state must persist across frames.**
> Creating a new `PlayerRuleState()` every Update will break your rules.

### PhysicsContext

* Read-only snapshot of the world
* Produced by Unity (`PhysicsMonitor`)
* Examples:

  * IsGrounded
  * IsTouchingWall
  * IsFalling

---

## Application Layer (The Traffic Controller)

This layer **coordinates**, it does not calculate.

### Key classes

* `ActionDispatcher`
* `AppOrchestrator`
* `PlayerActionContext`

### Responsibilities

* Gather requests
* Provide facts + rule state
* Dispatch requests to rules
* Collect results
* Forward results to simulation

### AppOrchestrator

```text
Requests + Facts + RuleState
        ↓
   ActionDispatcher
        ↓
    IActionResults
```

The Application layer does **not** know about:

* Velocity
* Gravity
* Delta time
* Unity transforms

---

## Features Layer (The Math)

This is where **numbers change**.

### What lives here

* `KinematicSolver`
* `CalcJump`
* `CalcFall`
* `CalcRun`

### Responsibilities

* Convert `IActionResult` → kinematic changes
* Modify:

  * Velocity
  * Acceleration
  * Gravity

### Key rule

> Features **do not decide IF something happens** — they only calculate HOW it happens.

### Example

* `JumpRules` says: *Jump approved*
* `CalcJump` says: *Add +22y velocity*

---

## Unity Layer (The Body)

The Unity layer is where **time exists**.

### What lives here

* `PlayerSimulationDriver`
* `PhysicsMonitor`
* `MovementResolver`
* `MovementController`

### Responsibilities

* Own `FixedUpdate`
* Own `deltaTime`
* Integrate velocity
* Resolve collisions
* Move the transform

Unity code **never evaluates rules**.

---

## PhysicsMonitor

### Purpose

Observes the world and reports facts.

### It provides

* Grounded
* Falling / Rising
* Wall contact
* Sliding

### It does NOT

* Apply gravity
* Modify velocity
* Trigger jumps

> PhysicsMonitor is a **sensor**, not a brain.

---

## PlayerSimulationDriver (The Heartbeat)

This is the **only class that owns simulation state**.

### Owns

* Current velocity
* Current acceleration
* Current gravity
* Current kinematic state

### Runs in

```csharp
FixedUpdate()
```

### Responsibilities

1. Receive `IActionResult`s
2. Pass them to `KinematicSolver`
3. Accumulate velocity
4. Integrate displacement
5. Resolve collisions
6. Apply movement

### Key rule

> Velocity must accumulate frame-to-frame.

---

## MovementResolver

### Purpose

* Takes displacement
* Performs raycasts
* Adjusts for collisions

### Input

* Displacement (Vector2)

### Output

* Collision-safe displacement

It does NOT:

* Integrate velocity
* Apply gravity

---

## MovementController

### Purpose

* Applies final displacement to Unity

Typically:

```csharp
transform.Translate(displacement)
```

This is the **final step**.

---

## Data Flow Summary

```
InputReader (Unity)
   ↓
ActionRequests
   ↓
ActionDispatcher (Application)
   ↓
ActionResults
   ↓
KinematicSolver (Features)
   ↓
KinematicState
   ↓
SimulationDriver (Unity)
   ↓
MovementResolver
   ↓
MovementController
```

---

## How External Systems Interface

### External systems SHOULD:

* Read player state (readonly)
* Send requests
* Subscribe to events

### External systems SHOULD NOT:

* Modify velocity
* Change gravity
* Bypass rules

### Examples

* Camera reads velocity
* Animation reads PhysicsContext
* Powerups modify PlayerStats

---

## Common Pitfalls (You Hit Most of These 😉)

* ❌ Resetting PlayerRuleState every frame
* ❌ Integrating velocity twice
* ❌ Applying gravity in multiple layers
* ❌ Treating impulses as continuous forces
* ❌ Using displacement where velocity is expected

---

## Mental Model to Keep

* **Rules answer YES/NO**
* **Results describe WHAT**
* **Simulation decides WHEN**
* **Unity decides WHERE**

If you ever feel stuck, ask:

> "Is this a decision, a calculation, or an application?"

That question will tell you which layer it belongs in.

---

*End of reference. This file is expected to evolve as your controller grows.*
