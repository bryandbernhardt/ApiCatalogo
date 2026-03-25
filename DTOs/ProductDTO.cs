using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCatalogo.Models;

namespace ApiCatalogo.DTOs;

public class ProductDTO
{
    public int Id { get; set; }
  
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
  
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
  
    [Required]
    public decimal Price { get; set; }
  
    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
  
    public float Stock { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
}