using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RateAmLib.Utils.Abtract;

public class ConsoleUtils
{
    public static void PrintTable(Table table)
    {
        foreach(var row in table.Rows)
        {
            foreach(var col in row.Cells)
            {
                Console.Write(col.Value);
                Console.Write('\t');
            }

            Console.WriteLine();
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("http://localhost:5002/swagger/v1/swagger.json", "Project B API v1");
            });
        }
        else
        {
            // Other configurations for non-development environments...
        }

        // Other middleware configurations...

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}