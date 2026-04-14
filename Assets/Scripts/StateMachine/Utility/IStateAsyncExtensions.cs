using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace PGLib.FSM.UniTaskUtility
{
    public static class IStateAsyncExtensions
    {
        public static UniTask Delay(this IState state, int millisecondsDelay, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update) =>
            UniTask.Delay(millisecondsDelay, ignoreTimeScale, delayTiming, cancellationToken: state.StateMachine.StateSwitchAndSceneDestruction.Token);
        
        public static UniTask Yield(this IState state, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update) =>
            UniTask.Yield(delayTiming, cancellationToken: state.StateMachine.StateSwitchAndSceneDestruction.Token);

        public static UniTask WaitWhile(this IState state, Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update) 
            => UniTask.WaitWhile(predicate, timing, state.StateMachine.StateSwitchAndSceneDestruction.Token);
        
        public static UniTask WaitUntil(this IState state, Func<bool> predicate, PlayerLoopTiming timing = PlayerLoopTiming.Update) 
            => UniTask.WaitUntil(predicate, timing, state.StateMachine.StateSwitchAndSceneDestruction.Token);

        public static CancellationToken GetCancellationToken(this IState state) =>
            state.StateMachine.StateSwitchAndSceneDestruction.Token;
    }
}