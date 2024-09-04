namespace TransportNetwork.Lib.Graph;

public class Edge
{
    public Guid Id { get; set; }
    public Node Source { get; set; }
    public Node Destination { get; set; }
    public float Cost { get; set; }
}