using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossMaterialControl : MonoBehaviour
{
    List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    List<Material> materials = new List<Material>();
    private void Awake()
    {
        Check();
    }

    public void Check()
    {
        skinnedMeshRenderers =  transform.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
        materials = skinnedMeshRenderers.ToList().Select(t => t.material).ToList();
    }

    public void changeMat(Material mat =null)
    {
        int indexer = 0;
        foreach (var renderer in skinnedMeshRenderers)
        { 
            renderer.material = mat == null ? materials[indexer] : mat;
            indexer ++;
        }
    }
}
