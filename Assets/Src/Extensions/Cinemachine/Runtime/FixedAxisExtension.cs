using System;

using Cinemachine;

using UnityEngine;

namespace CodeBlaze.Extensions.Cinemachine {

    public enum CameraAxis {

        X,
        Y,
        Z

    }
    
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
    public class FixedAxisExtension : CinemachineExtension {

        [Tooltip("Which axis to lock")] [SerializeField]
        private CameraAxis Axis;
        
        [Tooltip("Lock the camera's axit position to this value")]
        [SerializeField]
        private float Position = 10;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body) return;

            var pos = state.RawPosition;

            switch (Axis) {
                case CameraAxis.X:
                    pos.x = Position;
                    break;
                case CameraAxis.Y:
                    pos.y = Position;
                    break;
                case CameraAxis.Z:
                    pos.z = Position;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            state.RawPosition = pos;
        }

    }

}