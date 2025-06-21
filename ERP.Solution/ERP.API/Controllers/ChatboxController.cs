////using Synergy.App.ViewModel;
////using Synergy.App.Business;
////using Synergy.App.Common;
////using Synergy.App.Api.Controllers;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Text;
//using System.Net.Http;
//using System.Net.Http.Json;

//namespace Synergy.App.Api.Areas.DMS.Controllers
//{
//    [Route("dms/chatbox")]
//    [ApiController]
//    public class ChatboxController(
//        IServiceProvider serviceProvider)
//        : ControllerBase
//    {
//        private async Task AddMessage(string message, string sender, string sessionId)
//        {
//            //var formDetails = await cmsBusiness.GetFormDetails(new FormTemplateViewModel
//            //{
//            //    TemplateCode = "DMS_CONVERSATION",
//            //    DataAction = DataActionEnum.Create
//            //});
//            var model = new
//            {
//                HistoryId = sessionId,
//                Message = message,
//                Response = sender
//            };
//            var json = JsonConvert.SerializeObject(model);
//            formDetails.Json = json;
//            await cmsBusiness.ManageForm(formDetails);
//        }

//        [Route("GetResponse")]
//        [HttpGet]
//        public async Task<IActionResult> GetResponse(string query, string sessionId, string userId)
//        {
//            using var client = new HttpClient();
//            var payload = new { sender = $@"session_{sessionId}__user_{userId}", message = query };
//            const string url = "https://bot.aitalkx.com/webhooks/rest/webhook";
//            await AddMessage(query, "user", sessionId);
//            try
//            {
//                var response = await client.PostAsJsonAsync(url, payload);
//                var json = await response.Content.ReadAsStringAsync();
//                var data = JsonConvert.DeserializeObject<dynamic>(json);
//                await AddMessage(json, "rasa-bot", sessionId);
//                return Ok(data);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex);
//            }
//        }

//        [Route("GetHistory")]
//        [HttpGet]
//        public async Task<IActionResult> GetHistory(string userId)
//        {
//            //var formDetails = await cmsBusiness.GetData("cms", "F_DMS_History", "UserId", userId);
//            return Ok();
//        }

//        [Route("GetChatMessages")]
//        [HttpGet]
//        public async Task<IActionResult> GetChatMessages(string sessionId)
//        {
//            if (string.IsNullOrEmpty(sessionId)) return BadRequest("Invalid session ID");

//            var messages = await cmsBusiness.GetData("cms", "F_DMS_Conversation", "HistoryId", sessionId);
//            return Ok(messages);
//        }

//        [Route("NewConversation")]
//        [HttpGet]
//        public async Task<IActionResult> NewConversation(string name, string userId)
//        {
//            var formDetails = await cmsBusiness.GetFormDetails(new FormTemplateViewModel
//            {
//                TemplateCode = "DMS_HISTORY",
//                DataAction = DataActionEnum.Create
//            });
//            var model = new { UserId = userId, HistoryName = name };
//            formDetails.Json = JsonConvert.SerializeObject(model);
//            var historyModel = await cmsBusiness.ManageForm(formDetails);

//            if (!historyModel.IsSuccess) return BadRequest();

//            var sessionId = historyModel.Item.RecordId;
//            return Ok(new { id = sessionId });
//        }

//        [Route("DeleteHistory")]
//        [HttpGet]
//        public async Task<IActionResult> DeleteHistory(string sessionId)
//        {

//            if (string.IsNullOrEmpty(sessionId)) return BadRequest("Invalid session ID");
//            var historySuccess = await dmsBusiness.DeleteHistoryById(sessionId);
//            if (!historySuccess) return BadRequest();
//            // Optionally, delete associated conversation messages
//            var conversationTasks = new List<Task>();
//            var conversationFormDetails = await cmsBusiness.GetFormDetails(new FormTemplateViewModel
//            {
//                TemplateCode = "DMS_CONVERSATION",
//                DataAction = DataActionEnum.Delete,
//                RecordId = sessionId
//            });

//            conversationTasks.Add(cmsBusiness.ManageForm(conversationFormDetails));
//            // Return success response
//            await Task.WhenAll(conversationTasks);

//            return Ok(new { success = true });
//        }


//    }

//}
