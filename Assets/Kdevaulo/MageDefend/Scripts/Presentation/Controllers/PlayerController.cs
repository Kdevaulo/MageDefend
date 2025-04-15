using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class PlayerController : IPlayerContextProvider
    {
        public Transform Target { get; }

        private readonly InputSystem _playerInput;
        private readonly PlayerView _playerView;
        private readonly UnitModel _playerModel;

        private const float ControlDeadZone = 0.1f;

        private Vector3Int _moveDirection;
        private Direction _currentDirection;
        private bool _canMove;

        public PlayerController(InputSystem playerInput, UnitModel playerModel, PlayerView playerView)
        {
            _playerInput = playerInput;
            _playerModel = playerModel;
            _playerView = playerView;

            Target = playerView.transform;
        }

        public void Initialize()
        {
            _playerInput.MovePerformed += Move;
            _playerInput.MoveCanceled += CancelMove;

            _moveDirection = Vector3Int.forward;
        }

        public void Dispose()
        {
            _playerInput.MovePerformed -= Move;
            _playerInput.MoveCanceled -= CancelMove;
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

        public Vector3 GetDirection()
        {
            return _moveDirection;
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
            _canMove = false;
        }
    }
}