using System.Collections.Generic;
using ServerHint.Components;
using MEC;

namespace ServerHint.Models
{
    /// <summary>
    /// <see cref="HudManager"/> 中负责处理显示服务的一部分
    /// </summary>
    public class HudDisplay
    {
        private readonly Queue<Hint> queue = new Queue<Hint>();
        private readonly CoroutineHandle coroutineHandle;
        private bool breakNextFrame;

        /// <summary>
        /// 创建 <see cref="HudDisplay"/> 的实例类
        /// </summary>
        public HudDisplay()
        {
            coroutineHandle = Timing.RunCoroutine(HandleDequeue());
        }
        /// <summary>
        /// Hint的文本
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 向队列插入Hint
        /// </summary>
        /// <param name="content">显示文本</param>
        /// <param name="duration">显示时间</param>
        /// <param name="overrideQueue">是否覆盖其他Hint</param>
        public void Enqueue(string content, float duration, bool overrideQueue)
        {
            Hint hint = new Hint(content, duration);
            if (overrideQueue)
            {
                queue.Clear();
                breakNextFrame = true;
            }

            queue.Enqueue(hint);
        }
        /// <summary>
        /// 关闭Hint进程
        /// </summary>
        public void Kill()
        {
            Timing.KillCoroutines(coroutineHandle);
        }
        private IEnumerator<float> HandleDequeue()
        {
            while (true)
            {
                if (queue.TryDequeue(out Hint hint))
                {
                    breakNextFrame = false;
                    Content = hint.Content;
                    for (int i = 0; i < 50 * hint.Duration; i++)
                    {
                        if (breakNextFrame)
                        {
                            breakNextFrame = false;
                            break;
                        }

                        yield return Timing.WaitForOneFrame;
                    }

                    Content = string.Empty;
                    continue;
                }

                yield return Timing.WaitForOneFrame;
            }
        }
    }
}