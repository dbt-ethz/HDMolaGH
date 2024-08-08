using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilsUpdateTopology : GH_Component
    {
        public UtilsUpdateTopology()
          : base("Update Topology", "Topology",
            "update topology of a MolaMesh",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "mesh to be modified", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mola Mesh", "M", "result mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();
            DA.GetData(0, ref mMesh);

            mMesh.WeldVertices();
            mMesh.UpdateTopology();
            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.toppology;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("79613dcb-7b18-4378-9a6a-b5f7728a24c1"); }
        }
    }
}
