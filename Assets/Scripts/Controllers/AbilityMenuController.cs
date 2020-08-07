using Objects;
using System;
using Core;
using System.Collections.Generic;

namespace Controllers
{
    public interface IAbilityMenuView : IView
    {
        List<AbilityInfo> AbilityStatsList { get; set; }
        IMenuView MenuView { get; }
        void InitPanel();
        event Action DiscardEvent;
        event Action SaveEvent;
    }

    public class AbilityMenuController : IController<IAbilityMenuView>
    {
        private readonly IGame _game;

        private IAbilityMenuView _view;

        public AbilityMenuController(IGame game)
        {
            _game = game;

            if(game.PlayerAbilityStats != null)
            {
                _view.AbilityStatsList = _game.PlayerAbilityStats.AbilityStatsList;
            }
        }

        public void OnOpen(IAbilityMenuView view)
        {
            _view = view;
            _view?.InitPanel();

            _view.DiscardEvent += OnDiscard;
            _view.SaveEvent += OnSave;
        }

        public void OnClose(IAbilityMenuView view)
        {
            _view.DiscardEvent -= OnDiscard;
            _view.SaveEvent -= OnSave;
            
            _view = null;
        }

        private void OnDiscard()
        {
            _view?.MenuView.Open(new MenuController(_game));
            _view?.Close(this);
        }

        private void OnSave()
        {
            _game.PlayerAbilityStats.AbilityStatsList = _view.AbilityStatsList;
            _view?.MenuView.Open(new MenuController(_game));
            _view?.Close(this);
        }
    }
}