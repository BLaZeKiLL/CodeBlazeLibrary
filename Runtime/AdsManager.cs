using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBlaze.Extensions.Monetization {

    public class AdsManager : MonoBehaviour {

        [SerializeField] private string GameId;
        [SerializeField] private bool TestMode;

        private void Start() {
            Advertisement.Initialize(GameId, TestMode);
        }

    }

}