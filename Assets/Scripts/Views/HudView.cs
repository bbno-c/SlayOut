using Controllers;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
		protected override IHudView View => this;

		[SerializeField]
		private WeaponPanelView _weaponPanelView;
		public IWeaponPanelView WeaponPanelView => _weaponPanelView;

		[SerializeField]
		private EndGameView _endGameView;
		public IEndGameView EndGameView => _endGameView;

		public TextMeshProUGUI ScoreText;
		public TextMeshProUGUI HitpointsText;

		public void SetScore(int value)
		{
			ScoreText.text = value.ToString();
		}

		public void SetHealth(float value)
		{
			HitpointsText.text = Mathf.RoundToInt(value) + "%";
		}
	}
}
