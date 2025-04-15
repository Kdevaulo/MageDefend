using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class PlayerController
    {
        private readonly InputHandler _inputHandler;
        private readonly PlayerView _playerView;
        private readonly UnitModel _playerModel;

        private const float ControlDeadZone = 0.1f;

        private Vector3Int _moveDirection;
        private Direction _currentDirection;
        private bool _canMove;

        public PlayerController(InputHandler inputHandler, UnitModel playerModel, PlayerView playerView)
        {
            _inputHandler = inputHandler;
            _playerModel = playerModel;
            _playerView = playerView;
        }

        public void Initialize()
        {
            _inputHandler.MovePerformed += Move;
            _inputHandler.MoveCanceled += CancelMove;
        }

        public void Dispose()
        {
            _inputHandler.MovePerformed -= Move;
            _inputHandler.MoveCanceled -= CancelMove;
        }

        public void Tick()
        {
            if (_canMove)
            {
                var step = Time.deltaTime * _moveDirection.ToNumerics();
                _playerModel.Move(step);
                _playerView.Move(_playerModel.Position.ToUnity());
            }
        }

        private void Move(Vector2 offset)
        {
            var roundedVector = new Vector2Int(
                DirectionMap.RoundClamp(offset.x, ControlDeadZone),
                DirectionMap.RoundClamp(offset.y, ControlDeadZone));

            if (roundedVector == Vector2Int.zero)
            {
                CancelMove();
                return;
            }

            _moveDirection = new Vector3Int(roundedVector.x, 0, roundedVector.y);
            _canMove = true;

            _currentDirection = DirectionMap.GetDirection(roundedVector);
            var rotation = DirectionMap.GetRotation(_currentDirection);
            _playerView.Rotate(rotation);
        }

        private void CancelMove()
        {
            _moveDirection = Vector3Int.zero;
            _currentDirection = Direction.None;
            _canMove = false;
        }
    }
}