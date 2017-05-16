namespace GameCore.Collision
{
    public interface IHandleTriggers
    {
        void TriggerEnter(BaseCollider triggerCollider, BaseCollider other);
        void TriggerExit(BaseCollider triggerCollider, BaseCollider other);
    }
}
