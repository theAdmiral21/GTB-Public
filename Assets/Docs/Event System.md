# Event system

I've developed a scalable event system using scriptable objects. It has three components:
1. The event channel
2. The event emitter
3. The event library

## Event channel
The event channel is a scriptable object with an object, a subscribe/unsubscribe method, and a raise method. I currently have two versions of the event channel, one that returns void and one that returns objects. Same goes for the emitters.
```csharp
    public class BaseEventChannelSO<T> : ScriptableObject
    {
        private event Action<T> _listeners;

        public void Subscribe(Action<T> listener)
        {
            _listeners += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            _listeners -= listener;
        }

        internal void Raise(T payload)
        {
            _listeners?.Invoke(payload);
        }
    }
```
The interesting things to note here are:
1. The event is **private**
2. The raise method is **internal**

The event itself is private to prevent other classes from subscribing to it without going through the defined subscribe method.
Raise is internal because only objects that extend this class are allowed to raise the event. That is where emitters come in.

## Emitter
The emitter is a scriptable object inheriting from BaseEmitterSO. The base class only has two things of note:
1. A private serialized reference to the event channel
2. A public raise method

```c#
    public abstract class BaseEmitterSO<T> : ScriptableObject
    {
        [SerializeField] private BaseEventChannelSO<T> _channel;

        public void Raise(T payload)
        {
            _channel.Raise(payload);
        }
    }
```
In practice an emitter looks like this:

```c#
    [CreateAssetMenu(menuName = "Events/Emitters/Pick up event emitter")]
    public class PickUpEventEmitter : BaseEmitterSO<PickUpContext> { }
```
Pretty boring right? That's how I like it. This class is fully customizable via the inspector which is the real beauty of this design. EVERYTHING described here can be built via the inspector.

## Event library
Last but not least is the Event Library. The Event Library is just a fancy name for a scriptable object that holds specific event types. When I say specific event types, I mean whoever is emitting the events. For example I have a player event library containing events pertaining to respawning, picking up items, and eventually buffing the player.
```c#
    [CreateAssetMenu(menuName = "Channel Libraries/Player Channel Library")]
    public class PlayerChannelLibrary : ScriptableObject
    {
        public PickUpEventChannel OnItemPickUp;
        public DeathEventChannel OnPlayerDied;
    }
```
## Usage
In practice this is a little bit confusing to put together. There are A LOT of moving parts. To set up a new event, say a damage event, you first need to make a new **DamageEventChannel** and a **DamageEventEmitter** to go with it. After you have these two pieces make a new field in the PlayerChannelLibrary to hold a reference to the new event channel. Next determine what object will emit the PlayerDamageEvent and give them a reference to both the PlayerDamageEventChannel AND the PlayerDamageEmitter. Without both the emitter will not work. Finally, any objects that need to do something when the player takes damage get a reference to the event library and then subscribe/unsubscribe to the event they care about.

## Going Forward
I'm definitely making this into an editor tool because I spend more time making new files than I do writing code. 
