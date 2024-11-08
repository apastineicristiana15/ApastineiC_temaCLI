using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace temaLab4
{
    internal class CreateCube
    {
        bool visibility;
        Color color;
        List<Vector3> coordList;
        float gravity = 9.81f;
        float velocityY = 0f;      // Viteza pe axa Y
        bool isGravityBound = false;

        public CreateCube(RandomNrGenerator r)
        {
            visibility = true;
            color = r.GetRandomColor();
            coordList = new List<Vector3>();

            int size_offset = r.GetRandomInt(4, 6);
            int height_offset = r.GetRandomInt(5, 20);
            int radial_offset = r.GetRandomInt(5, 30);

            // Definirea vârfurilor cubului 
            coordList.Add(new Vector3(radial_offset, height_offset, radial_offset));              // V0 - (stânga jos față)
            coordList.Add(new Vector3(radial_offset, height_offset, radial_offset + size_offset)); // V1 - (dreapta jos față)
            coordList.Add(new Vector3(radial_offset + size_offset, height_offset, radial_offset)); // V2 - (stânga jos spate)
            coordList.Add(new Vector3(radial_offset + size_offset, height_offset, radial_offset + size_offset)); // V3 - (dreapta jos spate)

            coordList.Add(new Vector3(radial_offset, height_offset + size_offset, radial_offset));              // V4 - (stânga sus față)
            coordList.Add(new Vector3(radial_offset, height_offset + size_offset, radial_offset + size_offset)); // V5 - (dreapta sus față)
            coordList.Add(new Vector3(radial_offset + size_offset, height_offset + size_offset, radial_offset)); // V6 - (stânga sus spate)
            coordList.Add(new Vector3(radial_offset + size_offset, height_offset + size_offset, radial_offset + size_offset)); // V7 - (dreapta sus spate)
        }

        public void Draw()
        {
            if (visibility)
            {
                GL.Color3(color);

                // Fața frontală
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[0]);
                GL.Vertex3(coordList[1]);
                GL.Vertex3(coordList[3]);
                GL.Vertex3(coordList[2]);
                GL.End();

                // Fața din spate
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[4]);
                GL.Vertex3(coordList[5]);
                GL.Vertex3(coordList[7]);
                GL.Vertex3(coordList[6]);
                GL.End();

                // Fața de jos
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[0]);
                GL.Vertex3(coordList[1]);
                GL.Vertex3(coordList[5]);
                GL.Vertex3(coordList[4]);
                GL.End();

                // Fața de sus
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[2]);
                GL.Vertex3(coordList[3]);
                GL.Vertex3(coordList[7]);
                GL.Vertex3(coordList[6]);
                GL.End();

                // Fața din stânga
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[0]);
                GL.Vertex3(coordList[2]);
                GL.Vertex3(coordList[6]);
                GL.Vertex3(coordList[4]);
                GL.End();

                // Fața din dreapta
                GL.Begin(PrimitiveType.Quads);
                GL.Vertex3(coordList[1]);
                GL.Vertex3(coordList[3]);
                GL.Vertex3(coordList[7]);
                GL.Vertex3(coordList[5]);
                GL.End();
            }
        }

        // Functia care aplică efectul gravitației
        public void Update(float deltaTime)
        {
            if (isGravityBound)
            {
                // Aplicăm gravitația asupra vitezei verticale
                velocityY -= gravity * deltaTime;

                // Actualizăm coordonatele cubului
                for (int i = 0; i < coordList.Count; i++)
                {
                    coordList[i] = new Vector3(coordList[i].X, coordList[i].Y + velocityY * deltaTime, coordList[i].Z);
                }

                // Limita de jos a căderii pentru a simula impactul cu solul
                float groundLevel = 0f;  // Exemplu, presupunem că solul este la Y = 0
                if (coordList.Any(v => v.Y <= groundLevel))
                {
                    // Resetăm poziția la sol și viteza pentru a simula oprirea
                    for (int i = 0; i < coordList.Count; i++)
                    {
                        coordList[i] = new Vector3(coordList[i].X, Math.Max(coordList[i].Y, groundLevel), coordList[i].Z);
                    }
                    velocityY = 0f;  // Oprirea la impact
                }
            }
        }

        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        public void ToggleGravity()
        {
            isGravityBound = true;
        }

        public void UnsetGravity()
        {
            isGravityBound = false;
        }
    }
}