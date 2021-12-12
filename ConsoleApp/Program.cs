using System.Xml.Serialization;
using ConsoleApp;
using XmlLab_v10;

// var beer = new Beer()
// {
//     Chars = new Chars()
//     {
//         SpillMethod = new SpillMethod()
//     },
//     Manufacturer = new Manufacturer()
// };
//
// XmlSerializer xs = new XmlSerializer(typeof(Beer));
// TextWriter tw = new StreamWriter(@"beer.xml");
// xs.Serialize(tw, beer);
//
// Console.WriteLine("Done");

var beers = XmlHelper.ProcessXml();
foreach (var beer in beers)
{
    System.Console.WriteLine(beer.Id);
}

System.Console.WriteLine("\nXML validation result:");
XmlHelper.ValidateXml();

System.Console.WriteLine("\nXML -> JSON:");
XmlHelper.XmlToJson();