using System;
using UnityEngine;

namespace OSK.MVVM
{

    [Flags]
    public enum TransitionType
    {
        None = 0,
        Fade = 1 << 0, // 1
        Scale = 1 << 1, // 2
        Slide = 1 << 2, // 
    }

    public enum SlideDirection
    {
        Left,
        Right,
        Up,
        Down
    }


    [System.Serializable]
    public class TransitionOption
    {
        [Header("Transition Type")]
        public TransitionType type = TransitionType.Fade;
        public SlideDirection slideDirection = SlideDirection.Up;

        [Header("Animation Settings")]
        public float duration = 0.3f;
        public float delay = 0f;
        public float scaleFrom = 0.8f;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}