public interface IBehaviour
{
    void OnNewTileEntered();

    ITargetable GetCurrentTarget();

    void SetCurrentTarget(ITargetable targetable);
}
