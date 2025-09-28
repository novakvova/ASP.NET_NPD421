using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

[Area("Admin")]
public class FormsController : Controller
{
  public IActionResult BasicInputs() => View();
  public IActionResult InputGroups() => View();
}
