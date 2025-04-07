public abstract class UnitBaseState
{
    public abstract void EnterState(Unit unit);
    public abstract void UpdateState(Unit unit);
    public abstract void ExitState(Unit unit);
}
