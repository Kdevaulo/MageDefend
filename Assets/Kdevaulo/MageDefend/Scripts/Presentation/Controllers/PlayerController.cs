using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class PlayerController : IPlayerContextProvider
    {
        private const float ControlDeadZone = 0.1f;

        private readonly LocationController _locationController;
        private readonly SpellsController _spellsController;
        private readonly CameraFollower _cameraFollower;
        private readonly InputSystem _playerInput;
        private readonly UnitModel _playerModel;

        private Vector3Int _moveDirection;
        private PlayerView _playerView;
        private Quaternion _rotation;
        private Direction _currentDirection;
        private bool _canMove;

        public PlayerController(GameContext gameContext, InputSystem playerInput, LocationController locationController)
        {
            _locationController = locationController;
            _playerInput = playerInput;

            _cameraFollower = gameContext.CameraFollower;
            _playerModel = _locationController.PlayerModel;

            _spellsController = new SpellsController(_playerInput, gameContext, this, _locationController);
        }

        public void Initialize()
        {
            _playerInput.MovePerformed += Move;
            _playerInput.MoveCanceled += CancelMove;

            _moveDirection = Vector3Int.forward;

            _locationController.SpawnPlayer();
            _playerView = _locationController.PlayerView;
            _cameraFollower.SetTarget(_playerView.transform);

            _spellsController.Initialize();
        }

        public void Dispose()
        {
            _playerInput.MovePerformed -= Move;
            _playerInput.MoveCanceled -= CancelMove;

            _cameraFollower.SetTarget(null);

            _spellsController.Dispose();
        }

        public void Tick()
        {
            if (_canMove)
            {
                var velocity = _playerModel.MoveSpeed.Value * (Vector3) _moveDirection;
                _playerView.Move(velocity);
            }

            _playerView.SetRotation(_rotation);
            _cameraFollower.Tick();
            _spellsController.Tick();
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
            _rotation = DirectionMap.GetRotation(_currentDirection);
        }

        private void CancelMove()
        {
            _canMove = false;
        }
    }
}