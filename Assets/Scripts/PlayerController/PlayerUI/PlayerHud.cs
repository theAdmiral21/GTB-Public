using UnityEngine;
using UnityEngine.UI;
// using TMPro;
using System;
using System.Collections.Generic;
// using Assets.Scripts.Audio;

namespace Assets.Scripts.Player.PlayerUI
{
    public class PlayerHud : MonoBehaviour
    {
        [SerializeField] private Canvas hud;
        [SerializeField] private string canvasLayer;
        [SerializeField] private Image oneUp;
        // [SerializeField] private TextMeshProUGUI uiScore;
        // [SerializeField] private TextMeshProUGUI uiLives;
        // [SerializeField] private TextMeshProUGUI uiTime;
        [SerializeField] private GameObject DinoTracker;
        [SerializeField] private List<Image> dinoList = new List<Image>();
        [SerializeField] private Sprite dinoSprite;
        [SerializeField] private AudioClip DinosFoundSound;
        // Actions for interacting with the hud
        public Action<int> SpookChanged;
        public Action<string> LivesChanged;
        public Action<string> ScoreChanged;
        public Action<int> DinoFound;
        private void OnEnable()
        {
            SpookChanged += OnSpookChanged;
            LivesChanged += OnLivesChanged;
            ScoreChanged += OnScoreChanged;
            DinoFound += OnDinoFound;
        }
        private void OnDisable()
        {
            SpookChanged -= OnSpookChanged;
            LivesChanged -= OnLivesChanged;
            ScoreChanged -= OnScoreChanged;
            DinoFound -= OnDinoFound;
        }
        // public void ConfigHud(CharacterData characterData)
        // {
        //     Debug.Log($"[PlayerHud] CharacterData: {characterData.name}");
        //     oneUp.sprite = characterData.oneUp;
        //     Debug.Log($"Canvas sorting layer name: {hud.sortingLayerName}");

        // }
        public void SetUiCamera(Camera uiCamera)
        {
            if (uiCamera == null)
            {
                Debug.LogError($"[PlayerHud] No camera assigned {this.gameObject.name}");
                return;
            }
            // Get the player's camera on the camera brain
            hud.renderMode = RenderMode.ScreenSpaceCamera;
            hud.worldCamera = uiCamera;
            hud.sortingLayerName = canvasLayer;
            Debug.Log($"Canvas sorting layer name: {hud.sortingLayerName}");
        }

        private void OnSpookChanged(int val)
        {

        }
        private void OnLivesChanged(string val)
        {
            // uiLives.text = val;
        }
        private void OnScoreChanged(string val)
        {
            // uiScore.text = val;
        }

        private void OnDinoFound(int index)
        {
            index -= 1;
            Debug.Log($"[PlayerHud] Dino {index} found");
            // Update the dino that was found
            dinoList[index].sprite = dinoSprite;

            // Check if the player found all of the dinos
            foreach (Image dino in dinoList)
            {
                if (dino.sprite != dinoSprite)
                {
                    return;
                }
            }
            // SoundFXManager.Instance.PlaySFX(DinosFoundSound);
        }
    }
}