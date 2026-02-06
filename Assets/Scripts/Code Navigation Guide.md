## Code Navigation Guide

This section highlights the most relevant folders and files for understanding how the project works. Reviewers are encouraged to start here rather than browsing the project tree randomly.

---

### Entry Points

**GameSystemsManager** `Assets/Scripts/Unity/GameSystemsManager.cs`  
The central orchestrator for the scene. Owns the `GameStateManager` and coordinates high-level system initialization and state transitions. This is the best starting point for understanding overall control flow and ownership.

**Scene Bootstrapper** `Assets/Scripts/Game/Unity/Scene/SceneBootstrapper.cs` 
Responsible for constructing and wiring scene-level objects, then notifying the `GameSystemsManager` when initialization is complete. Does not advance game state directly.

---

### Game State

**GameStateManager** `Assets/Scripts/Game/Application/State/GameStateManager.cs`

Encapsulates the current high-level game state and enforces legal state transitions. Exposes state changes via events for other systems to observe.

---

### Input System

**Input Services** `Assets/Scripts/PlayerController/Unity/Inputs`

Contains services responsible for raw input processing, device detection, and input routing. Gameplay systems consume input through interfaces rather than directly querying Unity input APIs.

---

### Player & Movement

**Player Controller** `Assets/Scripts/PlayerController/`

Implements intent-based movement and player actions. Movement behavior is driven by stat assets rather than hardcoded values.

**Movement Stats** `Assets/Stats/PlayerMovementStats`

ScriptableObject assets used to tune movement behavior without modifying code.

---

### UI & Menus

**Menu Framework** `Assets/Scripts/Game/Unity/UI/Menus/`

A reusable, inspector-configured menu system designed to support multiple contexts (main menu, pause menu, etc.) with minimal duplication.

---

### Effects System

**Event-Driven Effects** `Assets/Scripts/PlayerController/Unity/Effects/`

Implements gameplay and visual effects that respond to events rather than being tightly coupled to gameplay logic.

---

### Notes for Reviewers

- Systems are intentionally decoupled and communicate primarily through interfaces and events.
- Serialized references are used for scene-level composition to keep wiring explicit and debuggable.
- Some systems are simplified or incomplete by design, as the focus of this demo is architecture rather than content volume.



