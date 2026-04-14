using Cysharp.Threading.Tasks;

namespace PGLib.FSM
{
    public abstract class AbstractState : IState
    {
        public StateMachine StateMachine { get; set; }

        public void StateBegin()
        {
            
            OnEnter();
            OnEnterAsync();
        }

        public void Update() => OnUpdate();
        public void StateEnd() => OnExit();
        
        protected TState Transition<TState>() where TState: class, IState => StateMachine.Transition<TState>();

        public virtual void OnEnter() {}
        public virtual void OnExit() {}
        public virtual void OnUpdate() {}

        public virtual UniTask OnEnterAsync() => UniTask.CompletedTask;
    }
}