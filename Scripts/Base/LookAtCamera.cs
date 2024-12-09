using UnityEngine;


namespace xb.common {
    public class LookAtCamera : MonoBehaviour {


        private enum Mode {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] private Mode mode;

        private void Start() {
            LookAtCam();
        }


        private void LookAtCam() {

            switch (mode) {
                case Mode.LookAt:
                    transform.LookAt(Camera.main.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 cameraDir = transform.position - Camera.main.transform.position;
                    transform.LookAt(transform.position + cameraDir);
                    break;
                case Mode.CameraForward:
                    transform.forward = Camera.main.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -Camera.main.transform.forward;
                    break;
            }
        }


        private void OnValidate() {
            if (Camera.main == null) {
                return;
            }
            LookAtCam();
        }


    }
}