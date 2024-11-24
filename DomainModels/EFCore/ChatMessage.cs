using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.EFCore
{
    public class ChatMessage : Common
    {
        public string User { get; set; }
        public string Message { get; set; }
    }
} 