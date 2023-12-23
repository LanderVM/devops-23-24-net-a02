using devops_23_24_net_a02.Server.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using shared.Equipment;
using Shouldly;

namespace Controller.Tests;

public class EquipmentControllerTest
{
  [Fact]
  public async Task GetAllActiveEquipmpent_ReturnsTheRightEquipment()
  {
    // Arrange
    using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
      .SetMinimumLevel(LogLevel.Trace)
      .AddConsole());

    var logger = loggerFactory.CreateLogger<EquipmentController>();

    var mockService = new Mock<IEquipmentService>();
    mockService.Setup(service => service.GetActiveEquipmentAsync())
      .ReturnsAsync(new EquipmentResult.ActiveEquipment
      {
        Equipment = new List<EquipmentDto.Index>
        {
          new()
          {
            Id = 1,
            Title = "Emmer",
            Price = 3.33M,
            Stock = 3,
            Attributes = new List<String> { "Zeep", "Badeendje" },
            ImageData = new EquipmentDto.ImageData { ImageUrl = "", AltText = "" }
          },
          new()
          {
            Id = 2,
            Title = "Geweer",
            Price = 4.44M,
            Stock = 4,
            Attributes = new List<String> { "Kogels", "Kruit" },
            ImageData = new EquipmentDto.ImageData { ImageUrl = "", AltText = "" }
          }
        },
        TotalAmount = 2
      });
    var controller = new EquipmentController(logger, mockService.Object);

    // Act
    var result = await controller.GetActiveEquipment();

    // Assert
    var viewResult = Assert.IsType<EquipmentResult.ActiveEquipment>(result);
    viewResult.TotalAmount.ShouldBe(2);
    var equipment = Assert.IsAssignableFrom<IEnumerable<EquipmentDto.Index>>(
      viewResult.Equipment);

    var anEquipment = equipment.FirstOrDefault()!;

    anEquipment.Id.ShouldBe(1);
    anEquipment.Title.ShouldBe("Emmer");
    anEquipment.Price.ShouldBe(3.33M);
    anEquipment.Stock.ShouldBe(3);
    anEquipment.Attributes.ShouldBe(new List<String> { "Zeep", "Badeendje" });
  }

  [Fact]
  public async Task PostAnEquipmentWithoutImage_CheckWetherServiceMethodIsCalled()
  {
    using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
      .SetMinimumLevel(LogLevel.Trace)
      .AddConsole());

    var logger = loggerFactory.CreateLogger<EquipmentController>();

    var mockService = new Mock<IEquipmentService>();
    mockService.Setup(service => service.CreateAsync(It.IsAny<EquipmentDto.Create>()))
      .ReturnsAsync(new EquipmentResult.Create { Id = 5 })
      .Verifiable();

    var controller = new EquipmentController(logger, mockService.Object);

    var result = await controller.Create(new EquipmentDto.Create
    {
      Title = "Barbeque set",
      Price = 5.55M,
      Stock = 5,
      Attributes = "Tang,Kolen",
      ImageContentType = null,
      IsActive = true
    });

    var viewResult = Assert.IsType<EquipmentResult.Create>(result);

    viewResult.Id.ShouldBe(5);

    mockService.Verify();
  }
}
