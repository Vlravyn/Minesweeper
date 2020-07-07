using Minesweeper.Services.Interfaces;

namespace Minesweeper.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}