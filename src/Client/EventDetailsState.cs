using GoogleMapsComponents.Maps.Places;
using MudBlazor;

namespace devops_23_24_net_a02.Client;

public class EventDetailsState
{
  public string FormattedAddress { get; set; }
  public PlaceGeometry PlaceGeometry { get; set; }
  public string PlaceTitle { get; set; }
  public int NumberOfPeople { get; set; }

  public bool HasTripleBeer = false;

  public DateRange dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);

  public TimeSpan? startTime = new TimeSpan();
  public TimeSpan? endTime = new TimeSpan();

}

