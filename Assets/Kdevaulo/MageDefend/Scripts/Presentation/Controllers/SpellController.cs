using System.Collections.Generic;

using Kdevaulo.MageDefend.Model;

using UnityEditor.SearchService;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellController
    {
        private Dictionary<SpellModel, SpellView> _launchedSpells = new Dictionary<SpellModel, SpellView>();

        private readonly IPlayerContextProvider _playerContextProvider;
        private readonly SpellsModel _spellsModel;
        private readonly InputSystem _input;
        private readonly SpellsData _spellsData;
        private readonly Transform _parent;
        private readonly Transform _target;

        public SpellController(InputSystem input, SpellsModel spellsModel, SpellsData spellsData, Transform parent,
            IPlayerContextProvider playerContextProvider)
        {
            _playerContextProvider = playerContextProvider;
            _spellsModel = spellsModel;
            _spellsData = spellsData;
            _parent = parent;
            _input = input;
        }

        public void Initialize()
        {
            _input.AttackPerformed += Cast;
            _input.AttackCanceled += CastCancel;
        }

        public void Dispose()
        {
            _input.AttackPerformed -= Cast;
            _input.AttackCanceled -= CastCancel;
        }

        public void Tick()
        {
            var itemsToRemove = new List<SpellModel>();

            foreach (var spellPair in _launchedSpells)
            {
                var spellModel = spellPair.Key;
                var spellView = spellPair.Value;

                var step = Time.deltaTime * spellModel.MoveDirection;
                spellModel.Move(step);

                spellView.Move(spellModel.Position.ToUnity());

                spellModel.DecreaseLifetime(Time.deltaTime);

                if (spellModel.IsFinished)
                {
                    itemsToRemove.Add(spellModel);
                }
            }

            foreach (var item in itemsToRemove)
            {
                var view = _launchedSpells[item];
                view.gameObject.SetActive(false);
                Object.Destroy(view.gameObject);
                _launchedSpells.Remove(item);
            }
        }

        private void Cast()
        {
            var id = "Fireball";

            if (_spellsModel.CanCast() && _spellsModel.TryCreateSpell(id, out var spell))
            {
                var targetPosition = _playerContextProvider.Target.position;
                var prefab = _spellsData.GetSpellPrefab(id);
                var view = Object.Instantiate(prefab, targetPosition, Quaternion.identity, _parent);

                spell.SetDirection(_playerContextProvider.GetDirection().ToNumerics());
                spell.SetPosition(targetPosition.ToNumerics());

                _launchedSpells.Add(spell, view);
            }
        }

        private void CastCancel()
        {
        }
    }
}