using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  이벤트에 영향을 끼치는 변수의 골자를 정의한 클래스예요. */
public class Variable
{
    public delegate float EquationDel(float x);
    public List<string> conditions = new List<string>();
}
