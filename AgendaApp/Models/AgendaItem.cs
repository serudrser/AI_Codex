using System.Text;

namespace AgendaApp.Models;

public record AgendaItem(
    string Title,
    DateTime StartTime,
    TimeSpan Duration,
    string? Location = null,
    string? Notes = null)
{
    public DateTime EndTime => StartTime + Duration;

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"Title: {Title}");
        builder.AppendLine($"When: {StartTime:dddd, MMMM d yyyy h:mm tt} - {EndTime:h:mm tt}");

        if (!string.IsNullOrWhiteSpace(Location))
        {
            builder.AppendLine($"Where: {Location}");
        }

        if (!string.IsNullOrWhiteSpace(Notes))
        {
            builder.AppendLine($"Notes: {Notes}");
        }

        return builder.ToString();
    }
}
