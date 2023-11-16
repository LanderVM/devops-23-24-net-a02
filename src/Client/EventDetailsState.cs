using GoogleMapsComponents.Maps.Places;
using MudBlazor;

namespace devops_23_24_net_a02.Client;

public class EventDetailsState
{
  public string address { get; set; } = "erikstraat 55";

  public DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

  public int AantalPersonen { get; set; } = 0;

  public TimeSpan? startTime = new TimeSpan();

  public TimeSpan? endTime = new TimeSpan();

  public PlaceGeometry PlaceGeometry { get; set; }
  public string PlaceTitle { get; set; }


}

