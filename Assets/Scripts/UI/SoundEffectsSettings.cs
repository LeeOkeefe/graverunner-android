using UnityEngine;
using UnityEngine.UI;
using AudioListener = UnityEngine.AudioListener;

namespace Assets.Scripts.UI
{
    internal sealed class SoundEffectsSettings : MonoBehaviour
    {
        [SerializeField] private Slider m_Slider;

        private void Start()
        {
            m_Slider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", 0.5F);
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set our audio listener volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            AudioListener.volume = m_Slider.value;
            PlayerPrefs.SetFloat("SoundEffectsVolume", AudioListener.volume);
            PlayerPrefs.Save();
        }

        public void IncreaseVolume()
        {
            if (m_Slider.value >= 1)
                return;

            m_Slider.value += 0.1F;
        }

        public void DecreaseVolume()
        {
            if (m_Slider.value <= 0)
                return;

            m_Slider.value -= 0.1F;
        }
    }
}