using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//유틸리티 함수를 저장하는 정적 클래스
public static class Utility
{
    //리스트를 랜덤으로 섞는 함수
    public static List<T> Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            System.Random random = new System.Random(Guid.NewGuid().GetHashCode());
            int rnd = random.Next(0, i);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
        return list;
    }

    //랜덤 함수
    public static int RandomValue(){
        System.Random r = new System.Random();
        return r.Next(0,99);
    }

    //유닉스 시간 변환기
    public static int TransferUnixToTime(){
         var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
         int _timestamp  = (int)(unixTimestamp%86400L);
         return (int)(_timestamp/3600);
    }

    //각국 표준시 변환
    public static int TransferStandardTime(int basic_standard_time, string culture){
        if(culture.Equals("KO",StringComparison.OrdinalIgnoreCase)){
            return basic_standard_time+9;
        }
        return basic_standard_time;
    }

    //원 정렬 함수
    public static Vector2[] makeCircle(Vector2 p_origin, int num_objects, float p_radius, ref double ceta){
        Vector2 pibot = p_origin;
        Vector2[] result = new Vector2[num_objects];
        ceta = 2 * Math.PI / num_objects;
        int i = 0;
        while(i < num_objects){
            pibot = p_origin + new Vector2((float)(p_radius * Math.Cos((i * ceta))),
                                    (float)(p_radius * Math.Sin((i * ceta))));
            result[i] = pibot;
            i++;
        }
        return result;
    }

    //범위 체크
    public static bool checkInRange(Vector2 p_check, Vector4 p_range){
        if(p_check.x >= p_range.x && p_check.x <= p_range.y &&
            p_check.y >= p_range.z && p_check.y <= p_range.w)
                return true;
        return false;
    }
}
