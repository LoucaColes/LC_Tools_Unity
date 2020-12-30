// Liscense - GPL-3.0-or-later

using TMPro;
using UnityEngine;

namespace LC.Tools.TMProFontSwapper
{
    public class FontNameText : MonoBehaviour
    {
        private TextMeshProUGUI textUGUI = null;
        private int fontId = 0;

        private void Awake()
        {
            textUGUI = GetComponent<TextMeshProUGUI>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            TMProFontSwapperManager.OnFontChangedDelegate += UpdateFontAsset;
        }

        private void OnDestroy()
        {
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
        /// Update the text component's text value with font asset's name
        /// </summary>
        /// <param name="_newAsset">New font asset for text component</param>
        public void UpdateFontAsset(TMP_FontAsset _newAsset, int _id)
        {
            textUGUI.text = _newAsset.name;
            fontId = _id;
        }
    }

}