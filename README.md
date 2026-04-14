# Unity State Machine (Portfolio)

Документация на английском языке ниже (English documentation is below).

## Русский

Легковесная state machine для Unity с поддержкой:
- переходов между состояниями через generic API (`Transition<TState>()`);
- жизненного цикла состояния (`StateBegin`, `Update`, `StateEnd`);
- async-входа в состояние через `UniTask`;
- автоматической отмены async-задач при смене состояния/уничтожении объекта.

### Структура
- `IState` — базовый контракт состояния.
- `AbstractState` — удобная базовая реализация (`OnEnter`, `OnUpdate`, `OnExit`, `OnEnterAsync`).
- `StateMachine` — регистрация, запуск, обновление и переключение состояний.
- `IStateAsyncExtensions` — `Delay/Yield/WaitUntil/WaitWhile` с токеном отмены текущего состояния.
- `TransitionState<TNextState>` — шаблон для промежуточных состояний с авто-переходом.

### Зависимости
- [Zenject](https://github.com/modesttree/Zenject)
- [UniRx](https://github.com/neuecc/UniRx)
- [UniTask](https://github.com/Cysharp/UniTask)

### Быстрый старт
1. Добавьте `StateMachine` на `GameObject`.
2. Создайте состояния на базе `AbstractState` (или реализуйте `IState`).
3. Зарегистрируйте состояния через `Register(...)`.
4. Установите начальное состояние через `SetFirstState(...)`.
5. Запустите машину через `StartFSM()` (или включите `AutoStart`).

Минимальный пример:

```csharp
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private StateMachine fsm;

    private void Start()
    {
        fsm.Register(new IdleState());
        fsm.Register(new MoveState());
        fsm.SetFirstState(typeof(IdleState));
        fsm.StartFSM();
    }
}
```

---

## English

A lightweight Unity state machine with:
- generic state transitions (`Transition<TState>()`);
- clear state lifecycle (`StateBegin`, `Update`, `StateEnd`);
- async state enter support via `UniTask`;
- automatic cancellation of async tasks on state switch/object destruction.

### Architecture
- `IState` — base state contract.
- `AbstractState` — convenient base implementation (`OnEnter`, `OnUpdate`, `OnExit`, `OnEnterAsync`).
- `StateMachine` — registration, startup, updates, and transitions.
- `IStateAsyncExtensions` — `Delay/Yield/WaitUntil/WaitWhile` bound to the active state's cancellation token.
- `TransitionState<TNextState>` — helper for transitional states with automatic next-state switch.

### Dependencies
- [Zenject](https://github.com/modesttree/Zenject)
- [UniRx](https://github.com/neuecc/UniRx)
- [UniTask](https://github.com/Cysharp/UniTask)

### Quick Start
1. Add `StateMachine` to a `GameObject`.
2. Create states from `AbstractState` (or implement `IState`).
3. Register states with `Register(...)`.
4. Set the first state with `SetFirstState(...)`.
5. Start the machine via `StartFSM()` (or enable `AutoStart`).
