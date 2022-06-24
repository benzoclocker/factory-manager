using Infrastructure.Container;
using UnityEngine;

namespace Services.Factory.UIFactory
{
    public interface IUIFactory : IService
    {
        GameObject CreateControllerUI();
    }
}