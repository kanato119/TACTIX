using Unity.Netcode.Components;

public class NetworkTransfromClient: NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
