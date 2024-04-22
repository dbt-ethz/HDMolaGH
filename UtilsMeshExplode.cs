using System;
using System.Collections.Generic;
using Mola;
using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace HDMolaGH
{
    public class UtilsMeshExplode : GH_Component
    {
        public UtilsMeshExplode()
          : base("Mesh Explode", "Explode",
            "Extract each face of a MolaMesh to a seperated mesh",
            "Mola", "4-Utils")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMesh", "M", "mesh to be converted", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("MolaMeshes", "Meshes", "a list of MolaMeshes", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            MolaMesh mMesh = new MolaMesh();

            DA.GetData(0, ref mMesh);

            List<MolaMesh> mMeshes = new List<MolaMesh>();
            foreach (var face in mMesh.Faces)
            {
                MolaMesh newMesh = new MolaMesh();
                newMesh.AddFace(UtilsVertex.face_vertices(mMesh, face));
                mMeshes.Add(newMesh);
            }

            DA.SetDataList(0, mMeshes);

        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("5494d762-dd2a-4270-8ac7-6c243b0e0ea6"); }
        }
    }
}
