using System;

using Kdevaulo.MageDefend.Model;

namespace Kdevaulo.MageDefend.Presentation
{
    [Serializable]
    public class SpellConfig
    {
        public SpellView SpellView;
        public Spell Spell;
        public string Id;
    }
}