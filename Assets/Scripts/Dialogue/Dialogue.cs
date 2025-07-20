using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public string[] dialogueText;
    public List<Response> response;
    public string[] loopingDialogue;
    public ItemProfile itemToGive;
    public ItemProfile desiredItem;
}
