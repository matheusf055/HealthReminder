using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RememberMedication.Domain.Common.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; private set; }

        public void GenerateNewId()
        {
            Id = Guid.NewGuid();
        }

        public static void GenerateNewId(IEnumerable<EntityBase> entities)
        {
            foreach (var entity in entities)
            {
                entity.GenerateNewId();
            }
        }
    }
}
