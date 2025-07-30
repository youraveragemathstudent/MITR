// Ripped from github gamesfiles of ultrabingo (BaphometsBingo v1.1.2 by Clearwater) and repurposed for use //

// Things I think I need to reference to make this shit work //
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

// MITR = Multiplayer In The Revamp //
// PITR = Player In The Revamp //

namespace MITR;

// Loads all assets needed to run the gui as classes stuff //
public static class AssetLoader
{
    // idk what this for //
    public static AssetBundle Assets;
    
    // Fonts for Ultrakill GUI and possibly chat if I get to that //
    public static TMP_FontAsset GameFont;
    public static Font GameFontLegacy;
    
    // why do I need this again? //
    public static Sprite UISprite;
    
    // UI Shit, names are self explanatory //
    public static GameObject MITREntryButton;
    public static GameObject MITRPauseButton;
    public static GameObject MITRSettings;
    public static GameObject MITRLobbiesSelectionMenu;
    public static GameObject MITRServerNuke;
    public static GameObject MITRMapSelectionMenu;
    public static GameObject MITRSkipScene;

    // idk if this is worth it to make work, I originally said bare bones but oh well.... //
    public static GameObject MITRInGameChat;
    
    // Equip feedbacker, reject knuckleblaster (reminds you if no arms equipped)//
    public static AudioClip NoArmsEquipped;
    
    // no arguments needed to load asset essential to the mod //
    public static void LoadAssets()
    {
        // I have yet to undertand the purpose of this line //
        Assets = AssetBundle.LoadFromFile(Path.Combine(Main.ModFolder,"MITR.resource"));
        
        // Font for UI loading thingy //
        GameFont = Assets.LoadAsset<TMP_FontAsset>("VCR_OSD_MONO_EXTENDED_TMP"); // No idea what this looks like. //
        GameFontLegacy = Assets.LoadAsset<Font>("VCR_OSD_MONO_LEGACY"); // Same as the comment written above. //
        
        // UI Button loading thingy //
        MITREntryButton = Assets.LoadAsset<GameObject>("MITREntryButton"); // Titled button to yet another UI. //
        MITRPauseCard = Assets.LoadAsset<GameObject>("MITRPauseButton"); // Sometimes there is moreto life than looking at a screen. //
        MITRMainMenu = Assets.LoadAsset<GameObject>("MITRSettings"); // Fix the shit that you can fix here. //
        MITRLobbyMenu = Assets.LoadAsset<GameObject>("MITRLobbiesSelectionMenu"); // join or create public server. //
        MITREndScreen = Assets.LoadAsset<GameObject>("MITRServerNuke"); // Time to server hop. //
        MITRMapSelectionMenu = Assets.LoadAsset<GameObject>("MITRMapSelectionMenu"); // We have reached the end, now what? //
        MITRSkipScene = Assets.LoadAsset<GameObject>("MITRSkipScene"); // I didn't feel like reading all that today. //
        
        MITRInGameChat = Assets.LoadAsset<GameObject>("MITRInGameChat"); // Game chat I think I might add //

        UISprite = Assets.LoadAsset<Sprite>("UISprite"); // Png Thingy //
        NoArmsEquipped = Assets.LoadAsset<AudioClip>("NoArmsEquipped"); // No Arms Equipped. //
    }
}
