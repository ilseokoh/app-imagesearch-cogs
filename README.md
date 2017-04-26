## Image Search ������Ʈ

���� �����ڴ� ����ũ�μ���Ʈ���� Xamarin PM(Product Manager)�� [James Montemagno](https://github.com/jamesmontemagno) �̰� [���� Repository](https://github.com/jamesmontemagno/app-imagesearch-cogs)���� fork �ؼ� �ѱ�ȭ �߽��ϴ�. 

�� ������Ʈ�� [Xamarin](http://xamarin.com) �����Դϴ�. �ܺ� ���񽺷� Microsoft Cognitive Services�� �ִ� Bing Search�� ������ �ִ� �ι� �󱼿��� ������ �˾Ƴ��� Emotion API�� ����߽��ϴ�. 

����� ������ ����� �Ǿ� ������ ä�� 9: https://channel9.msdn.com/Events/dotnetConf/2016/iOS--Android-Development-for-the-C-Developer-with-Xamarin

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