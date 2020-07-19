using Core;
using Objects;

namespace Controllers
{
    public interface IWeaponPanelView : IView
    {
        void InitPanel(WeaponInfo weapon);
        void UpdateWeapon(WeaponInfo weapon);
        void OnWeaponChange(WeaponInfo previous, WeaponInfo current);
    }

    public class WeaponPanelController : IController<IWeaponPanelView>
    {
        private readonly IGame _game;
        private IWeaponPanelView _view;

        public WeaponPanelController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IWeaponPanelView view)
        {
            _game.Player.WeaponHolder.ElementAdded += CreatePanel;
            _game.Player.WeaponHolder.WeaponChange += OnWeaponChange;
            _view = view;
        }

        public void OnClose(IWeaponPanelView view)
        {
            _game.Player.WeaponHolder.ElementAdded -= CreatePanel;
            _game.Player.WeaponHolder.WeaponChange -= OnWeaponChange;
            _view = null;
        }
        
        private void CreatePanel(WeaponInfo weapon)
        {
            _view?.InitPanel(weapon);

            if (weapon is RangeWeaponInfo)
            {
                RangeWeaponInfo wd = (RangeWeaponInfo)weapon;
                wd.AddAmmoEvent += _view.UpdateWeapon;
                wd.AmmoPickupEvent += _view.UpdateWeapon;
                wd.CreateBulletEvent += _view.UpdateWeapon;
                wd.AmmoPickupEventNotReload += _view.UpdateWeapon;
            }
        }

        private void OnWeaponChange(WeaponInfo current)
        {
            _view?.OnWeaponChange(_game.Player.WeaponHolder.PreviousWeapon, current);
        }
    }
}