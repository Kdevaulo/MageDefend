using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(SpellsData), menuName = nameof(MageDefend) + "/" + nameof(SpellsData))]
    public class SpellsData : ScriptableObject
    {
        [field: SerializeField] public SpellConfig[] Configs { get; private set; }

        public SpellView GetSpellPrefab(string id)
        {
            var config = Configs.FirstOrDefault(x => x.Id == id);
            Assert.IsNotNull(config);
            return config.SpellView;
        }

        public SpellParams[] GetSpellParams()
        {
            var spellParamsCollection = new List<SpellParams>(Configs.Length);

            foreach (var config in Configs)
            {
                var spellParams = new SpellParams(config.Id, config.Spell);
                spellParamsCollection.Add(spellParams);
            }

            return spellParamsCollection.ToArray();
        }
    }
}