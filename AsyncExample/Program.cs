using System.Text.Json;
using AsyncExample;

var response = await new HttpClient().GetAsync("https://jsonplaceholder.typicode.com/comments");

var responseContent = await response.Content.ReadAsStringAsync();

var comments = JsonSerializer.Deserialize<IEnumerable<Comment>>(
    responseContent,
    new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    });

Console.WriteLine("|{0,7}|{1,7}|{2,50}|{3,30}|{4,70}|", "PostId", "Id", "Name", "Email", "Body");

foreach (var comment in comments)
{
    comment.Body = comment.Body.Replace(Environment.NewLine, " ").Replace("\t", " ").Replace("\r", " ")
        .Replace("  ", " ");

    comment.Name = comment.Name.Replace(Environment.NewLine, " ").Replace("\t", " ").Replace("\r", " ")
        .Replace("  ", " ");

    comment.Email = comment.Email.Replace(Environment.NewLine, " ").Replace("\t", " ").Replace("\r", " ")
        .Replace("  ", " ");

    Console.WriteLine(
        "|{0,7}|{1,7}|{2,50}|{3,30}|{4,70}|",
        comment.PostId,
        comment.Id,
        comment.Name.Length > 50 ? comment.Name.Substring(0, 47) + "..." : comment.Name,
        comment.Email.Length > 30 ? comment.Email.Substring(0, 27) + "..." : comment.Email,
        comment.Body.Length > 100 ? comment.Body[..57] + "..." : comment.Body
    );
}