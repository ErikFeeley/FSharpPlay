using System;
using System.Linq;

namespace LibraryWars
{
    public class DigPow
    {
        // my shitty first implementation
        public static long DigPowGuy(int n, int p)
        {
            var origN = n;
            var newP = p;
            var array = n.ToString().Select(x => int.Parse(x.ToString())).ToArray();

            for (var index = 0; index < array.Length; index++)
            {
                array[index] = (int)Math.Pow(array[index], newP);
                newP++;
            }
            var sum = array.Sum();

            var target = sum / origN;

            if (target * origN == sum) return target;

            return -1;
        }

        // much more better versions
        public static long DigPowBetter(int n, int p)
        {
            var sum = Convert.ToInt32(n.ToString()
                .Select(s => Math.Pow(int.Parse(s.ToString()), p++))
                .Sum());

            return sum % n == 0 ? sum / n : -1;
        }


    }
}
