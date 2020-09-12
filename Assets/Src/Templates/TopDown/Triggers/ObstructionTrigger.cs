using System.Collections.Generic;

using UnityEngine;

namespace CodeBlaze.Templates.TopDown.Triggers {

    public class ObstructionTrigger : MonoBehaviour {

        [SerializeField] private List<GameObject> _activeTargets;
        [SerializeField] private List<GameObject> _hideTargets;
        
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;

            _activeTargets.ForEach(target => target.SetActive(true));
            _hideTargets.ForEach(target => target.SetActive(false));
        }

    }

}