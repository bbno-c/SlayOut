using Core;

public interface IWeaponPanelView : IView
    {
        
        IWeaponPanelView WeaponPanelView { get; }
    }

public class WeaponPanelController : IController<IWeaponPanelView>
{
        private IWeaponPanelView _view;
        
        private readonly IGame _game;
        
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
            
            _view = null;
        }
        
        private void CreatePanel(WeaponInfo weapon)
        {
            _view?.InitPanel(weapon);
        }
}
