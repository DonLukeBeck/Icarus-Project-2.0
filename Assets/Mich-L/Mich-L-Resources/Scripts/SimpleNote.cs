using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : MonoBehaviour
{
    // Simple note to have messages in the inspector

    [TextArea(15, 20)]
    [Tooltip("Write your notes here !")]
    public string Notes = "Note here";

    
}
