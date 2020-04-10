using Microsoft.EntityFrameworkCore;
using StackAGoal.Core.Interfaces;
using StackAGoal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackAGoal.Infrastructure.Services
{
    public class CheckpointsService : ICheckpointsService
    {
        protected readonly AppDbContext _context;
        public CheckpointsService(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }

        public void AddCheckpoint(Checkpoint checkpoint)
        {
            _context.Add(checkpoint);            
        }

        public void RemoveCheckpoint(Checkpoint checkpoint)
        {
            _context.Remove(checkpoint);
        }

        public Checkpoint GetCheckpoint(int checkpointId)
        {
            return _context.Checkpoints.SingleOrDefault(c => c.Id == checkpointId);
        }

        public IEnumerable<Checkpoint> GetCheckpointsByGoal(int goalId)
        {
            var checkpoints = _context.Checkpoints
                .Where(g => g.GoalId == goalId)
                .ToList();

            return checkpoints;
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
