// Liscense - GPL-3.0-or-later

using TMPro;
using UnityEngine;

namespace LC.Tools.TMProFontSwapper
{
    [CreateAssetMenu(fileName = "Font Swapper Data", menuName = "LC Tools/Scriptable Objects/Font Swapper Data")]
    public class FontSwapperData : ScriptableObject
    {
        public TMP_FontAsset[] fontAssets = new TMP_FontAsset[0];
    } 
}
