using Jarrus.GA.Solution;
using System;

namespace Jarrus.GA.Models
{
    public class GATask : GAProperties
    {
        public GATask(JarrusSolution solution) { Solution = solution; ValidateConstructor(); }

        public void ValidateConstructor() { if (Solution == null) { throw new ArgumentException("Must pass a valid solution"); } }
        public bool IsValid() { return Solution != null && UUID != null && UUID.Trim().Length > 0; }
    }
}
