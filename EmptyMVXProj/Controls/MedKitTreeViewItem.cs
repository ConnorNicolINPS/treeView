namespace EmptyMVXProj.Controls
{
    using EmptyMVXProjCore.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// the med kit version of the treeview item
    /// </summary>
    /// <seealso cref="System.Windows.Controls.TreeViewItem" />
    public class MedKitTreeViewItem : TreeViewItem
    {
        private TreeMember ThisMember => this.DataContext as TreeMember;

        public MedKitTreeViewItem()
        {
            this.MouseUp += MedKitTreeViewItem_MouseUp;
        }

        public bool NavigationOnly
        {
            get { return ThisMember.NavigationOnly; }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MedKitTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MedKitTreeViewItem;
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            ////var member = this.GetSelectedMember(e);
            ////if (member != null && ThisMember.Equals(member))
            ////{
            ////    if (member.NavigationOnly)
            ////    {
            ////        this.IsSelected = false;
            ////    }
            ////}

            ////e.Handled = true;
        }

        private TreeMember GetSelectedMember(RoutedEventArgs args) 
        {
            TreeMember member = null;

            if (args.Source is TreeViewItem)
            {
                var selectedItem = args.Source as TreeViewItem;
                member = selectedItem.DataContext as TreeMember;
            }
            else if (args.Source is ContentPresenter)
            {
                var selectedItem = args.Source as ContentPresenter;
                member = selectedItem.DataContext as TreeMember;
            }
            return member;
        }

        private void MedKitTreeViewItem_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var member = this.GetSelectedMember(e);
            if (member != null && ThisMember.Equals(member))
            {
                if (!this.IsExpanded)
                {
                    this.IsExpanded = true;
                }
            }
        }

        private T ParentOfType<T>(DependencyObject element) where T : DependencyObject
        {
            if (element == null)
                return default(T);
            else
                return Enumerable.FirstOrDefault<T>(Enumerable.OfType<T>((IEnumerable)GetParents(element)));
        }
        private IEnumerable<DependencyObject> GetParents(DependencyObject element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            while ((element = GetParent(element)) != null)
                yield return element;
        }

        private DependencyObject GetParent(DependencyObject element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            if (parent == null)
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                    parent = frameworkElement.Parent;
            }
            return parent;
        }

        protected override void OnExpanded(RoutedEventArgs e)
        {
            MedKitTreeView treeView = ParentOfType<MedKitTreeView>(this);
            treeView.InvokeExpandedEvent(this);

            base.OnExpanded(e);
        }
    }
}
