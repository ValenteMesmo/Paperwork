namespace GameCore
{
    public abstract class UpdateHandler
    {
        protected Entity ParentEntity { get; }

        public UpdateHandler(Entity ParentEntity)
        {
            this.ParentEntity = ParentEntity;
        }

       public abstract void Update();
    }
}
