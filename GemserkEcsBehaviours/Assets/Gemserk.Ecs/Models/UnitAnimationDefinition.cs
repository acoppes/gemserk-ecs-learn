using System;
using UnityEngine;

namespace Gemserk.Ecs.Models
{
    [Serializable]
    public struct UnityAnimationEvent
    {
        // TODO: preprocess animations from artists and get proper 
        // event id from event name and animation events database
        // used in editor for easily event selection maybe... 
        // Another idea could be to use strings in animation and scriptable,
        // but cache on load time an event id.

        public int eventId;
        public float time;
    }

    [Serializable]
    public class UnitAnimationDefinition
    {
        public string name;
        public int fps = 30;
        public Sprite[] frames;
        public UnityAnimationEvent[] events;
    }
}