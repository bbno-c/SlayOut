using Core;

namespace Controllers
{
    public interface IHudView : IView
    {
        void SetScore(int value);
        void SetHealth(float value);

        IEndGameView EndGameView { get; }
        IWeaponPanelView WeaponPanelView { get; }
    }

    public class HudController : IController<IHudView>
    {
        private readonly IGame _game;
        
        private IHudView _view;

        public HudController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IHudView view)
        {
            _game.EndGameEvent += OnEndGame;
            _game.ScoreChangedEvent += OnScoreChanged;
            _game.PlayerHealthChangeEvent += OnPlayerHealthChanged;
            _view = view;

            _view.WeaponPanelView?.Open(new WeaponPanelController(_game));
        }

        public void OnClose(IHudView view)
        {
            _game.EndGameEvent -= OnEndGame;
            _game.ScoreChangedEvent -= OnScoreChanged;
            _game.PlayerHealthChangeEvent -= OnPlayerHealthChanged;

            _view = null;
        }

        private void OnEndGame()
        {
            _view?.Close(this);
        }

        private void OnScoreChanged(int score)
        {
            _view.SetScore(score);
        }

        private void OnPlayerHealthChanged(float health)
        {
            _view?.SetHealth(health);
        }
    }
}
