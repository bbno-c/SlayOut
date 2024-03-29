using System;
using Core;

namespace Controllers
{
    public interface IMenuView : IView
    {
        event Action<string> PlayEvent;
        event Action AbilityMenuEvent;
        IAbilityMenuView AbilityMenuView { get; }
    }

    public class MenuController : IController<IMenuView>
    {
        private readonly IGame _game;

        private IMenuView _view;

        public MenuController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IMenuView view)
        {
            view.PlayEvent += OnPlay;
            view.AbilityMenuEvent += OnAbilityMenu;
            _view = view;
        }

        public void OnClose(IMenuView view)
        {
            view.PlayEvent -= OnPlay;
            view.AbilityMenuEvent -= OnAbilityMenu;
            _view = null;
        }

        private void OnPlay(string nick)
        {
            _view?.Close(this);
            _game.NewGame();
        }

        private void OnAbilityMenu()
        {
            _view?.AbilityMenuView?.Open(new AbilityMenuController(_game));
            _view?.Close(this);
        }
    }
}