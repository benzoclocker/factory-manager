using Core;
using Infrastructure.Container;
using UnityEngine;

namespace Services.Factory.UIFactory
{
    public interface IUIFactory : IService
    {
        IAlert Alert { get; }
        GameObject CreateControllerUI();
        GameObject CreateAlertUI();
    }
}