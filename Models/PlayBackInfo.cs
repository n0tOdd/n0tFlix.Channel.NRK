using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.NRK.Models
{
    public class PlayBackInfo
    {
        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Parent
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Next
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }

            [JsonProperty("parent")]
            public Parent Parent { get; set; }

            [JsonProperty("next")]
            public Next Next { get; set; }
        }

        public class CropInfo
        {
            [JsonProperty("x")]
            public double X { get; set; }

            [JsonProperty("y")]
            public double Y { get; set; }

            [JsonProperty("width")]
            public double Width { get; set; }

            [JsonProperty("height")]
            public double Height { get; set; }
        }

        public class Image
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("cropInfo")]
            public CropInfo CropInfo { get; set; }
        }

        public class CropInfo2
        {
            [JsonProperty("x")]
            public double X { get; set; }

            [JsonProperty("y")]
            public double Y { get; set; }

            [JsonProperty("width")]
            public double Width { get; set; }

            [JsonProperty("height")]
            public double Height { get; set; }
        }

        public class ImageInfo
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("cropInfo")]
            public CropInfo2 CropInfo { get; set; }
        }

        public class WebImage
        {
            [JsonProperty("imageUrl")]
            public string ImageUrl { get; set; }

            [JsonProperty("pixelWidth")]
            public int PixelWidth { get; set; }
        }

        public class Images
        {
            [JsonProperty("imageInfo")]
            public ImageInfo ImageInfo { get; set; }

            [JsonProperty("imageWidthCropInfo")]
            public string ImageWidthCropInfo { get; set; }

            [JsonProperty("webImages")]
            public IList<WebImage> WebImages { get; set; }

            [JsonProperty("isDefaultImage")]
            public bool IsDefaultImage { get; set; }
        }

        public class MediaAsset
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("carrierId")]
            public string CarrierId { get; set; }

            [JsonProperty("webVttSubtitlesUrl")]
            public string WebVttSubtitlesUrl { get; set; }

            [JsonProperty("timedTextSubtitlesUrl")]
            public string TimedTextSubtitlesUrl { get; set; }

            [JsonProperty("bufferDuration")]
            public object BufferDuration { get; set; }
        }

        public class BitrateInfo
        {
            [JsonProperty("startIndex")]
            public int StartIndex { get; set; }

            [JsonProperty("maxIndex")]
            public int MaxIndex { get; set; }
        }

        public class MediaAnalytics
        {
            [JsonProperty("show")]
            public string Show { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("contentLength")]
            public string ContentLength { get; set; }

            [JsonProperty("device")]
            public string Device { get; set; }

            [JsonProperty("playerId")]
            public string PlayerId { get; set; }

            [JsonProperty("deliveryType")]
            public string DeliveryType { get; set; }

            [JsonProperty("cdnName")]
            public string CdnName { get; set; }

            [JsonProperty("playerInfo")]
            public string PlayerInfo { get; set; }
        }

        public class ScoresStatistics
        {
            [JsonProperty("springStreamSite")]
            public string SpringStreamSite { get; set; }

            [JsonProperty("springStreamStream")]
            public string SpringStreamStream { get; set; }

            [JsonProperty("springStreamContentType")]
            public string SpringStreamContentType { get; set; }

            [JsonProperty("springStreamProgramId")]
            public string SpringStreamProgramId { get; set; }
        }

        public class ConvivaStatistics
        {
            [JsonProperty("assetName")]
            public string AssetName { get; set; }

            [JsonProperty("cdnName")]
            public string CdnName { get; set; }

            [JsonProperty("deviceType")]
            public string DeviceType { get; set; }

            [JsonProperty("playerName")]
            public string PlayerName { get; set; }

            [JsonProperty("isLive")]
            public bool IsLive { get; set; }

            [JsonProperty("playerVersion")]
            public string PlayerVersion { get; set; }

            [JsonProperty("contentType")]
            public string ContentType { get; set; }

            [JsonProperty("contentId")]
            public string ContentId { get; set; }

            [JsonProperty("episodeName")]
            public string EpisodeName { get; set; }

            [JsonProperty("seriesName")]
            public string SeriesName { get; set; }

            [JsonProperty("contentLength")]
            public int ContentLength { get; set; }
        }

        public class UsageRights
        {
            [JsonProperty("isGeoBlocked")]
            public bool IsGeoBlocked { get; set; }

            [JsonProperty("availableFrom")]
            public DateTime AvailableFrom { get; set; }

            [JsonProperty("availableTo")]
            public DateTime AvailableTo { get; set; }

            [JsonProperty("hasRightsNow")]
            public bool HasRightsNow { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("mediaElementType")]
            public string MediaElementType { get; set; }

            [JsonProperty("mediaType")]
            public string MediaType { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("mediaUrl")]
            public string MediaUrl { get; set; }

            [JsonProperty("mediaAssets")]
            public IList<MediaAsset> MediaAssets { get; set; }

            [JsonProperty("bitrateInfo")]
            public BitrateInfo BitrateInfo { get; set; }

            [JsonProperty("playerType")]
            public string PlayerType { get; set; }

            [JsonProperty("flashPlayerVersion")]
            public string FlashPlayerVersion { get; set; }

            [JsonProperty("flashPluginVersion")]
            public string FlashPluginVersion { get; set; }

            [JsonProperty("isAvailable")]
            public bool IsAvailable { get; set; }

            [JsonProperty("messageType")]
            public string MessageType { get; set; }

            [JsonProperty("mediaAnalytics")]
            public MediaAnalytics MediaAnalytics { get; set; }

            [JsonProperty("scoresStatistics")]
            public ScoresStatistics ScoresStatistics { get; set; }

            [JsonProperty("convivaStatistics")]
            public ConvivaStatistics ConvivaStatistics { get; set; }

            [JsonProperty("messageId")]
            public object MessageId { get; set; }

            [JsonProperty("isLive")]
            public bool IsLive { get; set; }

            [JsonProperty("usageRights")]
            public UsageRights UsageRights { get; set; }

            [JsonProperty("akamaiBeacon")]
            public string AkamaiBeacon { get; set; }

            [JsonProperty("liveBufferStartTime")]
            public object LiveBufferStartTime { get; set; }

            [JsonProperty("fullTitle")]
            public string FullTitle { get; set; }

            [JsonProperty("mainTitle")]
            public string MainTitle { get; set; }

            [JsonProperty("legalAge")]
            public string LegalAge { get; set; }

            [JsonProperty("relativeOriginUrl")]
            public string RelativeOriginUrl { get; set; }

            [JsonProperty("duration")]
            public string Duration { get; set; }

            [JsonProperty("shortIndexPoints")]
            public IList<object> ShortIndexPoints { get; set; }

            [JsonProperty("hasSubtitles")]
            public bool HasSubtitles { get; set; }

            [JsonProperty("subtitlesDefaultOn")]
            public bool SubtitlesDefaultOn { get; set; }

            [JsonProperty("subtitlesUrlPath")]
            public string SubtitlesUrlPath { get; set; }

            [JsonProperty("seriesId")]
            public string SeriesId { get; set; }

            [JsonProperty("seriesTitle")]
            public string SeriesTitle { get; set; }

            [JsonProperty("episodeNumberOrDate")]
            public string EpisodeNumberOrDate { get; set; }

            [JsonProperty("externalEmbeddingAllowed")]
            public bool ExternalEmbeddingAllowed { get; set; }

            [JsonProperty("startNextEpisode")]
            public int StartNextEpisode { get; set; }
        }
    }
}