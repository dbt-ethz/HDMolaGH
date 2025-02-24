using System;
using System.Collections.Generic;
using Mola;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Linq;

namespace HDMolaGH.code
{
    public class ArrayToMolaMesh : GH_Component
    {
        public ArrayToMolaMesh()
          : base("Array To MolaMesh", "Array To Mola",
            "convert a ND array to a mola mesh",
            "Mola", "5-Voxel")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Array", "A", "flattened ND array", GH_ParamAccess.list);
            pManager.AddIntegerParameter("nX", "nX", "x shape of ND array", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("nY", "nY", "y shape of ND array", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("nZ", "nZ", "z shape of ND array", GH_ParamAccess.item, 1);
            pManager.AddIntegerParameter("Target Value", "V", "the int value to be solid voxel in the mesh", GH_ParamAccess.item, 0);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Mesh", "M", "the result Mola mesh", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<int> IntList = new List<int>();
            DA.GetDataList(0, IntList);
            int[] IntArray = IntList.ToArray();

            int x = 1;
            int y = 1;
            int z = 1;
            DA.GetData(1, ref x);
            DA.GetData(2, ref y);
            DA.GetData(3, ref z);

            int targetValue = 0;
            DA.GetData(4, ref targetValue);

            bool[] boolArray = UtilsGrid.IntArray2Bool(IntArray, targetValue);
            bool[,,] reshapedArray = UtilsGrid.Reshape1DTo3D(boolArray, x, y, z);
            MolaMesh mMesh = UtilsGrid.VoxelMesh(reshapedArray);

            DA.SetData(0, mMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.array;
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("DF06CD94-1F97-4E6C-9EC7-BF6A04AE5C9C"); }
        }
    }
}