using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Mola;

namespace HDMolaGH
{
    class HDMeshToRhino
    {
        public static Mesh FillRhinoMesh(MolaMesh molaMesh)
        {
            Mesh rhinoMesh = new Mesh();
            // add vertices
            foreach (var v in molaMesh.Vertices)
            {
                rhinoMesh.Vertices.Add(v.x, v.y, v.z);
            }

            //add faces
            foreach (var f in molaMesh.Faces)
            {
                if (f.Length == 3)
                {
                    rhinoMesh.Faces.AddFace(f[0], f[1], f[2]);
                }
                else if (f.Length == 4)
                {
                    rhinoMesh.Faces.AddFace(f[0], f[1], f[2], f[3]);
                }
                else return null;
            }
            // add color
            //if (molaMesh.Colors.Count != molaMesh.VertexCount())
            //{
            //    molaMesh.Colors = Enumerable.Repeat(Color.red, molaMesh.VertexCount()).ToList();
            //}
            // rhinoMesh.VertexColors.SetColors(RhinoColorsFromMolaMesh(molaMesh));
            rhinoMesh.Normals.ComputeNormals();
            rhinoMesh.Compact();

            return rhinoMesh;
        }
        public static MolaMesh FillMolaMesh(Mesh rhinoMesh)
        {
            MolaMesh molaMesh = new MolaMesh();
            foreach (var v in rhinoMesh.Vertices)
            {
                molaMesh.AddVertex(v.X, v.Y, v.Z);
            }
            foreach (var f in rhinoMesh.Faces)
            {
                if (f.IsTriangle)
                {
                    molaMesh.AddFace(new int[3] { f.A, f.B, f.C });
                }
                else if (f.IsQuad)
                {
                    molaMesh.AddFace(new int[4] { f.A, f.B, f.C, f.D });
                }
            }
            //molaMesh.Colors = MolaColorsFromRhinoMesh(rhinoMesh).ToList();
            if (molaMesh.Colors.Count != molaMesh.VertexCount())
            {
                molaMesh.Colors = Enumerable.Repeat(Color.red, molaMesh.VertexCount()).ToList();
            }
            return molaMesh;
        }
        public static System.Drawing.Color[] RhinoColorsFromMolaMesh(MolaMesh mMesh)
        {
            System.Drawing.Color[] rhinoColors = new System.Drawing.Color[mMesh.Colors.Count];
            Color molaV = new Mola.Color();
            for (int i = 0; i < mMesh.VertexCount(); i++)
            {
                molaV = mMesh.Colors[i];
                rhinoColors[i] = System.Drawing.Color.FromArgb((int)molaV.r * 255, (int)molaV.g * 255, (int)molaV.b * 255, (int)molaV.a * 255);
            }
            return rhinoColors;
        }
        public static Color[] MolaColorsFromRhinoMesh(Mesh rMesh)
        {
            Color[] molaColors = new Color[rMesh.VertexColors.Count];
            System.Drawing.Color rhinoC = new System.Drawing.Color();
            for (int i = 0; i < rMesh.Vertices.Count; i++)
            {
                rhinoC = rMesh.VertexColors[i];
                molaColors[i] = new Color(rhinoC.R/255, rhinoC.G/255, rhinoC.B/255, rhinoC.A/255);
            }
            return molaColors;
        }
    }
}

