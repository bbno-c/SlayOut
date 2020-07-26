using System;
using UnityEngine;

namespace Objects
{
    [Serializable]
    public struct FireInput
    {
        public KeyCode Key;
        public FireType FireType;
    }
    public class InputController : MonoBehaviour
    {
        public FireInput[] FireInput;
        private Character _player;

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
                _player.Movement.MoveDirection(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f));

                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_player.transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                _player.Movement.LookDirection(angle);

                if (Input.GetKeyDown(KeyCode.Mouse1))
                    _player.Movement.Jump();
            }

            if (_player.Fire != null)
                foreach (var input in FireInput)
                    if (Input.GetKey(input.Key))
                        _player.Fire.Apply(input.FireType);

            if (_player.WeaponHolder != null)
                _player.WeaponHolder.ChangeWeapon(Input.GetAxis("Mouse ScrollWheel"));

            if (_player.WeaponHolder != null)
                if (Input.GetKeyDown(KeyCode.R))
                     _player.WeaponHolder.RangeWeapon.Reloading(_player.WeaponHolder.CurrentWeapon);
        }
    }
}