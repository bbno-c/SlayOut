﻿using System;
 using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	 public class MenuView : BaseView<IMenuView>, IMenuView
	 {
		 protected override IMenuView View => this;

		[SerializeField]
		private AbilityMenuView _abilityMenuView;
		public IAbilityMenuView AbilityMenuView => _abilityMenuView;

		public Text NickNameText;

		 public event Action<string> PlayEvent;
		public event Action AbilityMenuEvent;

		public void ActionPlay()
		 {
			 PlayEvent?.Invoke(NickNameText.text);
		 }

		public void AbilityMenu()
		{
			AbilityMenuEvent?.Invoke();
		}
	}
}