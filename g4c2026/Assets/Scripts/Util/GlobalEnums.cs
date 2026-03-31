using System.Collections.Generic;

// Audio

public enum AudioSetting {
    Music,
    SFX,
    Environment,
}

public enum AudioType {
    BGM,
    CameraClick,
    Rain,
    Walking,
    Null,
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
    Enter,
    Heal,
    Interact,
    Inform,
    Give,
    Obtain,
    Open,
    Picture,
    Turn,
    Wait
}

public enum SetupType {
    Path,
    Inform,
    Interact
}

public struct Setup {
    public SetupType SetupType;
    public List<string> Arguments;
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

// Interaction

public enum CharacterInteractionType { // this is what they expect the players to do to them
    Dialogue,
    Give,
    Get,
    Inform,
    None,
}

public enum TreeInteractionType {
    Grow,
    None,
}

// Item

public enum Character {
    Player,
    Narrator,
    Forest1HabitatA,
    Forest1,
    Forest2,
    Fishing1,
    Fishing2,
    Fishing3,
    Farm1,
}

public enum Item {
    None,
    RedBerry,
    OrangeBerry,
    Mushroom,
    RedCan,
    RedCanCrushed,
    BlueCan,
    BlueCanCrushed,
    Stick
}

public enum Location {
    
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
    Forest1SA,
    Forest1SB,
    Forest1SC,
    Forest1SHA,
    Forest1SHB,
    Forest1SHC,

    // Forest2
    Forest2A,
    Forest2B,
    Forest2SA,
    Forest2SB,
    Forest2SHA,
    Forest2SHB,

    // Farm
    Farm1A,
    Farm1B,
    Farm1SA,
    Farm1SHA,

    // Port
    Port1A,
    Port1B,
    Port1SA,
    Port1SHA,

    // Miscellaneous
    FishStash,
    BunnyTrash,
    BunnyDirt,
    DuckGroup,
    FoxApple,
    BunnyApple,
    SallyHoard,
    SammyGreed,
}

// World Generation
public enum ObjectType {
    TREE1 = 0,
    TREE2,
    TREEBARE,
    TREETRUNK,
    GROUNDCORNER = 4,
    GROUNDCENTER,
    GROUNDROUNDED,
    GROUNDGREEN,
    GROUNDBARE,
    GROUNDSIDE,
    BUSH,
    FLOWERBUSH,
    ORANGEFLOWERBUSH,
    WHITEFLOWERBUSH,
    BERRY,
    MUSHROOM,
    ORANGEBERRY,
    DARKGRASS,
    GRASSLEAVES,
    LEAVES,
    LIGHTGRASS,
    LIGHTSPECKLE,
    MEDIUMGRASS,
    MEDIUMSPECKLE,
    PATCHOFGRASS,
    TRASHCAN,
    REDCAN,
    REDCANCRUSHED,
    BLUECAN,
    BLUECANCRUSHED,
    BIRD,
    FARM,
    ALGAE,
    BOAT,
    CAMPINGTRUCK,
    OPENCRATE,
    BIGROCK,
    BIGTREE,
    CARROT,
    CRATE,
    DECORATIVECIRCLE,
    DIRTPILE,
    DOCK,
    FENCE,
    WATER,
    DEFORESTATION1,
    DEFORESTATION2,
    DEFORESTATION3,
    FARM1,
    PORT1,
}
