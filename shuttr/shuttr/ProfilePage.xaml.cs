﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shuttr
{
    /// <summary>
    /// Interaction logic for PhotosPage.xaml
    /// </summary>
    public partial class ProfilePage : UserControl
    {
        private MainWindow parent;
        private User displayedUser;

        /// <summary>
        /// Creates a new profile page for the specified user.
        /// Whenever a new user logs in, this constructor should be called.
        /// </summary>
        /// <param name="main"> The parent of this profile page </param>
        /// <param name="currentUser"> The user that is currently signed in </param>
        public ProfilePage(MainWindow main, User currentUser)
        {
            InitializeComponent();

            parent = main;
            displayedUser = currentUser;
            userName.Content = currentUser.UserName;
            profilePicture.Source = currentUser.ProfilePicture;
            DisplayPosts();

            if (displayedUser.Biography != null)
            {
                biographyText.Text = displayedUser.Biography;
            }
        }

        /// <summary>
        /// Set the MainWindow that owns this profile page.
        /// Parent is required to allow popups for Photos and Discussions.
        /// </summary>
        /// <param name="main"> The parent of this profile page </param>
        public void SetParent(MainWindow main)
        {
            parent = main;
            if (parent.followingSomeone)
            {
                //followButton.Content = "UNFOLLOW";
            }
            else
            {
                //followButton.Content = "FOLLOW";
            }
        }

        /// <summary>
        /// Assigns an appropriate click handler to a post.
        /// </summary>
        /// <param name="post"> A Photo or Discussion post </param>
        private void MakePostClickable(UserControl post)
        {
            if (post.GetType() == typeof(Discussion))
            {
                post.MouseLeftButtonDown += new MouseButtonEventHandler(this.DiscussionClick);
            }
            else
            {
                post.MouseLeftButtonDown += new MouseButtonEventHandler(this.PhotoClick);
            }
        }

        public void PhotoClick(object sender, MouseButtonEventArgs e)
        {
            Photo tmp = (Photo)sender;
            PhotoPopup photoPopup = new PhotoPopup(parent, this, tmp);
            photoPopup.SetValue(Grid.RowProperty, 2);
            photoPopup.SetValue(Grid.ColumnSpanProperty, 3);
            parent.mainGrid.Children.Add(photoPopup);
        }

        public void DiscussionClick(object sender, MouseButtonEventArgs e)
        {
            Discussion tmp = (Discussion)sender;
            DiscussionPopup discussionPopup = new DiscussionPopup(parent, tmp);
            discussionPopup.SetValue(Grid.RowProperty, 2);
            discussionPopup.SetValue(Grid.ColumnSpanProperty, 3);
            parent.mainGrid.Children.Add(discussionPopup);
        }

        public void SortClick(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(sortByMenu) || sender.Equals(currentSortOption))
            {
                sortByDropdown.IsOpen = !sortByDropdown.IsOpen;
            }
            else if (sender.Equals(sortPopular))
            {
                currentSortOption.Content = sortPopular.Content;
                sortByDropdown.IsOpen = !sortByDropdown.IsOpen;
            }
            else if (sender.Equals(sortNew))
            {
                currentSortOption.Content = sortNew.Content;
                sortByDropdown.IsOpen = !sortByDropdown.IsOpen;
            }
            else if (sender.Equals(sortMostCommented))
            {
                currentSortOption.Content = sortMostCommented.Content;
                sortByDropdown.IsOpen = !sortByDropdown.IsOpen;
            }
            else if (sender.Equals(sortMostUpvoted))
            {
                currentSortOption.Content = sortMostUpvoted.Content;
                sortByDropdown.IsOpen = !sortByDropdown.IsOpen;
            }
        }

        private void FilterClick(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(filterByMenu) || sender.Equals(currentFilterOption))
            {
                filterByDropdown.IsOpen = !filterByDropdown.IsOpen;
            }
            else if (sender.Equals(filterAll))
            {
                currentFilterOption.Content = filterAll.Content;
                filterByDropdown.IsOpen = false;
                DisplayPosts();
            }
            else if (sender.Equals(filterPhotos))
            {
                currentFilterOption.Content = filterPhotos.Content;
                filterByDropdown.IsOpen = false;
                FilterByPhotos();
            }
            else if (sender.Equals(filterDiscussions))
            {
                currentFilterOption.Content = filterDiscussions.Content;
                filterByDropdown.IsOpen = false;
                FilterByDiscussions();
            }
        }
        public void FilterByPhotos()
        {
            userProfileFeed.Children.Clear();
            foreach (KeyValuePair<int, Photo> photo in displayedUser.userPhotos)
            {
                Photo newPhoto = new Photo(photo.Value);
                newPhoto.main = this.parent;
                newPhoto.sidePhotoInfo.Visibility = Visibility.Collapsed;
                userProfileFeed.Children.Add(newPhoto);
                MakePostClickable(newPhoto);
            }

            if ((displayedUser.userDiscussions.Count == 0) && (displayedUser.userPhotos.Count == 0))
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(2);
                border.Opacity = 50;
                border.Width = 750;
                border.Margin = new Thickness(10);
                TextBlock text = new TextBlock();
                text.Text = "There are no posts here. \nPost some discussions and photos, and they will show on your profile!";
                text.FontFamily = new FontFamily("Microsoft YaHei");
                text.FontSize = 26;
                text.TextAlignment = TextAlignment.Center;
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextWrapping = TextWrapping.Wrap;

                border.Child = text;

                userProfileFeed.Children.Add(border);
            }
        }
        public void FilterByDiscussions()
        {
            userProfileFeed.Children.Clear();
            foreach (KeyValuePair<int, Discussion> discussion in displayedUser.userDiscussions)
            {
                Discussion newDiscussion = new Discussion(discussion.Value);
                newDiscussion.main = this.parent;
                userProfileFeed.Children.Add(newDiscussion);
                MakePostClickable(newDiscussion);
            }

            if ((displayedUser.userDiscussions.Count == 0) && (displayedUser.userPhotos.Count == 0))
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(2);
                border.Opacity = 50;
                border.Width = 750;
                border.Margin = new Thickness(10);
                TextBlock text = new TextBlock();
                text.Text = "There are no posts here. \nPost some discussions and photos, and they will show on your profile!";
                text.FontFamily = new FontFamily("Microsoft YaHei");
                text.FontSize = 26;
                text.TextAlignment = TextAlignment.Center;
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextWrapping = TextWrapping.Wrap;

                border.Child = text;

                userProfileFeed.Children.Add(border);
            }
        }

        private void FollowClick(object sender, RoutedEventArgs e)
        {
            /*
            if ((followButton.Content as string) == "FOLLOW")
            {
                followButton.Content = "UNFOLLOW";
                parent.followingSomeone = true;
            }
            else if ((followButton.Content as string) == "UNFOLLOW")
            {
                followButton.Content = "FOLLOW";
                parent.followingSomeone = false;
            }*/
        }

        /*private void HardcodedPhotosAndDiscussions()
        {
            userProfileFeed.Children.Add(new Photo());
            userProfileFeed.Children.Add(new Discussion());
            userProfileFeed.Children.Add(new Photo(parent.currPhotosPage.photoIdCounter, new BitmapImage(new Uri("Images/Coast.jpg", UriKind.Relative))));

            foreach (UserControl post in userProfileFeed.Children)
            {
                MakePostClickable(post);
                // Add to user's photos or discussions
                if (post is Photo)
                {
                    displayedUser.userPhotos.Add((Photo)post);
                }
                else if (post is Discussion)
                {
                    displayedUser.userDiscussions.Add((Discussion)post);
                }
            }
        }*/

        public void DisplayPosts()
        {
            userProfileFeed.Children.Clear();
            foreach (KeyValuePair<int, Photo> photo in displayedUser.userPhotos)
            {
                Photo newPhoto = new Photo(photo.Value);
                newPhoto.sidePhotoInfo.Visibility = Visibility.Collapsed;
                userProfileFeed.Children.Add(newPhoto);
                newPhoto.main = this.parent;
                MakePostClickable(newPhoto);
            }
            foreach (KeyValuePair<int, Discussion> discussion in displayedUser.userDiscussions)
            {
                Discussion newDiscussion = new Discussion(discussion.Value);
                newDiscussion.main = this.parent;
                userProfileFeed.Children.Add(newDiscussion);
                MakePostClickable(newDiscussion);
            }

            if ((displayedUser.userDiscussions.Count == 0) && (displayedUser.userPhotos.Count == 0))
            {
                Border border = new Border();
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(2);
                border.Opacity = 50;
                border.Width = 750;
                border.Margin = new Thickness(10);
                TextBlock text = new TextBlock();
                text.Text = "There are no posts here. \nPost some discussions and photos, and they will show on your profile!";
                text.FontFamily = new FontFamily("Microsoft YaHei");
                text.FontSize = 26;
                text.TextAlignment = TextAlignment.Center;
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextWrapping = TextWrapping.Wrap;

                border.Child = text;

                userProfileFeed.Children.Add(border);
            }
        }

        public void ProfilePictureClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == false)
                return;
            displayedUser.SetProfilePicture(new BitmapImage(new Uri(dialog.FileName)));
            profilePicture.Source = new BitmapImage(new Uri(dialog.FileName));
        }

        public void HoverProfilePicture(object sender, MouseEventArgs e)
        {
            profilePicture.Opacity = 0.45;
            changeProfilePicture.Visibility = Visibility.Visible;
        }

        public void LeaveProfilePicture(object sender, MouseEventArgs e)
        {
            profilePicture.Opacity = 1;
            changeProfilePicture.Visibility = Visibility.Hidden;
        }

        public void ChangeBiographyClick(object sender, MouseEventArgs e)
        {
            // Get the user's current bio
            string usersCurrentBiography = displayedUser.Biography;

            // If the user's bio is set to something, add its text to the text box.
            if (usersCurrentBiography == null)
            {
                biographyTextBox.Text = "";
            }
            else if (usersCurrentBiography.Length != 0)
            {
                biographyTextBox.Text = usersCurrentBiography;
            }

            biographyTextBox.Visibility = Visibility.Visible;
            updateBioButtons.Visibility = Visibility.Visible;
            biographyTextBox.Focus();
        }

        public void HoverBiography(object sender, MouseEventArgs e)
        {
            changeBiography.Visibility = Visibility.Visible;
            blurBio.Visibility = Visibility.Visible;
            biographyText.Opacity = 0.45;
        }

        public void LeaveBiography(object sender, MouseEventArgs e)
        {
            changeBiography.Visibility = Visibility.Hidden;
            blurBio.Visibility = Visibility.Hidden;
            biographyText.Opacity = 1;
        }

        public void CheckForEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UpdateBiography();
            }
        }

        public void UpdateBioClick(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(confirmUpdateBio))
            {
                UpdateBiography();
            }
            else if (sender.Equals(cancelUpdateBio))
            {
                biographyTextBox.Visibility = Visibility.Hidden;
                updateBioButtons.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateBiography()
        {
            // Get the text entered by the user.
            string newBiography = biographyTextBox.Text.ToString();

            // Set the new biography in the user object and his profile page.
            displayedUser.Biography = newBiography;
            biographyText.Text = newBiography;

            // Hide the text box and buttons
            biographyTextBox.Visibility = Visibility.Hidden;
            updateBioButtons.Visibility = Visibility.Hidden;
        }
    }
}
