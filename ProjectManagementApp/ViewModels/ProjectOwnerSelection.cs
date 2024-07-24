namespace ProjectManagementApp.ViewModels
{
    public class ProjectOwnerSelection
    {
        public ProjectOwnerSelection(string id, string? name)
        {
            Id = id;
            Name = name ?? "NO USERNAME";
        }

        public readonly string Id;
        public readonly string Name;

        // Note: this is important so the MudSelect can compare ownerss
        public override bool Equals(object o)
        {
            var other = o as ProjectOwnerSelection;
            return other?.Name == Name;
        }

        // Note: this is important too!
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        // Implement this for the Owner to display correctly in MudSelect
        public override string ToString() => Name;
    }
}
