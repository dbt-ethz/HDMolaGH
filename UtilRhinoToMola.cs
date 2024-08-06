using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilRhinoToMola : GH_Component
    {
        public UtilRhinoToMola()
          : base("Rhino to Mola", "To Mola",
              "convert a rhino mesh to a mola mesh",
              "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Rhino Mesh", "M", "mesh to be converted", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "the result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh rMesh = new Mesh();
            DA.GetData(0, ref rMesh);

            MolaMesh mMesh = HDMeshConverter.FillMolaMesh(rMesh);
            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.rhinotomola;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("e6e96c40-f6a6-4578-9653-b633e084eca5"); }
        }
    }
}
