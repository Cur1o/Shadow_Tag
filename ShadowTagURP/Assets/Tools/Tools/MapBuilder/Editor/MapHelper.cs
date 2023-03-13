using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapTool
{
public static class MapHelper 
{
       public static Texture2D RotateTexture(Texture2D originalTexture, bool clockwise)
        {

            Color32[] original = originalTexture.GetPixels32();
            Color32[] rotated = new Color32[original.Length];
            int w = originalTexture.width;
            int h = originalTexture.height;

            int iRotated, iOriginal;


            for (int j = 0; j < h; ++j)
            {
                for (int i = 0; i < w; ++i)
                {
                    iRotated = (i + 1) * h - j - 1;
                    iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                    rotated[iRotated] = original[iOriginal];
                }
            }

            Texture2D rotatedTexture = new Texture2D(h, w);
            rotatedTexture.SetPixels32(rotated);
            rotatedTexture.Apply();
            return rotatedTexture;
        }

        public static GUIStyle GetToolbarStyle(int size)
        {
            GUIStyle style = new GUIStyle("Button");

            style.padding = new RectOffset(1, 1, 1, 1);
            style.margin = new RectOffset(1, 1, 1, 1);
            style.fixedWidth = size;
            style.fixedHeight = size;
            return style;
        }

    }
}

