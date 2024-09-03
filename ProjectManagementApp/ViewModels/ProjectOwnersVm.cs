using ProjectManagementApp.Data;
using ProjectManagementApp.Services;

namespace ProjectManagementApp.ViewModels
{
	public class ProjectOwnersVm(IServiceProvider services)
	{
		private ProjectRoleService projectRoleService = services.GetRequiredService<ProjectRoleService>();
		private ProjectService projectService = services.GetRequiredService<ProjectService>();

		public async IAsyncEnumerable<KanBanSection> GetOwnerSectionsAsync()
		{
			foreach (var owner in await projectRoleService.GetUsersWithRoleAsync("owner"))
				yield return new KanBanSection(owner!.UserName ?? "USERNAME NOT FOUND", true);
		}

		/// <summary>
		/// Returns the project owners as KanbanItems
		/// </summary>
		/// <returns></returns>
		public IEnumerable<KanbanItem> GetProjectItems()
		{
			// Assign each project to the owner's section
			foreach (var project in projectService.GetProjects())
				yield return new KanbanItem(project.Name, project.Id, project.Owner.UserName, project.Owner.Id);
		}
	}
}
