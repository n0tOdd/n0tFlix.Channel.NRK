using MediaBrowser.Controller.Channels;
using MediaBrowser.Model.Channels;
using MediaBrowser.Model.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using n0tFlix.Channel.NRK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Channel.NRK
{
    public static class Worker
    {
        /// <summary>
        /// Henter alle channel typene vi bruker som start side da man åpner pluginet
        /// gir en søt liten oversikt over ting som man kanskje vil søke opp
        /// </summary>
        /// <returns><see cref="ChannelItemResult"/> containing the types of categories.</returns>
        public static async Task<ChannelItemResult> GetChannelCategoriesAsync(ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue("nrk-categories", out ChannelItemResult cachedValue))
            {
                logger.LogDebug("Function={function} FolderId={folderId} Cache Hit", nameof(GetChannelCategoriesAsync), "nrk-categories");
                return cachedValue;
            }
            else
            {
                logger.LogDebug("Function={function} FolderId={folderId} web download", nameof(GetChannelCategoriesAsync), "nrk-categories");
                HttpClient httpClient = new HttpClient();
                string json = new StreamReader(await httpClient.GetStreamAsync("https://psapi.nrk.no/tv/pages")).ReadToEnd();
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<Categories.root>(json);
                ChannelItemResult result = new ChannelItemResult();
                foreach (var v in root.PageListItems)
                {
                    result.Items.Add(new ChannelItemInfo
                    {
                        Id = "https://psapi.nrk.no/tv/pages/" + v.Id,
                        Name = v.Title,
                        FolderType = ChannelFolderType.Container,
                        Type = ChannelItemType.Folder,
                        MediaType = ChannelMediaType.Video,
                        HomePageUrl = "https://tv.nrk.no" + v.Links.Self.Href,
                        ImageUrl = v.Image.WebImages[0].Uri ?? v.Image.WebImages[1].Uri ?? v.Image.WebImages[2].Uri ?? v.Image.WebImages[3].Uri ?? v.Image.WebImages[4].Uri
                    });
                    result.TotalRecordCount++;
                }
                memoryCache.Set("nrk-categories", result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        public static async Task<ChannelItemResult> GetCategoryItemsAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue("nrk-categories-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogDebug("Function={function} FolderId={folderId} Cache Hit", nameof(GetCategoryItemsAsync), query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogDebug("Function={function} FolderId={folderId} web download", nameof(GetCategoryItemsAsync), query.FolderId);
                HttpClient httpClient = new HttpClient();
                string json = new StreamReader(await httpClient.GetStreamAsync(query.FolderId)).ReadToEnd();
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<CategoryItems.root>(json);
                ChannelItemResult result = new ChannelItemResult();
                foreach (var v in root.Sections)
                {
                    foreach (var p in v.Included.Plugs)
                    {
                        try

                        {
                            string mainurl = string.Empty;
                            if (p.TargetType == "series")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no/tv/catalog" + p.Series.Links.Self.Href,
                                    Name = p.DisplayContractContent.ContentTitle,
                                    ImageUrl = p.DisplayContractContent.DisplayContractImage.WebImages[0].Uri ?? p.DisplayContractContent.DisplayContractImage.WebImages[1].Uri,
                                    FolderType = ChannelFolderType.Container,
                                    Type = ChannelItemType.Folder,
                                    SeriesName = p.DisplayContractContent.ContentTitle,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.Series.Links.Self.Href,
                                    Overview = p.DisplayContractContent.Description,
                                });
                                result.TotalRecordCount++;
                            }
                            else if (p.TargetType == "standaloneProgram")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no" + p.StandaloneProgram.Links.Playback.Href,
                                    Name = p.DisplayContractContent.ContentTitle,
                                    ImageUrl = p.DisplayContractContent.DisplayContractImage.WebImages[0].Uri ?? p.DisplayContractContent.DisplayContractImage.WebImages[1].Uri,
                                    FolderType = ChannelFolderType.Container,
                                    Type = ChannelItemType.Media,
                                    Overview = p.DisplayContractContent.Description,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.StandaloneProgram.Links.Self.Href,
                                });
                                result.TotalRecordCount++;
                            }
                            else if (p.TargetType == "episode")
                            {
                                result.Items.Add(new ChannelItemInfo
                                {
                                    Id = "https://psapi.nrk.no" + p.Episode.Links.Playback.Href,
                                    Name = p.DisplayContractContent.ContentTitle,
                                    ImageUrl = p.DisplayContractContent.DisplayContractImage.WebImages[0].Uri ?? p.DisplayContractContent.DisplayContractImage.WebImages[1].Uri ?? p.DisplayContractContent.FallbackImage.WebImages[0].Uri,
                                    FolderType = ChannelFolderType.Container,
                                    Type = ChannelItemType.Media,
                                    Overview = p.DisplayContractContent.Description,
                                    MediaType = ChannelMediaType.Video,
                                    HomePageUrl = "htps://tv.nrk.no" + p.Episode.Links.Self.Href,
                                });
                                result.TotalRecordCount++;
                            }
                        }
                        catch (Exception e)
                        {
                            logger.LogError("Error trying to parse all category items from the nrk web tv channel");
                            logger.LogError(e.Message);
                        }
                    }
                }
                memoryCache.Set("nrk-categories-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        public static async Task<ChannelItemResult> GetSeasonInfoAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue("nrk-categories-seasoninfo-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogDebug("Function={function} FolderId={folderId} Cache Hit", nameof(GetSeasonInfoAsync), "nrk-categories-seasoninfo-" + query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogDebug("Function={function} FolderId={folderId} web download", nameof(GetSeasonInfoAsync), "nrk-categories-seasoninfo-" + query.FolderId);
                HttpClient httpClient = new HttpClient();
                string json = new StreamReader(await httpClient.GetStreamAsync(query.FolderId)).ReadToEnd();
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<SeasonInfo.root>(json);
                ChannelItemResult result = new ChannelItemResult();

                foreach (var emb in root.Embedded.Seasons)
                {
                    ChannelItemInfo info = new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        SeriesName = root.Sequential.Titles.Title,
                        Name = emb.Titles.Title,
                        Overview = root.Sequential.Titles.Subtitle,
                        HomePageUrl = "https://tv.nrk.no" + emb.Links.Series.Href,
                        Id = "https://psapi.nrk.no" + emb.Links.Self.Href,
                        MediaType = ChannelMediaType.Video,
                        Type = ChannelItemType.Folder
                    };
                    result.Items.Add(info);
                    result.TotalRecordCount++;
                }
                memoryCache.Set("nrk-categories-seasoninfo-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
        }

        public static async Task<ChannelItemResult> GetEpisodeInfoAsync(InternalChannelItemQuery query, ILogger logger, IMemoryCache memoryCache)
        {
            if (memoryCache.TryGetValue("nrk-episodeinfo-" + query.FolderId, out ChannelItemResult cachedValue))
            {
                logger.LogDebug("Function={function} FolderId={folderId} Cache Hit", nameof(GetSeasonInfoAsync), "nrk-episodeinfo-" + query.FolderId);
                return cachedValue;
            }
            else
            {
                logger.LogDebug("Function={function} FolderId={folderId} web download", nameof(GetCategoryItemsAsync), "nrk-episodeinfo-" + query.FolderId);
                HttpClient httpClient = new HttpClient();
                string json = new StreamReader(await httpClient.GetStreamAsync(query.FolderId)).ReadToEnd();
                var root = Newtonsoft.Json.JsonConvert.DeserializeObject<EpisodeInfo.root>(json);
                ChannelItemResult result = new ChannelItemResult();

                foreach (var ep in root.Embedded.Episodes)
                {
                    ChannelItemInfo info = new ChannelItemInfo()
                    {
                        FolderType = ChannelFolderType.Container,
                        SeriesName = root.Titles.Title,
                        Name = ep.Titles.Title,
                        ImageUrl = ep.Image[0].Url ?? ep.Image[1].Url ?? ep.Image[2].Url ?? ep.Image[3].Url ?? ep.Image[4].Url ?? ep.Image[5].Url,
                        Overview = ep.Titles.Subtitle,
                        HomePageUrl = ep.Links.Share.Href,
                        Id = "https://psapi.nrk.no" + ep.Links.Playback.Href,
                        MediaType = ChannelMediaType.Video,
                        Type = ChannelItemType.Media
                    };
                    result.Items.Add(info);
                    result.TotalRecordCount++;
                }
                memoryCache.Set("nrk-episodeinfo-" + query.FolderId, result, DateTimeOffset.Now.AddDays(7));
                return result;
            }
            return null;
        }

        public static async Task<IEnumerable<MediaSourceInfo>> GetChannelItemMediaInfo(string id, ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogDebug("Grabbing stream data for " + id);
            HttpClient httpClient = new HttpClient();
            string json = new StreamReader(await httpClient.GetStreamAsync(id), true).ReadToEnd();
            var root = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayBackInfo.root>(json);
            return new List<MediaSourceInfo>()
            {
                 new MediaSourceInfo()
                 {
                     Name = root.Title,
                     Path = root.MediaUrl,
                     Protocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     Id = root.Id,
                      IsRemote = true,
                     EncoderProtocol = MediaBrowser.Model.MediaInfo.MediaProtocol.File,
                     VideoType = MediaBrowser.Model.Entities.VideoType.VideoFile
                 }
            };
        }
    }
}