using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterManager : MonoBehaviour
{
    public List<ExCharacter> CharacterList = new List<ExCharacter>();


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < CharacterList.Count; i++)
            {
                CharacterList[i].DestoryCharacter();
            }
        }
    }
}
