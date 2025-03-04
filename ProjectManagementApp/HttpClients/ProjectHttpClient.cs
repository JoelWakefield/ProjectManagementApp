using ReactApp.Server.ViewModels;
using System.Text.Json;

namespace ProjectManagementApp.HttpClients
{
    public class ProjectHttpClient(HttpClient http)
    {
        public async Task<ProjectVm[]> GetProjectsAsync() =>
            await http.GetFromJsonAsync<ProjectVm[]>("project") ?? [];

        public async Task<ProjectVm> GetProjectDetailsAsync(string id) =>
            await http.GetFromJsonAsync<ProjectVm>($"project/details/{id}") ?? new();

        public async Task<EditProjectVm> GetProjectEditAsync(string id) =>
            await http.GetFromJsonAsync<EditProjectVm>($"project/edit/{id}") ?? new();

        public async Task UpdateProjectAsync(string id, EditProjectVm projectVm) =>
            await http.PutAsJsonAsync($"project/edit/{id}", projectVm);

        public async Task<string> CreateProjectAsync(CreateProjectVm projectVm)
        {
            var response = await http.PostAsJsonAsync("project/create", projectVm);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<string>(responseStream);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
