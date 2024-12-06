using Domain.Models;

namespace UI.Pages.Company;

public partial class Index
{
    private bool
        _isLoading = false;

    private int
        _pageSize = 15;

    private int
        _userId = default;

    private Paginate<Domain.Entities.Company> _companyPaginated = new();

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

            await GetCompaniesPaginatedAsync();

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

    private async Task GetCompaniesPaginatedAsync()
    {
        try
        {
            var responseModel = await _companyServices.GetCompaniesAsync(_userId, 1, _pageSize);
            _companyPaginated = responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task GetCompaniesPaginatedAsync(int page)
    {
        try
        {
            var responseModel = await _companyServices.GetCompaniesAsync(_userId, page, _pageSize);
            _companyPaginated = responseModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
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