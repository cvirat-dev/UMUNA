using UnityEngine;

namespace UUP.UI.Customisation
{
    /// <summary>
    /// We assume: A window has 3 containers: Top / Center / Bottom
    /// Color 1: Applies for Top-Container & Bottom-Container
    /// Color 2: Applies for the elements of Center-Container
    /// </summary>
    /// 

    [CreateAssetMenu(menuName = "UUP/CustomUI/WindowThemeSO", fileName = "WindowThemeSO")]
    public class WindowThemeSO : ScriptableObject
    {
        [Header("Top- & Bottom-Container")]
        public Color TopBottomColor;
        
        [Header("Center-Container")]
        public Color CenterColor;       
    }
}