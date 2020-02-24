using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    internal sealed class SoundEffectsSettings : MonoBehaviour
    {
        [SerializeField] private Slider m_Slider;
        [SerializeField] private AudioMixer m_Mixer;

        private void Start()
        {
            var midVolume = (m_Slider.maxValue + m_Slider.minValue) / 2;
            m_Slider.value = PlayerPrefs.GetFloat("SoundEffectsVolume", midVolume);
            m_Mixer.SetFloat("EffectsVolume", m_Slider.value);
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set the sounds effects volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            m_Mixer.SetFloat("EffectsVolume", m_Slider.value);
            PlayerPrefs.SetFloat("SoundEffectsVolume", m_Slider.value);
            PlayerPrefs.Save();
        }

        public void IncreaseVolume()
        {
            if (m_Slider.value >= m_Slider.maxValue)
                return;

            m_Slider.value += 10F;
        }

        public void DecreaseVolume()
        {
            if (m_Slider.value <= m_Slider.minValue)
                return;

            m_Slider.value -= 10F;
        }
    }
}