using UnityEngine;

namespace Assets.Scripts.Infracructure.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float RotationAngleX = 25.0f;
        public int Height = 10;
        public float OffsetZ = 10f;

        private Vector3 _target;
        private Vector3 _velocity = Vector3.zero;
        private float speedSmooting = 4f;

        private void Start()
        {

        }

        public void SetFocusTarget(GameObject target)
        {
            _target = target.transform.position;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, Height, 0) + FollowingPointPosition();
            transform.rotation = rotation;

            transform.position = Vector3.SmoothDamp(
                transform.position, 
                position, 
                ref _velocity, 
                Time.deltaTime * speedSmooting);
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _target;
            followingPosition.x = 0;
            followingPosition.z -= OffsetZ;
            return followingPosition;
        }

    }
}