using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace OSK.MVVM
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AnimTransition : MonoBehaviour, IUITransition
    {
        [SerializeField] private TransitionOption openOption = new TransitionOption();
        [SerializeField] private TransitionOption closeOption = new TransitionOption();

        public async UniTask PlayOpen(GameObject view, Action onComplete = null)
        {
            await PlayTransition(view, openOption, true, onComplete);
        }

        public async UniTask PlayClose(GameObject view, Action onComplete = null)
        {
            await PlayTransition(view, closeOption, false, onComplete);
        }

        private async UniTask PlayTransition(GameObject view, TransitionOption option, bool isOpen, Action onComplete)
        {
            CanvasGroup canvasGroup = view.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = view.AddComponent<CanvasGroup>();

            float elapsed = 0f;

            // start / end values
            Vector3 startScale, endScale;
            float startAlpha, endAlpha;
            Vector3 startPos, endPos;

            if(option.type == TransitionType.None)
            {
                onComplete?.Invoke();
                return;
            }
            
            // offset slide theo hướng
            Vector3 slideOffset = Vector3.zero;
            if (option.type.HasFlag(TransitionType.Slide))
            {
                switch (option.slideDirection)
                {
                    case SlideDirection.Up: slideOffset = new Vector3(0, Screen.height, 0); break;
                    case SlideDirection.Down: slideOffset = new Vector3(0, -Screen.height, 0); break;
                    case SlideDirection.Left: slideOffset = new Vector3(-Screen.width, 0, 0); break;
                    case SlideDirection.Right: slideOffset = new Vector3(Screen.width, 0, 0); break;
                }
            }

            if (isOpen)
            {
                startScale = option.type.HasFlag(TransitionType.Scale) ? Vector3.one * option.scaleFrom : Vector3.one;
                endScale = Vector3.one;

                startAlpha = option.type.HasFlag(TransitionType.Fade) ? 0f : 1f;
                endAlpha = 1f;

                startPos = option.type.HasFlag(TransitionType.Slide) ? slideOffset : Vector3.zero;
                endPos = Vector3.zero;
            }
            else
            {
                startScale = Vector3.one;
                endScale = option.type.HasFlag(TransitionType.Scale) ? Vector3.one * option.scaleFrom : Vector3.one;

                startAlpha = 1f;
                endAlpha = option.type.HasFlag(TransitionType.Fade) ? 0f : 1f;

                startPos = Vector3.zero;
                endPos = option.type.HasFlag(TransitionType.Slide) ? slideOffset : Vector3.zero;
            }

            // reset to start
            if (option.type.HasFlag(TransitionType.Scale))
                view.transform.localScale = startScale;

            if (option.type.HasFlag(TransitionType.Fade))
                canvasGroup.alpha = startAlpha;

            if (option.type.HasFlag(TransitionType.Slide))
                view.transform.localPosition = startPos;

            // delay
            if (option.delay > 0)
                await UniTask.Delay((int)(option.delay * 1000));

            // animate
            while (elapsed < option.duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / option.duration);
                float easedT = option.curve.Evaluate(t);

                if (option.type.HasFlag(TransitionType.Scale))
                    view.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, easedT);

                if (option.type.HasFlag(TransitionType.Fade))
                    canvasGroup.alpha = Mathf.LerpUnclamped(startAlpha, endAlpha, easedT);

                if (option.type.HasFlag(TransitionType.Slide))
                    view.transform.localPosition = Vector3.LerpUnclamped(startPos, endPos, easedT);

                await UniTask.Yield();
            }

            // đảm bảo kết quả cuối
            view.transform.localScale = endScale;
            canvasGroup.alpha = endAlpha;
            view.transform.localPosition = endPos;

            onComplete?.Invoke();
        }
    }
}