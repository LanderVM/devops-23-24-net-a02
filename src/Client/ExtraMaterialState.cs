﻿namespace devops_23_24_net_a02.Client;

public class ExtraMaterialState
{
  public Dictionary<int, int> ExtrasAmount = new();

  public void Clear()
  {
    ExtrasAmount = new Dictionary<int, int>();
  }
}
