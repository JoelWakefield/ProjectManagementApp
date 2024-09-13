using ProjectManagementApp.Data;

namespace ProjectManagementApp.SampleData
{
	public class PhaseSchedules
	{
		public static PhaseSchedule SimplePlanningBertSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimplePlanning().Id,
			UserId = Users.Bert.Id,
			Start = DateTime.UtcNow.AddDays(-8),
			End = DateTime.UtcNow.AddDays(-6)
		};
		public static PhaseSchedule SimplePlanningZahirSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimplePlanning().Id,
			UserId = Users.Zahir.Id,
			Start = DateTime.UtcNow.AddDays(-9),
			End = DateTime.UtcNow.AddDays(-6)
		};
		public static PhaseSchedule SimplePlanningAlayahSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimplePlanning().Id,
			UserId = Users.Alayah.Id,
			Start = DateTime.UtcNow.AddDays(-10),
			End = DateTime.UtcNow.AddDays(-6)
		};

		public static PhaseSchedule SimpleSetupZahirSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleSetup().Id,
			UserId = Users.Zahir.Id,
			Start = DateTime.UtcNow.AddDays(-5),
			End = DateTime.UtcNow.AddDays(-2)
		};
		public static PhaseSchedule SimpleSetupAlayahSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleSetup().Id,
			UserId = Users.Alayah.Id,
			Start = DateTime.UtcNow.AddDays(-4),
			End = DateTime.UtcNow.AddDays(-3)
		};

		public static PhaseSchedule SimpleDataEntryAlayahSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleDataEntry().Id,
			UserId = Users.Alayah.Id,
			Start = DateTime.UtcNow.AddDays(-1),
			End = DateTime.UtcNow.AddDays(4)
		};
		public static PhaseSchedule SimpleDataEntryZahirSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleDataEntry().Id,
			UserId = Users.Zahir.Id,
			Start = DateTime.UtcNow.AddDays(-4),
			End = DateTime.UtcNow.AddDays(2)
		};

		public static PhaseSchedule SimpleQABertSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleQA().Id,
			UserId = Users.Bert.Id,
			Start = DateTime.UtcNow.AddDays(5),
			End = DateTime.UtcNow.AddDays(6)
		};

		public static PhaseSchedule SimpleNotifyCompletionAlayahSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleNotifyCompletion().Id,
			UserId = Users.Alayah.Id,
			Start = DateTime.UtcNow.AddDays(7),
			End = DateTime.UtcNow.AddDays(7)
		};
		public static PhaseSchedule SimpleNotifyCompletionMyloSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimpleNotifyCompletion().Id,
			UserId = Users.Mylo.Id,
			Start = DateTime.UtcNow.AddDays(5),
			End = DateTime.UtcNow.AddDays(8)
		};

		public static PhaseSchedule SimplePostAnalyticsZahirSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimplePostAnalytics().Id,
			UserId = Users.Zahir.Id,
			Start = DateTime.UtcNow.AddDays(9),
			End = DateTime.UtcNow.AddDays(10)
		};
		public static PhaseSchedule SimplePostAnalyticsAlayahSchedule = new PhaseSchedule
		{
			PhaseId = PhaseData.SimplePostAnalytics().Id,
			UserId = Users.Alayah.Id,
			Start = DateTime.UtcNow.AddDays(7),
			End = DateTime.UtcNow.AddDays(10)
		};
	}
}
