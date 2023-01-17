using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Quiz/Character", order = 1)]
public class Character : ScriptableObject
{


    public Sprite sprite;

    public bool answered = false;

}
