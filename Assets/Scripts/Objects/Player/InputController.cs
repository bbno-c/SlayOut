using System;
using Unity.Mathematics;
using UnityEngine;

namespace Objects
{
    [Serializable]
    public struct FireInput
    {
        public KeyCode Key;
        public FireType FireType;
    }

    [Serializable]
    public struct AbilityInput
    {
        public KeyCode Key;
        public int Index;
    }

    public class InputController : MonoBehaviour
    {
        public FireInput[] FireInput;
        public AbilityInput[] AbilityInput;

        private Character _player;
        private Camera _camera;
        private CameraFollow _cameraFollow;

        private void Awake()
        {
            _camera = Camera.main;
            _cameraFollow = _camera.GetComponent<CameraFollow>();
        }

        public void SetPlayer(Character player)
        {
            _player = player;
        }

        void Update()
        {
            if (_player == null)
                return;

            if (_player.Movement != null)
            { 
                _player.Movement.MoveDirection(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f));

                var dir = _camera.ScreenToWorldPoint(Input.mousePosition) - (_player.transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x);
                _player.Movement.LookDirection(angle * Mathf.Rad2Deg);

                float pointX = (_player.transform.position.x + math.cos(angle) * (5));
                float pointY = (_player.transform.position.y + math.sin(angle) * (5));
                _cameraFollow.CameraDirection(pointX, pointY);

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    _player.Movement.Jump();
            }

            if (_player.WeaponHolder != null)
            {
                _player.WeaponHolder.ChangeWeapon(Input.GetAxis("Mouse ScrollWheel"));

                if (Input.GetKeyDown(KeyCode.R))
                     _player.WeaponHolder.RangeWeapon.Reloading(_player.WeaponHolder.CurrentWeapon);
            }

            if (_player.Fire != null)
                foreach (var input in FireInput)
                    if (Input.GetKey(input.Key))
                        _player.Fire.Apply(input.FireType);

            if (_player.AbilityHolder != null)
                foreach (var input in AbilityInput)
                    if (Input.GetKeyUp(input.Key))
                        _player.AbilityHolder.Apply(input.Index);
        }
    }
}