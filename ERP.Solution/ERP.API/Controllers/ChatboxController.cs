using ERP.HRService.Data;
using ERP.HRService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ERP.API.Controllers
{
    [Route("dms/chatbox")]
    [ApiController]
    public class ChatboxController : ControllerBase
    {
        private readonly HRDbContext _context;
        private readonly HttpClient _httpClient;

        public ChatboxController(HRDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        //private async Task AddMessage(string message, string role, string conversationId)
        //{
        //    var chatMessage = new ChatMessage
        //    {
        //        ConversationId = conversationId,
        //        Message = message,
        //        Role = role
        //    };
        //    _context.ChatMessages.Add(chatMessage);
        //    await _context.SaveChangesAsync();
        //}
        private async Task AddMessage(string message, string role, string conversationId)
        {
            try
            {
                


                //ConversationId = conversationId


                var chatMessage = new ChatMessage
                {
                    ConversationId = conversationId,
                    Message = message,
                    Role = role
                };

                _context.ChatMessages.Add(chatMessage);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    Console.WriteLine("No rows affected. Possible issue with foreign keys or context.");
                }
                else
                {
                    Console.WriteLine($"{result} message(s) saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddMessage error: {ex.Message}");
            }
        }


        [Route("GetResponse")]
        [HttpGet]
        public async Task<IActionResult> GetResponse(string query, string sessionId, string userId)
        {
            using var client = new HttpClient();

            // Correct the payload keys to match API expectations
            var payload = new
            {
                session_id = sessionId,
                query = query
            };

            const string url = "https://ai.aitalkx.com/es_agentic_summary";

            await AddMessage(query, "user", sessionId);

            try
            {
                var response = await client.PostAsJsonAsync(url, payload);

                response.EnsureSuccessStatusCode(); // throws exception for non-success codes

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(json);

                await AddMessage(json, "rasa-bot", sessionId);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /*
        [Route("GetHistory")]
        [HttpGet]
        public async Task<IActionResult> GetHistory(string userId)
        {
            //var formDetails = await cmsBusiness.GetData("cms", "F_DMS_History", "UserId", userId);
            return Ok();
        }
        */

        [Route("GetChatMessages")]
        [HttpGet]
        public async Task<IActionResult> GetChatMessages(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) return BadRequest("Invalid session ID");

            var messages = await _context.ChatMessages
                .Where(m => m.ConversationId == sessionId)
                .OrderBy(m => m.SentAt)
                .AsNoTracking()
                .ToListAsync();
                
            return Ok(messages);
        }

        [Route("NewConversation")]
        [HttpGet]
        public async Task<IActionResult> NewConversation(string name, string userId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Name and userId cannot be empty.");
            }

            var conversation = new Conversation
            {
                Title = name,
                UserId = userId
            };

            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            return Ok(new { id = conversation.Id });
        }

        /*
        [Route("DeleteHistory")]
        [HttpGet]
        public async Task<IActionResult> DeleteHistory(string sessionId)
        {

            if (string.IsNullOrEmpty(sessionId)) return BadRequest("Invalid session ID");
            var historySuccess = await dmsBusiness.DeleteHistoryById(sessionId);
            if (!historySuccess) return BadRequest();
            // Optionally, delete associated conversation messages
            var conversationTasks = new List<Task>();
            var conversationFormDetails = await cmsBusiness.GetFormDetails(new FormTemplateViewModel
            {
                TemplateCode = "DMS_CONVERSATION",
                DataAction = DataActionEnum.Delete,
                RecordId = sessionId
            });

            conversationTasks.Add(cmsBusiness.ManageForm(conversationFormDetails));
            // Return success response
            await Task.WhenAll(conversationTasks);

            return Ok(new { success = true });
        }
        */
    }
}
