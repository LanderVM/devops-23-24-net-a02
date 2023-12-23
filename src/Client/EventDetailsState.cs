using GoogleMapsComponents.Maps.Places;
using MudBlazor;
using shared.Common;

namespace devops_23_24_net_a02.Client;

public class EventDetailsState
{
  public DateRange dateRange = new(DateTime.Today, DateTime.Today.AddDays(2));

  public bool HasTripleBeer;
  public string FormattedAddress { get; set; }
  public AddressDto EventAddress { get; set; }
  public PlaceGeometry PlaceGeometry { get; set; }
  public string PlaceTitle { get; set; }
  public int NumberOfPeople { get; set; }

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
