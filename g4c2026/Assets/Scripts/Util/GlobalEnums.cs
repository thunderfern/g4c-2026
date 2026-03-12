using System.Collections.Generic;

// Audio

public enum AudioType {
    Null
}

public enum AudioPlayType {
    Override,
    Yield,
    Overlap
}

// Game

public enum GameState {
    Movement,
    Dialogue,
    Picture,
    Photobook,
    Settings,
    MainMenu
}

public enum GoalType {
    Interact,
    Give,
    Go,
    Picture
}

public struct StoryGoals {
    public string StoryName;
    public List<Goal> Goals;
}

public struct Goal {
    public GoalType GoalType;
    public List<string> Arguments;
    public string GoalDescription;

    public bool Equals(Goal other) {
        if (GoalType != other.GoalType) return false;
        if (Arguments.Count != other.Arguments.Count) return false;
        for (int i = 0; i < Arguments.Count; i++) if (Arguments[i] != other.Arguments[i]) return false;
        return true;
    }
}

// Item

public enum Character {
    Player,
    Narrator,
}


public enum Item {
    
}

public enum Location {
    
}

// Dialogue

public enum CharacterBackground {
    Blue
    
}

// Picture
public enum ThreatSection {
    Forest1,
    Forest2,
    None,
}

public enum ThreatSubSection {

    // Forest1
    Forest1A,
    Forest1B,
    Forest1HabitatA,
    Forest2HabitatB,

    // Forest2
    Forest2A,
    Forest2B,
}





