using TMPro;
using UnityEngine;

namespace LC.Tools.TMProFontSwapper.Example
{
    public class ExampleCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject hiddenPanel = null;
        [SerializeField] private TextMeshProUGUI showHiddenButtonText = null;

        private bool showHiddenPanel = false;

        // Start is called before the first frame update
        void Start()
        {
            // Hide hidden panel
            hiddenPanel.SetActive(showHiddenPanel);
        }

        /// <summary>
        /// Toggles visibility of a hidden panel
        /// </summary>
        public void ToggleHiddenPanel()
        {
            // Invert show hidden panel and set active using the new value
            showHiddenPanel = !showHiddenPanel;
            hiddenPanel.SetActive(showHiddenPanel);

            // Update button text based on show hidden panel
            showHiddenButtonText.text = showHiddenPanel ? "Hide Hidden" : "Show Hidden";
        }
    } 
}
