using Infrastructure.Container;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService : IService
    {
        public Vector3 Direction { get; }
        public bool InputInAction { get; }
    }
}