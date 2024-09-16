using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class AssemblyPriority : GH_AssemblyPriority
    {
        public override GH_LoadingInstruction PriorityLoad()
        {
            Console.WriteLine("init");
            // Initialize the Assembly Resolver here
            AssemblyResolver.Initialize(); // Call the static initializer or access any static field
            var trigger = typeof(HDMolaGH.AssemblyResolver);
            // Continue with normal loading
            return GH_LoadingInstruction.Proceed;
        }
    }
}