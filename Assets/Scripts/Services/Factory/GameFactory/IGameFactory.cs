using Infrastructure.Container;
using UnityEngine;

namespace Services.Factory.GameFactory
{
    public interface IGameFactory : IService
    {
        GameObject Player { get; }
        GameObject CreateCamera();
        GameObject CreatePlayer();
        GameObject CreateGround();
        GameObject CreateFirstFactory();
        GameObject CreateSecondFactory();
        GameObject CreateThirdFactory();
    }
}