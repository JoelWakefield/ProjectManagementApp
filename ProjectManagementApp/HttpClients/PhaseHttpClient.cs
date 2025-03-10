using ReactApp.Server.ViewModels;

namespace ProjectManagementApp.HttpClients
{
    public class PhaseHttpClient(HttpClient http)
    {
        public async Task<PhaseVm[]> GetPhasesAsync() =>
            await http.GetFromJsonAsync<PhaseVm[]>("phase") ?? [];
        
        public async Task<PhaseVm?> GetPhaseDetailsAsync(string id) =>
            await http.GetFromJsonAsync<PhaseVm>($"phase/details/{id}");
     
        public async Task<EditPhaseVm?> GetPhaseEditAsync(string id) =>
            await http.GetFromJsonAsync<EditPhaseVm>($"phase/edit/{id}");

        public async Task UpdatePhaseAsync(string id, EditPhaseVm PhaseVm) =>
            await http.PutAsJsonAsync($"phase/edit/{id}", PhaseVm);

        public async Task<HttpResponseMessage> CreatePhaseAsync(CreatePhaseVm phaseVm) =>
            await http.PostAsJsonAsync("phase/create", phaseVm);
    }
}
