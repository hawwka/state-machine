using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

namespace PGLib.FSM
{
    public class StateMachine : MonoBehaviour
    {
        public bool AutoStart = false;

        private Dictionary<Type, IState> stateMap = new Dictionary<Type, IState>();

        private IState currentState = null;
        private Type nextState = null;
        public CancellationTokenSource StateSwitchAndSceneDestruction;
        
        public IState CurrentState => currentState;

        public event Action<IState> StateChanged; 

        private void Start()
        {
            if (AutoStart)
            {
                StartFSM();
            }
        }

        public void StartFSM()
        {            
            currentState?.StateBegin();

            Observable.EveryUpdate()
                .Subscribe(_ => currentState?.Update())
                .AddTo(this);
            
            Observable.EveryLateUpdate()
                .Where(_ => nextState != null)
                .Subscribe(_ =>
                {
                    var stateSwitchLog = currentState == null
                        ? ""
                        : $"Покидаем состояние <color=yellow>{currentState?.GetType().Name}</color>. ";
                    stateSwitchLog += $"Следующее состояние: <color=red>{stateMap[nextState].GetType().Name}</color>";
                   
                    currentState?.StateEnd();
                    StateSwitchAndSceneDestruction?.Cancel();
                    currentState = stateMap[nextState];
                    StateChanged?.Invoke(currentState);
                    StateSwitchAndSceneDestruction = new CancellationTokenSource();
                    currentState.StateBegin();

                    nextState = null;
                }).AddTo(this);
        }

        public void SetFirstState(Type state)
        {
            currentState = stateMap[state];
        }

        public void Register(IState uniRxState)
        {
            stateMap.Add(uniRxState.GetType(), uniRxState);
            uniRxState.StateMachine = this;
        }

        public T Transition<T>() where T : class, IState
        {
            if (!stateMap.ContainsKey(typeof(T)))
            {
                Debug.LogError($"[{nameof(StateMachine)}.Transition] Состояние {typeof(T)} не зарегистрировано");
                return null;
            }
            nextState = typeof(T);
            return (T)stateMap[typeof(T)];
        }

        private void OnDestroy()
        {
            StateSwitchAndSceneDestruction.Cancel();
        }
    }
}