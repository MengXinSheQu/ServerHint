namespace ServerHint.Models
{
    /// <summary>
    /// Hint文本结构
    /// </summary>
    public struct Hint
    {
        /// <summary>
        /// 创建一个 <see cref="Hint"/> 结构
        /// </summary>
        /// <param name="content"><inheritdoc cref="Content"/></param>
        /// <param name="duration"><inheritdoc cref="Duration"/></param>
        public Hint(string content, float duration)
        {
            Content = content;
            Duration = duration;
        }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 显示时间
        /// </summary>
        public float Duration { get; set; }
    }
}