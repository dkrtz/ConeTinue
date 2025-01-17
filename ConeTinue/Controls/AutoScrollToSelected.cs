using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ConeTinue.Controls
{
	public class AutoScrollToSelected : Behavior<ListBox>
	{
		protected override void OnAttached()
		{
			AssociatedObject.SelectionChanged += (sender, args) => AssociatedObject.ScrollIntoView(AssociatedObject.SelectedItem);
			base.OnAttached();
		}
	}
}