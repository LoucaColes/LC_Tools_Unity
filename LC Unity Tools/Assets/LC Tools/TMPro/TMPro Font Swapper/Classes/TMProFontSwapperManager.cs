// Liscense - GPL-3.0-or-later

using TMPro;
using UnityEngine;

namespace LC.Tools.TMProFontSwapper
{
    public class TMProFontSwapperManager : MonoBehaviour
    {
        // For simplicity sake the font swapper manager is a singleton as there should only ever be one
        // But doesn't mean it has to be if you don't want it to be
        public static TMProFontSwapperManager instance = null;

        [SerializeField] private FontSwapperData fontSwapperData = null;

        private int currentFont = 0;

        public delegate void FontChangedDelegate(TMP_FontAsset _newAsset, int _id);
        public static event FontChangedDelegate OnFontChangedDelegate;

        public int CurrentFontId { get => currentFont; }

        private void Awake()
        {            
            // Setting up singleton
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            // Load font data
            LoadFontData();
        }

        /// <summary>
        /// Load the previous font Id
        /// This example is set up to use Player Prefs as a quick way
        /// to save and load, this should be replaced by an actual save system
        /// </summary>
        private void LoadFontData()
        {
            // Check if player pref has any previous data
            if (PlayerPrefs.HasKey("FontId"))
            {
                // Get saved font Id
                currentFont = PlayerPrefs.GetInt("FontId");

                // Find all active font swappers and update their font assets
                TMProFontSwapper[] fontSwappers = GameObject.FindObjectsOfType<TMProFontSwapper>();
                foreach (var swapper in fontSwappers)
                {
                    swapper.UpdateFontAsset(fontSwapperData.fontAssets[currentFont], currentFont);
                }

                // This is for the example but in a options menu you might need something like the font name text
                // It will update the text value to the name of the current font asset
                FontNameText fontNameText = GameObject.FindObjectOfType<FontNameText>();
                fontNameText.UpdateFontAsset(fontSwapperData.fontAssets[currentFont], currentFont);
            }
            else
            {
                // If no previous save data, create entry in player prefs and save
                PlayerPrefs.SetInt("FontId", currentFont);
                PlayerPrefs.Save();
            }
        }

        private void OnDestroy()
        {
            // Save font Id
            PlayerPrefs.SetInt("FontId", currentFont);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Increments current font id
        /// </summary>
        public void NextFont()
        {
            // Increment current font id
            currentFont++;

            // Wrap back to 0 if reached end of array
            if (currentFont >= fontSwapperData.fontAssets.Length)
            {
                currentFont = 0;
            }

            // Update all text with new font
            UpdateTextWithNewFont();
        }

        /// <summary>
        /// Decrements current font id
        /// </summary>
        public void PreviousFont()
        {
            // Decrement current font id
            currentFont--;

            // Wrap back to total fonts if reached the beginning
            if (currentFont < 0)
            {
                currentFont = fontSwapperData.fontAssets.Length - 1;
            }

            // Update all text with new font
            UpdateTextWithNewFont();
        }

        /// <summary>
        /// Calls the on font changed delegate to notify anything that is subscribed to it
        /// Should update subscribes with the new font asset
        /// </summary>
        public void UpdateTextWithNewFont()
        {
            OnFontChangedDelegate(fontSwapperData.fontAssets[currentFont], currentFont);
        }

        public TMP_FontAsset GetCurrentFontAsset()
        {
            return fontSwapperData.fontAssets[currentFont];
        }
    }

}