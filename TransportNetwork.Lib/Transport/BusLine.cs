using TransportNetwork.Lib.Graph;

namespace TransportNetwork.Lib.Transport;

public class BusLine
{
    public int Number { get; set; }
    public List<Edge> Route { get; set; }
}