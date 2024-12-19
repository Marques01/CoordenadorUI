using Domain.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace UI.Pages.Person;

public partial class Index
{
    private bool
        _isLoading = false;

    private int
        _pageSize = 15;

    private Paginate<Domain.Entities.Person> _personPaginated = new();

    private Paginate<Domain.Entities.Company> _companyPaginated = new();

    private int _userId;

    private readonly List<string> _loadingMessages = new()
    {
        "Carregando... Por favor, aguarde.",
        "Estamos preparando tudo para você...",
        "Um momento, estamos quase lá...",
        "Só mais um pouquinho...",
        "Estamos trabalhando nisso...",
        "Aguarde enquanto fazemos a mágica acontecer...",
        "Estamos colocando as coisas em ordem para você...",
        "Estamos a todo vapor por aqui...",
        "Estamos dando os retoques finais...",
        "Estamos polindo as últimas peças...",
        "Estamos quase prontos para você...",
        "Estamos carregando sua experiência...",
        "Estamos preparando o palco...",
        "Estamos afinando as coisas para você...",
        "Estamos carregando os últimos detalhes..."
    };

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _isLoading = true;

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            bool allowed = user.IsInRole("Administrator") || user.IsInRole("Developer");
            if (!allowed)
            {
                _navigationManager.NavigateTo("/user-not-authorized");
                return;
            }

            var claims = user.Claims.ToList();

            if (claims.Count > 2)
            {
                var idUserClaim = claims[2].Value;
                _userId = int.Parse(idUserClaim);
            }

            await GetCompaniesByUserAsync(_userId);

            var companyId = _companyPaginated.Items.Count() > 0 ? _companyPaginated.Items.First().CompanyId : 0;

            await GetPersonsByCompanyIdAsync(companyId);

            //await GetPersonsPaginatedAsync();

            _isLoading = false;
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    private string GetRandomLoadingMessage()
    {
        var random = new Random();
        int index = random.Next(_loadingMessages.Count);
        return _loadingMessages[index];
    }

    private async Task GetPersonsPaginatedAsync()
    {
        try
        {
            var responseModel = await _personServices.GetPersonsAsync(1, _pageSize);
            _personPaginated = responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task GetPersonsPaginatedAsync(int page)
    {
        try
        {
            var responseModel = await _personServices.GetPersonsAsync(page, _pageSize);
            _personPaginated = responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task GetCompaniesByUserAsync(int userId)
    {
        try
        {
            var responseModel = await _companyServices.GetCompaniesAsync(userId, 1, int.MaxValue);
            _companyPaginated = responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task OnCompanyChanged(ChangeEventArgs e)
    {
        try
        {
            int.TryParse(Convert.ToString(e.Value), out int value);

            if (value > 0)
            {
                // Implement code here
                await Task.Delay(1000);
                await GetPersonsByCompanyIdAsync(value);
                StateHasChanged();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private async Task GetPersonsByCompanyIdAsync(int id)
    {
        try
        {
            if (id == 0)
                return;

            var responseModel = await _personServices.GetPersonsByCompanyIdAsync(id, 1, _pageSize);
            _personPaginated = responseModel;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task SearchByNameAsync(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            // Implement search by name
            await Task.Delay(1000);
        }
    }
}