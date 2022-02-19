using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;

namespace ExampleMod
{
    [BepInPlugin(Id, Name, Version)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    [BepInDependency(PeasAPI.PeasAPI.Id)]
    public class ExampleModPlugin : BasePlugin
    {
        //For peasmod: tk.peasplayer.amongus.peasmod
        //I use tk because peasplayer.tk is my website
        //I also added .amongus so if I make a mod for a different game called Peasmod you could separate them
        public const string Id = "countryCodeOrDomain.nameOfCreator.nameOfMod";

        //These two things should be the same as in your .csproj
        public const string Name = "ExampleMod";
        public const string Version = "1.0.0";
        
        public Harmony Harmony { get; } = new Harmony(Id);
        
        public override void Load()
        {
            //This is how you can show messages in the console
            Logger<ExampleModPlugin>.Info("ExampleMod has been loaded!");

            Harmony.PatchAll();
        }
    }
}