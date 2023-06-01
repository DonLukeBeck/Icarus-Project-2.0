using UnityEngine;

// Wrapper for the robot colours - easier to move around than multiple parameters
[CreateAssetMenu]
public class RobotLivery : ScriptableObject
{
    public Color _RobotColorA = Color.black;
    public Color _RobotColorB = Color.black;
    public Color _RobotColorC = Color.black;
    public Color _RobotColorD = Color.black;
    public Color _RobotColorE = Color.black;
}
