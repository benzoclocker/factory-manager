using System;
using Infrastructure.Container;
using Infrastructure.StateMachine.GameStateMachine;
using Infrastructure.StateMachine.GameStateMachine.States;
using UnityEngine;

namespace Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(new GameStateMachine(ServiceContainer.Container));
            _game.GameStateMachine.Enter<BootstrapState>();
        }
    }
}