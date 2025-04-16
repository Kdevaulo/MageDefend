using System;

using Kdevaulo.MageDefend.Model;

namespace Kdevaulo.MageDefend.Presentation
{
    [Serializable]
    public class SpellVisual
    {
        public SpellUIView SpellUIPrefab;
        public SpellView SpellPrefab;
        public SpellParameter SpellParameter;
    }
}