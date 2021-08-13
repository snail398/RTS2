using UnityEngine; 

[CreateAssetMenu(fileName = "BuidingConfig", menuName = "Buildings/BuidingConfig", order = 1)]
public class BuidingConfig : ScriptableObject
{
    public string Name;
    public int Width;
    public int Height;
}
