using Cysharp.Threading.Tasks;
using PGLib.FSM;
using PGLib.FSM.UniTaskUtility;
using Zenject;

namespace PGLib.FSM.Utility
{
    public abstract class TransitionState<TNextState> : AbstractState where TNextState : class, IState
    {
        public override async UniTask OnEnterAsync()
        {
            await OnTransitionAsync();
            Transition<TNextState>();
        }

        protected abstract UniTask OnTransitionAsync();
    }
}
