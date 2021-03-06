﻿using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WetHatLab.OneNote.TaggingKit.common;
using WetHatLab.OneNote.TaggingKit.common.ui;

namespace WetHatLab.OneNote.TaggingKit.edit
{
    /// <summary>
    /// Interaction logic for HitHighlightedTag.xaml
    /// </summary>
    [ComVisible(false)]
    public partial class HitHighlightedTagButton : UserControl
    {
        /// <summary>
        /// Click event for this button.
        /// </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HitHighlightedTagButton));

        /// <summary>
        /// Create a new instance of the control
        /// </summary>
        public HitHighlightedTagButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add or remove the click handler.
        /// </summary>
        /// <remarks>clr wrapper for routed event</remarks>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }

            remove { RemoveHandler(ClickEvent, value); }
        }

        private void createHitHighlightedTag(HitHighlightedTagButtonModel mdl)
        {
            hithighlightedTag.Inlines.Clear();
            foreach (TextFragment f in mdl.HighlightedTagName)
            {
                Run r = new Run(f.Text);
                if (f.IsMatch)
                {
                    r.Background = Brushes.Yellow;
                }
                hithighlightedTag.Inlines.Add(r);
            }
        }
        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HitHighlightedTagButtonModel mdl = DataContext as HitHighlightedTagButtonModel;

            if (mdl != null)
            {
                createHitHighlightedTag(mdl);
                mdl.PropertyChanged += mdl_PropertyChanged;
            }
        }

        void mdl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e == HitHighlightedTagButtonModel.HIGHLIGHTED_TAGNAME)
            {
                HitHighlightedTagButtonModel mdl = sender as HitHighlightedTagButtonModel;
                createHitHighlightedTag(mdl);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent,this));
        }
    }
}
