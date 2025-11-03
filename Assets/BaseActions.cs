// BaseActions.cs
using UnityEngine;

public abstract class BaseActions : MonoBehaviour, IActions
{
    public virtual bool CanMove      => false;
    public virtual bool CanAnimate   => false;
    public virtual bool CanPlayMedia => false;
    public virtual void ToggleMove() {}
    public virtual void ToggleAnimation() {}
    public virtual void TogglePlayPause() {}
}
