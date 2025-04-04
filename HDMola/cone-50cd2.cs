using System;
using System.Collections;
using System.Collections.Generic;

using Rhino;
using Rhino.Geometry;

using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using Mola;


/// <summary>
/// This class will be instantiated on demand by the Script component.
/// </summary>
public abstract class Script_Instance_50cd2 : GH_ScriptInstance
{
  #region Utility functions
  /// <summary>Print a String to the [Out] Parameter of the Script component.</summary>
  /// <param name="text">String to print.</param>
  private void Print(string text) { /* Implementation hidden. */ }
  /// <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
  /// <param name="format">String format.</param>
  /// <param name="args">Formatting parameters.</param>
  private void Print(string format, params object[] args) { /* Implementation hidden. */ }
  /// <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj) { /* Implementation hidden. */ }
  /// <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj, string method_name) { /* Implementation hidden. */ }
  #endregion

  #region Members
  /// <summary>Gets the current Rhino document.</summary>
  private readonly RhinoDoc RhinoDocument;
  /// <summary>Gets the Grasshopper document that owns this script.</summary>
  private readonly GH_Document GrasshopperDocument;
  /// <summary>Gets the Grasshopper script component that owns this script.</summary>
  private readonly IGH_Component Component;
  /// <summary>
  /// Gets the current iteration count. The first call to RunScript() is associated with Iteration==0.
  /// Any subsequent call within the same solution will increment the Iteration count.
  /// </summary>
  private readonly int Iteration;
  #endregion
  /// <summary>
  /// This procedure contains the user code. Input parameters are provided as regular arguments,
  /// Output parameters as ref arguments. You don't have to assign output parameters,
  /// they will have a default value.
  /// </summary>
  #region Runscript
  private void RunScript(double x, double y, int z, ref object A, ref object B)
  {
        MolaMesh mesh = MeshFactory.CreateCone((float)y, (float)x, 0.5f, 3, z);
        mesh = MeshSubdivision.Grid(mesh, 2, 5);
        List<float> values = MeshAnalysis.FaceArea(mesh);
        bool[] mask = new bool[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] > 0.7f)
            {
                mask[i] = true;
            }
        }

        List<MolaMesh> meshes = MeshTools.Split(mesh, mask);
        A = meshes[0];
        B = meshes[1];


        //MolaMesh meshA = (MolaMesh)x;
        //MolaMesh meshB = (MolaMesh)y;
        //meshA = MeshSubdivision.ExtrudeTapered(meshA, 0.8f, 0.8f, false);
        //meshB = MeshSubdivision.ExtrudeTapered(meshB, -0.5f, 0.6f, true);
        //MolaMesh mesh = MeshTools.Merge(new List<MolaMesh> { meshA, meshB });
        mesh = MeshTools.WeldVertices(mesh);
        mesh = MeshTools.UpdateTopology(mesh);
        mesh = MeshTools.Offset(mesh, 0.2f);

        A = mesh;
    }
  #endregion
  #region Additional

  #endregion
}