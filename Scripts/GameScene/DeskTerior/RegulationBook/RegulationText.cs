using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegulationText
{
    public string Text { get; private set; }
    public TextObjectType ObjectType { get; private set; }

    public RegulationText(string text, TextObjectType objectType)
    {
        Text = text;
        ObjectType = objectType;
    }
}
