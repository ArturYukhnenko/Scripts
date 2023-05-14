using UnityEngine;

namespace Storage
{
    public interface IItem {
        string Name { get; }
        Sprite Icon { get; }
        int Price { get; }
    }
}