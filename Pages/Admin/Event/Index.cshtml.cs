using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MetaX.Pages.Admin.Event
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }


       public IActionResult OnPostGoToPageUpdate() 
        {
            return RedirectToPage("Update");
        }
        public IActionResult OnPostGoToPageCreate()
        {
            return RedirectToPage("Create");
        }
    }
}
