using UnityEngine;

namespace Core.Environment
{
    public interface IPickable 
    {
        Transform BoxTransform { get; }
        PickableBoxType BoxType { get; }
        bool IsPickable { get; set; }
        void UnPickFromFactory();
    }
}