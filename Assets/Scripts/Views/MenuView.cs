﻿using System;
 using Controllers;
 using UnityEngine.UI;

namespace Views
{
	 public class MenuView : BaseView<IMenuView>, IMenuView
	 {
		 protected override IMenuView View => this;

		 public Text NickNameText;

		 public event Action<string> PlayEvent;

		 public void ActionPlay()
		 {
			 PlayEvent?.Invoke(NickNameText.text);
		 }
	 }
}