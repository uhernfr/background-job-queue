using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace teste_background_job_web.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
      _logger = logger;
    }

    public void OnGet()
    {

    }
  }
}