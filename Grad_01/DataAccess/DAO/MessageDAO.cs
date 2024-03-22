using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using BusinessObjects.DTO;
using BusinessObjects;

namespace DataAccess.DAO
{
    public class MessageDAO
    {
        public void SendMessage(MessageDTOs messageDTO)
        {
            // Convert MessageDTO to Message entity
            var message = new Message
            {
                SenderId = messageDTO.SenderId,
                ReceiverId = messageDTO.ReceiverId,
                Content = messageDTO.Content,
                Timestamp = DateTime.Now
            };

            // Implement logic to store the message in the database
            // Example implementation:
            using (var context = new AppDbContext())
            {
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public List<MessageDTOs> GetMessages(Guid senderId, Guid receiverId)
        {
            // Implement logic to retrieve messages between the sender and receiver
            // Example implementation:
            using (var context = new AppDbContext())
            {
                var messages = context.Messages
                    .Where(m => m.SenderId == senderId && m.ReceiverId == receiverId)
                    .ToList();

                // Convert messages to MessageDTOs
                var messageDTOs = new List<MessageDTOs>();
                foreach (var message in messages)
                {
                    messageDTOs.Add(new MessageDTOs
                    {
                        SenderId = message.SenderId,
                        ReceiverId = message.ReceiverId,
                        Content = message.Content,
                        Timestamp = message.Timestamp
                    });
                }

                return messageDTOs;
            }
        }
    }
}
