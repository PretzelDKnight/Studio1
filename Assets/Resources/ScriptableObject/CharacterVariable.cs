using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterVariable : ScriptableObject
{
    public Character character;

    public void TurnStart()
    {
        character.BeginTurn();
    }

    public void TurnEnd()
    {
        character.EndTurn();
    }
}
