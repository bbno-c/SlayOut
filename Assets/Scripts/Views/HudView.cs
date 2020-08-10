using Controllers;
using TMPro;
using UnityEngine;

namespace Views
{
	public class HudView : BaseView<IHudView>, IHudView
	{
		protected override IHudView View => this;

		[SerializeField]
		private EndGameView _endGameView;
		public IEndGameView EndGameView => _endGameView;

		[SerializeField]
		private WeaponPanelView _weaponPanelView;
		public IWeaponPanelView WeaponPanelView => _weaponPanelView;

		[SerializeField]
		private AbilityPanelView _abilityPanelView;
		public IAbilityPanelView AbilityPanelView => _abilityPanelView;

		[SerializeField]
		private WeaponStateBarView _weaponStateBarView;
		public IWeaponStateBarView WeaponStateBarView => _weaponStateBarView;

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
