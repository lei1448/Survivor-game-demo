require("Object")
require("SplitTools")
Json = require("dkjson")

------UnityClass-----
GameObject = CS.UnityEngine.GameObject
Resource = CS.UnityEngine.Resource
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
TextAsset = CS.UnityEngine.TextAsset

------图集对象类------
SpriteAtlas = CS.UnityEngine.U2D.SpriteAtlas

------UI相关------
UI = CS.UnityEngine.UI
Panel = UI.Panel
Text = UI.Text
TextMeshProUGUI_Str = "TMPro.TextMeshProUGUI"
Image = UI.Image
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect
UIBehaviour = CS.UnityEngine.EventSystems.UIBehaviour

------场景物体------
Canvas = GameObject.Find("Canvas")

------常用工具------
Vector3 = CS.UnityEngine.Vector3
Vector2 = CS.UnityEngine.Vector2


------AA包------
AAMgr = CS.AAMgr.Instance