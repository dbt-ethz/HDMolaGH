using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    //public class SubdivideLinearSplitQuad : GH_Component
    //{
    //    public SubdivideLinearSplitQuad()
    //      : base("Subdivide Linear Split Quad", "Linear Split Quad",
    //          "split faces liearl..",
    //          "Mola", "2-Subdivisions")
    //    {
    //    }
    //    protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
    //    {
    //        pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
    //        pManager.AddNumberParameter("Min split width", "W", "minimal split width", GH_ParamAccess.item, 1);
    //        pManager.AddIntegerParameter("Split direction", "D", "split direction", GH_ParamAccess.item, 0);
    //        pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
    //    }
    //    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    //    {
    //        pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
    //    }
    //    protected override void SolveInstance(IGH_DataAccess DA)
    //    {
    //        MolaMesh mMesh = new MolaMesh();
    //        double w = 1;
    //        int d = 0;
    //        int iteration = 1;

    //        DA.GetData(0, ref mMesh);
    //        DA.GetData(1, ref w);
    //        DA.GetData(2, ref d);
    //        DA.GetData(3, ref iteration);

    //        for (int i = 0; i < iteration; i++)
    //        {
    //            mMesh = MeshSubdivision.SubdivideMeshLinearSplitQuad(mMesh, (float)w, d);
    //        }

    //        DA.SetData(0, mMesh);
    //    }
    //    protected override System.Drawing.Bitmap Icon
    //    {
    //        get
    //        {
    //            // You can add image files to your project resources and access them like this:
    //            //return Resources.IconForThisComponent;
    //            return null;
    //        }
    //    }
    //    public override Guid ComponentGuid
    //    {
    //        get { return new Guid("383e845f-64ca-4ae4-a854-23d6bdeaa6fd"); }
    //    }
    //}
}
