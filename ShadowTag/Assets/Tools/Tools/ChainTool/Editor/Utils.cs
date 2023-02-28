using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainTool
{
    public static class Utils
    {
        public static Vector3 GetBezierPoint(Vector3 startPosition, Vector3 startTangent, Vector3 endTangent, Vector3 endPosition, float t)
        {
            float s = 1.0f - t;
            return startPosition * s * s * s + startTangent * s * s * t * 3.0f + endTangent * s * t * t * 3.0f + endPosition * t * t * t;
        }

        public static List<Vector3> CreateSpawnPoints(Vector3 start, Vector3 end, Vector3 tangentStart, Vector3 tangentEnd, float gap, float width, float accuracy )
        {
            var positions = new List<Vector3>();

            Vector3 lastPoint = GetBezierPoint(start, tangentStart, tangentEnd, end, 0);

            for (float i = accuracy; i <=1.0f; i += accuracy)
            {

                float t = 1.0f - i;
                Vector3 trial = GetBezierPoint(start, tangentStart, tangentEnd, end, t);

                if(Vector3.Distance(trial, lastPoint) >= width + gap)
                {
                    lastPoint = trial;
                    positions.Add(trial);
                }
            }
            return positions;
        }
    }

}