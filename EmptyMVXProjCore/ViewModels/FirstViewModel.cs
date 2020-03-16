namespace EmptyMVXProjCore.ViewModels
{
    using EmptyMVXProjCore.Model;
    using MvvmCross.Core.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FirstViewModel : MvxViewModel
    {
        private List<SnomedNavigations> navigationItems;
        private List<TreeMember> hirarchicalTreeList;

        private string selectedItemText;

        private MvxCommand<TreeMember> itemExpandedEvent;

        public FirstViewModel()
        {
            this.navigationItems = new List<SnomedNavigations>();
            this.HirarchicalTreeList = new List<TreeMember>();

            this.CreateDummyData();
            this.ConvertDataToTreeMember();
        }

        public List<TreeMember> HirarchicalTreeList 
        {
            get { return this.hirarchicalTreeList; }
            set { this.SetProperty(ref this.hirarchicalTreeList, value); }
        }

        public string SelectedItemText
        {
            get { return this.selectedItemText; }
            set { this.SetProperty(ref this.selectedItemText, value); }
        }
        public IMvxCommand ItemExpandedEvent
        {
            get { return this.itemExpandedEvent ?? (this.itemExpandedEvent = new MvxCommand<TreeMember>(ItemExpandedEventAction)); }
        }

     
        private void ConvertDataToTreeMember()
        {
            // first get top level members
            // then add all children to those parents 
            // keep going till all items are gone
            List<SnomedNavigations> toplevel = navigationItems.Where(x => x.ParentID == 0).ToList();

            foreach (var item in toplevel)
            {
                HirarchicalTreeList.Add(new TreeMember(item.ConceptID, item.Term, Convert.ToBoolean(item.Navigation)));
                navigationItems.Remove(item);
            }

            //rest of the list
            while (navigationItems.Count > 0)
            {
                for (int i = navigationItems.Count-1; i >= 0; i--)
                {
                    List<TreeMember> parents = this.GetParents(HirarchicalTreeList, navigationItems[i].ParentID);

                    if (parents != null && parents.Count > 0)
                    {
                        foreach (TreeMember parent in parents)
                        {
                            parent.Children.Add(new TreeMember(navigationItems[i].ConceptID, navigationItems[i].Term, Convert.ToBoolean(navigationItems[i].Navigation)));
                        }

                        navigationItems.Remove(navigationItems[i]);
                    }
                }
            }
        }

        private List<TreeMember> GetParents(List<TreeMember> TreeList, long parentID)
        {
            List<TreeMember> parents = new List<TreeMember>();

            foreach (TreeMember treeItem in TreeList)
            {
                if (treeItem.Title.Equals(parentID))
                {
                   parents.Add(treeItem);
                }
                
                if (treeItem.Children != null && treeItem.Children.Count > 0)
                {
                    parents.AddRange(GetParents(treeItem.Children, parentID));
                }
            }

            if (parents != null)
            {
                return parents;
            }

            return null;
        }

        private void CreateDummyData()
        {
            // add a bunch of dummy data to use
            navigationItems.Add(new SnomedNavigations(11111, "test term for 11111", 0, 0));
            navigationItems.Add(new SnomedNavigations(11112, "test term for 11112-1", 0, 11111));
            navigationItems.Add(new SnomedNavigations(11112, "test term for 11112-2", 0, 11117));
            navigationItems.Add(new SnomedNavigations(11112, "test term for 11112-3", 0, 11121));
            navigationItems.Add(new SnomedNavigations(11112, "test term for 11112-4", 0, 11125));
            navigationItems.Add(new SnomedNavigations(11113, "test term for 11113", 0, 11112));
            navigationItems.Add(new SnomedNavigations(11114, "test term for 11114", 0, 11115));
            navigationItems.Add(new SnomedNavigations(11115, "test term for 11115", 0, 11111));
            navigationItems.Add(new SnomedNavigations(11116, "test term for 11116", 0, 11115));
            navigationItems.Add(new SnomedNavigations(11117, "test term for 11117", 0, 11115));
            navigationItems.Add(new SnomedNavigations(11118, "test term for 11118", 0, 11111));
            navigationItems.Add(new SnomedNavigations(11119, "test term for 11119-1", 0, 11122));
            navigationItems.Add(new SnomedNavigations(11119, "test term for 11119-2", 0, 11112));
            navigationItems.Add(new SnomedNavigations(11119, "test term for 11119-3", 0, 11130));
            navigationItems.Add(new SnomedNavigations(11119, "test term for 11119-4", 0, 11140));
            navigationItems.Add(new SnomedNavigations(11120, "test term for 11120", 1, 11122));
            navigationItems.Add(new SnomedNavigations(11121, "test term for 11121", 1, 11111));
            navigationItems.Add(new SnomedNavigations(11122, "test term for 11122", 0, 0));
            navigationItems.Add(new SnomedNavigations(11123, "test term for 11123", 0, 11124));
            navigationItems.Add(new SnomedNavigations(11124, "test term for 11124", 0, 0));
            navigationItems.Add(new SnomedNavigations(11125, "test term for 11125", 0, 11124));
            navigationItems.Add(new SnomedNavigations(11126, "test term for 11126", 0, 11125));
            navigationItems.Add(new SnomedNavigations(11127, "test term for 11127", 0, 11115));
            navigationItems.Add(new SnomedNavigations(11128, "test term for 11128", 0, 11137));
            navigationItems.Add(new SnomedNavigations(11129, "test term for 11129", 0, 11112));
            navigationItems.Add(new SnomedNavigations(11130, "test term for 11130", 0, 11140));
            navigationItems.Add(new SnomedNavigations(11131, "test term for 11131", 0, 11132));
            navigationItems.Add(new SnomedNavigations(11132, "test term for 11132", 0, 11129));
            navigationItems.Add(new SnomedNavigations(11133, "test term for 11133", 0, 11135));
            navigationItems.Add(new SnomedNavigations(11134, "test term for 11134", 0, 11137));
            navigationItems.Add(new SnomedNavigations(11135, "test term for 11135", 1, 11137));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-1", 1, 11112));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-2", 1, 11112));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-3", 1, 11123));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-4", 1, 11124));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-5", 1, 11137));
            navigationItems.Add(new SnomedNavigations(11136, "test term for 11136-6", 1, 11140));
            navigationItems.Add(new SnomedNavigations(11137, "test term for 11137", 1, 0));
            navigationItems.Add(new SnomedNavigations(11138, "test term for 11138", 0, 11137));
            navigationItems.Add(new SnomedNavigations(11139, "test term for 11139", 0, 11140));
            navigationItems.Add(new SnomedNavigations(11140, "test term for 11140", 0, 0));
        }

        private void ItemExpandedEventAction(TreeMember member)
        {
            var test = "this is a test";
        }
    }
}
