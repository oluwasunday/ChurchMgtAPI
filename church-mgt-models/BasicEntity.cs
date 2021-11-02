using System;

namespace church_mgt_models
{
    public abstract class BasicEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
