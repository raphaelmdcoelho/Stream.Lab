var path = "./Data.txt";

#region Stream

Stream GetFileStream(string path)
{
    return File.Open(path, FileMode.Open);
}

async Task<string> GetStringContent(Stream stream)
{
    using StreamReader sr = new StreamReader(stream);
    var content = await sr.ReadToEndAsync();

    return content;
}

async Task<string> ProcessFromStream()
{
    var fileStream = GetFileStream(path);
    var fileStringContent = await GetStringContent(fileStream);

    fileStream.Close();

    return fileStringContent;
}

#endregion

# region File

Task<string> ProcessFromFile()
{
    var result = String.Empty;
    foreach (var character in File.ReadAllLines(path))
        result += character + "\n";

    return Task.FromResult(result);
}

#endregion

# region BufferedStream

string ProcessFromStreamBuffered()
{
    string result = String.Empty;

    using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    using (BufferedStream bs = new BufferedStream(fs))
    using (StreamReader sr = new StreamReader(bs))
    {
        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            Console.WriteLine(line);
            Console.WriteLine("Press any key to read the next line: ");
            Console.ReadKey();
            result += line + "\n";
        }
    }

    return result;
}

#endregion

async Task<string> Run()
{
    Console.WriteLine("Choose between the following commands:");
    Console.WriteLine("1 - FromStream");
    Console.WriteLine("2 - FromFile");
    Console.WriteLine("3 - FromStreamBuffered");
    Console.WriteLine("Insert you command:");

    string? stdin = Console.ReadLine();

    return Convert.ToInt32(stdin) switch
    {
        1 => await ProcessFromStream(),
        2 => await ProcessFromFile(),
        3 => ProcessFromStreamBuffered(),
        _ => "No command found"
    };
}

var result = await Run();

Console.WriteLine(result);

Console.ReadKey();