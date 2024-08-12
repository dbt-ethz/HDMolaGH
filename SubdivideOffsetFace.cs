using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class SubdivideOffsetFace : GH_Component
    {
        public SubdivideOffsetFace()
         : base("Subdivide Offset Face", "Offset Face",
             "create an offset of a mesh",
             "Mola", "2-Subdivisions")
        {
        }
        public override GH_Exposure Exposure => GH_Exposure.secondary;
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be subdivided", GH_ParamAccess.item);
            pManager.AddNumberParameter("Offset", "O", "offset distance", GH_ParamAccess.item, 0.2); 
            pManager.AddBooleanParameter("CloseBorders", "C", "close the borders tween mesh and offset", GH_ParamAccess.item, true); 
            pManager.AddIntegerParameter("Iteration", "I", "subdivide times", GH_ParamAccess.item, 1);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            double o = 0.2;
            bool c = true;
            int iteration = 1;

            DA.GetData(0, ref mMesh);
            DA.GetData(1, ref o);
            DA.GetData(2, ref c);
            DA.GetData(3, ref iteration);

            for (int i = 0; i < iteration; i++)
            {
                mMesh = MeshTools.Offset(mMesh, (float)o, c);
            }

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.offsetface;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("5dd4e602-17f5-4c28-90b6-a7e740063188"); }
        }
    }
}
