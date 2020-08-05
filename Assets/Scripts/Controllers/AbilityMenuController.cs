using Objects;
using System;
using Core;
using System.Collections.Generic;

namespace Controllers
{
    public interface IAbilityMenuView : IView
    {
        List<AbilityInfo> AbilityStats { get; set; }
        //event Action<string> PlayEvent;

        void InitPanel();
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
            //view.PlayEvent += OnPlay;
            _view = view;
            _view.InitPanel();
        }

        public void OnClose(IAbilityMenuView view)
        {
            //view.PlayEvent -= OnPlay;
            _game.PlayerAbilityStats.AbilityStatsList = _view.AbilityStats;
            _view = null;
        }
    }
}