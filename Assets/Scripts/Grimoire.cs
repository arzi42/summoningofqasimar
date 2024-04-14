using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Grimoire
    {
        private static List<Demon> _summonedDemons = new List<Demon>();

        public static int Count => _summonedDemons.Count;

        public static bool Has(string name)
        {
            foreach (var demon in _summonedDemons)
            {
                if (demon.Name == name)
                    return true;
            }
            return false;
        }

        public static bool HasWithSigilAndSlot(string sigil, string slot)
        {
            foreach (var demon in _summonedDemons)
            {
                if (demon.Sigil == sigil && demon.Slot == slot)
                    return true;
            }
            return false;
        }
        
        public static Demon GetWithSigilAndSlot(string sigil, string slot)
        {
            foreach (var demon in _summonedDemons)
            {
                if (demon.Sigil == sigil && demon.Slot == slot)
                    return demon;
            }

            return new Demon();
        }

        
        public static void Add(Demon demon)
        {
            _summonedDemons.Add(demon);
        }

        public static Demon Get(int index)
        {
            return _summonedDemons[index];
        }
    }
}