using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetGenerator.Models;

public class Product
{
    // This will not be shown in the excel file when generated.
    public int Id { get; set; }

    [Display(Name = "Product")]
    public string Name { get; set; } = default!;

    [Display(Name = "Price")]
    public decimal Price { get; set; }

    [Display(Name = "Number of Sales")]
    public int NumberOfSales { get; set; }
}
