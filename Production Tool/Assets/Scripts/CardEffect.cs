using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public struct CardEffect
{
    public string name;
    public string[] triggers;
    public string[] targets;
    public float targetNumericalValue;
    public string targetTextValue;

    public string[] actions;
    public float actionNumericalValue;
    public string actionTextValue;

    public string[] endActions;

}
