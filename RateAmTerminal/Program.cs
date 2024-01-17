using RateAmLib;

Console.WriteLine("Hello, World!");
DataParser o =new DataParser();

var data =    DataParser.ScrapeRateAm();
DataParser.DataToObject(data);



