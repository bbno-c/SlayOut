using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Controllers;
using UnityEngine.UI;
using Objects;
using Unity.Mathematics;
using System;

namespace Views
{
    public class WeaponStateBarView : BaseView<IWeaponStateBarView>, IWeaponStateBarView
    {
        protected override IWeaponStateBarView View => this;

        public Slider Slider;
        private float _time;
        private float _timer;

        public void SetTimer(float time)
        {
            _time = time;
            _timer = time;
            Slider.value = Slider.maxValue;
            Slider.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                Slider.value -= Mathf.Clamp01(Time.deltaTime / _time); // Equals to -> 0.01f * ((Time.deltaTime*100)/_time);
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    _time = 1;
                    Slider.gameObject.SetActive(false);
                }
            }
        }
    }
}