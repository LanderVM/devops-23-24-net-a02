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

  public DateRange dateRange;
  
  public void Clear()
  {
    FormattedAddress = null;
    PlaceGeometry = null;
    PlaceTitle = null;
    NumberOfPeople = 0;
    HasTripleBeer = false;
    dateRange = new DateRange(DateTime.Now.Date, DateTime.Now.AddDays(3).Date);
  }
  
}

