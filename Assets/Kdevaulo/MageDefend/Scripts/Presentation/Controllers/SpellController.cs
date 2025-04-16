using System;
using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEditor.SearchService;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellController
    {
        public static Dictionary<SpellModel, SpellView> LaunchedSpells = new Dictionary<SpellModel, SpellView>();

        private readonly IPlayerContextProvider _playerContextProvider;
        private readonly SpellsModel _spellsModel;
        private readonly InputSystem _input;
        private readonly SpellsData _spellsData;
        private readonly Transform _spellUIParent;
        private readonly Transform _parent;
        private readonly Transform _target;

        private Dictionary<string, SpellUIView> _preparedSpells = new Dictionary<string, SpellUIView>();
        private List<string> _spellsIds = new List<string>();

        private string _chosenID;

        public SpellController(InputSystem input, SpellsModel spellsModel, GameContext context,
            IPlayerContextProvider playerContextProvider)
        {
            _playerContextProvider = playerContextProvider;
            _spellUIParent = context.UISpellParent;
            _spellsModel = spellsModel;
            _spellsData = context.SpellsData;
            _parent = context.Parent;
            _input = input;
        }

        public void Initialize()
        {
            _input.PreviousPerformed += ChoosePrevious;
            _input.AttackPerformed += Cast;
            _input.NextPerformed += ChooseNext;

            CreateUISpells();
            _chosenID = _spellsIds.First();
            _preparedSpells[_chosenID].Enable();
        }

        public void Dispose()
        {
            _input.PreviousPerformed -= ChoosePrevious;
            _input.AttackPerformed -= Cast;
            _input.NextPerformed -= ChooseNext;

            foreach (var pair in _preparedSpells)
            {
                Object.Destroy(pair.Value.gameObject);
            }

            _preparedSpells.Clear();
        }

        public void Tick()
        {
            var itemsToRemove = new List<SpellModel>();

            foreach (var spellPair in LaunchedSpells)
            {
                var spellModel = spellPair.Key;
                var spellView = spellPair.Value;

                var step = Time.deltaTime * spellModel.MoveDirection * spellModel.MoveSpeed;
                spellView.Move(step.ToUnity());

                spellModel.DecreaseLifetime(Time.deltaTime);

                if (spellModel.IsFinished)
                {
                    itemsToRemove.Add(spellModel);
                }
            }

            foreach (var item in itemsToRemove)
            {
                var view = LaunchedSpells[item];
                Object.Destroy(view.gameObject);
                LaunchedSpells.Remove(item);
            }
        }

        private void CreateUISpells()
        {
            foreach (var config in _spellsData.Configs)
            {
                var view = Object.Instantiate(config.SpellUIPrefab, _spellUIParent);
                _preparedSpells[config.Id] = view;
                _spellsIds.Add(config.Id);
            }
        }

        private void Cast()
        {
            if (_spellsModel.CanCast() && _spellsModel.TryCreateSpell(_chosenID, out var spell))
            {
                var targetPosition = _playerContextProvider.Target.position;
                var prefab = _spellsData.GetSpellPrefab(_chosenID);
                var view = Object.Instantiate(prefab, targetPosition, Quaternion.identity, _parent);

                spell.SetDirection(_playerContextProvider.GetDirection().ToNumerics());
                LaunchedSpells.Add(spell, view);
            }
        }

        private void ChooseNext()
        {
            ChooseOffset(1);
        }

        private void ChoosePrevious()
        {
            ChooseOffset(-1);
        }

        private void ChooseOffset(int direction)
        {
            var index = _spellsIds.IndexOf(_chosenID);
            index += direction;
            var clampedIndex = Math.Clamp(index, 0, _spellsIds.Count - 1);
            _chosenID = _spellsIds[clampedIndex];

            foreach (var id in _spellsIds)
            {
                _preparedSpells[id].Disable();
            }

            _preparedSpells[_chosenID].Enable();
        }
    }
}