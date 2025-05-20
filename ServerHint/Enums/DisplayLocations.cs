namespace ServerHint.Enums
{
    /// <summary>
    /// 玩家Hint类型
    /// </summary>
    public enum DisplayLocation
    {
        /// <summary>
        /// 屏幕顶端
        /// </summary>
        Top,
        /// <summary>
        /// 在 <see cref="Middle"/> 和 <see cref="Top"/> 之间
        /// </summary>
        MiddleTop,
        /// <summary>
        /// 屏幕正中间
        /// </summary>
        Middle,
        /// <summary>
        /// 在 <see cref="Middle"/> 和 <see cref="Bottom"/> 之间
        /// </summary>
        MiddleBottom,
        /// <summary>
        /// 屏幕底端
        /// </summary>
        Bottom,
    }
}