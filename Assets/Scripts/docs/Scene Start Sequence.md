# Scene Start up
The following diagrams depict the start up order for the given scene from a very high level. It glosses over most of the objects that are contained within other classes like the `AppOrchestrator`. The point is to show how Unity's builtin lifecycle is managed.  
The key takeaways are:
- Instantiation is shared between the game systems manager and the scene boot strapper.
- The game systems manager builds the game context in order to distribute services.
- Services are distributed via the boot strapper.
- Systems that require the game context register with the boot strapper in Awake
- In Start, the boot strapper initializes registered systems in two passes, initialize and post initialize in order to prevent race conditions.
- The game state is held in "Booting" until the boot strapper finishes initialization at which point the game systems manager requests a change to the gameplay state.


# Scene Awake Sequence

```mermaid
sequenceDiagram

    participant GameSystemsManager
    create participant GameStateManager
    GameSystemsManager->>GameStateManager:
    create participant SimManager
    GameSystemsManager->>SimManager:
    create participant Services
    GameSystemsManager->>Services:
    create participant GameContext
    GameSystemsManager->>GameContext: Store services in Game Context
    participant GameContextRegistry
    GameContext->>GameContextRegistry: Register game context

    participant SceneBootStrapper
    participant SceneInitializationRegistry
    SceneBootStrapper->>SceneInitializationRegistry: Register boot strapper

    participant PlayerActionOrchestrator
    PlayerActionOrchestrator->>SceneInitializationRegistry: Register

    participant ActionMapManager
    ActionMapManager->>SceneInitializationRegistry: Register
    participant PauseMenuPresenter
    PauseMenuPresenter->>SceneInitializationRegistry: Register
    participant MenuOrchestrator
    MenuOrchestrator->>SceneInitializationRegistry: Register

```

# Scene Start Sequence
```mermaid
sequenceDiagram

    participant GameSystemsManager
    participant GameStateManager
    participant SceneBootStrapper
    participant PlayerActionOrchestrator
    participant ActionMapManager
    participant PauseMenuPresenter
    participant MenuOrchestrator
    SceneBootStrapper->>PlayerActionOrchestrator:Initialize
    SceneBootStrapper->>ActionMapManager:Initialize
    SceneBootStrapper->>PauseMenuPresenter:Initialize
    SceneBootStrapper->>MenuOrchestrator:Initialize

    SceneBootStrapper->>PlayerActionOrchestrator:Post Initialize
    SceneBootStrapper->>ActionMapManager:Post Initialize
    SceneBootStrapper->>PauseMenuPresenter:Post Initialize
    SceneBootStrapper->>MenuOrchestrator:Post Initialize

    
    SceneBootStrapper--)GameSystemsManager:Emit ready event
    GameSystemsManager->>GameStateManager:Request state change to Gameplay
    GameStateManager--)ActionMapManager:Emit state change event

```