namespace PGLib.FSM
{
    public interface IState
    {
        void StateBegin();       // Вызывается при входе в состояние
        void Update();      // Вызывается каждый кадр
        void StateEnd();        // Вызывается при выходе из состояния
        
        StateMachine StateMachine { get; set; }
    }
}