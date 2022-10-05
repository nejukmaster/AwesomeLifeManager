using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Data;

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

    public static float Mapping(float p_num, Vector2 from, Vector2 to)
    {
        if (p_num != 0) {
            return (to.y - to.x) * p_num / (from.y - from.x) + to.x;
        }
        else
            return 0;
    }

    public static Vector2 Mapping(Vector2 p_vec2, Vector4 from, Vector4 to)
    {
        return new Vector2(Mapping(p_vec2.x, new Vector2(from.x, from.y), new Vector2(to.x, to.y)), Mapping(p_vec2.y, new Vector2(from.z, from.w), new Vector2(to.z, to.w)));
    }

    public static float pathFunction(float p_pos, float p_delta, Vector2 p_range)
    {
        float r = p_pos + p_delta;
        if (p_range.x > r)
        {
            r = Mathf.Abs(r) + p_range.x;
        }
        else if (p_range.y < r)
        {
            r = -1 * Mathf.Abs(r) + p_range.y;
        }
        return r;
    }
}
