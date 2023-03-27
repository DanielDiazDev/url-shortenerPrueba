using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Service;

namespace UrlShortener.Controllers;

public class UrlController : Controller
{
    private readonly IUrlShorteningService _urlShorteningService;
    private readonly IUrlRepository _urlRepository;

    public UrlController(IUrlShorteningService urlShorteningService, IUrlRepository urlRepository)
    {
        _urlShorteningService = urlShorteningService;
        _urlRepository = urlRepository;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var urls = await _urlRepository.GetAll();
        return View(urls);
    }
    public IActionResult Create()
    {
        return View(new Url());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Url model)
    {
        if (ModelState.IsValid)
        {
            model = await _urlShorteningService.GetShortUrl(model.OriginalUrl);
            model.Id = Guid.NewGuid();
            await _urlRepository.Add(model);
            return RedirectToAction("Details", new { id = model.Id });
        }
        return View(model);
    }

    public async Task<ActionResult> Details(Guid id)
    {
        var url = await _urlRepository.GetById(id);
        if (url == null)
        {
            return NotFound();
        }

        return View(url);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(Guid id)
    {
        var url = await _urlRepository.GetById(id);
        if (url == null)
        {
            return NotFound();
        }

        await _urlRepository.Delete(id);
        return RedirectToAction("Index");
    }
}