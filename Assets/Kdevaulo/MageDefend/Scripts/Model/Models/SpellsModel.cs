using System.Linq;

namespace Kdevaulo.MageDefend.Model
{
    public class SpellsModel
    {
        private readonly SpellParameter[] _spellParameters;

        public SpellsModel(SpellParameter[] spellParameters)
        {
            _spellParameters = spellParameters;
        }

        public bool TryGetSpellParameters(string id, out SpellParameter spellParameter)
        {
            spellParameter = _spellParameters.FirstOrDefault(x => x.Id == id);

            return spellParameter != null;
        }
    }
}