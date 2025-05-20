#pragma warning disable IDE0051

using System.Collections.Generic;
using System.Linq;
using ServerHint.Enums;
using ServerHint.Models;
using Hints;
using UnityEngine;
using LabApi.Features.Wrappers;

namespace ServerHint.Components
{
    /// <summary>
    /// 玩家Hint控制器
    /// </summary>
    public class HudManager : MonoBehaviour
    {
        private const string HudTemplate = "<line-height=95%><voffset=8.5em><alpha=#ff>\n\n\n<align=center>{0}{1}{2}{3}{4}</align>";
        private object[] toFormat;
        private Player player;
        private float globalTimer;
        private string hint;

        /// <summary>
        /// 所有玩家对应的 <see cref="HudManager"/> 组件
        /// </summary>
        public static Dictionary<Player, HudManager> Instances { get; } = new Dictionary<Player, HudManager>();

        /// <summary>
        /// 获取所有附加在 <see cref="DisplayLocation"/> 上的 <see cref="HudDisplay"/>
        /// </summary>
        public SortedList<DisplayLocation, HudDisplay> Displays { get; } =
            new SortedList<DisplayLocation, HudDisplay>
            {
                [DisplayLocation.Top] = new HudDisplay(),
                [DisplayLocation.MiddleTop] = new HudDisplay(),
                [DisplayLocation.Middle] = new HudDisplay(),
                [DisplayLocation.MiddleBottom] = new HudDisplay(),
                [DisplayLocation.Bottom] = new HudDisplay(),
            };

        /// <summary>
        /// 向玩家发送Hint
        /// </summary>
        /// <param name="player">目标玩家</param>
        /// <param name="message">发送的信息</param>
        /// <param name="duration">发送的时间</param>
        /// <param name="overrideCurrent">覆盖其他的Hint</param>
        /// <param name="displayLocation">Hint位置</param>
        public static void ShowHint(Player player, string message, float duration = 3f, bool overrideCurrent = true, DisplayLocation displayLocation = DisplayLocation.MiddleBottom)
        {
            if (Instances.TryGetValue(player, out HudManager hudManager))
                hudManager.Displays[displayLocation].Enqueue(message, duration, overrideCurrent);
        }
        /// <summary>
        /// 删除 <see cref="HudManager"/>
        /// </summary>
        public void Destroy() => Destroy(this);
        void Start()
        {
            player = Player.Get(gameObject);
            Instances.Add(player, this);
        }
        void FixedUpdate()
        {
            globalTimer += Time.deltaTime;
            if (globalTimer > 1f)
            {
                UpdateHints();
                globalTimer = 0;
            }
        }
        void OnDestroy()
        {
            Instances.Remove(player);
            foreach (var display in Displays.Values)
                display.Kill();
        }
        private void UpdateHints()
        {
            toFormat = Displays.Values.Select(display => FormatStringForHud(display.Content ?? string.Empty, 6)).ToArray<object>();
            hint = string.Format(HudTemplate, toFormat);
            HintParameter[] parameters =
            {
                new StringHintParameter(hint),
            };
            player.ReferenceHub.hints.Show(new TextHint(hint, parameters, durationScalar: 2));
        }
        private string FormatStringForHud(string text, int needNewLine)
        {
            int curNewLine = text.Count(x => x == '\n');
            for (int i = 0; i < needNewLine - curNewLine; i++)
                text += '\n';
            return text;
        }
    }
}