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
    /// Interaction logic for PhotosPage.xaml
    /// </summary>
    public partial class ProfilePage : UserControl
    {
        private MainWindow parent;
        private User displayedUser;

        public ProfilePage()
        {
            InitializeComponent();
        }

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
        }

        /// <summary>
        /// Set the MainWindow that owns this profile page.
        /// Parent is required to allow popups for Photos and Discussions.
        /// </summary>
        /// <param name="main"> The parent of this profile page </param>
        public void SetParent(MainWindow main)
        {
            parent = main;
        }

        /// <summary>
        /// Will add all the posts of the current user to the scroll view.
        /// </summary>
        private void DisplayUserPosts()
        {
            foreach (var post in displayedUser.Posts)
            {
                userProfileFeed.Children.Add(post);
                MakePostClickable(post);
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
        }

        public void DiscussionClick(object sender, MouseButtonEventArgs e)
        {
            Discussion tmp = (Discussion)sender;
            DiscussionPopup discussionPopup = new DiscussionPopup(parent, this, tmp);
            discussionPopup.SetValue(Grid.RowProperty, 2);
            discussionPopup.SetValue(Grid.ColumnSpanProperty, 3);
            parent.mainGrid.Children.Add(discussionPopup);
        }
    }
}
