﻿using System;
using Foundation;
using UIKit;

using ImageSearch.ViewModel;
using SDWebImage;

namespace ImageSearch.iOS
{
    public partial class ViewController : UIViewController, IUICollectionViewDataSource
	{

        ImageSearchViewModel viewModel;


		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            viewModel = new ImageSearchViewModel();

            CollectionViewImages.WeakDataSource = this;


            ButtonSearch.TouchUpInside += async (sender, args) =>
            {
                ButtonSearch.Enabled = false;
                ActivityIsLoading.StartAnimating();

                // iOS 빌트인 애니메이션
                await UIView.AnimateAsync(1.0, () => CollectionViewImages.Alpha = 0);

                // Shared 코드 호출
                await viewModel.SearchForImagesAsync(TextFieldQuery.Text);
                CollectionViewImages.ReloadData();

                await UIView.AnimateAsync(1.0, () => CollectionViewImages.Alpha = 1);

                ActivityIsLoading.StopAnimating();
                ButtonSearch.Enabled = true;
            };

            SetupCamera();
		}

    
        

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


        public nint GetItemsCount(UICollectionView collectionView, nint section) => 
            viewModel.Images.Count;

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("imagecell", indexPath) as ImageCell;

            var item = viewModel.Images[indexPath.Row];

            cell.Caption.Text = item.Title;

            cell.Image.SetImage(new NSUrl(item.ThumbnailLink));



            return cell;
        }

        void SetupCamera()
        {
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(
                UIBarButtonSystemItem.Camera, async delegate
                {
                    ButtonSearch.Enabled = false;
                    ActivityIsLoading.StartAnimating();

                    await viewModel.TakePhotoAndAnalyzeAsync(false);

                    ButtonSearch.Enabled = true;
                    ActivityIsLoading.StopAnimating();
                });
        }
    }
}

