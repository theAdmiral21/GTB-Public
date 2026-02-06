using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.Player.PlayerUI
{
    public class StandardPauseMenu : MonoBehaviour, IStandardMenu
    {
        [SerializeField] private GameObject FirstObject;
        public GameObject FirstSelected => FirstObject;

        void Start()
        {
            if (FirstObject == null)
            {
                Debug.LogError($"FirstObject for {this} is missing.");
            }
        }

        public void SetupMenu()
        {
            // Make sure all of the buttons are not highlighted
            ResetButtons(this.gameObject);
            // Highlight the first button
            // FirstObject.GetComponent<ButtonEventHandler>().OnSelect(null);
        }

        private void ResetButtons(GameObject menu)
        {
            // ButtonEventHandler[] buttonScripts = menu.GetComponentsInChildren<ButtonEventHandler>();
            // if (buttonScripts.Length == 0)
            // {
            Debug.LogWarning("Could not find any buttons with ButtonEvenHandler. Is this script here by mistake?");
            // }
            // else
            // {
            //     foreach (var button in buttonScripts)
            //     {
            //         button.OnDeselect(null);
            //     }
            // }
        }
    }
}