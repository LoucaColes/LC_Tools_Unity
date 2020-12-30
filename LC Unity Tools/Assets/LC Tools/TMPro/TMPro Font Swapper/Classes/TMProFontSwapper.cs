// Liscense - GPL-3.0-or-later

using TMPro;
using UnityEngine;

namespace LC.Tools.TMProFontSwapper
{
    public class TMProFontSwapper : MonoBehaviour
    {
        private TextMeshProUGUI textUGUI = null;
        private int fontId = 0;

        private void Awake()
        {
            // Get text component
            textUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            // Subscribe to font change delegate
            TMProFontSwapperManager.OnFontChangedDelegate += UpdateFontAsset;
        }

        private void OnDestroy()
        {
            // Unsubscribe from font change delegate
            TMProFontSwapperManager.OnFontChangedDelegate -= UpdateFontAsset;
        }

        private void OnEnable()
        {
            if (TMProFontSwapperManager.instance == null) { return; }

            // Check if the font has changed since last shown
            if (fontId == TMProFontSwapperManager.instance.CurrentFontId) { return; }

            // If changed then update with new font
            textUGUI.font = TMProFontSwapperManager.instance.GetCurrentFontAsset();
            fontId = TMProFontSwapperManager.instance.CurrentFontId;
        }

        /// <summary>
        /// Update the text component's font asset
        /// </summary>
        /// <param name="_newAsset">New font asset for text component</param>
        public void UpdateFontAsset(TMP_FontAsset _newAsset, int _id)
        {
            textUGUI.font = _newAsset;
            fontId = _id;
        }
    }

}