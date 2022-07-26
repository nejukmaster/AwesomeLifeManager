using System.Collections;
using System.Collections.Generic;
using System;

//유틸리티 함수를 저장하는 정적 클래스
public static class Utility
{
    //리스트를 랜덤으로 섞는 함수
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

    //랜덤 함수
    public static int RandomValue(){
        Random r = new Random();
        return r.Next(0,99);
    }
}
