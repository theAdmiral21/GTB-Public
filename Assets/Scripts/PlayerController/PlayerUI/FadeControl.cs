using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FadeControl : MonoBehaviour
    {
        [SerializeField] public float DesiredAlpha { get; set; }
        [SerializeField] public float CurrentAlpha { get; set; }
        public float fadeTime { get; set; } = 2.0f;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            CurrentAlpha = canvasGroup.alpha;
        }

        public void FadeOut()
        {
            DesiredAlpha = 1f;
        }

        public void FadeIn()
        {
            DesiredAlpha = 0f;
        }

        private void Update()
        {
            if (DesiredAlpha != CurrentAlpha)
            {
                CurrentAlpha = Mathf.MoveTowards(CurrentAlpha, DesiredAlpha, fadeTime * Time.deltaTime);
                canvasGroup.alpha = CurrentAlpha;
            }
        }
    }
}