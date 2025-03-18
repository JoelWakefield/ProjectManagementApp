namespace ProjectManagementApp.ViewModels
{
    public class StageSelection
    {
        public StageSelection(string id, int orderId, string? name)
        {
            Id = id;
            Name = name ?? "NO STAGE NAME";
            OrderId = orderId;
        }

        public readonly string Id;
        public readonly string Name;
        public readonly int OrderId;

        // Note: this is important so the MudSelect can compare Stages
        public override bool Equals(object o)
        {
            var other = o as StageSelection;
            return other?.Id == Id;
        }

        // Note: this is important too - I have no idea why!
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        // Implement this for the Stage to display correctly in MudSelect
        public override string ToString() => Name;
    }
}
