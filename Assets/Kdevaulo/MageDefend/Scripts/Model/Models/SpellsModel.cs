using System.Linq;

namespace Kdevaulo.MageDefend.Model
{
    public class SpellsModel
    {
        private readonly SpellParameters[] _spellParameters;

        public SpellsModel(SpellParameters[] spellParameters)
        {
            _spellParameters = spellParameters;
        }

        public bool TryGetSpellParameters(string id, out SpellParameters spellParameters)
        {
            spellParameters = _spellParameters.FirstOrDefault(x => x.Id == id);

            return spellParameters != null;
        }

        public bool CanCast()
        {
            return true;
        }
    }
}