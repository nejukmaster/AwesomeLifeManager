using System.Collections;
using System.Collections.Generic;
using System;

public static class Utility
{
    public static List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }
}
