using UnityEngine;

namespace Assets.Scripts.Extensions
{
    internal static class CanvasGroupExtensions
    {
        /// <summary>
        /// Enables or disables all the properties of a Canvas Group
        /// </summary>
        public static void ToggleGroup(this CanvasGroup group, bool show)
        {
            group.alpha = show ? 1 : 0;
            group.blocksRaycasts = show;
            group.interactable = show;
        }
    }
}