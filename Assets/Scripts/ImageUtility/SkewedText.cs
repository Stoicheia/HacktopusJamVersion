using System;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
 
public class SkewedText : TextMeshProUGUI 
{
    [SerializeField] public float skewX;
 
    [SerializeField]
    public float skewY;

    private void LateUpdate()
    {
        ForceMeshUpdate();
        int vertCount = mesh.vertices.Count();
        var m = new Vector3[vertCount];

        var height = rectTransform.rect.height;
        var width = rectTransform.rect.width;
        var xskew = height * Mathf.Tan(Mathf.Deg2Rad * skewX);
        var yskew = width * Mathf.Tan(Mathf.Deg2Rad * skewY);
 
        var ymin = rectTransform.rect.yMin;
        var xmin = rectTransform.rect.xMin;
   
        for (int i = 0; i < vertCount; i++)
        {
            var oldPos = mesh.vertices[i];
            var newOffset = new Vector3(Mathf.Lerp(0, xskew, (oldPos.y - ymin) / height), Mathf.Lerp(0, yskew, (oldPos.x - xmin) / width), 0);
            m[i] = oldPos + newOffset;
        }

        Mesh newMesh = mesh;
        newMesh.SetVertices(m);

        canvasRenderer.SetMesh(newMesh);
        
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        
    }
}