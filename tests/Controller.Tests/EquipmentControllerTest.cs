using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devops_23_24_net_a02.Server.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using shared.Equipment;
using Shouldly;

namespace Controller.Tests;

public class EquipmentControllerTest
{

  [Fact]
  public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
  {
    // Arrange
    using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole());

    ILogger<EquipmentController> logger = loggerFactory.CreateLogger<EquipmentController>();

    var mockRepo = new Mock<IEquipmentService>();
    mockRepo.Setup(repo => repo.GetActiveEquipmentAsync())
        .ReturnsAsync(new EquipmentResult.ActiveEquipment
        {
          Equipment = new List<EquipmentDto.Index> { new EquipmentDto.Index {
          Id = 1,
          Title = "Emmer",
          Price = 3.33M,
          Stock = 3,
          Attributes = new List<String> {"Zeep","Badeendje" },
          ImageData = new EquipmentDto.ImageData{
          ImageUrl = "",
          AltText = "",
          }
        },new EquipmentDto.Index {
          Id = 2,
          Title = "Geweer",
          Price = 4.44M,
          Stock = 4,
          Attributes = new List<String> {"Kogels","Kruit" },
          ImageData = new EquipmentDto.ImageData{
        ImageUrl = "",
        AltText = "",
        }
          },

        },
          TotalAmount = 2
        });
    var controller = new EquipmentController(logger, mockRepo.Object);

    // Act
    var result = await controller.GetActiveEquipment();

    // Assert
    EquipmentResult.ActiveEquipment viewResult = Assert.IsType<EquipmentResult.ActiveEquipment>(result);
    viewResult.TotalAmount.ShouldBe(2);
    IEnumerable<EquipmentDto.Index> equipment = Assert.IsAssignableFrom<IEnumerable<EquipmentDto.Index>>(
        viewResult.Equipment);

    EquipmentDto.Index anEquipment = equipment.FirstOrDefault()!;

    anEquipment.Id.ShouldBe(1);
    anEquipment.Title.ShouldBe("Emmer");
    anEquipment.Price.ShouldBe(3.33M);
    anEquipment.Stock.ShouldBe(3);
    anEquipment.Attributes.ShouldBe(new List<String> { "Zeep", "Badeendje" });
    
  }
}
