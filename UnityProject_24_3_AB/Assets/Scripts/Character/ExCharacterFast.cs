using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterFast : ExCharacter
{
    //override ¿Á¡§¿«
    protected override void Move()
    {
        transform.Translate(
            Vector3.forward * speed * 2 * Time.deltaTime);
    }
}
