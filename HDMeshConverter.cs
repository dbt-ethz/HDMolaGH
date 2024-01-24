using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    class HDMeshConverter
    {
        public static Mesh FillRhinoMesh(MolaMesh mMesh)
        {
            Mesh rMesh = new Mesh();
            // add vertices
            Point3f[] rVertices = new Point3f[mMesh.VertexCount()];
            for (int i = 0; i < mMesh.VertexCount(); i++)
            {
                rVertices[i] = new Point3f(mMesh.Vertices[i].x, mMesh.Vertices[i].y, mMesh.Vertices[i].z);
            }
            rMesh.Vertices.AddVertices(rVertices);

            //add faces
            MeshFace[] rFaces = new MeshFace[mMesh.FacesCount()];
            for (int i = 0; i < mMesh.FacesCount(); i++)
            {
                if(mMesh.Faces[i].Length == 3)
                {
                    rFaces[i] = new MeshFace(mMesh.Faces[i][0], mMesh.Faces[i][1], mMesh.Faces[i][2]);
                }
                else if (mMesh.Faces[i].Length == 4)
                {
                    rFaces[i] = new MeshFace(mMesh.Faces[i][0], mMesh.Faces[i][1], mMesh.Faces[i][2], mMesh.Faces[i][3]);
                }
            }
            rMesh.Faces.AddFaces(rFaces);

            // add color
            System.Drawing.Color[] rColors = RhinoColorsFromMolaMesh(mMesh);
            if(rColors.Length == rMesh.Vertices.Count)
            {
                rMesh.VertexColors.SetColors(rColors);
            }
            
            rMesh.Normals.ComputeNormals();
            rMesh.Compact();

            return rMesh;
        }
        public static MolaMesh FillMolaMesh(Mesh rMesh)
        {
            MolaMesh mMesh = new MolaMesh();
            foreach (var v in rMesh.Vertices)
            {
                mMesh.AddVertex(v.X, v.Y, v.Z);
            }
            foreach (var f in rMesh.Faces)
            {
                if (f.IsTriangle)
                {
                    mMesh.AddFace(new int[3] { f.A, f.B, f.C });
                }
                else if (f.IsQuad)
                {
                    mMesh.AddFace(new int[4] { f.A, f.B, f.C, f.D });
                }
            }

            Color[] mColors = MolaColorsFromRhinoMesh(rMesh);
            if(mColors.Length != mMesh.VertexCount())
            {
                mColors = Enumerable.Repeat(Color.magenta, mMesh.VertexCount()).ToArray();
            }

            mMesh.Colors = mColors.ToList();
            return mMesh;
        }
        public static System.Drawing.Color[] RhinoColorsFromMolaMesh(MolaMesh mMesh)
        {
            System.Drawing.Color[] rhinoColors = new System.Drawing.Color[mMesh.Colors.Count];
            for (int i = 0; i < mMesh.Colors.Count; i++)
            {
                var molaV = mMesh.Colors[i];
                rhinoColors[i] = System.Drawing.Color.FromArgb((int)(molaV.a * 255), (int)(molaV.r * 255), (int)(molaV.g * 255), (int)(molaV.b * 255));
            }
            return rhinoColors;
        }
        public static Color[] MolaColorsFromRhinoMesh(Mesh rMesh)
        {
            Color[] molaColors = new Color[rMesh.VertexColors.Count];
            for (int i = 0; i < rMesh.VertexColors.Count; i++)
            {
                var rhinoC = rMesh.VertexColors[i];
                molaColors[i] = new Color(rhinoC.R/255, rhinoC.G/255, rhinoC.B/255, rhinoC.A/255);
            }
            return molaColors;
        }
    }
}

