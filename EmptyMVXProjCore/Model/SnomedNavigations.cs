namespace EmptyMVXProjCore.Model
{
    public class SnomedNavigations : BaseModel
    {
		private long conceptID;
		private string term;
		private int navigation;
		private long parentID;

		public SnomedNavigations()
		{
		}

		public SnomedNavigations(long conId, string term, int nav, long parId)
		{
			this.ConceptID = conId;
			this.Term = term;
			this.Navigation = nav;
			this.ParentID = parId;
		}

		public int Navigation
		{
			get { return navigation; }
			set { navigation = value; }
		}

		public string Term
		{
			get { return term; }
			set { term = value; }
		}

		public long ConceptID
		{
			get { return conceptID; }
			set { conceptID = value; }
		}

		public long ParentID
		{
			get { return parentID; }
			set { parentID = value; }
		}
	}
}
