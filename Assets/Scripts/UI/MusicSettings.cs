using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class MusicSettings : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Slider m_Slider;

        private void Start()
        {
            m_Slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5F);
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set our audioSource volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            audioSource.volume = m_Slider.value;
            PlayerPrefs.SetFloat("MusicVolume", audioSource.volume);
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
