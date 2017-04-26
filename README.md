## Image Search 프로젝트

원래 저작자는 마이크로소프트에서 Xamarin PM(Product Manager)인 [James Montemagno](https://github.com/jamesmontemagno) 이고 [원래 Repository](https://github.com/jamesmontemagno/app-imagesearch-cogs)에서 fork 해서 한글화 했습니다. 

본 프로젝트는 [Xamarin](http://xamarin.com) 샘플입니다. 외부 서비스로 Microsoft Cognitive Services에 있는 Bing Search와 사진에 있는 인물 얼굴에서 감정을 알아내는 Emotion API를 사용했습니다. 

데모와 설명은 영어로 되어 있지만 채널 9: https://channel9.msdn.com/Events/dotnetConf/2016/iOS--Android-Development-for-the-C-Developer-with-Xamarin

## Setup

You must use setup a few API keys that can be set in ImageSearch/Services/ServiceKeys.cs

### Microsoft Cognitive Services

Setup a Emotion API key for Cognitive Services at: https://www.microsoft.com/cognitive-services/

Setup a Bing Search API Key at: https://microsoft.com/cognitive-services


## Open source libraries
The following amazing libraries were used to create this app:

* [User Dialogs Plugin for Xamarin](https://github.com/aritchie/userdialogs)
* [Media Plugin for Xamarin](https://github.com/jamesmontemagno/Xamarin.Plugins/tree/master/Media)
* [MVVM Helpers](https://github.com/jamesmontemagno/mvvm-helpers)
* [Json.NET](http://www.newtonsoft.com/json)

## License

Under MIT

## ObservableRangeCollection

ObservableRangeCollection is a helper class by the Xamarin Evangelist James Montemagno.
The source is available in his github: https://github.com/jamesmontemagno/mvvm-helpers
ObservableRangeCollection intends to help when adding/replacing Collections to a ObservableCollection.
In a "regular" ObservableCollection, for each new item added to the Collection, a OnCollectionChanged event would raise.
This is where ObservableRangeCollection gets in. It allows to replace/add elements to the Collection without firing an event for each element.

https://channel9.msdn.com/Shows/XamarinShow/The-Xamarin-Show-12-MVVM-Helpers