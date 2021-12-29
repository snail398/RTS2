using System;
using System.Collections.Generic;
using GameObjectView;

[Serializable]
public class DefaultBases
{
    public List<DefaultBase> Bases;
}

[Serializable]
public class DefaultBase
{
    public byte PlayerId;
    public MineralZoneView MineralZoneView;
}