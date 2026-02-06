using Game.Core.UI.Menus.Abstractions;
using UnityEngine;


namespace Game.UI.Menus.Unity.Presenters.Abstractions
{
    public abstract class UITween : MonoBehaviour, IUITween
    {
        public string TweenName;
        // public Tween Tween;
        public virtual bool Blocking => true;

        // For simplicity sake, the demo will not have any tweens in it.
        // public abstract Tween BuildTween();
        public abstract void PlayForward();
        public abstract void PlayBackwards();
    }

}
