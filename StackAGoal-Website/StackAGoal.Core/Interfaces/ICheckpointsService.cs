using StackAGoal.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackAGoal.Core.Interfaces
{
    public interface ICheckpointsService
    {
        void AddCheckpoint(Checkpoint checkpoint);
        void RemoveCheckpoint(Checkpoint checkpoint);
        Checkpoint GetCheckpoint(int checkpointId);
        IEnumerable<Checkpoint> GetCheckpointsByGoal(int goalsId); 
        int Save();
    }
}
