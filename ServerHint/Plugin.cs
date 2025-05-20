using HarmonyLib;
using LabApi.Events.Handlers;
using LabApi.Loader.Features.Plugins;
using System;

namespace ServerHint
{
    public class Plugin : Plugin<Config>
    {
        private Harmony harmony;
        public override string Author => "原作者: LulaczTV , 萌新社区开发团队进行重置";
        public override string Name => "ServerHint";
        public override string Description => "一个玩家的Hint显示框架";
        public override Version Version => new Version(0, 7, 2, 1);
        public override Version RequiredApiVersion => new Version(0, 7, 2, 1);
        public override void Enable()
        {
            harmony = new Harmony($"ServerHint.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();
            PlayerEvents.Left += EventHandlers.OnLeft;
            PlayerEvents.Joined += EventHandlers.OnJoined;
        }
        public override void Disable()
        {
            PlayerEvents.Left -= EventHandlers.OnLeft;
            PlayerEvents.Joined -= EventHandlers.OnJoined;
            harmony.UnpatchAll();
            harmony = null;
        }
    }
}