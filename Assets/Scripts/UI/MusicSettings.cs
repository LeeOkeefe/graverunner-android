using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    internal sealed class MusicSettings : MonoBehaviour
    {
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private AudioMixer m_Mixer;

        private void Start()
        {
            var musicOn = PlayerPrefs.GetInt("MusicOn", 1);
            m_Toggle.isOn = musicOn == 1;
            SetMusicVolume();

            m_Toggle.onValueChanged.AddListener(delegate
            {
                ValueChangeCheck();
            });
        }

        /// <summary>
        /// Toggle the music volume on or off 
        /// </summary>
        public void ValueChangeCheck()
        {
            PlayerPrefs.SetInt("MusicOn", m_Toggle.isOn ? 1 : 0);
            SetMusicVolume();
            PlayerPrefs.Save();
        }

        private void SetMusicVolume()
        {
            if (m_Toggle.isOn)
            {
                m_Mixer.SetFloat("MusicVolume", 0);
            }
            else
            {
                m_Mixer.SetFloat("MusicVolume", -80);
            }
        }
    }
}
