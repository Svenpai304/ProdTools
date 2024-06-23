using System;
using System.Collections.Generic;

[Serializable]
public struct CardEffect
{
    public string name;
    public string[] triggers;
    public string[] targets;
    public string[] effects;
    public string[] endActions;

}
