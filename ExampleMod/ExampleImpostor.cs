using System;
using System.Collections.Generic;
using BepInEx.IL2CPP;
using PeasAPI;
using PeasAPI.Components;
using PeasAPI.CustomButtons;
using PeasAPI.Options;
using PeasAPI.Roles;
using UnityEngine;

namespace ExampleMod
{
    //That attribute registers the role so it will be loaded in the game
    [RegisterCustomRole]
    public class ExampleImpostor : BaseRole
    {
        public ExampleImpostor(BasePlugin plugin) : base(plugin)
        {
        }

        // [REQUIRED]
        //The name of the role which will be shown in the intro and below the name
        public override string Name => "Example Impostor";
        
        // [REQUIRED]
        //The description that will be shown in the intro
        public override string Description => "Be a good impostor";
        
        // [REQUIRED]
        //The description that will be shown in the game settings
        public override string LongDescription =>
            "This is an impostor that is an example to understand how to make one";
        
        //The image that will be shown in the game settings
        public override Sprite Icon => Utility.CreateSprite("ExampleMod.Resources.Role.png");
        
        // [REQUIRED]
        //The text that will be added to the task list
        public override string TaskText => "Be a good impostor";
        
        // [REQUIRED]
        //The color of the role, will be shown in the intro, the name, the settings etc
        public override Color Color => Palette.ImpostorRed;
        
        // [REQUIRED]
        //This defines who can see that the player is the role. If you choose custom you have to override the method IsRoleVisible()
        public override Visibility Visibility => Visibility.Impostor;
        
        // [REQUIRED]
        //This defines one who's team the role is. If it's impostor the role will be assigned to an impostor else to a crewmate
        public override Team Team => Team.Impostor;

        // [OPTIONAL]
        //If the player should get tasks
        public override bool AssignTasks => true;
        
        // [REQUIRED]
        //This defines if the player is supposed to do tasks
        public override bool HasToDoTasks => false;

        // [OPTIONAL]
        //If this is enabled an option will be added to the role settings, which will set the count and chance, despite what you define in the code
        public override bool CreateRoleOption => true;
        
        // [OPTIONAL]
        //The maximum amount of players that can get the role
        public override int MaxCount => 3;
        
        // [OPTIONAL]
        //If you have disabled the role option, this will be the amount of times someone will get the role
        public override int Count => 0;

        // [OPTIONAL]
        //If you have disabled the role option, this will be the chance if someone gets the role
        public override int Chance => 100;

        // [OPTIONAL]
        //The option that you can change in the advanced options menu of the role in the role settings
        public override Dictionary<string, CustomOption> AdvancedOptions { get; set; } = new()
        {
            {
                //The name of the option which will only be used in the code
                "ExampleNumberOption", 
                //The custom number
                new CustomNumberOption("IdOfOptionWithoutSpacesAndOnlyLetters", "Example Number Option", 0f, 10f, 1f, 5f, NumberSuffixes.None)
            }
        };

        // [OPTIONAL]
        //The prefix of the options that will be displayed in the text in the lobby
        public override string AdvancedOptionsPrefix => "└ ";

        // [OPTIONAL]
        //The array of gamemodes in which you can play this role (if you add gamemodes you can only play the role in those)
        public override Type[] GameModeWhitelist => Array.Empty<Type>();

        // [OPTIONAL]
        //The kill range the player with the role has if it can kill, currently it's set to use the default value
        public override float KillDistance { get; set; } = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];

        // [OPTIONAL]
        //If the player is able to kill in general (if victim is null) or if the player is allowed to kill a certain player
        public override bool CanKill(PlayerControl victim = null) => !victim || !victim.Data.Role.IsImpostor;
        
        // [OPTIONAL]
        //If the player is able to vent
        public override bool CanVent => true;
        
        // [OPTIONAL]
        //The systems the player can sabotage
        public override bool CanSabotage(SystemTypes? sabotage) => true;
        
        // [OPTIONAL]
        //If the game should end with that reason
        public override bool ShouldGameEnd(GameOverReason reason) => true;

        public CustomButton SomeFancyButton;

        // LISTENERS
        //All of them are optional and pretty self explaining

        public override void OnGameStart()
        {
            //Creating a button should be done in the game start listener
            //The order of the optional arguments doesn't matter and you also don't need the "nameOfTheArgument:" part for the required arguments
            SomeFancyButton = CustomButton.AddButton(onClick: () => { /* What should happen now */ }, cooldown: 5f, image: Utility.CreateSprite("ExampleMod.Resources.Button.png", pixelsPerUnit: 128f), 
                couldBeUsed: player => !player.Data.IsDead && player.IsRole(this),  canBeUsed: player => true, 
                /* Every argument that now follows is not required */ positionOffset: new Vector2(0f, 0f), effectDuration: 5f, onEffectEnd: () => { /* What should happen now */ }, text: "<size=40%>Button", textOffset: new Vector2(0f, 0.5f), 
                target: CustomButton.TargetType.Player, targetColor: Color.magenta, choosePlayerTarget: player => player.Data.Role.IsImpostor);
            //Two optional argumetns are left out, "chooseObjectTarget" and "chooseObjectTargetCustom". They work like "choosePlayerTarget" but they use a GameObject
        }
        public override void OnGameStop() { }
        public override void OnUpdate() { }
        public override void OnMeetingUpdate(MeetingHud meeting) { }
        //If this returns false, the player will not be killed
        public override bool PreKill(PlayerControl killer, PlayerControl victim)
        {
            return true;
        }
        public override void OnKill(PlayerControl killer, PlayerControl victim) { }
        //If this returns false, the player will not be exlied
        public override bool PreExile(PlayerControl victim)
        {
            return true;
        }
        public override void OnExiled(PlayerControl victim) { }
        public override void OnRevive(PlayerControl player) { }
        public override void OnTaskComplete(PlayerControl player, PlayerTask task) { }
    }
}