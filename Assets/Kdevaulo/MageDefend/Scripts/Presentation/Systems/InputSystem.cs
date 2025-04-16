using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kdevaulo.MageDefend.Presentation
{
    public class InputSystem
    {
        public event Action<Vector2> MovePerformed;
        public event Action MoveCanceled;

        public event Action NextPerformed;
        public event Action NextCanceled;

        public event Action PreviousPerformed;
        public event Action PreviousCanceled;

        public event Action AttackPerformed;
        public event Action AttackCanceled;

        private readonly PlayerInput _playerInput;

        private readonly Dictionary<string, InputActionHandlers> _actionsMap =
            new Dictionary<string, InputActionHandlers>();

        public InputSystem(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            RegisterAction("Move", ctx => MovePerformed?.Invoke(ctx.ReadValue<Vector2>()),
                _ => MoveCanceled?.Invoke());

            RegisterAction("Attack", ctx => AttackPerformed?.Invoke(),
                _ => AttackCanceled?.Invoke());

            RegisterAction("Previous", _ => PreviousPerformed?.Invoke(), _ => PreviousCanceled?.Invoke());
            RegisterAction("Next", _ => NextPerformed?.Invoke(), _ => NextCanceled?.Invoke());
        }

        public void Dispose()
        {
            foreach (var container in _actionsMap)
            {
                var inputAction = container.Value.Action;
                inputAction.performed -= container.Value.Performed;
                inputAction.canceled -= container.Value.Canceled;
            }

            _actionsMap.Clear();
        }

        private void RegisterAction(string actionName, Action<InputAction.CallbackContext> performed,
            Action<InputAction.CallbackContext> cancelled)
        {
            var action = _playerInput.actions.FindAction(actionName, true);
            _actionsMap[actionName] = new InputActionHandlers(performed, cancelled, action);
            action.performed += performed;
            action.canceled += cancelled;
        }
    }

    public class InputActionHandlers
    {
        public Action<InputAction.CallbackContext> Performed { get; }
        public Action<InputAction.CallbackContext> Canceled { get; }
        public InputAction Action { get; }

        public InputActionHandlers(Action<InputAction.CallbackContext> performed,
            Action<InputAction.CallbackContext> cancelled, InputAction action)
        {
            Action = action;
            Performed = performed;
            Canceled = cancelled;
        }
    }
}