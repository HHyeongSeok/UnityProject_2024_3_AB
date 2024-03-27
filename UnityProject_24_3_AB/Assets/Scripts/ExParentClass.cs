using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExParentClass : MonoBehaviour
{
    //protected로 선언된 변수는 같은 클래스 및 파생 클래스에서 접근
    protected int protectedValueParent;
}
public class ExChildClass : ExParentClass
{
    void Start()
    {
        Debug.Log(protectedValueParent);
    }
}
