using System;

public class UIState
{
    public readonly Type[] Panels;

    public UIState(Type[] panels) {
        this.Panels = panels;
    }
}

public class LoginState : UIState
{
    public LoginState(Type[] panels) : base(panels) { }
}

public class SelectionState : UIState
{
    public SelectionState(Type[] panels) : base(panels) { }
}

public class CreateRoomState : UIState
{
    public CreateRoomState(Type[] panels) : base(panels) { }
}

public class JoinRandomRoomState : UIState
{
    public JoinRandomRoomState(Type[] panels) : base(panels) { }
}

public class RoomListState : UIState
{
    public RoomListState(Type[] panels) : base(panels) { }
}

public class InsideRoomState : UIState
{
    public InsideRoomState(Type[] panels) : base(panels) { }
}
