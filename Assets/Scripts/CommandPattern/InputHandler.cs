using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Player player;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();

        // 监听输入事件
        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += OnMove;

    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    // 当移动输入发生变化时 (按下或松开)
    private void OnMove(InputAction.CallbackContext context)
    {
        // 1. 读取输入值
        Vector2 moveInput = context.ReadValue<Vector2>();

        // 2. 创建一个移动命令
        ICommand moveCommand = new MoveCommand(moveInput);

        // 3. 将命令发送给Player执行
        player.ExecuteCommand(moveCommand);
    }

}