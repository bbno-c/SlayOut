using Core;
using Objects;
using System.Collections.Generic;

namespace Controllers
{
    public interface IAbilityPanelView : IView
    {
        void InitPanel();
    }

    public class AbilityPanelController : IController<IAbilityPanelView>
    {
        private readonly IGame _game;
        private IAbilityPanelView _view;

        public AbilityPanelController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IAbilityPanelView view)
        {
            view?.InitPanel();
            _view = view;
        }

        public void OnClose(IAbilityPanelView view)
        {
            
            _view = null;
        }
    }
}