using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Models;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace BusinessLogic.Controllers
{
    [PluginController("YouTube")]
    public class YouTubeApiController : UmbracoAuthorizedApiController
    {
        
        [HttpGet]
        public string GetHello(string name)
        {
            return "Hello " + name;
        }
        
        [HttpPost]
        public SearchListResponse VideosForChannel(ApiModel model)
        {
            //Convert string orderby to the enum that we expect
            try
            {
                var order = (SearchResource.ListRequest.OrderEnum)Enum.Parse(typeof(SearchResource.ListRequest.OrderEnum), model.OrderBy, true);

                //Go & get the videos
                var channelVideos = YouTubeHelper.GetVideosForChannel(model.PageToken, model.ChannelId, model.SearchQuery, order);

                //Return the response from YouTube API
                return channelVideos;

            }
            catch (ArgumentException)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest);
                message.Content = new StringContent("Order by cannot be converted to the enum");

                throw new HttpResponseException(message);
            }
        }

        
        [HttpGet]
        public ChannelListResponse ChannelFromId(string channelId)
        {
            return YouTubeHelper.GetChannelFromId(channelId);
        }

        
        [HttpGet]
        public ChannelListResponse ChannelFromUsername(string username)
        {
            return YouTubeHelper.GetChannelFromUsername(username);
        }
    }
}
