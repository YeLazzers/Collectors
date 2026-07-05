using UnityEngine;

namespace YeLazzers.Game
{
    public class ModeStateMachine
    {
        private readonly PlayerInputRouter _router;

        private IGameMode _active;

        public ModeStateMachine(PlayerInputRouter router)
        {
            _router = router;

            _router.Moved += OnPointerMoved;
            _router.LmbClicked += OnLmbDown;
            _router.RmbClicked += OnRmbDown;
        }

        public void Activate(IGameMode mode)
        {
            if (_active == mode)
                return;

            _active?.OnExit();
            _active = mode;
            _router.SetRaycastLayer(mode.RaycastLayer);
            _active.OnEnter(_router.RaycastAtLastScreenPosition());
        }

        private void OnPointerMoved(PointerContext context)
        {
            _active?.OnMouseMove(context);
        }

        private void OnLmbDown(PointerContext context)
        {
            _active?.OnLmbDown(context);
        }

        private void OnRmbDown(PointerContext context)
        {
            _active?.OnRmbDown(context);
        }
    }
}
