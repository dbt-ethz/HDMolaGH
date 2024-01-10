using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    public class PrimitiveSphere : GH_Component
    {
        public PrimitiveSphere()
          : base("Sphere", "Sphere",
            "create a sphere",
            "Mola", "Primitive")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Radius", "R", "radius of the sphere", GH_ParamAccess.item, 1.0);
            pManager.AddNumberParameter("x", "x", "x coordinate of center", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("y", "y", "y coordinate of center", GH_ParamAccess.item, 0.0);
            pManager.AddNumberParameter("z", "z", "z coordinate of center", GH_ParamAccess.item, 0.0);
            pManager.AddIntegerParameter("URes", "U", "U resolution", GH_ParamAccess.item, 10);
            pManager.AddIntegerParameter("VRes", "V", "V resolution", GH_ParamAccess.item, 10);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mesh", "M", "a sphere Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double r = 1;
            double x = 0;
            double y = 0;
            double z = 0;
            int u = 10;
            int v = 10;


            DA.GetData(0, ref r);
            DA.GetData(1, ref x);
            DA.GetData(2, ref y);
            DA.GetData(3, ref z);
            DA.GetData(4, ref u);
            DA.GetData(5, ref v);

            MolaMesh mMesh = MeshFactory.CreateSphere((float)r, (float)x, (float)y, (float)z, u, v);
            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("C45D9C66-EEAC-40A9-99DD-86E4DE13C4EC"); }
        }
    }
}