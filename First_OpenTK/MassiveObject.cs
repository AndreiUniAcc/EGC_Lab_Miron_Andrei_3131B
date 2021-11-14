﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace First_OpenTK
{
    public class MassiveObject
    {

        private const String FILENAME = "C:\\Users\\forij\\OneDrive\\Desktop\\Andrei\\EGC\\Program EGc\\EGC_Lab_Miron_Andrei_3131B\\First_OpenTK\\assets\\lowpolytree.obj";
        private const int FACTOR_SCALARE_IMPORT = 3;

        private List<Vector3> coordsList;
        private bool visibility;
        private Color meshColor;
        private bool hasError;

        public MassiveObject(Color col)
        {


            try
            {
                coordsList = LoadFromObjFile(FILENAME);

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a esuat: obiect negasit/coordonate lipsa!");
                    return;
                }
                visibility = false;
                meshColor = col;
                hasError = false;
                Console.WriteLine("Obiect 3D Ã®ncarcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: assets file <" + FILENAME + "> is missing!!!");
                hasError = true;
            }
        }

        public void ToggleVisibility()
        {
            if (hasError == false)
            {
                visibility = !visibility;
            }
        }

        public void Draw()
        {
            const int FACTOR_TRANSLATE = 10;
            if (hasError == false && visibility == true)
            {
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert.X - FACTOR_TRANSLATE, vert.Y, vert.Z);
                }
                GL.End();
            }
        }

        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();


            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                if (line.Trim().Length > 2)
                {
                    string ch1 = line.Trim().Substring(0, 1);
                    string ch2 = line.Trim().Substring(1, 1);
                    if (ch1 == "v" && ch2 == " ")
                    {

                        string[] block = line.Trim().Split(' ');
                        if (block.Length == 4)
                        {
                            // ATENTIE: Pericol!!!
                            float xval = float.Parse(block[1].Trim()) * FACTOR_SCALARE_IMPORT;
                            float yval = float.Parse(block[2].Trim()) * FACTOR_SCALARE_IMPORT;
                            float zval = float.Parse(block[3].Trim()) * FACTOR_SCALARE_IMPORT;

                            vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));

                        }
                    }
                }
            }

            return vlc3;
        }
    }
}
