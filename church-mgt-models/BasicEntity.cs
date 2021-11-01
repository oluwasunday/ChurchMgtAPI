using System;

namespace church_mgt_models
{
    public abstract class BasicEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
