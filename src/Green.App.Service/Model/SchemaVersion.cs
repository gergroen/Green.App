using System;

namespace Green.App.Model
{
    public class SchemaVersion : IEntity
    {
        public virtual Guid Id { get;  set; }
        public virtual long Version { get;  set; }
        public virtual string Script { get;  set; }
    }
}
