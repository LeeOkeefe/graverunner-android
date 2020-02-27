using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    internal sealed class SoundEffectsSettings : MonoBehaviour
    {
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private AudioMixer m_Mixer;

        private void Start()
        {
            var effectsOn = PlayerPrefs.GetInt("EffectsOn", 1);
            m_Toggle.isOn = effectsOn == 1;
            SetEffectsVolume();

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
            PlayerPrefs.SetInt("EffectsOn", m_Toggle.isOn ? 1 : 0);
            SetEffectsVolume();
            PlayerPrefs.Save();
        }

        private void SetEffectsVolume()
        {
            if (m_Toggle.isOn)
            {
                m_Mixer.SetFloat("EffectsVolume", 0);
            }
            else
            {
                m_Mixer.SetFloat("EffectsVolume", -80);
            }
        }
    }
}