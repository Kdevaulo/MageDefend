using System;

using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    [Serializable]
    public class SpellConfig
    {
        public SpellUIView SpellUIPrefab;
        public SpellView SpellPrefab;
        public Spell Spell;
        public string Id;
    }
}