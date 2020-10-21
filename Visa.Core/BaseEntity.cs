using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Core
{
    public interface IBaseEntity
    {
        int Id { get; set; }
    }
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
