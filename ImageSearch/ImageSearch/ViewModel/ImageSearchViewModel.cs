using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using ImageSearch.Services;
using System.Net.Http;
using Newtonsoft.Json;
using ImageSearch.Model;
using Plugin.Media;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using ImageSearch.Model.BingSearch;
using Plugin.Connectivity;

namespace ImageSearch.ViewModel
{
    public class ImageSearchViewModel
    {
        public ObservableRangeCollection<ImageResult> Images { get; }

        public ImageSearchViewModel()
        {
            Images = new ObservableRangeCollection<ImageResult>();
        }
        
        /// <summary>
        /// Bing Search API에 검색 요청
        /// </summary>
        /// <param name="query">검색어</param>
        /// <returns></returns>
        public async Task<bool> SearchForImagesAsync(string query)
        {
            if(!CrossConnectivity.Current.IsConnected)
            {
                await UserDialogs.Instance.AlertAsync("You are offline");
                return false;
            }
           
			//Bing Image API URL
			var url = $"https://api.cognitive.microsoft.com/bing/v5.0/images/" + 
				      $"search?q={query}" +
					  $"&count=20&offset=0&mkt=ko-kr&safeSearch=Strict";

            try
            {
                // 헤더에 키 설정
                var headerKey = "Ocp-Apim-Subscription-Key";
                var headerValue = CognitiveServicesKeys.BingSearch;

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add(headerKey, headerValue);

                var json = await client.GetStringAsync(url);

                // JSON 결과 Deserialization
                var stuff = JsonConvert.DeserializeObject<SearchResult>(json);

                // 전체 결과에서 필요한 것만 Select 
                var items = stuff.Images.Select(i => new ImageResult
                {
                    ContextLink = i.ContentUrl,
                    FileFormat = i.EncodingFormat,
                    ImageLink = i.ContentUrl,
                    ThumbnailLink = i.ThumbnailUrl ?? i.ContentUrl,
                    Title = i.Name
                });

                // 컬렉션에 추가
                Images.ReplaceRange(items);

            }
            catch (Exception ex)
            {
                return false;
            }

			return true;
        }

        /// <summary>
        /// Emotion API 에 요청하는 메서드
        /// </summary>
        /// <param name="imageUrl">요청할 이미지 URL</param>
        /// <returns></returns>
        public async Task AnalyzeImageAsync(string imageUrl)
        {
            var result = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var stream = await client.GetStreamAsync(imageUrl);

                    // Emotion 서비스 요청 부분
                    var emotion = await EmotionService.GetAverageHappinessScoreAsync(stream);

                    // 문자열로 공개
                    result = EmotionService.GetHappinessMessage(emotion);
                }
            }
            catch(Exception ex)
            {
                result =  "Unable to analyze image";
            }

            await UserDialogs.Instance.AlertAsync(result);
           
        }
        
        /// <summary>
        /// 사진 찍기 요청
        /// </summary>
        /// <param name="useCamera"></param>
        /// <returns></returns>
        public async Task TakePhotoAndAnalyzeAsync(bool useCamera = true)
        {
            string result = "Error";
            MediaFile file = null;
            IProgressDialog progress;

            try
            {
                // CrossMedia 플러그인을 사용하여 사진찍기
                await CrossMedia.Current.Initialize();

                if (useCamera)
                {
                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "face.jpg",
                        PhotoSize = PhotoSize.Medium
                    });
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }
               
                if (file == null)
                    result = "No photo taken.";
                else
                {
                    var emotion = await EmotionService.GetAverageHappinessScoreAsync(file.GetStream());

                    result = EmotionService.GetHappinessMessage(emotion);
                }
            }
            catch(Exception ex)
            {
                result =  ex.Message;
            }

            await UserDialogs.Instance.AlertAsync(result);
        }

    }
}
