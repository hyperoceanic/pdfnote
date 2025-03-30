using PDFNote;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: PDFNote.exe sample.ymal");
            Console.WriteLine("Please provide the name of a config file (eg 'sample.yaml').");
            Console.WriteLine("If the file does not exist, it will be created for you with some default values.");
            return;
        }

        var configFile = args[0];

        if (!File.Exists(configFile))
        {
            Console.WriteLine($"Creating {configFile}.");
            DocumentModel doc = Sample.BuildDoc();
            string yaml = ToYaml(doc);
            Console.WriteLine(yaml);
            File.WriteAllText(configFile, yaml);
        }

        Console.WriteLine($"Using {configFile}.");

        var model = Load(configFile);

        var x = new DocumentWriter(model);
        x.Create();

        Console.WriteLine("Done.");
    }

    private static DocumentModel Load(string filename)
    {
        var yaml = File.ReadAllText(filename);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        try
        {
            var result = deserializer.Deserialize<DocumentModel>(yaml);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException.Message);
            throw;
        }
    }

    private static string ToYaml(DocumentModel doc)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        var result = serializer.Serialize(doc);
        return result;
    }
}