using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]


public class SimpleRobotBonesToUV : MonoBehaviour
{
    // Link a bone in the rig, to Uvs in the shader.

    public GameObject AnimatedBoneToLinkToUvs;
    public Material MaterialToAnimate;
	public string PropertyNameX;
    public string PropertyNameY;
    
    public float UvCenterOffsetFactor;
    /// This value = 1/16 of the texture for a 8*8 Atlas = 0,0625
    
	private void LateUpdate()
	{
        Vector3 my;
        my = AnimatedBoneToLinkToUvs.transform.position;
        if (MaterialToAnimate != null)
        {
            MaterialToAnimate.SetFloat(PropertyNameX, -my.x + UvCenterOffsetFactor);
            MaterialToAnimate.SetFloat(PropertyNameY, my.y - UvCenterOffsetFactor);
        }
    }
}
	
