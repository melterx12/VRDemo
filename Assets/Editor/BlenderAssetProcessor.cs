using UnityEngine;
using UnityEditor;
using System.IO;

public class BlenderAssetProcessor : AssetPostprocessor
{

    public void OnPostprocessModel(GameObject obj)
    {
        ModelImporter importer = assetImporter as ModelImporter;
        if (Path.GetExtension(importer.assetPath) == ".blend")
            RotateObject(obj.transform);

        obj.transform.rotation = Quaternion.identity;
    }

    private void RotateObject(Transform obj)
    {
        Vector3 objRotation = obj.eulerAngles;
        objRotation.x += 90f;
        obj.eulerAngles = objRotation;

        //If a meshFilter is attached, rotate the vertex mesh data
        MeshFilter meshFilter = obj.GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (meshFilter)
            RotateMesh(meshFilter.sharedMesh);

        foreach (Transform child in obj)
            RotateObject(child);
    }

    private void RotateMesh(Mesh mesh)
    {
        int index = 0;

        //Switch all vertex z values with y values
        Vector3[] vertices = mesh.vertices;
        for (index = 0; index < vertices.Length; index++)
            vertices[index] = new Vector3(vertices[index].x, vertices[index].z, vertices[index].y);

        mesh.vertices = vertices;

        //For each submesh invert the order of vertices for all triangles
        for (int submesh = 0; submesh < mesh.subMeshCount; submesh++)
        {
            int[] triangles = mesh.GetTriangles(submesh);
            for (index = 0; index < triangles.Length; index += 3)
            {
                int intermediate = triangles[index];
                triangles[index] = triangles[index + 2];
                triangles[index + 2] = intermediate;
            }
            mesh.SetTriangles(triangles, submesh);
        }

        //Recalculate mesh data
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}