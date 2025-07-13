using UnityEngine;

[CreateAssetMenu(fileName = "Character Profile", menuName = "ScriptableObjects/CharaterProfile")]
public class CharacterProfile : ScriptableObject
{
    public string characterName;
    public Dialogue dialogue;
}
