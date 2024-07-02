//interface for objects that can be possessed by the player
public interface Possessable
{
    //Possess the object, should change the camera, audio listener, change owner and start listening to player input.
    void Possess(ulong clientID);
    void Unpossess();

};