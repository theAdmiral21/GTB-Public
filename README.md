# GTB-Unity-Public

This repository is a public technical showcase of core systems developed for *Quest for the Golden Tennis Ball*. It is intended to demonstrate how I approach architecture, system decoupling, and long-term maintainability in a Unity project.

*This repository is intended to be read as much as it is to be played.*

If you enjoyed reading this, check out the [Golden Tennis Ball Devlog](https://theadmiral21.github.io/Golden-Tennis-Ball-Devlog/)

### Highlighted Systems
- Intent-based movement system
- Stat-driven control tuning
- Event-driven effects system
- Real-time input device management
- Reusable, inspector-configured menu system
- Extensible scene management flow
- Centralized orchestration of decoupled game systems

---

## Purpose

This demo serves three primary purposes:

1. Provide a public example of my programming work.
2. Demonstrate a simple but scalable approach to building decoupled systems in Unity.
3. Offer a playable slice of *Quest for the Golden Tennis Ball* for experimentation and feedback.

The project is intentionally scoped as a systems-focused demo rather than a polished vertical slice.

## Architecture Overview

This project is structured around a small set of long-lived, decoupled systems coordinated at the scene level. The [Code Navigation Guide](<Code Navigation Guide.md>) contains references to files and systems with their paths. Use that guide to quickly orient yourself within the repo.

### Core Concepts

**GameSystemsManager (GSM)**  
The `GameSystemsManager` acts as the central orchestrator for the scene. It owns global services such as game state management and is the sole authority responsible for advancing high-level game state (e.g., Boot → Gameplay → Pause). Other systems may observe or request state changes, but only the GSM mutates global state.

**Scene Bootstrapper**  
The scene bootstrapper is responsible for constructing and wiring scene-specific objects. It performs registration and validation work, then notifies the GSM when the scene is ready. The bootstrapper does not advance game state directly, which helps avoid race conditions during initialization.

**Decoupled Systems**  
Gameplay systems (input, movement, UI, effects, etc.) are designed to communicate through narrow interfaces and events rather than direct references. This allows systems to be developed, tested, and refactored independently without cascading changes.

---

### State Flow

High-level state transitions follow a guarded flow:
1. Scene Load
2. Bootstrapper constructs & registers systems
3. Bootstrapper signals readiness
4. GameSystemsManager validates conditions
5. GameStateManager advances state

This ensures all systems are subscribed and ready before gameplay begins.

---

### Input Architecture

Input handling is separated from gameplay logic. Raw input is processed by dedicated input services and routed to interested systems based on the current game state. This allows input devices to be hot-swapped at runtime and supports different control schemes (keyboard/mouse, gamepad) without gameplay code needing to know the source of input.

---

### Data-Driven Tuning

Player movement and control behavior are driven by stat assets rather than hardcoded values. This enables rapid iteration on gameplay feel without requiring code changes and keeps tuning concerns separate from movement logic.

---

### Design Goals

The architecture favors:
- Explicit ownership and responsibility
- Minimal coupling between systems
- Deterministic initialization and state transitions
- Readability over cleverness
- Ease of debugging and refactoring

This demo intentionally prioritizes clarity and maintainability over maximum abstraction.

---

## Usage

To open and run the demo:

1. Ensure **Unity 6** is installed.
2. Clone the repository (including assets).
3. Open the project in Unity.
4. Press Play in the main scene.

---

## Controls

The game supports both keyboard/mouse and gamepad input.  
*Note: The pause menu currently supports mouse input only.*

### Keyboard

| Action | Input |
|------|------|
| Move | WASD |
| Jump | Space |
| Double Jump | Space (while airborne) |
| Bark | E |
| Sprint | Shift |
| Quick Step | Double-tap A or D |

### Gamepad

| Action | Input |
|------|------|
| Move | Left Stick or D-Pad |
| Jump | South Button |
| Double Jump | South Button (while airborne) |
| Bark | West Button |
| Sprint | Left or Right Trigger |
| Quick Step | Double-tap Move |

### Wall Jumping

The player can wall jump by initiating a wall slide and either:
- Pressing jump to hop upward, or
- Providing directional input towards the wall and pressing jump to leap off

---

## Adjusting Movement Stats

The controller has not undergone final tuning. Movement values can be adjusted via:
`Assets/Stats/PlayerMovementStats`

Changes will take effect after restarting the scene.

Documentation for each stat can be found in: `Assets/Scripts/PlayerController/Unity/PlayerMovementSO.cs`


---

## Summary

Thanks for checking out the demo!  
Development progress for *Quest for the Golden Tennis Ball* is documented on the [devlog](https://theadmiral21.github.io/Golden-Tennis-Ball-Devlog/).

If you’re interested in the project or have questions about the systems showcased here, feel free to reach out on GitHub.
