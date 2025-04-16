using System;
using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Kdevaulo.MageDefend.Presentation
{
    public class SpellsController
    {
        private readonly IPlayerContextProvider _playerContextProvider;
        private readonly LocationController _locationController;
        private readonly SpellsModel _spellsModel;
        private readonly InputSystem _input;
        private readonly SpellsConfig _spellsConfig;
        private readonly Transform _spellUIParent;
        private readonly Transform _parent;
        private readonly Transform _target;

        private readonly Dictionary<string, SpellUIView> _preparedSpells = new Dictionary<string, SpellUIView>();
        private readonly List<SpellController> _spellControllers = new List<SpellController>();
        private readonly List<string> _spellsIds = new List<string>();

        private string _chosenID;

        public SpellsController(InputSystem input, GameContext context,
            IPlayerContextProvider playerContextProvider, LocationController locationController)
        {
            _playerContextProvider = playerContextProvider;
            _locationController = locationController;
            _spellUIParent = context.UISpellParent;
            _spellsConfig = context.SpellsConfig;
            _parent = context.Parent;
            _input = input;

            _spellsModel = new SpellsModel(context.SpellsConfig.GetSpellParams());
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
            var itemsToRemove = new List<SpellController>();

            foreach (var spellController in _spellControllers)
            {
                spellController.Tick();

                if (spellController.IsFinished)
                {
                    itemsToRemove.Add(spellController);
                }
            }

            foreach (var item in itemsToRemove)
            {
                item.Dispose();
                _spellControllers.Remove(item);
            }

            itemsToRemove.Clear();
        }

        private void CreateUISpells()
        {
            foreach (var config in _spellsConfig.Configs)
            {
                var view = Object.Instantiate(config.SpellUIPrefab, _spellUIParent);
                var id = config.SpellParameter.Id;
                _preparedSpells[id] = view;
                _spellsIds.Add(id);
            }
        }

        private void Cast()
        {
            if (_spellsModel.TryGetSpellParameters(_chosenID, out var spell))
            {
                var targetPosition = _locationController.PlayerView.transform.position;
                var prefab = _spellsConfig.GetSpellPrefab(_chosenID);
                var view = Object.Instantiate(prefab, targetPosition, Quaternion.identity, _parent);
                var controller = new SpellController(_playerContextProvider.GetDirection(), spell, view,
                    _locationController);

                _spellControllers.Add(controller);
                controller.Initialize();
            }
        }

        private void ChooseNext()
        {
            Choose(true);
        }

        private void ChoosePrevious()
        {
            Choose(false);
        }

        private void Choose(bool isNext)
        {
            var index = _spellsIds.IndexOf(_chosenID);
            index += isNext ? 1 : -1;
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