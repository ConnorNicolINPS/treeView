namespace EmptyMVXProj.Controls
{
    using EmptyMVXProjCore.Model;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// the medKit treeview
    /// </summary>
    /// <seealso cref="System.Windows.Controls.TreeView" />
    public class MedKitTreeView : TreeView
    {
        public static readonly DependencyProperty ExpandingMemberProperty =
            DependencyProperty.Register("ExpandingMember", typeof(TreeMember), typeof(MedKitTreeView), new PropertyMetadata(null));


        public event EventHandler ItemExpandedEvent;

        public MedKitTreeView()
        {
            this.SelectedItemChanged += MedKitTreeView_SelectedItemChanged;
        }

        [BindableAttribute(true)]
        public TreeMember ExpandingMember
        {
            get { return (TreeMember)this.GetValue(ExpandingMemberProperty); }
            set { this.SetValue(ExpandingMemberProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MedKitTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is MedKitTreeViewItem;
        }

        public void CollapseAll()
        {
            var allItems = this.GetAllItems(this);
            foreach (var item in allItems)
            {
                item.IsExpanded = false;
            }
        }

        public void InvokeExpandedEvent(MedKitTreeViewItem item)
        {
            this.ExpandingMember = item.DataContext as TreeMember;
            this.ItemExpandedEvent?.Invoke(item, EventArgs.Empty);
        }

        private void MedKitTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var items = this.GetAllItems(this);

            MedKitTreeViewItem newItem = items.Where(x => x.DataContext.Equals(e.NewValue as TreeMember)).FirstOrDefault();
            if (newItem != null && newItem.NavigationOnly)
            {
                newItem.IsSelected = false;

                MedKitTreeViewItem oldItem = items.Where(x => x.DataContext.Equals(e.OldValue as TreeMember)).FirstOrDefault();
                oldItem.IsSelected = true;
            }

            e.Handled = true;
        }

        private Collection<MedKitTreeViewItem> GetAllItems(ItemsControl itemsControl)
        {
            Collection<MedKitTreeViewItem> allItems = new Collection<MedKitTreeViewItem>();
            for (int i = 0; i < itemsControl.Items.Count; i++)
            {
                var childItemContainer = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as MedKitTreeViewItem;
                if (childItemContainer != null)
                {
                    allItems.Add(childItemContainer);
                    var childItems = GetAllItems(childItemContainer);
                    foreach (MedKitTreeViewItem childItem in childItems)
                    {
                        allItems.Add(childItem);
                    }
                }
            }

            return allItems;
        }
    }
}
