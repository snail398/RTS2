using UnityEngine;

namespace Battle.Player
{
    public interface ISelectable
    {
        Transform Transform { get; }
        int Width { get; }
        int Height { get; }
    }
}