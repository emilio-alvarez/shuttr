﻿using System;
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
    /// Interaction logic for SearchResultsPage.xaml
    /// </summary>
    public partial class SearchResultsPage : UserControl
    {
        private Photo photo;
        private Discussion discussion;
        private MainWindow parent;

        public SearchResultsPage(MainWindow main)
        {
            InitializeComponent();

            parent = main;

            InitializePosts();

            currentFilterOption.Content = filterAll.Content;
            currentSortOption.Content = sortPopular.Content;

            FilterPhotosDiscussions();
        }

        private void InitializePosts()
        {
            photo = new Photo(102, "Images/Mountains.jpg");
            photo.username = "Lean";
            photo.title = "Rate my mountains photo";
            photo.score = 5;
            photo.displaySideInfo();
            photo.commentCount = 1;
            photo.comments.Add(new Comment("Emilio", "Cool photo. Where was this?"));
            photo.main = this.parent;
            photo.IsPrivate = false;
            photo.displaySideInfo();

            discussion = new Discussion(102, "Jessi", "Good places to take mountain pictures in Alberta?", "I'm visiting Alberta this summer, and was wondering what the best places to take mountain pictures are. Ideally they would be near the Banff area.", 2);
            discussion.score = 1;
            discussion.GetComments().Add(new Comment("Lawrence", "Lake Louise looks spectacular during the summer! Just go early to avoid the crowds."));
            discussion.GetComments().Add(new Comment("Anonymoose", "Basically anywhere near the rockies. Alberta is pretty amazing like that."));
            discussion.numReplies = 2;
            discussion.main = this.parent;

            photo.MouseLeftButtonDown += ClickPost;
            discussion.MouseLeftButtonDown += ClickPost;
        }

        private void FilterPhotosDiscussions()
        {
            resultsFeed.Children.Clear();

            // Make sure the posts are sorted properly
            if ((currentSortOption.Content == sortPopular.Content) || (currentSortOption.Content == sortMostUpvoted.Content))
            {
                resultsFeed.Children.Add(photo);
                resultsFeed.Children.Add(discussion);
            }
            else
            {
                resultsFeed.Children.Add(discussion);
                resultsFeed.Children.Add(photo);
            }
        }
        
        private void FilterPhotos()
        {
            resultsFeed.Children.Clear();
            resultsFeed.Children.Add(photo);
        }

        private void FilterDiscussions()
        {
            resultsFeed.Children.Clear();
            resultsFeed.Children.Add(discussion);
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
                sortByDropdown.IsOpen = false;

                resultsFeed.Children.Clear();

                // If showing both photos and discussions, make sure posts are sorted
                if (currentFilterOption.Content == filterAll.Content)
                {
                    resultsFeed.Children.Add(photo);
                    resultsFeed.Children.Add(discussion);
                }
                else if (currentFilterOption.Content == filterPhotos.Content)
                {
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterDiscussions.Content)
                {
                    resultsFeed.Children.Add(discussion);
                }
            }
            else if (sender.Equals(sortNew))
            {
                currentSortOption.Content = sortNew.Content;
                sortByDropdown.IsOpen = false;

                resultsFeed.Children.Clear();

                // If showing both photos and discussions, make sure posts are sorted
                if (currentFilterOption.Content == filterAll.Content)
                {
                    resultsFeed.Children.Add(discussion);
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterPhotos.Content)
                {
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterDiscussions.Content)
                {
                    resultsFeed.Children.Add(discussion);
                }
            }
            else if (sender.Equals(sortMostCommented))
            {
                currentSortOption.Content = sortMostCommented.Content;
                sortByDropdown.IsOpen = false;

                resultsFeed.Children.Clear();

                // If showing both photos and discussions, make sure posts are sorted
                if (currentFilterOption.Content == filterAll.Content)
                {
                    resultsFeed.Children.Add(discussion);
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterPhotos.Content)
                {
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterDiscussions.Content)
                {
                    resultsFeed.Children.Add(discussion);
                }
            }
            else if (sender.Equals(sortMostUpvoted))
            {
                currentSortOption.Content = sortMostUpvoted.Content;
                sortByDropdown.IsOpen = false;

                resultsFeed.Children.Clear();

                // If showing both photos and discussions, make sure posts are sorted
                if (currentFilterOption.Content == filterAll.Content)
                {
                    resultsFeed.Children.Add(photo);
                    resultsFeed.Children.Add(discussion);
                }
                else if (currentFilterOption.Content == filterPhotos.Content)
                {
                    resultsFeed.Children.Add(photo);
                }
                else if (currentFilterOption.Content == filterDiscussions.Content)
                {
                    resultsFeed.Children.Add(discussion);
                }
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
                FilterPhotosDiscussions();
            }
            else if (sender.Equals(filterPhotos))
            {
                currentFilterOption.Content = filterPhotos.Content;
                filterByDropdown.IsOpen = false;
                FilterPhotos();
            }
            else if (sender.Equals(filterDiscussions))
            {
                currentFilterOption.Content = filterDiscussions.Content;
                filterByDropdown.IsOpen = false;
                FilterDiscussions();
            }
        }

        private void ClickPost(object sender, MouseEventArgs e)
        {
            if (sender.Equals(discussion))
            {
                DiscussionPopup discussionPopup = new DiscussionPopup(parent, discussion);
                discussionPopup.SetValue(Grid.RowProperty, 2);
                discussionPopup.SetValue(Grid.ColumnSpanProperty, 3);
                parent.mainGrid.Children.Add(discussionPopup);
            }
            else
            {
                PhotoPopup photoPopup = new PhotoPopup(parent, photo);
                photoPopup.SetValue(Grid.RowProperty, 2);
                photoPopup.SetValue(Grid.ColumnSpanProperty, 3);
                parent.mainGrid.Children.Add(photoPopup);
            }
        }
    }
}
