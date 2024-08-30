using ProjectManagementApp.Data;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.ViewModels
{
	public class ProjectOwnersVm(IServiceProvider services)
	{
		private ProjectRoleService projectRoleService = services.GetRequiredService<ProjectRoleService>();
		private ProjectService projectService = services.GetRequiredService<ProjectService>();

		public async Task<List<KanBanSection>> GetOwnerSectionsAsync()
		{
			List<KanBanSection> sections = new List<KanBanSection>();

			// Add the owner sections
			var owners = (await projectRoleService.GetUsersWithRoleAsync("owner")).ToList();

			foreach (var owner in owners)
				sections.Add(new KanBanSection(owner!.UserName ?? "USERNAME NOT FOUND", true));

			return sections;
		}

		public List<KanbanItem> GetProjectItems(string unownedSectionName)
		{
			var items = new List<KanbanItem>();

			// Assign each project to the owner's section
			foreach (var project in projectService.GetProjects())
			{
				ApplicationUser? owner = project.Owner;

				if (owner == null)
					items.Add(new KanbanItem(project.Name, project.Id!, unownedSectionName, String.Empty));
				else
					items.Add(new KanbanItem(project.Name, project.Id!, owner.UserName!, owner.Id));
			}

			return items;
		}
	}
}
