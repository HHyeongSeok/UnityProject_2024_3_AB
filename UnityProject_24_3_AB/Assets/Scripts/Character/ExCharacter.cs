using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacter : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    protected virtual void Move() //viryual 가상 함수로 만들어서 상속자에게 함수를 변환하도록 한다.
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void DestoryCharacter()
    {
        Destroy(gameObject);
    }
}
