using Core;

namespace Controllers
{
    public interface IWeaponPanelView : IView
    {
        void InitPanel(WeaponInfo weapon);
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
            _view = view;
        }

        public void OnClose(IWeaponPanelView view)
        {
            _game.Player.WeaponHolder.ElementAdded -= CreatePanel;
            _view = null;
        }
        
        private void CreatePanel(WeaponInfo weapon)
        {
            _view?.InitPanel(weapon);
        }
    }
}