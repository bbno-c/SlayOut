using Objects;
using System;
using Core;
using System.Collections.Generic;

namespace Controllers
{
    public interface IAbilityMenuView : IView
    {
        List<AbilityInfo> AbilityStats { get; set; }
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

            }
            // передать класс настроек во вьюху
            // распарсить во вьюху десерриализованный здесь ?(где-то на загрузке) PlayerAbilityStats, если он есть
        }

        public void OnOpen(IAbilityMenuView view)
        {
            _view = view;
            _view.InitPanel();

            _view.DiscardEvent += OnDiscard;
            _view.SaveEvent += OnSave;
        }

        public void OnClose(IAbilityMenuView view)
        {
            _view.DiscardEvent -= OnDiscard;
            _view.SaveEvent -= OnSave;

            _game.PlayerAbilityStats.AbilityStatsList = _view.AbilityStats;
            _view = null;
        }

        private void OnDiscard()
        {
            _view?.MenuView.Open(new MenuController(_game));
            _view?.Close(this);
            
        }

        private void OnSave()
        {
            //
            _view?.MenuView.Open(new MenuController(_game));
            _view?.Close(this);
        }
    }
}