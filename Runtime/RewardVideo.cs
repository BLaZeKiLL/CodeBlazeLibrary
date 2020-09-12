using System;

using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBlaze.Extensions.Monetization {

    public class RewardVideo : IUnityAdsListener, IDisposable {

        private string PlacementId;
        
        private Action OnFinish;
        private Action OnSkip;
        private Action OnError;

        public RewardVideo(string placementId, Action onFinish, Action onSkip) {
            PlacementId = placementId;
            OnFinish = onFinish;
            OnSkip = onSkip;
            OnError = null;
            
            Advertisement.AddListener(this);
        }

        public RewardVideo(string placementId, Action onFinish, Action onSkip, Action onError) {
            PlacementId = placementId;
            OnFinish = onFinish;
            OnSkip = onSkip;
            OnError = onError;
            
            Advertisement.AddListener(this);
        }

        public void OnUnityAdsReady(string placementId) {
            
        }

        public void OnUnityAdsDidError(string message) {
            Debug.LogError(message);
            OnError?.Invoke();
        }

        public void OnUnityAdsDidStart(string placementId) {
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            switch (showResult) {
                case ShowResult.Finished:
                    OnFinish();

                    break;
                case ShowResult.Skipped:
                    OnSkip();

                    break;
                case ShowResult.Failed:
                    Debug.LogWarning ("The ad did not finish due to an error.");
                    OnError?.Invoke();

                    break;
            }
        }

        public void Dispose() {
            Advertisement.RemoveListener(this);
        }

        public bool Show() {
            if (!Advertisement.IsReady()) return false;

            Advertisement.Show(PlacementId);

            return true;

        }

    }

}