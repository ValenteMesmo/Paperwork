using GameCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PaperWork.GameEntities.Papers.Updates
{
    public class ComputeHorizontalCombosCombo : IHandleEntityUpdates
    {
        private readonly Func<PapersEntity, IEnumerable<PapersEntity>> GetHorizontalCombo;

        public ComputeHorizontalCombosCombo(Func<PapersEntity, IEnumerable<PapersEntity>> GetHorizontalCombo)
        {
            this.GetHorizontalCombo = GetHorizontalCombo;
        }

        public void Update(Entity entity)
        {
            if ((entity.As<PapersEntity>()).VerticalSpeed.Get() != 0)
                return;

            var combo = GetHorizontalCombo(entity.As<PapersEntity>());
            if (combo.Any())
            {
                foreach (var item in combo)
                {
                    item.SelfDestruct();
                }
                //Debug.WriteLine(combo.Count());
            }
        }
    }
}
