using System.Data;
using System.Text;
using System.Text.Json;
using dBASE.NET;
using My.GIS;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var _tool = new ShapefileTools();
//var _layer =  _tool.ReadShapefile("/Users/bre/shp/a.shp");
//Console.WriteLine(JsonSerializer.Serialize(_layer));
app.MapGet("/", () => "Hello World!");
app.Map("/shp/{*a}", (string a) =>
{
   //var layer = _tool.ReadShapefile($"/Users/bre/shp/{a}.shp");
   var layer = _tool.ReadShapefile("a.shp");
   return layer;
});
/*var dbf = new Dbf();
dbf.Read("a.dbf");*/
/*foreach (var field in dbf.Fields)
{
   Console.WriteLine(field.Name); 
}*/
/*foreach(DbfRecord record in dbf.Records) {
   for(int i = 0;  i < dbf.Fields.Count; i++) {
      Console.WriteLine(record[i]);
   }
}*/
string dbfFilename = "a.dbf";
var dbf = new Dbf();
dbf.Read(dbfFilename);
var table =new DataTable();
//create columns 
foreach (var field in dbf.Fields)
{
   var column = new DataColumn();
   column.DataType = System.Type.GetType("System.String");
   column.ColumnName = field.Name;
    
   table.Columns.Add(column);
}

foreach (var record in dbf.Records)
{
   int i = 0;
   var row = table.NewRow();
   foreach (var field in dbf.Fields)
   {
      row[field.Name] = record[i];
      i += 1;
   }
   table.Rows.Add(row);
}

/*Console.WriteLine(table.Rows.Count);
foreach(DataRow dataRow in table.Rows)
{
   foreach(var item in dataRow.ItemArray)
   {
      Console.WriteLine(item);
   }
}*/


app.Run();