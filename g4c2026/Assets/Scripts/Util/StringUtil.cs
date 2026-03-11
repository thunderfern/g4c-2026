using UnityEngine;
using System;
using System.Collections.Generic;

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

    // for stories

    public static List<string> ParseStoryLine(string s) {
        List<string> parts = new List<string>();

        while (s != "") {
            //Debug.Log(s);
            string cur = StringUntilSpace(ref s);
            if (cur[cur.Length - 1] == ':') cur = cur.Substring(0, cur.Length - 1);
            parts.Add(cur);
        }
        return parts;
    }

    public static string StringUntilSpace(ref string s) {
        string partUntilSpace = "";
        bool isQuote = false;
        int endidx = 0;
        for (int i = 0; i < s.Length; i++) {
            endidx = i;
            if (s[i] == ' ' && !isQuote) break;
            if (s[i] == '"') isQuote = !isQuote;
            else partUntilSpace += s[i];
        }
        s = s.Substring(Math.Min(endidx + 1, s.Length));
        return partUntilSpace;
    }

};