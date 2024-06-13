using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostBehavior_PM : MonoBehaviour
{
    public Ghost_PM ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        ghost = GetComponent<Ghost_PM>();
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;

        CancelInvoke();
    }

}
