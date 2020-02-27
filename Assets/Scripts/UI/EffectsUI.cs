using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class EffectsUI : MonoBehaviour
    {
        [SerializeField]
        private Image[] slots;

        private void Start()
        {
            foreach (var slot in slots)
            {
                slot.sprite = null;
                slot.color = Color.clear;
            }
        }

        /// <summary>
        /// Updates the slot with the effect icon if the effect is active,
        /// otherwise we set the slot to null
        /// </summary>
        public void UpdateSlot(Image effectIcon, bool isActive)
        {
            if (isActive)
            {
                if (slots.Any(i => i.sprite == effectIcon.sprite))
                    return;

                var slotIndex = FindFirstEmptySlot();

                slots[slotIndex].color = new Color(0, 0.6f, 0.245f);
                slots[slotIndex].sprite = effectIcon.sprite;
            }
            else
            {
                foreach (var slot in slots)
                {
                    if (slot.sprite == effectIcon.sprite)
                    {
                        slot.sprite = null;
                        slot.color = Color.clear;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the first empty slot in the array
        /// </summary>
        private int FindFirstEmptySlot()
        {
            for (var i = 0; i < slots.Length; i++)
            {
                if (slots[i].sprite == null)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
