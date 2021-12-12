using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using XmlLab_v10;

namespace ConsoleApp;

public static class XmlHelper
{
    public static void XmlToJson()
    {
        var xDoc = new XmlDocument();
        xDoc.Load("beers.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
            System.Console.WriteLine(JsonConvert.SerializeXmlNode(n, Newtonsoft.Json.Formatting.Indented, false));
    }

    public static Beer[] ProcessXml()
    {
        var beers = new List<Beer>();

        var xDoc = new XmlDocument();
        xDoc.Load("beers.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
        {
            var gun = new Beer()
            {
                Id = Convert.ToInt32(n.SelectSingleNode("@Id")?.InnerText),
                BeerType = Enum.Parse<BeerType>(n.SelectSingleNode("BeerType")?.InnerText ?? "Unknown"),
                Chars = new Chars()
                {
                    AlcoholPercentage = Convert.ToInt32(n.SelectSingleNode("//AlcoholPercentage")?.InnerText),
                    IsFiltered = Convert.ToBoolean(n.SelectSingleNode("//IsFiltered")?.InnerText),
                    NumberOfCalories = Convert.ToInt32(n.SelectSingleNode("//NumberOfCalories")?.InnerText),
                    SpillMethod = new SpillMethod()
                    {
                        BottleType = Enum.Parse<BottleType>(n.SelectSingleNode("//SpillMethod//BottleType")?.InnerText ?? "Unknown"),
                        Volume = Convert.ToDouble(n.SelectSingleNode("//SpillMethod//Volume")?.InnerText),
                    },
                    Transparency = Convert.ToInt32(n.SelectSingleNode("//Transparency")?.InnerText),
                }
            };

            beers.Add(gun);
        }

        beers.Sort();

        return beers.ToArray();
    }

    public static void ValidateXml()
    {
        var schema = new XmlSchemaSet();
        schema.Add("", "beer.xsd");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load("invalid.xml");
        xmlDoc.Schemas.Add(schema);
        xmlDoc.Validate(ValidationEventHandler!);
    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
            System.Console.WriteLine(e.Message);
    }
}