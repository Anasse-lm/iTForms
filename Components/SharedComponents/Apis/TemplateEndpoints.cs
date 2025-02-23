using iTFORMS.Models;

namespace iTFORMS.Components.SharedComponents.Apis;
public static class TemplateEndpoints
{
    public static void MapTemplateEndpoints(this WebApplication app)
    {
        var templates = new List<string>();
        app.MapGet("/api/templates", () =>
        {
            var templates = new List<string> { "Template 1", "Template 2" };
            return Results.Ok(templates);
        });

        app.MapPost("/api/templates", async (HttpContext context) =>
        {
            var newTemplate = await context.Request.ReadFromJsonAsync<Template>();
            if (newTemplate == null || string.IsNullOrEmpty(newTemplate.Title))
            {
                return Results.BadRequest("Template title is required.");
            }

            newTemplate.CreatedAt = DateTime.UtcNow;
            templates.Add(newTemplate.Title);
            return Results.Created($"/api/templates/{newTemplate.Title}", newTemplate);
        });
    }
}

