using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;
using UnityEngine.Assertions;

namespace Kdevaulo.MageDefend.Presentation
{
    [CreateAssetMenu(fileName = nameof(SpellsConfig), menuName = nameof(MageDefend) + "/" + nameof(SpellsConfig))]
    public class SpellsConfig : ScriptableObject
    {
        [field: SerializeField] public SpellVisual[] Configs { get; private set; }

        public SpellView GetSpellPrefab(string id)
        {
            var config = Configs.FirstOrDefault(x => x.SpellParameter.Id == id);
            Assert.IsNotNull(config);
            return config.SpellPrefab;
        }

        public SpellParameter[] GetSpellParams()
        {
            var parameters = Configs.Select(config => config.SpellParameter);

            return parameters.ToArray();
        }
    }
}