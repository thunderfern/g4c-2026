using UnityEngine;

public static class StringUtil {
    public static Vector3 StringToVector3(string s) {
        string withoutBrackets = s.Substring(1, s.Length - 3);
        string[] splitString = withoutBrackets.Split(",");
        return new Vector3(float.Parse(splitString[0]), float.Parse(splitString[1]), float.Parse(splitString[2]));
    }

    public static Quaternion StringToQuaternion(string s) {
        string withoutBrackets = s.Substring(1, s.Length - 3);
        string[] splitString = withoutBrackets.Split(",");
        return new Quaternion(float.Parse(splitString[0]), float.Parse(splitString[1]), float.Parse(splitString[2]), float.Parse(splitString[3]));
    }

};