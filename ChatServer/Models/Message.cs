using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatServer.Models
{
    /// <summary>
    /// 消息基类
    /// </summary>
    public abstract class MessageBase
    {
        public int Id { get; set; }
        //public int FromId { get; set; }
        //public int ToId { get; set; }
        public string FromUsername { get; set; }
        public string ToUsername { get; set; }
        public string TargetNickName { get; set; }
        public string SendTime { get; set; }
        public bool IsRead { get; set; }
        public bool IsSelf { get; set; }
    }
    /// <summary>
    /// 文字消息
    /// </summary>
    public class Message : MessageBase
    {
        public string Content { get; set; }
    }
    /// <summary>
    /// 图片消息
    /// </summary>
    public class ImageMessage : MessageBase
    {
        public byte[] Image { get; set; }
    }
}