using System.Collections.Generic;

namespace Kdevaulo.MageDefend.Model
{
    public class SpellsModel
    {
        private Dictionary<string, Spell> _spellParams = new Dictionary<string, Spell>();

        public void Initialize(SpellParams[] spellParams)
        {
            foreach (var item in spellParams)
            {
                _spellParams[item.Id] = item.Spell;
            }
        }

        public void Dispose()
        {
            _spellParams.Clear();
        }

        public bool TryCreateSpell(string id, out SpellModel model)
        {
            model = null;

            if (!_spellParams.TryGetValue(id, out var data))
                return false;

            if (data.IsLocked)
                return false;

            model = new SpellModel(data);
            return true;
        }

        public bool CanCast()
        {
            return true;
        }
    }
}