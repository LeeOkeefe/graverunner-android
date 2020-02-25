using System;
using UnityEngine;

namespace Effects
{
    internal sealed class Effect
    {
        public float ExpiryTime;
        public Action UndoAction;

        public Effect(float duration, Action action)
        {
            ExpiryTime = Time.time + duration;
            UndoAction = action;
        }
    }
}
