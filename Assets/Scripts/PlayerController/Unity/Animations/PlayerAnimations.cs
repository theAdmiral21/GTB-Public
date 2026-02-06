using UnityEngine;

namespace PlayerController.Animations
{

    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private AudioClip pawSound;

        public void PlayPawStep()
        {
            // SoundFXManager.Instance.PlaySFX(pawSound);
        }
    }
}