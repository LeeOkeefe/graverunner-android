using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    internal static class AdsManager
    {
        private const string GameId = "3490400";

        private static float m_TimeTillNextAd;

        public static void Initialize()
        {
            Advertisement.Initialize(GameId, true);
        }

        public static void ShowAd()
        {
            Debug.Log($"Current time is : {Time.time} and time till next ad is : {m_TimeTillNextAd}");

            if (!CanShowAd() || Advertisement.isShowing)
                return;

            Advertisement.Show();
            m_TimeTillNextAd = Time.time + 90;
        }

        private static bool CanShowAd()
        {
            return Time.time >= m_TimeTillNextAd;
        }
    }
}
