namespace EmptyMVXProjCore.Model
{
	using System.Collections.Generic;

	public class TreeMember : BaseModel
    {
		private long title;
		private string description;
		private bool navigtationOnly;
		private List<TreeMember> children;

		public TreeMember()
		{
		}

		public TreeMember(long title, string description, bool navigation)
		{
			this.Title = title;
			this.Description = description;
			this.NavigationOnly = navigation;
			this.Children = new List<TreeMember>();
		}

		public long Title
		{
			get { return this.title; }
			set { this.SetProperty(ref this.title, value); }
		}
		public string Description
		{
			get { return this.description; }
			set { this.SetProperty(ref this.description, value); }
		}
		public bool NavigationOnly
		{
			get { return this.navigtationOnly; }
			set { this.SetProperty(ref this.navigtationOnly, value); }
		}
		public List<TreeMember> Children
		{
			get { return this.children; }
			set { this.SetProperty(ref this.children, value); }
		}
	}
}
