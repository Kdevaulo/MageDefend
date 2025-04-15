namespace Kdevaulo.MageDefend.Model
{
    public class SpellParams
    {
        public string Id;
        public Spell Spell;

        public SpellParams(string id, Spell spell)
        {
            Id = id;
            Spell = spell;
        }
    }
}