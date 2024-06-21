using System;
using System.Collections.Generic;

[Serializable]
public struct CardEffect
{
    public string name;
    public List<string> triggers;
    public List<string> targets;
    public List<string> effects;
    public List<string> endActions;

}
